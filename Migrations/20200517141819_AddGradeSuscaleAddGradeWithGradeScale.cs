using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace lista_7.Migrations
{
    public partial class AddGradeSuscaleAddGradeWithGradeScale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "gradeScaleId",
                table: "Grades",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GradeScales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UpBorder = table.Column<int>(nullable: false),
                    DownBorder = table.Column<int>(nullable: false),
                    AllowHalfGrades = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeScales", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_gradeScaleId",
                table: "Grades",
                column: "gradeScaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_GradeScales_gradeScaleId",
                table: "Grades",
                column: "gradeScaleId",
                principalTable: "GradeScales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_GradeScales_gradeScaleId",
                table: "Grades");

            migrationBuilder.DropTable(
                name: "GradeScales");

            migrationBuilder.DropIndex(
                name: "IX_Grades_gradeScaleId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "gradeScaleId",
                table: "Grades");
        }
    }
}
