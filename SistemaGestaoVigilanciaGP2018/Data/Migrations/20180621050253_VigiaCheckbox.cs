using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaGestaoVigilanciaGP2018.Migrations
{
    public partial class VigiaCheckbox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConfirmarVigia",
                table: "PedidoVigilancia",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmarVigia",
                table: "PedidoVigilancia");
        }
    }
}
