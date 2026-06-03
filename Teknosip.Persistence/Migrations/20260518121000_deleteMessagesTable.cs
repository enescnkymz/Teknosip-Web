using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teknosip.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class deleteMessagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Message_AspNetUsers_ReceiverId",
            //    table: "Message");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Message_AspNetUsers_SenderId",
            //    table: "Message");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Message",
            //    table: "Message");

            //migrationBuilder.RenameTable(
            //    name: "Message",
            //    newName: "Messages");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Message_SenderId",
            //    table: "Messages",
            //    newName: "IX_Messages_SenderId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Message_ReceiverId",
            //    table: "Messages",
            //    newName: "IX_Messages_ReceiverId");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "Id",
            //    table: "Messages",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Messages",
            //    table: "Messages",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Messages_AspNetUsers_ReceiverId",
            //    table: "Messages",
            //    column: "ReceiverId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Messages_AspNetUsers_SenderId",
            //    table: "Messages",
            //    column: "SenderId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
