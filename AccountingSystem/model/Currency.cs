using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AccountingSystem.controller;


namespace AccountingSystem.Model
{
    public class Currency :model.BaseEntity
    {
        private static Currency instance;
        public string code { get; set; }
        public string currencyType { get; set; }
        public decimal exchangeRate { get; set; }

        private Currency()
        {

            
        }
        public static Currency getInstance()
        {
            if (instance == null)
                    instance = new Currency();
            return instance;
        }
        private Currency(int id, string name, string code, decimal exchangeRate, string currencyType)
        {

            this.Id = id;
            this.Name = name;
            this.code = code;
            this.currencyType = currencyType;
            this.exchangeRate = exchangeRate;
        }
        public void setAll(int id, string name, string code, decimal exchangeRate, string currencyType)
        {
          

            this.Id = id;
            this.Name = name;
            this.code = code;
            this.currencyType = currencyType;
            this.exchangeRate = exchangeRate;
        }
       
        public List<Currency> getCurrenciesFromDataTable(DataTable dataTable)
        {

            List<Currency> currencies = new List<Currency>();
            foreach (DataRow row in dataTable.Rows)
            {
                Currency currency = new Currency((int)row["id"], (string)row["name"], (string)row["code"], (decimal)row["exchangeRate"], (string)row["currencyType"]);

                currencies.Add(currency);
            }
            return currencies;
        }
        public DataRow getDataRowFromCurrency(DataTable dataTable)
        {
            DataRow row = dataTable.NewRow();
            row["id"] = Id;
            row["name"] = Name;
            row["code"] = code;
            row["currencyType"] = currencyType;
            row["exchangeRate"] = exchangeRate;

            return row;
        }
    }
}
