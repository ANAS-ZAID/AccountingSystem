namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PurchaseDetail
    {
        public int id { get; set; }

        public int? purchaseID { get; set; }

        public int? measurementItemId { get; set; }

        public int? itemId { get; set; }

        public decimal? quantity { get; set; }

        public decimal? unitPrice { get; set; }

        public string description { get; set; }

        public string type { get; set; }

        [Column(TypeName = "date")]
        public DateTime? endDate { get; set; }

        public virtual Classify item { get; set; }

        public virtual MeasurementsItem MeasurementsItem { get; set; }

        public virtual Purchase Purchase { get; set; }
    }
}
