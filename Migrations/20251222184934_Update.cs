using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContratosAPI.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Empresa_contratante",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_StatusContrato_status",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_TipoContraente_tipo_contraente",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_TipoContrato_tipo",
                table: "Contrato");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "Funcionario",
                newName: "estado_id");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "Empresa",
                newName: "estado_id");

            migrationBuilder.RenameColumn(
                name: "tipo_contraente",
                table: "Contrato",
                newName: "tipo_contraente_id");

            migrationBuilder.RenameColumn(
                name: "tipo",
                table: "Contrato",
                newName: "tipo_contrato_id");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Contrato",
                newName: "status_id");

            migrationBuilder.RenameColumn(
                name: "contratante",
                table: "Contrato",
                newName: "contratante_id");

            migrationBuilder.RenameColumn(
                name: "contraente",
                table: "Contrato",
                newName: "contraente_id");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_tipo_contraente",
                table: "Contrato",
                newName: "IX_Contrato_tipo_contraente_id");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_tipo",
                table: "Contrato",
                newName: "IX_Contrato_tipo_contrato_id");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_status",
                table: "Contrato",
                newName: "IX_Contrato_status_id");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_contratante",
                table: "Contrato",
                newName: "IX_Contrato_contratante_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Empresa_contratante_id",
                table: "Contrato",
                column: "contratante_id",
                principalTable: "Empresa",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_StatusContrato_status_id",
                table: "Contrato",
                column: "status_id",
                principalTable: "StatusContrato",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_TipoContraente_tipo_contraente_id",
                table: "Contrato",
                column: "tipo_contraente_id",
                principalTable: "TipoContraente",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_TipoContrato_tipo_contrato_id",
                table: "Contrato",
                column: "tipo_contrato_id",
                principalTable: "TipoContrato",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Empresa_contratante_id",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_StatusContrato_status_id",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_TipoContraente_tipo_contraente_id",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_TipoContrato_tipo_contrato_id",
                table: "Contrato");

            migrationBuilder.RenameColumn(
                name: "estado_id",
                table: "Funcionario",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "estado_id",
                table: "Empresa",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "tipo_contrato_id",
                table: "Contrato",
                newName: "tipo");

            migrationBuilder.RenameColumn(
                name: "tipo_contraente_id",
                table: "Contrato",
                newName: "tipo_contraente");

            migrationBuilder.RenameColumn(
                name: "status_id",
                table: "Contrato",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "contratante_id",
                table: "Contrato",
                newName: "contratante");

            migrationBuilder.RenameColumn(
                name: "contraente_id",
                table: "Contrato",
                newName: "contraente");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_tipo_contrato_id",
                table: "Contrato",
                newName: "IX_Contrato_tipo");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_tipo_contraente_id",
                table: "Contrato",
                newName: "IX_Contrato_tipo_contraente");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_status_id",
                table: "Contrato",
                newName: "IX_Contrato_status");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_contratante_id",
                table: "Contrato",
                newName: "IX_Contrato_contratante");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Empresa_contratante",
                table: "Contrato",
                column: "contratante",
                principalTable: "Empresa",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_StatusContrato_status",
                table: "Contrato",
                column: "status",
                principalTable: "StatusContrato",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_TipoContraente_tipo_contraente",
                table: "Contrato",
                column: "tipo_contraente",
                principalTable: "TipoContraente",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_TipoContrato_tipo",
                table: "Contrato",
                column: "tipo",
                principalTable: "TipoContrato",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
