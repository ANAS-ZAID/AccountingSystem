namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Purchase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Purchase()
        {
            PurchaseDetails = new HashSet<PurchaseDetail>();
        }

        public int id { get; set; }
        public int? originalInvoiceId { get; set; }
        public decimal? exchangeRate { get; set; }

        public int? number { get; set; }

        public int? cashierId { get; set; }

        public int? employeeId { get; set; }

        public int? storeId { get; set; }

        public int? supplierId { get; set; }

        public int? currencyId { get; set; }

        public DateTime? date { get; set; }

        public DateTime? enteryDate { get; set; }

        [Column(TypeName = "text")]
        public string paymentType { get; set; }

        [Column(TypeName = "text")]
        public string type { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        public decimal? amountPaid { get; set; }

        [Column(TypeName = "text")]
        public string priceType { get; set; }

        public int? brancheId { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Cashier Cashier { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }

        public virtual Store Store { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
