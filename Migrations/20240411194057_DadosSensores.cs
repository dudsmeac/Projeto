using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISensor.Migrations
{
    /// <inheritdoc />
    public partial class DadosSensores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Dados(equipmentId, Timestamp, Value) Values ('EQ-12495', '2023-02-15T01:30:00.000-05:00', '78.42')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Dados");
        }
    }
}
