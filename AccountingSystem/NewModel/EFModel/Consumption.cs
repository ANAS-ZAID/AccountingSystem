namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Consumption")]
    public partial class Consumption
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        public int? classifyId { get; set; }

        public decimal? quantity { get; set; }

        public int? storeId { get; set; }

        public int? saleID { get; set; }

        public virtual Classify Classify { get; set; }

        public virtual Sale Sale { get; set; }

        public virtual Store Store { get; set; }
    }
}
