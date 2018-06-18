using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaGestaoVigilanciaGP2018.Data.Migrations
{
    public partial class objectosTabelaVigi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
