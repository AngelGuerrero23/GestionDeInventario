using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDeInventario.Migrations
{
    /// <inheritdoc />
    public partial class DOS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntradaDetalle_Producto_ProductoId",
                table: "EntradaDetalle");

            migrationBuilder.DropIndex(
                name: "IX_EntradaDetalle_ProductoId",
                table: "EntradaDetalle");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaDetalle_EntradaId",
                table: "EntradaDetalle",
                column: "EntradaId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntradaDetalle_Entrada_EntradaId",
                table: "EntradaDetalle",
                column: "EntradaId",
                principalTable: "Entrada",
                principalColumn: "EntradaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntradaDetalle_Entrada_EntradaId",
                table: "EntradaDetalle");

            migrationBuilder.DropIndex(
                name: "IX_EntradaDetalle_EntradaId",
                table: "EntradaDetalle");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaDetalle_ProductoId",
                table: "EntradaDetalle",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntradaDetalle_Producto_ProductoId",
                table: "EntradaDetalle",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
