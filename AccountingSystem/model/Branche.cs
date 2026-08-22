using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Instrumentation;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AccountingSystem.Model;

namespace AccountingSystem.model
{
    public class Branch : BaseEntity

    {
        private static Branch instance;

        public string administratorName { get; set; }

        public string phoneNumber { get; set; }

        public string address { get; set; }

        public int? storeId { get; set; }

        public int cityId { get; set; }

        public int areaId { get; set; }



        public virtual Store Store { get; set; }

        public virtual City City { get; set; }

        public virtual Area Area { get; set; }
        public static Branch getInstance()
        {
            if (instance == null)
                instance = new Branch();
            return instance;

        }

        public void setAll(int id, string name, string administratorName, string phoneNumber, int cityId, int areaId, string address, int? storeId)
        {


            this.Id = id;
            this.Name = name;
            this.administratorName = administratorName;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.storeId = storeId;
            this.cityId = cityId;
            this.areaId = areaId;

        }

        public List<Branch> getBranchsFromDataTable(DataTable dataTable)
        {

            List<Branch> branches = new List<Branch>();
            foreach (DataRow row in dataTable.Rows)
            {
                Branch branch = getInstance();
                branch.setAll((int)row["id"], (string)row["name"], (string)row["administratorName"], (string)row["phoneNumber"], (int)row["cityId"], (int)row["areaId"],
                    (string)row["address"], (int)row["storeId"]);
                branches.Add(branch);
            }
            return branches;
        }
        public DataRow getDataRow(DataTable dataTable)
        {

            DataTable data = dataTable.Clone();
            data.Columns.Remove("cityName");
            data.Columns.Remove("areaName");
            DataRow row = data.NewRow();
            row["id"] = Id;
            row["name"] = Name;
            row["administratorName"] = administratorName;
            row["phoneNumber"] = phoneNumber;

            row["cityId"] = cityId;
            row["areaId"] = areaId;
            row["address"] = address;
            if (storeId != null)
                row["storeId"] = storeId;
            else row["storeId"] = DBNull.Value;
           
            return row;
        }
        public Branch fromDataRow(DataRow row)
        {
            Branch branch = getInstance();
            branch.setAll((int)row["id"], (string)row["name"], (string)row["administratorName"], (string)row["phoneNumber"], (int)row["cityId"], (int)row["areaId"], (string)row["address"], (int)row["storeId"]);
            return branch;
        }
    }
}
