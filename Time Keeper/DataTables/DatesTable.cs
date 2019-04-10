namespace Time_Keeper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DatesTable")]
    public partial class Date
    {
        [Key]
        public DateTime DateID { get; set; }
        public virtual List<Entry> Entries { get; set; }
        public virtual List<Total> Totals { get; set; }
    }
}
