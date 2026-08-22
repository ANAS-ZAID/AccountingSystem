namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompositeItem")]
    public partial class CompositeItem
    {
        public int id { get; set; }

        public int? measurementItemId { get; set; }

        public int? componentItemId { get; set; }

        public decimal? quantity { get; set; }

        public decimal? purchasePrice { get; set; }

        public decimal? sellingPrice { get; set; }

        public virtual MeasurementsItem MeasurementsItem { get; set; }

        public virtual MeasurementsItem ComponentItem { get; set; }
    }
}
