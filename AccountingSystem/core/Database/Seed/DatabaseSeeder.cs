using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.core.Database.Seed
{
    public sealed class DatabaseSeedResult
    {
        public bool Seeded { get; set; }
        public string AdminLoginName { get; set; }
        public string AdminPassword { get; set; }
    }

    /// <summary>
    /// Creates the minimum master data required to start using a new/empty database.
    /// The seeder is intentionally conservative: if any of the core master-data tables
    /// already contains records, it does not insert anything.
    /// </summary>
    public static class DatabaseSeeder
    {
        private const string AdminLoginName = "admin";

        private static readonly string[] PermissionNames =
        {
            "employee",
            "currency",
            "cashier",
            "store",
            "city",
            "branch",
            "accountGruop",
            "accountingGuide",
            "uintGuide",
            "classifyGruop",
            "area",
            "custmore",
            "supplier",
            "catch",
            "expanse",
            "simpleJournalEntries",
            "compoundJournalEntries",
            "openingBalances",
            "item",
            "sale",
            "purchase",
            "salesReturns",
            "purchasesReturns",
            "inventoryTransfer"
        };

        public static DatabaseSeedResult SeedIfRequired()
        {
            using (var context = new AccountingDbContext())
            {
                if (!IsSeedTargetEmpty(context))
                {
                    return new DatabaseSeedResult { Seeded = false };
                }

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var groups = CreateAccountGroups(context);
                        var accounts = CreateChartOfAccounts(context, groups);

                        CreateCurrency(context);
                        var city = CreateCity(context);
                        var area = CreateArea(context, city);
                        CreateUnit(context);

                        var store = CreateStore(context, accounts.StoreAccount);
                        var branch = CreateBranch(context, store, city, area);
                        var cashier = CreateCashier(context, accounts.CashierAccount);
                        var employeeType = CreateEmployeeType(context);

                        string temporaryPassword = GenerateTemporaryPassword(12);
                        var admin = CreateAdministrator(
                            context,
                            accounts.EmployeeAccount,
                            branch,
                            cashier,
                            employeeType,
                            temporaryPassword);

                        CreateAdministratorPermissions(context, admin);

                        context.SaveChanges();
                        transaction.Commit();

                        return new DatabaseSeedResult
                        {
                            Seeded = true,
                            AdminLoginName = AdminLoginName,
                            AdminPassword = temporaryPassword
                        };
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private static bool IsSeedTargetEmpty(AccountingDbContext context)
        {
            return !context.AccountsGroups.Any()
                && !context.ChartOfAccounts.Any()
                && !context.Currencies.Any()
                && !context.Cities.Any()
                && !context.Areas.Any()
                && !context.Stores.Any()
                && !context.Branches.Any()
                && !context.Cashiers.Any()
                && !context.EmployeesTypes.Any()
                && !context.Employees.Any()
                && !context.Permissions.Any()
                && !context.Units.Any();
        }

        private static AccountGroupsSeed CreateAccountGroups(AccountingDbContext context)
        {
            var result = new AccountGroupsSeed
            {
                Assets = new AccountsGroup { name = "الأصول" },
                Liabilities = new AccountsGroup { name = "الخصوم" },
                Equity = new AccountsGroup { name = "حقوق الملكية" },
                Revenue = new AccountsGroup { name = "الإيرادات" },
                Expenses = new AccountsGroup { name = "المصروفات" }
            };

            context.AccountsGroups.Add(result.Assets);
            context.AccountsGroups.Add(result.Liabilities);
            context.AccountsGroups.Add(result.Equity);
            context.AccountsGroups.Add(result.Revenue);
            context.AccountsGroups.Add(result.Expenses);
            context.SaveChanges();

            return result;
        }

        private static ChartOfAccountsSeed CreateChartOfAccounts(AccountingDbContext context, AccountGroupsSeed groups)
        {
            var assets = NewAccount("الأصول", 1000, "رئيسي", SharedData.balanceSheet, 1, groups.Assets, null, null);
            var liabilities = NewAccount("الخصوم", 2000, "رئيسي", SharedData.balanceSheet, 1, groups.Liabilities, null, null);
            var equity = NewAccount("حقوق الملكية", 3000, "رئيسي", SharedData.balanceSheet, 1, groups.Equity, null, null);
            var revenue = NewAccount("الإيرادات", 4000, "رئيسي", SharedData.profitLoss, 1, groups.Revenue, null, null);
            var expenses = NewAccount("المصروفات", 5000, "رئيسي", SharedData.profitLoss, 1, groups.Expenses, null, null);

            context.ChartOfAccounts.Add(assets);
            context.ChartOfAccounts.Add(liabilities);
            context.ChartOfAccounts.Add(equity);
            context.ChartOfAccounts.Add(revenue);
            context.ChartOfAccounts.Add(expenses);
            context.SaveChanges();

            var cash = NewAccount("النقدية وما في حكمها", 1100, "رئيسي", SharedData.balanceSheet, 2, groups.Assets, assets, SharedData.accountLocations["cashiers"]);
            var customers = NewAccount("حسابات العملاء", 1200, "رئيسي", SharedData.balanceSheet, 2, groups.Assets, assets, SharedData.accountLocations["custmoresAndSuplires"]);
            var inventory = NewAccount("المخزون", 1300, "رئيسي", SharedData.balanceSheet, 2, groups.Assets, assets, SharedData.accountLocations["stores"]);
            var employees = NewAccount("حسابات الموظفين", 1400, "رئيسي", SharedData.balanceSheet, 2, groups.Assets, assets, SharedData.accountLocations["employees"]);
            var suppliers = NewAccount("حسابات الموردين", 2100, "رئيسي", SharedData.balanceSheet, 2, groups.Liabilities, liabilities, SharedData.accountLocations["s"]);
            var capital = NewAccount("رأس المال", 3100, "فرعي", SharedData.balanceSheet, 2, groups.Equity, equity, null);
            var salesRevenue = NewAccount("إيرادات المبيعات", 4100, "فرعي", SharedData.profitLoss, 2, groups.Revenue, revenue, null);
            var operatingExpenses = NewAccount("المصروفات التشغيلية", 5100, "رئيسي", SharedData.profitLoss, 2, groups.Expenses, expenses, null);

            context.ChartOfAccounts.Add(cash);
            context.ChartOfAccounts.Add(customers);
            context.ChartOfAccounts.Add(inventory);
            context.ChartOfAccounts.Add(employees);
            context.ChartOfAccounts.Add(suppliers);
            context.ChartOfAccounts.Add(capital);
            context.ChartOfAccounts.Add(salesRevenue);
            context.ChartOfAccounts.Add(operatingExpenses);
            context.SaveChanges();

            var cashierAccount = NewAccount("الصندوق الرئيسي", 1110, "فرعي", SharedData.balanceSheet, 3, groups.Assets, cash, SharedData.accountLocations["cashiers"]);
            var storeAccount = NewAccount("المخزن الرئيسي", 1310, "فرعي", SharedData.balanceSheet, 3, groups.Assets, inventory, SharedData.accountLocations["stores"]);
            var employeeAccount = NewAccount("مدير النظام", 1410, "فرعي", SharedData.balanceSheet, 3, groups.Assets, employees, SharedData.accountLocations["employees"]);
            var salaries = NewAccount("الرواتب والأجور", 5110, "فرعي", SharedData.profitLoss, 3, groups.Expenses, operatingExpenses, null);
            var generalExpenses = NewAccount("مصروفات عامة", 5120, "فرعي", SharedData.profitLoss, 3, groups.Expenses, operatingExpenses, null);

            context.ChartOfAccounts.Add(cashierAccount);
            context.ChartOfAccounts.Add(storeAccount);
            context.ChartOfAccounts.Add(employeeAccount);
            context.ChartOfAccounts.Add(salaries);
            context.ChartOfAccounts.Add(generalExpenses);
            context.SaveChanges();

            return new ChartOfAccountsSeed
            {
                CashierAccount = cashierAccount,
                StoreAccount = storeAccount,
                EmployeeAccount = employeeAccount
            };
        }

        private static ChartOfAccount NewAccount(
            string name,
            int accountNumber,
            string type,
            string natureOfAccount,
            int rank,
            AccountsGroup group,
            ChartOfAccount parent,
            string accountLocation)
        {
            return new ChartOfAccount
            {
                name = name,
                accountNumber = accountNumber,
                type = type,
                natureOfAccount = natureOfAccount,
                rankk = rank,
                accountGroupId = group == null ? (int?)null : group.id,
                parentId = parent == null ? (int?)null : parent.id,
                accountLocation = accountLocation
            };
        }

        private static void CreateCurrency(AccountingDbContext context)
        {
            context.Currencies.Add(new Currency
            {
                name = "الريال اليمني",
                code = "YER",
                exchangeRate = 1m,
                currencyType = "رئيسية"
            });
            context.SaveChanges();
        }

        private static City CreateCity(AccountingDbContext context)
        {
            var city = new City { name = "المدينة الرئيسية" };
            context.Cities.Add(city);
            context.SaveChanges();
            return city;
        }

        private static Area CreateArea(AccountingDbContext context, City city)
        {
            var area = new Area
            {
                name = "المنطقة الرئيسية",
                cityId = city.id
            };
            context.Areas.Add(area);
            context.SaveChanges();
            return area;
        }

        private static void CreateUnit(AccountingDbContext context)
        {
            context.Units.Add(new Unit { name = "حبة" });
            context.SaveChanges();
        }

        private static Store CreateStore(AccountingDbContext context, ChartOfAccount account)
        {
            var store = new Store
            {
                name = "المخزن الرئيسي",
                address = "",
                accountId = account.id
            };
            context.Stores.Add(store);
            context.SaveChanges();
            return store;
        }

        private static Branch CreateBranch(AccountingDbContext context, Store store, City city, Area area)
        {
            var branch = new Branch
            {
                name = "الفرع الرئيسي",
                administratorName = "مدير النظام",
                phoneNumber = "",
                address = "",
                storeId = store.id,
                cityId = city.id,
                areaId = area.id
            };
            context.Branches.Add(branch);
            context.SaveChanges();
            return branch;
        }

        private static Cashier CreateCashier(AccountingDbContext context, ChartOfAccount account)
        {
            var cashier = new Cashier
            {
                name = "الصندوق الرئيسي",
                accountId = account.id
            };
            context.Cashiers.Add(cashier);
            context.SaveChanges();
            return cashier;
        }

        private static EmployeesType CreateEmployeeType(AccountingDbContext context)
        {
            var employeeType = new EmployeesType { name = "مدير النظام" };
            context.EmployeesTypes.Add(employeeType);
            context.SaveChanges();
            return employeeType;
        }

        private static Employee CreateAdministrator(
            AccountingDbContext context,
            ChartOfAccount account,
            Branch branch,
            Cashier cashier,
            EmployeesType employeeType,
            string password)
        {
            var employee = new Employee
            {
                name = "مدير النظام",
                loginName = AdminLoginName,
                password = password,
                phoneNamber = "",
                status = true,
                accountId = account.id,
                brancheId = branch.id,
                cashierId = cashier.id,
                employeeTypeId = employeeType.id
            };

            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        private static void CreateAdministratorPermissions(AccountingDbContext context, Employee administrator)
        {
            int nextPermissionId = context.Permissions.Any()
                ? context.Permissions.Max(x => x.id) + 1
                : 1;

            foreach (string permissionName in PermissionNames)
            {
                context.Permissions.Add(new Permission
                {
                    id = nextPermissionId++,
                    employeeId = administrator.id,
                    tableName = permissionName,
                    addPermission = true,
                    deletePermission = true,
                    updatePermission = true,
                    viewPermission = true,
                    importFromExcelPermission = true
                });
            }

            context.SaveChanges();
        }

        private static string GenerateTemporaryPassword(int length)
        {
            const string alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789";
            var password = new char[length];
            var randomBytes = new byte[length];

            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetBytes(randomBytes);
            }

            for (int i = 0; i < password.Length; i++)
            {
                password[i] = alphabet[randomBytes[i] % alphabet.Length];
            }

            return new string(password);
        }

        private sealed class AccountGroupsSeed
        {
            public AccountsGroup Assets { get; set; }
            public AccountsGroup Liabilities { get; set; }
            public AccountsGroup Equity { get; set; }
            public AccountsGroup Revenue { get; set; }
            public AccountsGroup Expenses { get; set; }
        }

        private sealed class ChartOfAccountsSeed
        {
            public ChartOfAccount CashierAccount { get; set; }
            public ChartOfAccount StoreAccount { get; set; }
            public ChartOfAccount EmployeeAccount { get; set; }
        }
    }
}
