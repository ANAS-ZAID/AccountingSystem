namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InventoryTransfer")]
    public partial class InventoryTransfer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InventoryTransfer()
        {
            InventoryTransferDetails = new HashSet<InventoryTransferDetail>();
        }

        public int id { get; set; }
        public decimal? exchangeRate { get; set; }

        public int? number { get; set; }

        public int? employeeId { get; set; }

        public int? fromStoreId { get; set; }

        public int? toStoreId { get; set; }

        public int? currencyId { get; set; }

        public DateTime? date { get; set; }

        public DateTime? enteryDate { get; set; }

        public DateTime? updateDate { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        public int? brancheId { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Store FromStore { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryTransferDetail> InventoryTransferDetails { get; set; }

        public virtual Store ToStore { get; set; }
    }
}
