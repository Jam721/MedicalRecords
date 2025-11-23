using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MedicalRecords.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Symptoms = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Specialization = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LicenseNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DoctorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DiseasePatient",
                columns: table => new
                {
                    DiseasesId = table.Column<int>(type: "integer", nullable: false),
                    PatientsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseasePatient", x => new { x.DiseasesId, x.PatientsId });
                    table.ForeignKey(
                        name: "FK_DiseasePatient_Diseases_DiseasesId",
                        column: x => x.DiseasesId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiseasePatient_Patients_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Diseases",
                columns: new[] { "Id", "Description", "Name", "Symptoms" },
                values: new object[,]
                {
                    { 1, "Острое инфекционное заболевание дыхательных путей", "Грипп", "Высокая температура, головная боль, слабость, боль в мышцах" },
                    { 2, "Повышенное артериальное давление", "Гипертония", "Головная боль, головокружение, шум в ушах" },
                    { 3, "Эндокринное заболевание, связанное с нарушением усвоения глюкозы", "Сахарный диабет", "Жажда, частое мочеиспускание, усталость" },
                    { 4, "Хроническое воспалительное заболевание дыхательных путей", "Астма", "Одышка, кашель, хрипы" },
                    { 5, "Воспаление суставов", "Артрит", "Боль в суставах, скованность, припухлость" },
                    { 6, "Воспаление слизистой оболочки желудка", "Гастрит", "Боль в желудке, тошнота, изжога" },
                    { 7, "Неврологическое заболевание", "Мигрень", "Сильная головная боль, тошнота, светочувствительность" },
                    { 8, "Дегенеративное заболевание позвоночника", "Остеохондроз", "Боль в спине, ограничение подвижности" },
                    { 9, "Воспаление легких", "Пневмония", "Кашель, высокая температура, боль в груди" },
                    { 10, "Повышенная чувствительность иммунной системы", "Аллергия", "Чихание, зуд, сыпь, отеки" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "FirstName", "LastName", "LicenseNumber", "Specialization" },
                values: new object[,]
                {
                    { 1, "Иван", "Петров", "LIC001", "Кардиолог" },
                    { 2, "Мария", "Иванова", "LIC002", "Невролог" },
                    { 3, "Алексей", "Сидоров", "LIC003", "Терапевт" },
                    { 4, "Ольга", "Кузнецова", "LIC004", "Дерматолог" },
                    { 5, "Сергей", "Васильев", "LIC005", "Офтальмолог" },
                    { 6, "Екатерина", "Николаева", "LIC006", "Педиатр" },
                    { 7, "Дмитрий", "Орлов", "LIC007", "Хирург" },
                    { 8, "Анна", "Захарова", "LIC008", "Гинеколог" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Age", "DoctorId", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 45, 1, "Андрей", "Смирнов", "+79123456789" },
                    { 2, 32, 2, "Елена", "Попова", "+79123456780" },
                    { 3, 50, 3, "Дмитрий", "Козлов", "+79123456781" },
                    { 4, 28, 1, "Наталья", "Морозова", "+79123456782" },
                    { 5, 60, 3, "Виктор", "Орлов", "+79123456783" },
                    { 6, 35, 4, "Светлана", "Волкова", "+79123456784" },
                    { 7, 42, 5, "Михаил", "Лебедев", "+79123456785" },
                    { 8, 29, 6, "Ольга", "Семенова", "+79123456786" },
                    { 9, 55, 7, "Александр", "Павлов", "+79123456787" },
                    { 10, 38, 8, "Ирина", "Федорова", "+79123456788" }
                });

            migrationBuilder.InsertData(
                table: "DiseasePatient",
                columns: new[] { "DiseasesId", "PatientsId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 4 },
                    { 2, 9 },
                    { 3, 1 },
                    { 3, 5 },
                    { 3, 9 },
                    { 4, 2 },
                    { 4, 10 },
                    { 5, 3 },
                    { 5, 5 },
                    { 6, 6 },
                    { 7, 2 },
                    { 7, 8 },
                    { 8, 5 },
                    { 8, 9 },
                    { 9, 7 },
                    { 10, 4 },
                    { 10, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiseasePatient_PatientsId",
                table: "DiseasePatient",
                column: "PatientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_Name",
                table: "Diseases",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_LicenseNumber",
                table: "Doctors",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Specialization",
                table: "Doctors",
                column: "Specialization");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_LastName_FirstName",
                table: "Patients",
                columns: new[] { "LastName", "FirstName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiseasePatient");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
