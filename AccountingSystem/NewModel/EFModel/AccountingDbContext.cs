using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AccountingSystem.NewModel.EFModel
{
    public partial class AccountingDbContext : DbContext
    {
        public AccountingDbContext()
            : base("name=AccountingDbContext")
        {
        }

        public virtual DbSet<AccountsGroup> AccountsGroups { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<BeginningInventory> BeginningInventories { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Cashier> Cashiers { get; set; }
        public virtual DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Classify> Classifies { get; set; }
        public virtual DbSet<ClassifyGroup> ClassifyGroups { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompositeItem> CompositeItems { get; set; }
        public virtual DbSet<CompoundEntry> CompoundEntries { get; set; }
        public virtual DbSet<Consumption> Consumptions { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeesType> EmployeesTypes { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<InventoryTransfer> InventoryTransfers { get; set; }
        public virtual DbSet<InventoryTransferDetail> InventoryTransferDetails { get; set; }
        public virtual DbSet<JournalEntry> JournalEntries { get; set; }
        public virtual DbSet<MeasurementsItem> MeasurementsItems { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<SaleDetail> SaleDetails { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SimpleEntry> SimpleEntries { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Trade> Trades { get; set; }
        public virtual DbSet<TypesClassify> TypesClassifies { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountsGroup>()
                .HasMany(e => e.ChartOfAccounts)
                .WithOptional(e => e.AccountsGroup)
                .HasForeignKey(e => e.accountGroupId);

            modelBuilder.Entity<BeginningInventory>()
                .Property(e => e.quantity)
                .HasPrecision(10, 2);

            modelBuilder.Entity<BeginningInventory>()
                .Property(e => e.unitPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.BeginningInventories)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.brancheId);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.CompoundEntries)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.brancheId);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.brancheId);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.InventoryTransfers)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.brancheId);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Purchases)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.brancheId);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Sales)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.brancheId);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.SimpleEntries)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.brancheId);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Vouchers)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.brancheId);

            modelBuilder.Entity<ChartOfAccount>()
                .Property(e => e.natureOfAccount)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAccount>()
                .HasMany(e => e.Cashiers)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.accountId);

            modelBuilder.Entity<ChartOfAccount>()
                .HasMany(e => e.JournalEntries)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.accountId);

            modelBuilder.Entity<ChartOfAccount>()
                .HasMany(e => e.Trades)
                .WithOptional(e => e.ChartOfAccount)
                .HasForeignKey(e => e.accountId);

            modelBuilder.Entity<ChartOfAccount>()
                .HasMany(e => e.Childrens)
                .WithOptional(e => e.perantAccount)
                .HasForeignKey(e => e.parentId);

            modelBuilder.Entity<ChartOfAccount>()
                .HasMany(e => e.Vouchers)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.accountId);

            modelBuilder.Entity<ChartOfAccount>()
                .HasMany(e => e.SimpleEntriesCredit)
                .WithOptional(e => e.AccountCredit)
                .HasForeignKey(e => e.creditAccount);

            modelBuilder.Entity<ChartOfAccount>()
                .HasMany(e => e.Customers)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.accountId);

            modelBuilder.Entity<ChartOfAccount>()
                .HasMany(e => e.SimpleEntriesDebit)
                .WithOptional(e => e.AccountDebit)
                .HasForeignKey(e => e.debitAccount);

            modelBuilder.Entity<ChartOfAccount>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.accountId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChartOfAccount>()
                .HasMany(e => e.Suppliers)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.accountId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChartOfAccount>()
                .HasMany(e => e.Stores)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.accountId);

            modelBuilder.Entity<Classify>()
                .HasMany(e => e.BeginningInventories)
                .WithOptional(e => e.item)
                .HasForeignKey(e => e.itemId);

            modelBuilder.Entity<Classify>()
                .HasMany(e => e.Childrens)
                .WithOptional(e => e.perantItem)
                .HasForeignKey(e => e.parentId);

            modelBuilder.Entity<Classify>()
                .HasMany(e => e.Inventories)
                .WithOptional(e => e.item)
                .HasForeignKey(e => e.itemId);

            modelBuilder.Entity<Classify>()
                .HasMany(e => e.InventoryTransferDetails)
                .WithOptional(e => e.item)
                .HasForeignKey(e => e.itemId);

            modelBuilder.Entity<Classify>()
                .HasMany(e => e.PurchaseDetails)
                .WithOptional(e => e.item)
                .HasForeignKey(e => e.itemId);

            modelBuilder.Entity<Classify>()
                .HasMany(e => e.SaleDetails)
                .WithOptional(e => e.item)
                .HasForeignKey(e => e.itemId);

            modelBuilder.Entity<Classify>()
                .HasMany(e => e.MeasurementsItems)
                .WithOptional(e => e.item)
                .HasForeignKey(e => e.itemId);

            modelBuilder.Entity<CompositeItem>()
                .Property(e => e.quantity)
                .HasPrecision(10, 2);

            modelBuilder.Entity<CompositeItem>()
                .Property(e => e.purchasePrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<CompositeItem>()
                .Property(e => e.sellingPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Consumption>()
                .Property(e => e.quantity)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Currency>()
                .Property(e => e.exchangeRate)
                .HasPrecision(18, 6);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Trades)
                .WithOptional(e => e.Currency)
                .HasForeignKey(e => e.currencyFromId);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Trades1)
                .WithOptional(e => e.Currency1)
                .HasForeignKey(e => e.currencyToId);

            modelBuilder.Entity<Customer>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeesType>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.EmployeesType)
                .HasForeignKey(e => e.employeeTypeId);

            modelBuilder.Entity<InventoryTransfer>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryTransferDetail>()
                .Property(e => e.quantity)
                .HasPrecision(10, 2);

            modelBuilder.Entity<InventoryTransferDetail>()
                .Property(e => e.unitPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<MeasurementsItem>()
                .Property(e => e.purchasePrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<MeasurementsItem>()
                .Property(e => e.sellingPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<MeasurementsItem>()
                .Property(e => e.WholesalePrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<MeasurementsItem>()
                .Property(e => e.WholesalePurchasePrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<MeasurementsItem>()
                .Property(e => e.descountPrice)
                .HasPrecision(10, 5);

            modelBuilder.Entity<MeasurementsItem>()
                .Property(e => e.minimumPurchaseAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<MeasurementsItem>()
                .HasMany(e => e.BeginningInventories)
                .WithOptional(e => e.MeasurementsItem)
                .HasForeignKey(e => e.measurementItemId);

            modelBuilder.Entity<MeasurementsItem>()
                .HasMany(e => e.CompositeItems)
                .WithOptional(e => e.MeasurementsItem)
                .HasForeignKey(e => e.componentItemId);

            modelBuilder.Entity<MeasurementsItem>()
                .HasMany(e => e.CompositeItems1)
                .WithOptional(e => e.ComponentItem)
                .HasForeignKey(e => e.measurementItemId);

            modelBuilder.Entity<MeasurementsItem>()
                .HasMany(e => e.Inventories)
                .WithOptional(e => e.MeasurementsItem)
                .HasForeignKey(e => e.measurementItemId);

            modelBuilder.Entity<MeasurementsItem>()
                .HasMany(e => e.InventoryTransferDetails)
                .WithOptional(e => e.MeasurementsItem)
                .HasForeignKey(e => e.measurementItemId);

            modelBuilder.Entity<MeasurementsItem>()
                .HasMany(e => e.PurchaseDetails)
                .WithOptional(e => e.MeasurementsItem)
                .HasForeignKey(e => e.measurementItemId);

            modelBuilder.Entity<MeasurementsItem>()
                .HasMany(e => e.SaleDetails)
                .WithOptional(e => e.MeasurementsItem)
                .HasForeignKey(e => e.measurementItemId);

            modelBuilder.Entity<PurchaseDetail>()
                .Property(e => e.quantity)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PurchaseDetail>()
                .Property(e => e.unitPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Purchase>()
                .Property(e => e.paymentType)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase>()
                .Property(e => e.priceType)
                .IsUnicode(false);

            modelBuilder.Entity<SaleDetail>()
                .Property(e => e.descountPrice)
                .HasPrecision(10, 5);

            modelBuilder.Entity<SaleDetail>()
                .Property(e => e.quantity)
                .HasPrecision(10, 2);

            modelBuilder.Entity<SaleDetail>()
                .Property(e => e.unitPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Sale>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.paymentType)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.priceType)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.orderType)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.orderTime)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.InventoryTransfers)
                .WithOptional(e => e.FromStore)
                .HasForeignKey(e => e.fromStoreId);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.InventoryTransfers1)
                .WithOptional(e => e.ToStore)
                .HasForeignKey(e => e.toStoreId);

            modelBuilder.Entity<Trade>()
                .Property(e => e.date)
                .IsUnicode(false);

            modelBuilder.Entity<Trade>()
                .Property(e => e.conversionPrice)
                .HasPrecision(10, 5);

            modelBuilder.Entity<TypesClassify>()
                .HasMany(e => e.Classifies)
                .WithOptional(e => e.TypesClassify)
                .HasForeignKey(e => e.typeClassifyId);
        }
    }
}
