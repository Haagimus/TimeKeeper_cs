namespace Time_Keeper
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TotalsTable")]
    public partial class Total
    {
        public long TotalID { get; set; }

        [Column(TypeName = "real")]
        public double? Hours { get; set; }

        [StringLength(2147483647)]
        public string Comments { get; set; }

        public virtual Program ProgramID { get; set; }
        public virtual Date DateID { get; set; }
    }
}
