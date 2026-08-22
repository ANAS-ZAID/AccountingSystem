namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Operation
    {
        public int id { get; set; }

        public string operationName { get; set; }

        public string operationType { get; set; }

        public int? employeeId { get; set; }

        public string description { get; set; }

        public int? operationNumber { get; set; }

        public DateTime? date { get; set; }
    }
}
