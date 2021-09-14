using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SwaggerTraining.Migrations
{
    public partial class InitilMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeveloperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName" },
                values: new object[] { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new DateTime(1610, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eddard", "Stark" });

            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName" },
                values: new object[] { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), new DateTime(1652, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jaime", "Lannister" });

            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName" },
                values: new object[] { new Guid("d902b665-1190-4c70-9915-b9c2d7680450"), new DateTime(1653, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daenerys", "Targaryen" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "DeveloperId", "Name" },
                values: new object[,]
                {
                    { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), "A training for OpenAPI and Swashbuckle.", new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "OpenAPI Training" },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "A training for RESTful APIs in .NETCore.", new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "RESTful APIs Training" },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9ba2"), "A training for Docker.", new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "Docker Training" },
                    { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c7a"), "A training for Git.", new Guid("d902b665-1190-4c70-9915-b9c2d7680450"), "Git Training" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DeveloperId",
                table: "Projects",
                column: "DeveloperId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Developers");
        }
    }
}
