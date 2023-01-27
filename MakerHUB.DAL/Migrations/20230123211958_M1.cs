using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakerHUB.DAL.Migrations
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLikeds",
                columns: table => new
                {
                    ActivityLikedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLikeds", x => x.ActivityLikedId);
                    table.ForeignKey(
                        name: "FK_ActivityLikeds_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingLikeds",
                columns: table => new
                {
                    MeetingLikedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeetingId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingLikeds", x => x.MeetingLikedId);
                });

            migrationBuilder.CreateTable(
                name: "MeetingParticipants",
                columns: table => new
                {
                    MeetingParticipantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeetingId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingParticipants", x => x.MeetingParticipantId);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    MeetingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    MinParticipant = table.Column<int>(type: "int", nullable: false),
                    MaxParticipant = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.MeetingId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Pseudo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "MeetingId");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateCreated", "DateModified", "Email", "Firstname", "Image", "Lastname", "MeetingId", "PasswordHash", "PasswordSalt", "Pseudo", "Role" },
                values: new object[] { 1, new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6805), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6832), "olivier@gmail.com", "Olivier", "aa76be4e-329d-e188-42af-2b5fa2f1260a", "B", null, new byte[] { 156, 83, 33, 138, 201, 246, 114, 149, 86, 240, 182, 234, 245, 254, 64, 61, 30, 231, 163, 188, 11, 179, 237, 124, 145, 219, 183, 78, 216, 216, 50, 73, 144, 7, 76, 39, 142, 44, 193, 13, 65, 83, 114, 97, 163, 65, 157, 35, 158, 38, 150, 86, 148, 232, 244, 49, 204, 67, 41, 24, 241, 42, 230, 45 }, new byte[] { 150, 89, 230, 140, 252, 60, 71, 196, 229, 150, 110, 40, 1, 159, 59, 78, 186, 70, 33, 53, 165, 88, 140, 226, 179, 92, 198, 112, 251, 136, 85, 109, 216, 33, 116, 97, 70, 52, 31, 115, 56, 92, 151, 39, 184, 18, 42, 4, 251, 42, 165, 127, 248, 202, 80, 5, 102, 251, 146, 246, 156, 124, 190, 7, 0, 248, 238, 235, 40, 113, 125, 157, 75, 221, 13, 136, 249, 221, 227, 41, 210, 114, 102, 16, 17, 137, 83, 124, 133, 155, 74, 15, 65, 79, 75, 112, 8, 226, 203, 136, 95, 159, 38, 242, 163, 63, 42, 89, 17, 1, 139, 54, 175, 141, 211, 132, 222, 219, 250, 128, 250, 255, 61, 123, 116, 189, 245, 133 }, "olivier", "USER" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateCreated", "DateModified", "Email", "Firstname", "Image", "Lastname", "MeetingId", "PasswordHash", "PasswordSalt", "Pseudo", "Role" },
                values: new object[] { 2, new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6837), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6838), "philippe@gmail.com", "Philippe", "14e155f7-198d-0dda-cb07-7e9ab42b0a46", "D", null, new byte[] { 178, 234, 29, 158, 86, 0, 12, 166, 98, 216, 149, 86, 108, 146, 109, 249, 21, 106, 76, 133, 182, 143, 241, 42, 226, 2, 199, 138, 32, 171, 46, 26, 100, 16, 169, 237, 134, 222, 181, 235, 119, 158, 58, 196, 24, 160, 24, 219, 172, 77, 81, 199, 74, 178, 221, 79, 145, 138, 26, 170, 178, 66, 150, 55 }, new byte[] { 131, 247, 167, 74, 151, 76, 205, 62, 229, 121, 45, 4, 11, 3, 19, 213, 135, 76, 170, 203, 136, 210, 127, 138, 56, 74, 75, 252, 34, 221, 47, 252, 140, 207, 62, 213, 178, 153, 215, 192, 198, 18, 222, 134, 251, 205, 219, 199, 157, 126, 97, 236, 209, 18, 43, 251, 194, 202, 139, 117, 210, 137, 101, 109, 157, 189, 131, 112, 84, 92, 170, 203, 80, 34, 48, 74, 186, 84, 162, 7, 151, 88, 33, 4, 15, 46, 75, 148, 0, 144, 30, 23, 239, 249, 205, 65, 120, 60, 143, 70, 73, 49, 247, 187, 180, 251, 141, 214, 118, 26, 61, 143, 199, 117, 253, 115, 121, 235, 199, 251, 164, 95, 127, 130, 224, 12, 181, 25 }, "philippe", "USER" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateCreated", "DateModified", "Email", "Firstname", "Image", "Lastname", "MeetingId", "PasswordHash", "PasswordSalt", "Pseudo", "Role" },
                values: new object[] { 3, new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6841), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6843), "julie@gmail.com", "Julie", "9ca30b7b-00e4-0893-9de5-7a65cc10cd8b", "H", null, new byte[] { 142, 74, 147, 102, 178, 78, 177, 5, 101, 251, 91, 59, 189, 132, 70, 6, 118, 199, 33, 109, 71, 61, 44, 12, 241, 62, 74, 58, 136, 214, 33, 108, 221, 237, 251, 62, 151, 228, 211, 129, 198, 191, 108, 229, 174, 43, 121, 0, 119, 44, 48, 254, 145, 46, 33, 110, 200, 255, 138, 100, 163, 187, 232, 24 }, new byte[] { 240, 180, 72, 9, 212, 74, 186, 227, 228, 227, 38, 146, 2, 116, 58, 10, 56, 147, 6, 219, 182, 83, 57, 63, 8, 185, 115, 102, 187, 237, 98, 213, 161, 47, 153, 0, 96, 81, 39, 65, 115, 44, 108, 138, 118, 226, 89, 124, 226, 165, 34, 115, 194, 200, 65, 86, 101, 28, 146, 190, 202, 1, 80, 234, 172, 39, 4, 177, 156, 116, 249, 254, 66, 121, 67, 104, 149, 117, 255, 82, 118, 80, 189, 19, 21, 25, 20, 72, 226, 132, 186, 210, 212, 151, 23, 156, 249, 160, 119, 106, 235, 33, 14, 176, 4, 234, 2, 90, 21, 35, 9, 178, 44, 87, 90, 69, 19, 233, 0, 40, 87, 106, 233, 222, 172, 94, 206, 150 }, "julie", "USER" });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "ActivityId", "Date", "DateCreated", "DateModified", "Description", "Image", "IsPublic", "Latitude", "Longitude", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 20, 10, 15, 30, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6965), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6967), "Ramassage des déchets", "501e6d67-633a-6d63-4917-71cfd39c1207", false, 50.457441247787578, 4.8709311693713362, "Rue van Opré à Jambes", 1 },
                    { 2, new DateTime(2022, 12, 27, 10, 15, 30, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6975), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6976), "Ramassage des déchets", "833d9866-40ea-e0f9-973a-0661f7ff1a61", false, 50.46135977534567, 4.8742238489005887, "Rue Mazy à Jambes", 1 },
                    { 3, new DateTime(2022, 12, 1, 10, 15, 30, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6981), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6982), "Ramassage des déchets", "67a2e64b-f155-b710-246e-b7765d16bab0", false, 50.459010804528624, 4.8634192270445045, "Route Merveilleuse à Namur", 1 },
                    { 4, new DateTime(2023, 1, 4, 11, 15, 30, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6987), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6988), "Ramassage des déchets", "f379538d-6e67-63c1-d935-8a693a5cfd06", true, 50.45831557105943, 4.8660996998271129, "Pont de Jambes", 1 },
                    { 5, new DateTime(2023, 1, 6, 11, 15, 30, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6993), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6995), "Ramassage des déchets", "60cbf3b4-dfe9-d0d3-3d43-43520506cfef", true, 50.457072552411546, 4.8591006873929858, "Le paranoma Citadelle", 1 },
                    { 6, new DateTime(2023, 1, 15, 11, 15, 30, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(6999), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7001), "Ramassage des déchets", "988d4161-b23e-45f7-4962-1bbaff2d9165", false, 50.454912993186618, 4.8553943545755427, "Château de Namur", 1 },
                    { 7, new DateTime(2023, 1, 3, 11, 15, 30, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7005), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7007), "Ramassage des déchets", "b7124b6c-f782-92e5-c1ae-c7ede7984215", true, 50.292073135414462, 5.0957738631991845, "Centre de Ciney", 3 },
                    { 8, new DateTime(2023, 1, 8, 11, 15, 30, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7011), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7013), "Ramassage des déchets", "567db65f-1ad9-a52d-72e8-e2c1f0f10fb8", true, 50.288714887095047, 5.0966957848596559, "Ciney park Saint-Roch", 3 },
                    { 9, new DateTime(2022, 12, 18, 17, 15, 30, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7017), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7019), "Ramassage des déchets", "80f82e28-8616-27db-4ac6-09a1715c5a76", true, 50.195352893252299, 4.8369342880655646, "Hastiere grand route", 3 },
                    { 10, new DateTime(2022, 11, 18, 17, 15, 30, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7024), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7025), "Ramassage des déchets", "342d73c4-0964-da22-90a8-df6ac44766d0", true, 50.074736140503816, 4.6072779911991404, "Viroinvale", 3 }
                });

            migrationBuilder.InsertData(
                table: "Meetings",
                columns: new[] { "MeetingId", "Date", "DateCreated", "DateModified", "Description", "Image", "Latitude", "Longitude", "MaxParticipant", "MinParticipant", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 2, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7079), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7081), "Rendez-vous en dessous du pont de Sambre à 10h", "d3622751-8931-35cd-545c-55bb673bc105", 50.43053889673353, 4.6140972122412283, 10, 2, "Sambre de Tamines", 1 },
                    { 2, new DateTime(2023, 2, 10, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7088), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7090), "Rendez-vous près de la gare à 11h30", "567db65f-1ad9-a52d-72e8-e2c1f0f10fb8", 50.291399089228918, 5.0914419559866975, 12, 4, "Ciney", 1 },
                    { 3, new DateTime(2023, 1, 31, 8, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7094), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7096), "Rendez-vous à 8h30", "ef4be8ab-704f-4821-8d79-fcf341f7f498", 50.311528901590549, 5.1073765406444149, 12, 4, "Ciney Technobel", 3 },
                    { 4, new DateTime(2022, 12, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7101), new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7102), "Rendez-vous à 10h30 près du grand chêne", "3f779eff-6877-7448-be50-7b1fbf8e1d20", 50.41672494682598, 4.6242254669166618, 12, 4, "Bois de Falisolle", 3 }
                });

            migrationBuilder.InsertData(
                table: "ActivityLikeds",
                columns: new[] { "ActivityLikedId", "ActivityId", "DateCreated", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7046), 1 },
                    { 2, 1, new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7050), 2 },
                    { 3, 1, new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7052), 3 },
                    { 4, 2, new DateTime(2023, 1, 23, 22, 19, 58, 28, DateTimeKind.Local).AddTicks(7055), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_UserId",
                table: "Activities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLikeds_ActivityId",
                table: "ActivityLikeds",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLikeds_UserId",
                table: "ActivityLikeds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingLikeds_MeetingId",
                table: "MeetingLikeds",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingLikeds_UserId",
                table: "MeetingLikeds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingParticipants_MeetingId",
                table: "MeetingParticipants",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingParticipants_UserId",
                table: "MeetingParticipants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_UserId",
                table: "Meetings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MeetingId",
                table: "Users",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Pseudo",
                table: "Users",
                column: "Pseudo",
                unique: true,
                filter: "[Pseudo] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_UserId",
                table: "Activities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLikeds_Users_UserId",
                table: "ActivityLikeds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingLikeds_Meetings_MeetingId",
                table: "MeetingLikeds",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "MeetingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingLikeds_Users_UserId",
                table: "MeetingLikeds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingParticipants_Meetings_MeetingId",
                table: "MeetingParticipants",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "MeetingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingParticipants_Users_UserId",
                table: "MeetingParticipants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Users_UserId",
                table: "Meetings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Users_UserId",
                table: "Meetings");

            migrationBuilder.DropTable(
                name: "ActivityLikeds");

            migrationBuilder.DropTable(
                name: "MeetingLikeds");

            migrationBuilder.DropTable(
                name: "MeetingParticipants");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Meetings");
        }
    }
}
