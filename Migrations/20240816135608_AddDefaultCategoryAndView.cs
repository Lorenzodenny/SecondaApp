using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondaApp.Migrations
{
    public partial class AddDefaultCategoryAndView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Creazione della tabella Categorie
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.CategoriaId);
                });

            // Inserisci la categoria "Default"
            migrationBuilder.InsertData(
                table: "Categorie",
                columns: new[] { "Nome" },
                values: new object[] { "Default" });

            // Aggiungi la colonna CategoriaId alla tabella Prodotti
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Prodotti",
                nullable: false,
                defaultValue: 1);

            // Aggiorna i record esistenti per impostare CategoriaId con l'ID della categoria "Default"
            migrationBuilder.Sql(@"
            UPDATE Prodotti
            SET CategoriaId = (SELECT CategoriaId FROM Categorie WHERE Nome = 'Default')
            WHERE CategoriaId IS NULL");

            // Creazione della relazione con la tabella Prodotti
            migrationBuilder.AddForeignKey(
                name: "FK_Prodotti_Categorie_CategoriaId",
                table: "Prodotti",
                column: "CategoriaId",
                principalTable: "Categorie",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminazione della vista
            migrationBuilder.Sql("DROP VIEW IF EXISTS ProdottoCategoriaView");

            // Elimina la relazione e la tabella Categorie
            migrationBuilder.DropForeignKey(
                name: "FK_Prodotti_Categorie_CategoriaId",
                table: "Prodotti");

            migrationBuilder.DropTable(
                name: "Categorie");

            migrationBuilder.DropIndex(
                name: "IX_Prodotti_CategoriaId",
                table: "Prodotti");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Prodotti");
        }
    }

}
