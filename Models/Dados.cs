using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APISensor.Models;

public class Dados
{
    public Dados() {
        DadosSensores = new Collection<CSVDados>();
    }

    [Key]
    public string EquipmentId { get; set; }
    public DateTime Timestamp { get; set; }
    [Required]
    [Column(TypeName ="decimal(10,2)")]
    public decimal? Value { get; set; }

    public ICollection<CSVDados>? DadosSensores { get; set; }

}
