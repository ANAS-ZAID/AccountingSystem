using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.core.shared;

using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.model
{
    public class LoginData
    {
     public static NewModel.EFModel.Employee employee;
     public static NewModel.EFModel.Branch branch;
     public static Permission currencyPermissions;
     public static Permission cityPermissions;
     public static Permission areaPermissions;
     public static Permission branchePermissions;
     public static Permission accountGruopPermission;
     public static Permission classifyGruopPermission;
     public static Permission accountingGuidePermission;
     public static Permission uintGuidePermission;
     public static Permission cashierPermission;
        //static public Employee Employee { get { return employee; } }

        public static  void lodeLoginData(NewModel.EFModel.Employee employee, NewModel.EFModel.Branch branch)
        {
            LoginData.employee = employee;
          //permissions=employee.Permissions.ToDictionary(p => p.tableName);
            LoginData.branch = branch;

       
        }
        static List<string> tablesNames=new List<string>() { "home", "employee","","currency", "cashier",
             "store", "city", "branches", "accountGruop", "accountingGuide", "", "", "", };

        //public   static Dictionary<string, Permission> permissionsTables = new Dictionary<string, Permission>()
        //{
        //    {"home",new Permission() { employeeId=0,tableName=permissionTablesCell["home"].name,cell=permissionTablesCell["home"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
        //    {"employee",new Permission() { employeeId=0,tableName=permissionTablesCell["employee"].name,cell=permissionTablesCell["employee"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
        //    {"currency",new Permission() { employeeId=0,tableName=permissionTablesCell["currency"].name,cell=permissionTablesCell["currency"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
        //    {"cashier",new Permission() { employeeId=0,tableName=permissionTablesCell["cashier"].name,cell=permissionTablesCell["cashier"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
        //    {"store",new Permission() { employeeId=0,tableName=permissionTablesCell["store"].name,cell=permissionTablesCell["store"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
        //    {"city",new Permission() { employeeId=0,tableName=permissionTablesCell["city"].name,cell=permissionTablesCell["city"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
        //    {"branch",new Permission() { employeeId=0,tableName=permissionTablesCell["branch"].name,cell=permissionTablesCell["branch"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
        //    {"accountGruop",new Permission() { employeeId=0,tableName=permissionTablesCell["accountGruop"].name,cell=permissionTablesCell["accountGruop"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
        //    {"accountingGuide",new Permission() { employeeId=0,tableName=permissionTablesCell["accountingGuide"].name,cell=permissionTablesCell["accountingGuide"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
        //    {"uintGuide",new Permission() { employeeId=0,tableName=permissionTablesCell["uintGuide"].name,cell=permissionTablesCell["uintGuide"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
        //    {"classifyGruop",new Permission() { employeeId=0,tableName=permissionTablesCell["classifyGruop"].name,cell=permissionTablesCell["classifyGruop"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
        //    {"area",new Permission() { employeeId=0,tableName=permissionTablesCell["area"].name,cell=permissionTablesCell["area"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },

        //};
        static Dictionary<string, AppCell> permissionTablesCell = new Dictionary<string, AppCell>() {
           // { "home" ,new AppCell() { caption = "الصفحه الرئيسيه", name = "home" } },
            { "employee" ,new AppCell() { caption = "إدارة الموظفين", name = "employee" } },
            { "currency" ,new AppCell() { caption = "إدارة العملات", name = "currency" } },
            { "accountingGuide" ,new AppCell() { caption = "الدليل المحاسبي", name = "accountingGuide" } },
            { "custmore" ,new AppCell() { caption = "إدارة العملاء", name = "custmore" } },
            { "cashier" ,new AppCell() { caption = "إدارة الصناديق", name = "cashier" } },
            { "supplier" ,new AppCell() { caption = "إدارة الموردين", name = "supplier" } },
            { "area" ,new AppCell() { caption = "إدارة المناطق", name = "area" } },
            { "store" ,new AppCell() { caption = "إدارة المخازن", name = "store" } },
            { "accountGruop" ,new AppCell() { caption = "مجموغات الحسابات", name = "accountGruop" } },
            { "classifyGruop" ,new AppCell() { caption = "مجموعات الأصناف", name = "classifyGruop" } },
            { "uintGuide" ,new AppCell() { caption = "دليل الوحدات", name = "uintGuide" } },
            { "branch" ,new AppCell() { caption = "إدارة الفروع", name = "branch" } },
            { "city" ,new AppCell() { caption = "أدارة المدن", name = "city" } },
            { "catch" ,new AppCell() { caption = "سندات القبض", name = "catch" } },
            { "expanse" ,new AppCell() { caption = "سندات الصرف", name = "expanse" } },
            { "simpleJournalEntries" ,new AppCell() { caption = "القيود البسيطة", name = "simpleJournalEntries" } },
           { "compoundJournalEntries" ,new AppCell() { caption = "القيود المركبة", name = "compoundJournalEntries" } },
           { "openingBalances" ,new AppCell() { caption = "الأرصده الإفتتاحيه", name = "openingBalances" } },
           { "item" ,new AppCell() { caption = "إدارة الأصناف", name = "item" } },
           { "sale" ,new AppCell() { caption = "إدارة المبيعات", name = "sale" } },
           { "purchase" ,new AppCell() { caption = "إدارة المشتريات", name = "purchase" } },
           { "salesReturns" ,new AppCell() { caption = " إدارة مرتجع المبيعات", name = "salesReturns" } },
           { "purchasesReturns" ,new AppCell() { caption = "إدارة مرتجع المشتريات", name = "purchasesReturns" } },
           { "inventoryTransfer" ,new AppCell() { caption = "إدارة التحويل بين المخازن", name = "inventoryTransfer" } },
         //  { "accountStatement" ,new AppCell() { caption = "كشف حساب", name = "accountStatement" } 
            
         //   },
           
           
};
        
        public static Dictionary<string, Permission> permissions = new Dictionary<string, Permission>() {
       //     {"home",new Permission() { employeeId=0,tableName=permissionTablesCell["home"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"employee",new Permission() { employeeId=0,tableName=permissionTablesCell["employee"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"currency",new Permission() { employeeId=0,tableName=permissionTablesCell["currency"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"cashier",new Permission() { employeeId=0,tableName=permissionTablesCell["cashier"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"store",new Permission() { employeeId=0,tableName=permissionTablesCell["store"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"city",new Permission() { employeeId=0,tableName=permissionTablesCell["city"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"branch",new Permission() { employeeId=0,tableName=permissionTablesCell["branch"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"accountGruop",new Permission() { employeeId=0,tableName=permissionTablesCell["accountGruop"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"accountingGuide",new Permission() { employeeId=0,tableName=permissionTablesCell["accountingGuide"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"uintGuide",new Permission() { employeeId=0,tableName=permissionTablesCell["uintGuide"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"classifyGruop",new Permission() { employeeId=0,tableName=permissionTablesCell["classifyGruop"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"area",new Permission() { employeeId=0,tableName=permissionTablesCell["area"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"custmore",new Permission() { employeeId=0,tableName=permissionTablesCell["custmore"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"supplier",new Permission() { employeeId=0,tableName=permissionTablesCell["supplier"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"catch",new Permission() { employeeId=0,tableName=permissionTablesCell["catch"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"expanse",new Permission() { employeeId=0,tableName=permissionTablesCell["expanse"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"simpleJournalEntries",new Permission() { employeeId=0,tableName=permissionTablesCell["simpleJournalEntries"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"compoundJournalEntries",new Permission() { employeeId=0,tableName=permissionTablesCell["compoundJournalEntries"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"openingBalances",new Permission() { employeeId=0,tableName=permissionTablesCell["openingBalances"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"item",new Permission() { employeeId=0,tableName=permissionTablesCell["item"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"sale",new Permission() { employeeId=0,tableName=permissionTablesCell["sale"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"purchase",new Permission() { employeeId=0,tableName=permissionTablesCell["purchase"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"salesReturns",new Permission() { employeeId=0,tableName=permissionTablesCell["salesReturns"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"purchasesReturns",new Permission() { employeeId=0,tableName=permissionTablesCell["purchasesReturns"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"inventoryTransfer",new Permission() { employeeId=0,tableName=permissionTablesCell["inventoryTransfer"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
           // {"accountStatement",new Permission() { employeeId=0,tableName=permissionTablesCell["accountStatement"].name,addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },

        };
        public static Dictionary<string, PermissionGUI> permissionsTables = new Dictionary<string, PermissionGUI>() {
           // {"home",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["home"].name,cell=permissionTablesCell["home"],addPermission=false,updatePermission=false,viewPermission=false,deletePermission=false} },
            {"employee",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["employee"].name,cell=permissionTablesCell["employee"],addPermission=false,updatePermission=false,viewPermission=false,deletePermission=false} },
            {"currency",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["currency"].name,cell=permissionTablesCell["currency"],addPermission=false,updatePermission=false,viewPermission=false,deletePermission=false} },
            {"cashier",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["cashier"].name,cell=permissionTablesCell["cashier"],addPermission=false,updatePermission=false,viewPermission=false,deletePermission=false} },
            {"store",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["store"].name,cell=permissionTablesCell["store"],addPermission=false,updatePermission=false,viewPermission=false,deletePermission=false} },
            {"city",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["city"].name,cell=permissionTablesCell["city"],addPermission=false,updatePermission=false,viewPermission=false,deletePermission=false} },
            {"branch",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["branch"].name,cell=permissionTablesCell["branch"],addPermission=false,updatePermission=false,viewPermission=false,deletePermission=false} },
            {"accountGruop",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["accountGruop"].name,cell=permissionTablesCell["accountGruop"],addPermission=false,updatePermission=false,viewPermission=false,deletePermission=false} },
            {"accountingGuide",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["accountingGuide"].name,cell=permissionTablesCell["accountingGuide"],addPermission=false,updatePermission=false,viewPermission=false,deletePermission=false} },
            {"uintGuide",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["uintGuide"].name,cell=permissionTablesCell["uintGuide"],addPermission=false,updatePermission=false,viewPermission=false,deletePermission=false} },
            {"classifyGruop",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["classifyGruop"].name,cell=permissionTablesCell["classifyGruop"],addPermission=false,updatePermission=false,viewPermission=false,deletePermission=false} },
            {"area",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["area"].name,cell=permissionTablesCell["area"],addPermission=false,updatePermission=false,viewPermission=false,deletePermission=false} },
            {"custmore",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["custmore"].name,cell=permissionTablesCell["custmore"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"supplier",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["supplier"].name,cell=permissionTablesCell["supplier"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"catch",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["catch"].name,cell=permissionTablesCell["catch"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"expanse",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["expanse"].name,cell=permissionTablesCell["expanse"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"simpleJournalEntries",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["simpleJournalEntries"].name,cell=permissionTablesCell["simpleJournalEntries"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"compoundJournalEntries",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["compoundJournalEntries"].name,cell=permissionTablesCell["compoundJournalEntries"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"openingBalances",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["openingBalances"].name,cell=permissionTablesCell["openingBalances"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"item",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["item"].name,cell=permissionTablesCell["item"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"sale",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["sale"].name,cell=permissionTablesCell["sale"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"purchase",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["purchase"].name,cell=permissionTablesCell["purchase"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"salesReturns",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["salesReturns"].name,cell=permissionTablesCell["salesReturns"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"purchasesReturns",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["purchasesReturns"].name,cell=permissionTablesCell["purchasesReturns"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
            {"inventoryTransfer",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["inventoryTransfer"].name,cell=permissionTablesCell["inventoryTransfer"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },
       //     {"accountStatement",new PermissionGUI() { employeeId=0,tableName=permissionTablesCell["accountStatement"].name,cell=permissionTablesCell["accountStatement"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true} },

        };

        //{
        //   {"home", new AppCell() {caption =   "الصفحه الرئيسيه",name= "home"} },
        //   //// new AppCell() {caption ="إدارة الموظفين",name= "home"},
        //   // new AppCell() {caption ="الدليل المحاسبي",name= "home"},
        //   // new AppCell() {caption =  "إدارة العملاء",name= "home"},
        //   // new AppCell() {caption = "إدارة الصناديق",name= "home"},
        //   // new AppCell() {caption =  "إدارة الموردين",name= "home"},
        //   // new AppCell() {caption ="إدارة العملات",name= "home"},
        //   // new AppCell() {caption = "إدارة الصناديق",name= "home"},
        //   // new AppCell() {caption ="إدارة المخازن",name= "home"},
        //   // new AppCell() {caption ="سندات القبض",name= "home"},
        //   // new AppCell() {caption ="سندات الصرف",name= "home"},
        //   // new AppCell() {caption =  "القيود البسيطة",name= "home"},
        //   // new AppCell() {caption ="القيود المركبة",name= "home"},
        //   // new AppCell() {caption =,name= "home"},



        //};

        //public  List<Permission> permissions = new List<Permission>() {

        //      new Permission() { employeeId=0,tableName=permissionTablesNames["home"].name,cell=permissionTablesNames["home"],addPermission=true,updatePermission=true,viewPermission=true,deletePermission=true},

    }
}
