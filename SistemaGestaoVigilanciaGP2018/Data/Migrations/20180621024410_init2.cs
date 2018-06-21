using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaGestaoVigilanciaGP2018.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoVigilancia_Curso_CursoId",
                table: "PedidoVigilancia");

            migrationBuilder.DropIndex(
                name: "IX_PedidoVigilancia_CursoId",
                table: "PedidoVigilancia");

            migrationBuilder.AddColumn<string>(
                name: "Curso",
                table: "PedidoVigilancia",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UC",
                table: "PedidoVigilancia",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Curso",
                table: "PedidoVigilancia");

            migrationBuilder.DropColumn(
                name: "UC",
                table: "PedidoVigilancia");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoVigilancia_CursoId",
                table: "PedidoVigilancia",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoVigilancia_Curso_CursoId",
                table: "PedidoVigilancia",
                column: "CursoId",
                principalTable: "Curso",
                principalColumn: "IdC",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
