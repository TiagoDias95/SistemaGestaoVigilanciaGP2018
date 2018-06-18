using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaGestaoVigilanciaGP2018.Data.Migrations
{
    public partial class pedidoVigilancia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "PedidoVigilancia",
            //    columns: table => new
            //    {
            //        PrimeiroNome = table.Column<string>(nullable: false),
            //        DataVigilancia = table.Column<DateTime>(nullable: false),
            //        NumeroDocente = table.Column<string>(nullable: false),
            //        UltimoNome = table.Column<string>(nullable: false),
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PedidoVigilancia", x => x.PrimeiroNome);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "PedidoVigilancia");
        }
    }
}
