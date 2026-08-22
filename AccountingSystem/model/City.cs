using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AccountingSystem.model;


namespace AccountingSystem.Model
{
    public class City : model.BaseEntity
    {
        private static City instance;


     private   City()
        {
            

        }
        public static City getInstance()
        {
            if (instance == null)
                instance = new City();
            return instance;

        }
     private    City(int id, string name)
        {

            this.Id = id;
            this.Name = name;
         
        }
        public void setAll(int id, string name)
        {


            this.Id = id;
            this.Name = name;
          
        }

        public List<City> getCitiesFromDataTable(DataTable dataTable)
        {

            List<City> cities = new List<City>();
            foreach (DataRow row in dataTable.Rows)
            {
                City city = new City((int)row["id"], (string)row["name"]);

                cities.Add(city);
            }
            return cities;
        }
        public DataRow getDataRowFromCity(DataTable dataTable)
        {
            DataRow row = dataTable.NewRow();
            row["id"] = Id;
            row["name"] = Name;
            return row;
        }
        public City getCityFromDataRow(DataRow dataRow)
        {
            return new City((int) dataRow["id"],(string)dataRow["name"]);
        }
    }
}
