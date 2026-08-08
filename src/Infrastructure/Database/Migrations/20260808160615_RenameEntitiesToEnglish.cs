using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RenameEntitiesToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Renomeia as tabelas para inglês preservando os dados (RenameTable), em vez do
            // Drop+Create que o `dotnet ef migrations add` gera por padrão quando só o nome
            // da tabela muda — ver ADR-0010, seção de Consequências.
            migrationBuilder.RenameTable(name: "Usuario", newName: "User");
            migrationBuilder.RenameTable(name: "Academia", newName: "Gym");
            migrationBuilder.RenameTable(name: "Aluno", newName: "Member");
            migrationBuilder.RenameTable(name: "Gestor", newName: "Manager");
            migrationBuilder.RenameTable(name: "Funcionario", newName: "Employee");
            migrationBuilder.RenameTable(name: "Instrutor", newName: "Instructor");
            migrationBuilder.RenameTable(name: "Administrador", newName: "Administrator");
            migrationBuilder.RenameTable(name: "Ficha", newName: "TrainingSheet");
            migrationBuilder.RenameTable(name: "FichaMedica", newName: "MedicalRecord");
            migrationBuilder.RenameTable(name: "Contrato", newName: "Contract");
            migrationBuilder.RenameTable(name: "PlanoContrato", newName: "ContractPlan");
            migrationBuilder.RenameTable(name: "Licenca", newName: "License");
            migrationBuilder.RenameTable(name: "PlanoLicenca", newName: "LicensePlan");
            migrationBuilder.RenameTable(name: "Agenda", newName: "Schedule");
            migrationBuilder.RenameTable(name: "Treino", newName: "Workout");
            migrationBuilder.RenameTable(name: "LogAtividade", newName: "ActivityLog");
            migrationBuilder.RenameTable(name: "TelefoneUsuario", newName: "UserPhone");
            migrationBuilder.RenameTable(name: "TelefoneAcademia", newName: "GymPhone");
            migrationBuilder.RenameTable(name: "Frequencia", newName: "Attendance");
            migrationBuilder.RenameTable(name: "XpHistorico", newName: "XpHistory");

            // "avaliacao" saiu do AppDbContext (nenhum repository nunca a populou) — dropada
            // de propósito. Se ela tiver dados reais no Azure, pare aqui antes de aplicar.
            migrationBuilder.DropTable(name: "avaliacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "User", newName: "Usuario");
            migrationBuilder.RenameTable(name: "Gym", newName: "Academia");
            migrationBuilder.RenameTable(name: "Member", newName: "Aluno");
            migrationBuilder.RenameTable(name: "Manager", newName: "Gestor");
            migrationBuilder.RenameTable(name: "Employee", newName: "Funcionario");
            migrationBuilder.RenameTable(name: "Instructor", newName: "Instrutor");
            migrationBuilder.RenameTable(name: "Administrator", newName: "Administrador");
            migrationBuilder.RenameTable(name: "TrainingSheet", newName: "Ficha");
            migrationBuilder.RenameTable(name: "MedicalRecord", newName: "FichaMedica");
            migrationBuilder.RenameTable(name: "Contract", newName: "Contrato");
            migrationBuilder.RenameTable(name: "ContractPlan", newName: "PlanoContrato");
            migrationBuilder.RenameTable(name: "License", newName: "Licenca");
            migrationBuilder.RenameTable(name: "LicensePlan", newName: "PlanoLicenca");
            migrationBuilder.RenameTable(name: "Schedule", newName: "Agenda");
            migrationBuilder.RenameTable(name: "Workout", newName: "Treino");
            migrationBuilder.RenameTable(name: "ActivityLog", newName: "LogAtividade");
            migrationBuilder.RenameTable(name: "UserPhone", newName: "TelefoneUsuario");
            migrationBuilder.RenameTable(name: "GymPhone", newName: "TelefoneAcademia");
            migrationBuilder.RenameTable(name: "Attendance", newName: "Frequencia");
            migrationBuilder.RenameTable(name: "XpHistory", newName: "XpHistorico");

            migrationBuilder.CreateTable(
                name: "avaliacao",
                columns: table => new
                {
                    id_avaliacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ab = table.Column<double>(type: "float", nullable: false),
                    abdomen = table.Column<double>(type: "float", nullable: false),
                    altura = table.Column<double>(type: "float", nullable: false),
                    ax = table.Column<double>(type: "float", nullable: false),
                    braco_d = table.Column<double>(type: "float", nullable: false),
                    braco_e = table.Column<double>(type: "float", nullable: false),
                    coxa_d = table.Column<double>(type: "float", nullable: false),
                    coxa_e = table.Column<double>(type: "float", nullable: false),
                    data = table.Column<System.DateOnly>(type: "date", nullable: false),
                    deltoide = table.Column<double>(type: "float", nullable: false),
                    densidade = table.Column<double>(type: "float", nullable: false),
                    femur = table.Column<double>(type: "float", nullable: false),
                    glicemia = table.Column<double>(type: "float", nullable: false),
                    hora = table.Column<System.TimeOnly>(type: "time", nullable: false),
                    id_aluno = table.Column<int>(type: "int", nullable: false),
                    id_professor = table.Column<int>(type: "int", nullable: false),
                    idade = table.Column<int>(type: "int", nullable: false),
                    p_gordo = table.Column<double>(type: "float", nullable: false),
                    p_magro = table.Column<double>(type: "float", nullable: false),
                    p_osseo = table.Column<double>(type: "float", nullable: false),
                    p_viscera = table.Column<double>(type: "float", nullable: false),
                    pa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    peitoral = table.Column<double>(type: "float", nullable: false),
                    perna_d = table.Column<double>(type: "float", nullable: false),
                    perna_e = table.Column<double>(type: "float", nullable: false),
                    peso = table.Column<double>(type: "float", nullable: false),
                    pr = table.Column<double>(type: "float", nullable: false),
                    pt = table.Column<double>(type: "float", nullable: false),
                    px = table.Column<double>(type: "float", nullable: false),
                    quadril = table.Column<double>(type: "float", nullable: false),
                    rt = table.Column<double>(type: "float", nullable: false),
                    se = table.Column<double>(type: "float", nullable: false),
                    sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    si = table.Column<double>(type: "float", nullable: false),
                    torax = table.Column<double>(type: "float", nullable: false),
                    tr = table.Column<double>(type: "float", nullable: false),
                    ub = table.Column<double>(type: "float", nullable: false),
                    ur = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_avaliacao", x => x.id_avaliacao);
                });
        }
    }
}
