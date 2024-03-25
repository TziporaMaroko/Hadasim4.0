using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Hadasim4._0.Models;
public class MemberVaccinationRelation
{
    [Key, Column(Order = 0)]
    [ForeignKey("Vaccination")]
    public int VaccinationId { get; set; }

    [Key, Column(Order = 1)]
    [ForeignKey("Member")]
    public int MemberId { get; set; }

    public DateTime Date { get; set; }
}
