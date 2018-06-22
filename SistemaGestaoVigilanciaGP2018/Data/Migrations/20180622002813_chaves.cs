using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaGestaoVigilanciaGP2018.Migrations
{
    public partial class chaves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cu_id",
                table: "UnidadeCurricular",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CursoIdC",
                table: "UnidadeCurricular",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UC",
                table: "Curso",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UCIdd",
                table: "Curso",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnidadeCurricularIdUC",
                table: "Curso",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeCurricular_CursoIdC",
                table: "UnidadeCurricular",
                column: "CursoIdC");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_UnidadeCurricularIdUC",
                table: "Curso",
                column: "UnidadeCurricularIdUC");

            migrationBuilder.AddForeignKey(
                name: "FK_Curso_UnidadeCurricular_UnidadeCurricularIdUC",
                table: "Curso",
                column: "UnidadeCurricularIdUC",
                principalTable: "UnidadeCurricular",
                principalColumn: "IdUC",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnidadeCurricular_Curso_CursoIdC",
                table: "UnidadeCurricular",
                column: "CursoIdC",
                principalTable: "Curso",
                principalColumn: "IdC",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curso_UnidadeCurricular_UnidadeCurricularIdUC",
                table: "Curso");

            migrationBuilder.DropForeignKey(
                name: "FK_UnidadeCurricular_Curso_CursoIdC",
                table: "UnidadeCurricular");

            migrationBuilder.DropIndex(
                name: "IX_UnidadeCurricular_CursoIdC",
                table: "UnidadeCurricular");

            migrationBuilder.DropIndex(
                name: "IX_Curso_UnidadeCurricularIdUC",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "Cu_id",
                table: "UnidadeCurricular");

            migrationBuilder.DropColumn(
                name: "CursoIdC",
                table: "UnidadeCurricular");

            migrationBuilder.DropColumn(
                name: "UC",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "UCIdd",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "UnidadeCurricularIdUC",
                table: "Curso");
        }
    }
}
