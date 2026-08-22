namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Sales = new HashSet<Sale>();
        }

        public int id { get; set; }

        [Required]
        public string name { get; set; }

        public string phoneNamber { get; set; }

        [Column(TypeName = "text")]
        public string address { get; set; }

        public int? accountId { get; set; }

        public int? cityId { get; set; }

        public int? areaId { get; set; }

        public virtual Area Area { get; set; }

        public virtual ChartOfAccount Account { get; set; }

        public virtual City City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
