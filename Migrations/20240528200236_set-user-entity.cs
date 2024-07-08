using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurePass.Migrations
{
  /// <inheritdoc />
  public partial class setuserentity : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
          name: "ProfileId",
          table: "User",
          type: "text",
          nullable: true,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "ProfileUrl",
          table: "User",
          type: "text",
          nullable: true,
          defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "ProfileId",
          table: "User");

      migrationBuilder.DropColumn(
          name: "ProfileUrl",
          table: "User");
    }
  }
}
