using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DA.Migrations
{
    /// <inheritdoc />
    public partial class RelacionTicketsAgentes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AgenteId",
                table: "Tickets",
                column: "AgenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Agentes_AgenteId",
                table: "Tickets",
                column: "AgenteId",
                principalTable: "Agentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Agentes_AgenteId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AgenteId",
                table: "Tickets");
        }
    }
}
