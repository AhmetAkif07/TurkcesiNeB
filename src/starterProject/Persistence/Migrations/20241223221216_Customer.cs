using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("c03e6522-b9d8-4bea-b048-5e154db9178e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cfff26b5-3029-46a3-80bf-b41e32137c3d"));

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    recid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("customer_pkey", x => x.recid);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("f43ce19d-4f27-4cdf-b00f-75e287b95157"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "narch@tr", new byte[] { 171, 108, 14, 202, 224, 23, 136, 123, 243, 68, 49, 5, 120, 57, 47, 244, 189, 139, 86, 61, 153, 165, 7, 245, 124, 74, 66, 62, 86, 136, 233, 10, 107, 209, 151, 108, 146, 68, 213, 103, 153, 151, 78, 88, 54, 103, 242, 92, 59, 19, 189, 96, 52, 237, 158, 82, 97, 29, 247, 9, 90, 94, 124, 100 }, new byte[] { 153, 32, 218, 55, 109, 140, 124, 66, 124, 249, 155, 216, 189, 31, 107, 208, 170, 39, 117, 68, 39, 88, 19, 136, 6, 105, 42, 28, 133, 1, 9, 99, 168, 247, 180, 230, 187, 219, 90, 172, 19, 60, 88, 178, 64, 167, 79, 46, 62, 185, 169, 29, 225, 27, 189, 185, 63, 42, 81, 106, 225, 250, 17, 222, 31, 220, 218, 155, 143, 188, 234, 162, 107, 167, 100, 62, 21, 68, 118, 149, 46, 205, 77, 140, 209, 104, 130, 234, 214, 26, 112, 27, 121, 236, 86, 1, 78, 131, 1, 236, 107, 146, 201, 193, 220, 63, 28, 190, 106, 26, 208, 4, 64, 13, 50, 172, 123, 227, 173, 214, 51, 64, 25, 145, 27, 51, 238, 67 }, null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("8afcac04-2c7f-4295-8417-adb158e64539"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("f43ce19d-4f27-4cdf-b00f-75e287b95157") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("8afcac04-2c7f-4295-8417-adb158e64539"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f43ce19d-4f27-4cdf-b00f-75e287b95157"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("cfff26b5-3029-46a3-80bf-b41e32137c3d"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "alitas7875@gmail.com", new byte[] { 55, 205, 56, 224, 129, 104, 214, 141, 105, 201, 157, 15, 223, 199, 82, 77, 25, 182, 167, 175, 231, 80, 198, 8, 15, 248, 203, 58, 6, 127, 59, 229, 5, 20, 18, 140, 85, 176, 72, 147, 79, 244, 81, 171, 186, 171, 190, 132, 71, 192, 184, 184, 110, 173, 213, 192, 132, 231, 222, 118, 106, 71, 107, 130 }, new byte[] { 121, 20, 104, 141, 113, 230, 141, 179, 187, 105, 92, 239, 124, 116, 135, 230, 148, 218, 255, 81, 6, 23, 117, 173, 55, 28, 21, 138, 24, 16, 153, 102, 204, 39, 27, 23, 238, 172, 176, 5, 88, 189, 18, 48, 79, 154, 53, 23, 171, 192, 95, 161, 174, 130, 220, 57, 3, 207, 108, 44, 227, 130, 205, 151, 230, 48, 131, 118, 113, 27, 8, 250, 11, 148, 81, 254, 247, 108, 146, 204, 130, 179, 121, 227, 138, 72, 107, 144, 56, 187, 244, 119, 2, 216, 237, 105, 184, 116, 221, 229, 121, 78, 187, 32, 29, 239, 8, 122, 160, 5, 226, 214, 123, 254, 98, 235, 74, 193, 43, 85, 46, 59, 135, 171, 234, 46, 10, 247 }, null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("c03e6522-b9d8-4bea-b048-5e154db9178e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("cfff26b5-3029-46a3-80bf-b41e32137c3d") });
        }
    }
}
