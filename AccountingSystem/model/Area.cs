using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.controller;
using AccountingSystem.Model;

namespace AccountingSystem.model
{
    public class Area:model.BaseEntity
    {
        private static Area instance;
       public  int cityId {  get; set; }
        private Area()
        {


        }
        public static Area getInstance()
        {
            if (instance == null)
                instance = new Area();
            return instance;

        }
        private Area(int id, string name,int cityId)
        {

            this.Id = id;
            this.Name = name;
            this.cityId=cityId;

        }
        public void setAll(int id, string name,int cityId)
        {


            this.Id = id;
            this.Name = name;
            this.cityId = cityId;

        }

        public List<Area> getAreasFromDataTable(DataTable dataTable)
        {

            List<Area> cities = new List<Area>();
            foreach (DataRow row in dataTable.Rows)
            {
                Area city = new Area((int)row["id"], (string)row["name"], (int)row["cityId"]);

                cities.Add(city);
            }
            return cities;
        }
        public DataRow getDataRowFromArea(DataTable dataTable)
        {
            
            DataTable data=dataTable.Clone();
            data.Columns.Remove("cityName");
            DataRow row = data.NewRow();
            row["id"] = Id;
            row["name"] = Name;
            row["cityId"]= cityId;
            
            return row;
        }
        public Area getAreaFromDataRow(DataRow dataRow)
        {
            return new Area((int)dataRow["id"], (string)dataRow["name"],(int)dataRow["cityId"]);
        }
    }
}
