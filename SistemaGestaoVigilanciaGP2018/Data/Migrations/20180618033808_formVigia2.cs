using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaGestaoVigilanciaGP2018.Data.Migrations
{
    public partial class formVigia2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curso_PedidoVigilancia_PedidoVigilanciaPrimeiroNome",
                table: "Curso");

            migrationBuilder.DropForeignKey(
                name: "FK_UnidadeCurricular_PedidoVigilancia_PedidoVigilanciaPrimeiroNome",
                table: "UnidadeCurricular");

            migrationBuilder.DropIndex(
                name: "IX_UnidadeCurricular_PedidoVigilanciaPrimeiroNome",
                table: "UnidadeCurricular");

            migrationBuilder.DropIndex(
                name: "IX_Curso_PedidoVigilanciaPrimeiroNome",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "PedidoVigilanciaPrimeiroNome",
                table: "UnidadeCurricular");

            migrationBuilder.DropColumn(
                name: "PedidoVigilanciaPrimeiroNome",
                table: "Curso");

            migrationBuilder.AddColumn<int>(
                name: "IdpedidoV",
                table: "UnidadeCurricular",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdpedidoV",
                table: "Curso",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdpedidoV",
                table: "UnidadeCurricular");

            migrationBuilder.DropColumn(
                name: "IdpedidoV",
                table: "Curso");

            migrationBuilder.AddColumn<string>(
                name: "PedidoVigilanciaPrimeiroNome",
                table: "UnidadeCurricular",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PedidoVigilanciaPrimeiroNome",
                table: "Curso",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeCurricular_PedidoVigilanciaPrimeiroNome",
                table: "UnidadeCurricular",
                column: "PedidoVigilanciaPrimeiroNome");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_PedidoVigilanciaPrimeiroNome",
                table: "Curso",
                column: "PedidoVigilanciaPrimeiroNome");

            migrationBuilder.AddForeignKey(
                name: "FK_Curso_PedidoVigilancia_PedidoVigilanciaPrimeiroNome",
                table: "Curso",
                column: "PedidoVigilanciaPrimeiroNome",
                principalTable: "PedidoVigilancia",
                principalColumn: "PrimeiroNome",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnidadeCurricular_PedidoVigilancia_PedidoVigilanciaPrimeiroNome",
                table: "UnidadeCurricular",
                column: "PedidoVigilanciaPrimeiroNome",
                principalTable: "PedidoVigilancia",
                principalColumn: "PrimeiroNome",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
