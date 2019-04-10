namespace Time_Keeper
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProgramsTable")]
    public partial class Program
    {
        public long ProgramID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Name { get; set; }

        public int Order { get; set; }

        [StringLength(2147483647)]
        public string Code { get; set; }

        [StringLength(2147483647)]
        public string Notes { get; set; }

        public virtual List<Entry> Entries { get; set; }
        public virtual List<Total> Totals { get; set; }
    }
}
