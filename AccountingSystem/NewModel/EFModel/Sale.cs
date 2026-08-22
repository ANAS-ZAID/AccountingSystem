namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sale()
        {
            Consumptions = new HashSet<Consumption>();
            SaleDetails = new HashSet<SaleDetail>();
        }

        public int id { get; set; }
        public int? originalInvoiceId { get; set; }

        public int? number { get; set; }

        public int? cashierId { get; set; }

        public int? employeeId { get; set; }

        public int? storeId { get; set; }

        public int? customerId { get; set; }

        public int? currencyId { get; set; }

        [Column(TypeName = "text")]
        public string type { get; set; }

        public DateTime? date { get; set; }

        public DateTime? enteryDate { get; set; }

        [Column(TypeName = "text")]
        public string paymentType { get; set; }

        [Column(TypeName = "text")]
        public string priceType { get; set; }

        [Column(TypeName = "text")]
        public string orderType { get; set; }

        [Column(TypeName = "text")]
        public string orderTime { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        public decimal? amountPaid { get; set; }

        public decimal? descountPrice { get; set; }
        public decimal? exchangeRate { get; set; }
        public int? brancheId { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Cashier Cashier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consumption> Consumptions { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }

        public virtual Store Store { get; set; }
    }
}
