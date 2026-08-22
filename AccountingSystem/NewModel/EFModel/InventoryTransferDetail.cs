namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InventoryTransferDetail
    {
        
        public int id { get; set; }

        public int? inventoryTransferId { get; set; }

        public int? measurementItemId { get; set; }

        public int? itemId { get; set; }

        public string type { get; set; }

        public decimal? quantity { get; set; }
       
        public decimal? unitPrice { get; set; }

        public string description { get; set; }
        [DefaultValue(true)]
        public bool main { get; set; } = true;
        public virtual Classify item { get; set; }

        public virtual InventoryTransfer InventoryTransfer { get; set; }

        public virtual MeasurementsItem MeasurementsItem { get; set; }
    }
}
