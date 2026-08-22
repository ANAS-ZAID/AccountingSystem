namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            BeginningInventories = new HashSet<BeginningInventory>();
            CompoundEntries = new HashSet<CompoundEntry>();
            Trades = new HashSet<Trade>();
            Permissions = new HashSet<Permission>();
            InventoryTransfers = new HashSet<InventoryTransfer>();
            JournalEntries = new HashSet<JournalEntry>();
            Purchases = new HashSet<Purchase>();
            Sales = new HashSet<Sale>();
            SimpleEntries = new HashSet<SimpleEntry>();
            Vouchers = new HashSet<Voucher>();
        }

        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string password { get; set; }

        public string phoneNamber { get; set; }

        public bool? status { get; set; }

        [Required]
        public string loginName { get; set; }

        public int accountId { get; set; }

        public int? cashierId { get; set; }

        public int? brancheId { get; set; }

        public int? employeeTypeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BeginningInventory> BeginningInventories { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Cashier Cashier { get; set; }

        public virtual ChartOfAccount Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompoundEntry> CompoundEntries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Trade> Trades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Permission> Permissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryTransfer> InventoryTransfers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JournalEntry> JournalEntries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase> Purchases { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SimpleEntry> SimpleEntries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Voucher> Vouchers { get; set; }

        public virtual EmployeesType EmployeesType { get; set; }
    }
}
