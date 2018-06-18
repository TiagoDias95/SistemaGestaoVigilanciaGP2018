using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaGestaoVigilanciaGP2018.Data.Migrations
{
    public partial class formVigia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidoVigilancia",
                columns: table => new
                {
                    PrimeiroNome = table.Column<string>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    DataVigilancia = table.Column<DateTime>(nullable: false),
                    HoraVigilancia = table.Column<DateTime>(nullable: false),
                    IdPedido = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumeroDocente = table.Column<string>(nullable: false),
                    UCid = table.Column<int>(nullable: false),
                    UltimoNome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoVigilancia", x => x.IdPedido);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    IdC = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeCurso = table.Column<string>(nullable: true),
                    PedidoVigilanciaPrimeiroNome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.IdC);
                    table.ForeignKey(
                        name: "FK_Curso_PedidoVigilancia_PedidoVigilanciaPrimeiroNome",
                        column: x => x.PedidoVigilanciaPrimeiroNome,
                        principalTable: "PedidoVigilancia",
                        principalColumn: "PrimeiroNome",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeCurricular",
                columns: table => new
                {
                    IdUC = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeUC = table.Column<string>(nullable: true),
                    PedidoVigilanciaPrimeiroNome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeCurricular", x => x.IdUC);
                    table.ForeignKey(
                        name: "FK_UnidadeCurricular_PedidoVigilancia_PedidoVigilanciaPrimeiroNome",
                        column: x => x.PedidoVigilanciaPrimeiroNome,
                        principalTable: "PedidoVigilancia",
                        principalColumn: "PrimeiroNome",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curso_PedidoVigilanciaPrimeiroNome",
                table: "Curso",
                column: "PedidoVigilanciaPrimeiroNome");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeCurricular_PedidoVigilanciaPrimeiroNome",
                table: "UnidadeCurricular",
                column: "PedidoVigilanciaPrimeiroNome");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "UnidadeCurricular");

            migrationBuilder.DropTable(
                name: "PedidoVigilancia");
        }
    }
}
