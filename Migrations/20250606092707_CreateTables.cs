using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enclosures",
                columns: table => new
                {
                    EnclosureId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MaxCapacity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enclosures", x => x.EnclosureId);
                });

            migrationBuilder.CreateTable(
                name: "Zookeepers",
                columns: table => new
                {
                    ZookeeperId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zookeepers", x => x.ZookeeperId);
                });

            migrationBuilder.CreateTable(
                name: "ZookeeperAndAnimals",
                columns: table => new
                {
                    ZookeeperAndAnimalId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ZookeeperId = table.Column<int>(type: "INTEGER", nullable: false),
                    AnimalId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZookeeperAndAnimals", x => x.ZookeeperAndAnimalId);
                    table.ForeignKey(
                        name: "FK_ZookeeperAndAnimals_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZookeeperAndAnimals_Zookeepers_ZookeeperId",
                        column: x => x.ZookeeperId,
                        principalTable: "Zookeepers",
                        principalColumn: "ZookeeperId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZookeeperAndEnclosures",
                columns: table => new
                {
                    ZookeeperAndEnclosureId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ZookeeperId = table.Column<int>(type: "INTEGER", nullable: false),
                    EnclosureId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZookeeperAndEnclosures", x => x.ZookeeperAndEnclosureId);
                    table.ForeignKey(
                        name: "FK_ZookeeperAndEnclosures_Enclosures_EnclosureId",
                        column: x => x.EnclosureId,
                        principalTable: "Enclosures",
                        principalColumn: "EnclosureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZookeeperAndEnclosures_Zookeepers_ZookeeperId",
                        column: x => x.ZookeeperId,
                        principalTable: "Zookeepers",
                        principalColumn: "ZookeeperId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_EnclosureId",
                table: "Animals",
                column: "EnclosureId");

            migrationBuilder.CreateIndex(
                name: "IX_ZookeeperAndAnimals_AnimalId",
                table: "ZookeeperAndAnimals",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_ZookeeperAndAnimals_ZookeeperId",
                table: "ZookeeperAndAnimals",
                column: "ZookeeperId");

            migrationBuilder.CreateIndex(
                name: "IX_ZookeeperAndEnclosures_EnclosureId",
                table: "ZookeeperAndEnclosures",
                column: "EnclosureId");

            migrationBuilder.CreateIndex(
                name: "IX_ZookeeperAndEnclosures_ZookeeperId",
                table: "ZookeeperAndEnclosures",
                column: "ZookeeperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Enclosures_EnclosureId",
                table: "Animals",
                column: "EnclosureId",
                principalTable: "Enclosures",
                principalColumn: "EnclosureId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Enclosures_EnclosureId",
                table: "Animals");

            migrationBuilder.DropTable(
                name: "ZookeeperAndAnimals");

            migrationBuilder.DropTable(
                name: "ZookeeperAndEnclosures");

            migrationBuilder.DropTable(
                name: "Enclosures");

            migrationBuilder.DropTable(
                name: "Zookeepers");

            migrationBuilder.DropIndex(
                name: "IX_Animals_EnclosureId",
                table: "Animals");
        }
    }
}
