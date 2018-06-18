using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaGestaoVigilanciaGP2018.Data.Migrations
{
    public partial class trocasdechaves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoVigilancia",
                table: "PedidoVigilancia");

            migrationBuilder.AddColumn<int>(
                name: "PedidoVigilanciaIdPedido",
                table: "UnidadeCurricular",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrimeiroNome",
                table: "PedidoVigilancia",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "PedidoVigilanciaIdPedido",
                table: "Curso",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoVigilancia",
                table: "PedidoVigilancia",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeCurricular_PedidoVigilanciaIdPedido",
                table: "UnidadeCurricular",
                column: "PedidoVigilanciaIdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_PedidoVigilanciaIdPedido",
                table: "Curso",
                column: "PedidoVigilanciaIdPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_Curso_PedidoVigilancia_PedidoVigilanciaIdPedido",
                table: "Curso",
                column: "PedidoVigilanciaIdPedido",
                principalTable: "PedidoVigilancia",
                principalColumn: "IdPedido",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnidadeCurricular_PedidoVigilancia_PedidoVigilanciaIdPedido",
                table: "UnidadeCurricular",
                column: "PedidoVigilanciaIdPedido",
                principalTable: "PedidoVigilancia",
                principalColumn: "IdPedido",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curso_PedidoVigilancia_PedidoVigilanciaIdPedido",
                table: "Curso");

            migrationBuilder.DropForeignKey(
                name: "FK_UnidadeCurricular_PedidoVigilancia_PedidoVigilanciaIdPedido",
                table: "UnidadeCurricular");

            migrationBuilder.DropIndex(
                name: "IX_UnidadeCurricular_PedidoVigilanciaIdPedido",
                table: "UnidadeCurricular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoVigilancia",
                table: "PedidoVigilancia");

            migrationBuilder.DropIndex(
                name: "IX_Curso_PedidoVigilanciaIdPedido",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "PedidoVigilanciaIdPedido",
                table: "UnidadeCurricular");

            migrationBuilder.DropColumn(
                name: "PedidoVigilanciaIdPedido",
                table: "Curso");

            migrationBuilder.AlterColumn<string>(
                name: "PrimeiroNome",
                table: "PedidoVigilancia",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoVigilancia",
                table: "PedidoVigilancia",
                column: "PrimeiroNome");
        }
    }
}
