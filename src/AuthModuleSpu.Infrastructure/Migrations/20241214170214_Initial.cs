using System;
using System.Collections.Generic;
using Common.Domain;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuthModuleSpu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE attachment_type status AS ENUM ('INPUT', 'OUTPUT');");
            
            migrationBuilder.Sql("CREATE user_to_task_type status AS ENUM ('CREATOR', 'COLLABORATOR');");
            
            migrationBuilder.CreateTable(
                name: "jobs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    requested_metrics = table.Column<List<string>>(type: "jsonb", nullable: false),
                    report = table.Column<List<string>>(type: "jsonb", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "varchar(255)", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "job_attachments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    job_id = table.Column<long>(type: "bigint", nullable: false),
                    s3_file_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    s3_bucket_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    type = table.Column<string>(type: "attachment_type", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_attachments", x => x.id);
                    table.ForeignKey(
                        name: "FK_job_attachments_jobs_job_id",
                        column: x => x.job_id,
                        principalTable: "jobs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    text = table.Column<string>(type: "text", nullable: false),
                    job_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_notifications_jobs_job_id",
                        column: x => x.job_id,
                        principalTable: "jobs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "queue",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    progress = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_queue", x => x.id);
                    table.ForeignKey(
                        name: "FK_queue_jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "jobs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "job_permissions",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    job_id = table.Column<long>(type: "bigint", nullable: false),
                    permissions = table.Column<Permission>(type: "jsonb", nullable: false),
                    user_type = table.Column<string>(type: "user_to_task_type", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_permissions", x => new { x.job_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_job_permissions_jobs_job_id",
                        column: x => x.job_id,
                        principalTable: "jobs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_job_permissions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notification_receivers",
                columns: table => new
                {
                    notification_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    is_read = table.Column<bool>(type: "bool", nullable: false, defaultValue: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_receivers", x => new { x.notification_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_notification_receivers_notifications_notification_id",
                        column: x => x.notification_id,
                        principalTable: "notifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notification_receivers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_job_attachments_job_id_s3_file_name_s3_bucket_name",
                table: "job_attachments",
                columns: new[] { "job_id", "s3_file_name", "s3_bucket_name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_job_permissions_user_id",
                table: "job_permissions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_jobs_name",
                table: "jobs",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notification_receivers_user_id",
                table: "notification_receivers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_job_id",
                table: "notifications",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_queue_JobId",
                table: "queue",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TYPE attachment_type;");
            
            migrationBuilder.Sql("DROP TYPE user_to_task_type;");
            
            migrationBuilder.DropTable(
                name: "job_attachments");

            migrationBuilder.DropTable(
                name: "job_permissions");

            migrationBuilder.DropTable(
                name: "notification_receivers");

            migrationBuilder.DropTable(
                name: "queue");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "jobs");
        }
    }
}
