using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioAPI.infra.Migrations
{
    public partial class SeedSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Username", "Password", "Email", "Role", "IsActive" },
                values: new object[] { 1, "Admin", "Gft@1234", "vinicius.escobedo@invalido.com", 0, true});

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Username", "Password", "Email", "Role", "IsActive" },
                values: new object[] { 2, "User", "Gft@1234", "vinicius.escobedo@invalido.com", 1, true});

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Technology", "IsActive" },
                values: new object[] { 1, "2022_TURMA_01", "DOTNET", true});

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Technology", "IsActive" },
                values: new object[] { 2, "2022_TURMA_01", "JAVA", true});

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Technology", "IsActive" },
                values: new object[] { 3, "2022_TURMA_02", "ANGULAR", true});

            migrationBuilder.InsertData(
                table: "Starters",
                columns: new[] { "Id", "Name", "Cpf", "Abbreviation", "Email", "Photo", "CategoryId", "IsActive" },
                values: new object[] { 1, "Leonardo da Vinci", "74485295281", "lovi", "leonardo.vinci@invalido.com", "1a0e54b8-love.jpg", 1, true});

            migrationBuilder.InsertData(
                table: "Starters",
                columns: new[] { "Id", "Name", "Cpf", "Abbreviation", "Email", "Photo", "CategoryId", "IsActive" },
                values: new object[] { 2, "Rafael Sanzio", "42813815705", "rlso", "rafael.sanzio@invalido.com", "b5468c08-rlso.jpg", 1, true});

            migrationBuilder.InsertData(
                table: "Starters",
                columns: new[] { "Id", "Name", "Cpf", "Abbreviation", "Email", "Photo", "CategoryId", "IsActive" },
                values: new object[] { 3, "Michelangelo Buonarroti", "87817848998", "mobi", "michelangelo.buonarroti@invalido.com", "670464cf-mobi.jpg", 2, true});

            migrationBuilder.InsertData(
                table: "Starters",
                columns: new[] { "Id", "Name", "Cpf", "Abbreviation", "Email", "Photo", "CategoryId", "IsActive" },
                values: new object[] { 4, "Donato di Niccoló", "38766311063", "dono", "donato.nicollo@invalido.com", "d8069d23-dono.jpg", 2, true});

            migrationBuilder.InsertData(
                table: "Starters",
                columns: new[] { "Id", "Name", "Cpf", "Abbreviation", "Email", "Photo", "CategoryId", "IsActive" },
                values: new object[] { 5, "Sandro Botticelli", "21454316160", "sobi", "sandro.botticelli@invalido.com", "Default.jpg", 3, false});             
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
