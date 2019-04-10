namespace Time_Keeper
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EntriesTable")]
    public partial class Entry
    {
        public long EntryID { get; set; }

        public DateTime In { get; set; }

        public DateTime? Out { get; set; }

        [Column(TypeName = "real")]
        public double? Hours { get; set; }

        public virtual Program ProgramID { get; set; }
        public virtual Date DateID { get; set; }
    }
}
