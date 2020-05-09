using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TesteTailorIT.Infra.Data.Models.Base;

namespace TesteTailorIT.Infra.Data.Models
{
    public class HabilidadeDataModel : DataModel
    {
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Descricao { get; set; }
    }
}