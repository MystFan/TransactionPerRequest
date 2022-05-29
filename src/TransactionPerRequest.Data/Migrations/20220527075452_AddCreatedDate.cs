using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionPerRequest.Data.Migrations
{
    public partial class AddCreatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Post",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Post");
        }
    }
}
