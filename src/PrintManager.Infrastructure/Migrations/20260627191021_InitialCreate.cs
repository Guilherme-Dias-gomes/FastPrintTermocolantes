using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeCliente = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DescricaoServico = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorPago = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFinalizacao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ObservacaoInterna = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Login = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SenhaHash = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PedidoArquivos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PedidoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeArquivo = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    CaminhoArquivo = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoArquivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoArquivos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoArquivos_PedidoId",
                table: "PedidoArquivos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoArquivos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
