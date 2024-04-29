using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurePass.Migrations
{
    /// <inheritdoc />
    public partial class ImplSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vault_User_UserId",
                table: "Vault");

            migrationBuilder.DropTable(
                name: "Register");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vault",
                table: "Vault");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "Vault",
                newName: "VaultEntity");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "UserEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Vault_UserId",
                table: "VaultEntity",
                newName: "IX_VaultEntity_UserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VaultEntity",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserEntity",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaultEntity",
                table: "VaultEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEntity",
                table: "UserEntity",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RecordEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    VaultId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordEntity_UserEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordEntity_VaultEntity_VaultId",
                        column: x => x.VaultId,
                        principalTable: "VaultEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordEntity_UserId",
                table: "RecordEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordEntity_VaultId",
                table: "RecordEntity",
                column: "VaultId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaultEntity_UserEntity_UserId",
                table: "VaultEntity",
                column: "UserId",
                principalTable: "UserEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaultEntity_UserEntity_UserId",
                table: "VaultEntity");

            migrationBuilder.DropTable(
                name: "RecordEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaultEntity",
                table: "VaultEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEntity",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VaultEntity");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserEntity");

            migrationBuilder.RenameTable(
                name: "VaultEntity",
                newName: "Vault");

            migrationBuilder.RenameTable(
                name: "UserEntity",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_VaultEntity_UserId",
                table: "Vault",
                newName: "IX_Vault_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vault",
                table: "Vault",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Register",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    VaultId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Register_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Register_Vault_VaultId",
                        column: x => x.VaultId,
                        principalTable: "Vault",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Register_UserId",
                table: "Register",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Register_VaultId",
                table: "Register",
                column: "VaultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vault_User_UserId",
                table: "Vault",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
