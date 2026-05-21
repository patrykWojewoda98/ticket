using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket_category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_category", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket_priority",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_priority", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket_status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_status", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailVerified = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    Role = table.Column<string>(type: "longtext", nullable: false, defaultValue: "user")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Scope = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessTokenExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RefreshTokenExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_account_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_company_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "session",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IpAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserAgent = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session", x => x.Id);
                    table.ForeignKey(
                        name: "FK_session_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AssigneeId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ticket_ticket_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ticket_category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ticket_ticket_priority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "ticket_priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_ticket_status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ticket_status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_user_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ticket_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comment_ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comment_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket_attachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    Filename = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Path = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UploadedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ticket_attachment_ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_attachment_user_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket_history",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OldValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NewValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ticket_history_ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_history_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket_notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Read = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ticket_notification_ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_notification_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_account_UserId",
                table: "account",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_comment_TicketId",
                table: "comment",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_comment_UserId",
                table: "comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_company_Email",
                table: "company",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_company_UserId",
                table: "company",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_session_Token",
                table: "session",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_session_UserId",
                table: "session",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_AssigneeId",
                table: "ticket",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_CategoryId",
                table: "ticket",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_PriorityId",
                table: "ticket",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_StatusId",
                table: "ticket",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_UserId",
                table: "ticket",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_attachment_TicketId",
                table: "ticket_attachment",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_attachment_UploadedBy",
                table: "ticket_attachment",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_category_Id",
                table: "ticket_category",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_category_Name",
                table: "ticket_category",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_history_TicketId",
                table: "ticket_history",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_history_UserId",
                table: "ticket_history",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_notification_TicketId",
                table: "ticket_notification",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_notification_UserId",
                table: "ticket_notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_priority_Id",
                table: "ticket_priority",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_priority_Name",
                table: "ticket_priority",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_status_Id",
                table: "ticket_status",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_status_Name",
                table: "ticket_status",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_Email",
                table: "user",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "session");

            migrationBuilder.DropTable(
                name: "ticket_attachment");

            migrationBuilder.DropTable(
                name: "ticket_history");

            migrationBuilder.DropTable(
                name: "ticket_notification");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "ticket_category");

            migrationBuilder.DropTable(
                name: "ticket_priority");

            migrationBuilder.DropTable(
                name: "ticket_status");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
