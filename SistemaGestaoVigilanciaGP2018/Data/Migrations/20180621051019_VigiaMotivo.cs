using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaGestaoVigilanciaGP2018.Migrations
{
    public partial class VigiaMotivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Motivo",
                table: "PedidoVigilancia",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Motivo",
                table: "PedidoVigilancia");
        }
    }
}
