namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Permission
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? employeeId { get; set; }

        public string tableName { get; set; }

        public bool? addPermission { get; set; }

        public bool? deletePermission { get; set; }

        public bool? updatePermission { get; set; }

        public bool? viewPermission { get; set; }

        public bool? importFromExcelPermission { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
