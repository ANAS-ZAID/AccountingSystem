namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Classify")]
    public partial class Classify
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Classify()
        {
            BeginningInventories = new HashSet<BeginningInventory>();
            Consumptions = new HashSet<Consumption>();
            Childrens = new HashSet<Classify>();
            Inventories = new HashSet<Inventory>();
            InventoryTransferDetails = new HashSet<InventoryTransferDetail>();
            PurchaseDetails = new HashSet<PurchaseDetail>();
            SaleDetails = new HashSet<SaleDetail>();
            MeasurementsItems = new HashSet<MeasurementsItem>();
        }

        public int id { get; set; }

        [Required]
        public string nameAr { get; set; }

        [Required]
        public string nameEn { get; set; }

        public byte[] image { get; set; }

        [Required]
        public string type { get; set; }

        public string description { get; set; }

        public int? ClassifyNumber { get; set; }

        public int? parentId { get; set; }

        public int? ClassifyGroupId { get; set; }

        public int? typeClassifyId { get; set; }

        public int? companyId { get; set; }

        public bool? visible { get; set; }

        public int? rankk { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BeginningInventory> BeginningInventories { get; set; }

        public virtual Company Company { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consumption> Consumptions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Classify> Childrens { get; set; }

        public virtual Classify perantItem { get; set; }

        public virtual ClassifyGroup ClassifyGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventory> Inventories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryTransferDetail> InventoryTransferDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeasurementsItem> MeasurementsItems { get; set; }

        public virtual TypesClassify TypesClassify { get; set; }
    }
}
