using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.controller
{
    public class HomeScreenController
    {
        AccountingDbContext dBContext;
        public string countCustomers{ get {
                string c = "0";
                if(dBContext.Customers.Any())
                    c= dBContext.Customers.ToList().Count().ToString();
                return c; } }
        public string countPurchases { get {
                string c = "0";
                if(dBContext.Purchases.Any())
                    c= dBContext.Purchases.ToList().Count().ToString();
                return c; } }
        public string countSales { get {
                string c = "0";
                if(dBContext.Sales.Any())
                c=    dBContext.Sales.ToList().Count().ToString();
                return c;  } }
        public string countItems { get {
                string c = "0";
                if(dBContext.Classifies.Any())
                    c= dBContext.Classifies.ToList().Count().ToString();
                return c; } }
        public string countCashier { get {
                string c = "0";
                if (dBContext.Cashiers.Any())
                 c=   dBContext.Cashiers.ToList().Count().ToString();
                return c; } }    
        public string countEmpolyee
        { get {
                string c = "0";
                if (dBContext.Employees.Any())
                  c=  dBContext.Employees.ToList().Count().ToString();
                return c; } }
       public HomeScreenController()
        {
            dBContext = new AccountingDbContext();
        }
    }
}
