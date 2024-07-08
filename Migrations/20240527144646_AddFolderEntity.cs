using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurePass.Migrations
{
  /// <inheritdoc />
  public partial class AddFolderEntity : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<Guid>(
          name: "FolderId",
          table: "Record",
          type: "uuid",
          nullable: true);

      migrationBuilder.CreateTable(
          name: "Folder",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "uuid", nullable: false),
            Name = table.Column<string>(type: "text", nullable: false),
            Description = table.Column<string>(type: "text", nullable: true),
            UserId = table.Column<Guid>(type: "uuid", nullable: false),
            VaultId = table.Column<Guid>(type: "uuid", nullable: false),
            IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Folder", x => x.Id);
            table.ForeignKey(
                      name: "FK_Folder_User_UserId",
                      column: x => x.UserId,
                      principalTable: "User",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_Folder_Vault_VaultId",
                      column: x => x.VaultId,
                      principalTable: "Vault",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Record_FolderId",
          table: "Record",
          column: "FolderId");

      migrationBuilder.CreateIndex(
          name: "IX_Folder_UserId",
          table: "Folder",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_Folder_VaultId",
          table: "Folder",
          column: "VaultId");

      migrationBuilder.AddForeignKey(
          name: "FK_Record_Folder_FolderId",
          table: "Record",
          column: "FolderId",
          principalTable: "Folder",
          principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_Record_Folder_FolderId",
          table: "Record");

      migrationBuilder.DropTable(
          name: "Folder");

      migrationBuilder.DropIndex(
          name: "IX_Record_FolderId",
          table: "Record");

      migrationBuilder.DropColumn(
          name: "FolderId",
          table: "Record");
    }
  }
}
