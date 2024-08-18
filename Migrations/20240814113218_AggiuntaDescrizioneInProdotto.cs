using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondaApp.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntaDescrizioneInProdotto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descrizione",
                table: "Prodotti",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descrizione",
                table: "Prodotti");
        }
    }
}
