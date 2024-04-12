using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APISensor.Models;

public class CSVDados
{

    public int CSVId { get; set; }
    public required string EquipmentId { get; set; }
    public DateTime Timestamp { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Value { get; set; }


}
