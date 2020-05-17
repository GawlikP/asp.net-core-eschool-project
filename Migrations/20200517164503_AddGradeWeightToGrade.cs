using Microsoft.EntityFrameworkCore.Migrations;

namespace lista_7.Migrations
{
    public partial class AddGradeWeightToGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Grades",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Grades");
        }
    }
}
