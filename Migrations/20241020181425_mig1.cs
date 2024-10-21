using Microsoft.EntityFrameworkCore.Migrations;

namespace HACKATHON.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.KategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Sorumlular",
                columns: table => new
                {
                    SorumluId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parola = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sorumlular", x => x.SorumluId);
                });

            migrationBuilder.CreateTable(
                name: "Gruplar",
                columns: table => new
                {
                    GrupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrupAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gruplar", x => x.GrupId);
                    table.ForeignKey(
                        name: "FK_Gruplar_Kategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "KategoriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DegerTurleri",
                columns: table => new
                {
                    DegerTuruId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DegerTuruKodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegerTuruAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VeriPeriyodu = table.Column<int>(type: "int", nullable: false),
                    GrupId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DegerTurleri", x => x.DegerTuruId);
                    table.ForeignKey(
                        name: "FK_DegerTurleri_Gruplar_GrupId",
                        column: x => x.GrupId,
                        principalTable: "Gruplar",
                        principalColumn: "GrupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SorumluAtamalari",
                columns: table => new
                {
                    SorumluAtamaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SorumluId = table.Column<int>(type: "int", nullable: false),
                    DegerTuruId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SorumluAtamalari", x => x.SorumluAtamaId);
                    table.ForeignKey(
                        name: "FK_SorumluAtamalari_DegerTurleri_DegerTuruId",
                        column: x => x.DegerTuruId,
                        principalTable: "DegerTurleri",
                        principalColumn: "DegerTuruId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SorumluAtamalari_Sorumlular_SorumluId",
                        column: x => x.SorumluId,
                        principalTable: "Sorumlular",
                        principalColumn: "SorumluId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VeriGirisleri",
                columns: table => new
                {
                    VeriGirisiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Yil = table.Column<int>(type: "int", nullable: false),
                    Donem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deger = table.Column<float>(type: "real", nullable: false),
                    DegerTuruId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeriGirisleri", x => x.VeriGirisiId);
                    table.ForeignKey(
                        name: "FK_VeriGirisleri_DegerTurleri_DegerTuruId",
                        column: x => x.DegerTuruId,
                        principalTable: "DegerTurleri",
                        principalColumn: "DegerTuruId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DegerTurleri_GrupId",
                table: "DegerTurleri",
                column: "GrupId");

            migrationBuilder.CreateIndex(
                name: "IX_Gruplar_KategoriId",
                table: "Gruplar",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_SorumluAtamalari_DegerTuruId",
                table: "SorumluAtamalari",
                column: "DegerTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_SorumluAtamalari_SorumluId",
                table: "SorumluAtamalari",
                column: "SorumluId");

            migrationBuilder.CreateIndex(
                name: "IX_VeriGirisleri_DegerTuruId",
                table: "VeriGirisleri",
                column: "DegerTuruId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SorumluAtamalari");

            migrationBuilder.DropTable(
                name: "VeriGirisleri");

            migrationBuilder.DropTable(
                name: "Sorumlular");

            migrationBuilder.DropTable(
                name: "DegerTurleri");

            migrationBuilder.DropTable(
                name: "Gruplar");

            migrationBuilder.DropTable(
                name: "Kategoriler");
        }
    }
}
