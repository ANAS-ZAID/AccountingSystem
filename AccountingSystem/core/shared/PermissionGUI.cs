using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AccountingSystem.NewModel.EFModel;


namespace AccountingSystem.core.shared
{
    public class PermissionGUI
    {
        public int id { get; set; }

        public int? employeeId { get; set; }

        public string tableName { get; set; }

        public bool addPermission { get; set; }

        public bool deletePermission { get; set; }

        public bool updatePermission { get; set; }

        public bool viewPermission { get; set; }

        public bool? importFromExcelPermission { get; set; }
        //  
        public virtual Employee Employee { get; set; }
        public AppCell cell { get; set; }

    }
}
