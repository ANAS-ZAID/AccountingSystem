namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MeasurementsItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeasurementsItem()
        {
            BeginningInventories = new HashSet<BeginningInventory>();
            CompositeItems = new HashSet<CompositeItem>();
            CompositeItems1 = new HashSet<CompositeItem>();
            Inventories = new HashSet<Inventory>();
            InventoryTransferDetails = new HashSet<InventoryTransferDetail>();
            PurchaseDetails = new HashSet<PurchaseDetail>();
            SaleDetails = new HashSet<SaleDetail>();
        }

        public int id { get; set; }

        public int? UnitId { get; set; }

        public int? barcode { get; set; }

        public int? itemId { get; set; }

        public decimal? purchasePrice { get; set; }

        public decimal? sellingPrice { get; set; }

        public decimal? WholesalePrice { get; set; }

        public decimal? WholesalePurchasePrice { get; set; }

        public decimal? descountPrice { get; set; }

        public decimal? minimumPurchaseAmount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BeginningInventory> BeginningInventories { get; set; }

        public virtual Classify item { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompositeItem> CompositeItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompositeItem> CompositeItems1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventory> Inventories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryTransferDetail> InventoryTransferDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }

        public virtual Unit Unit { get; set; }
    }
}
