using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitalitas.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Academias",
                columns: table => new
                {
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdLicenca = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdGestor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeAcademia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Quadra = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    TipoAcademia = table.Column<int>(type: "int", nullable: false),
                    EmailInstitucional = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academias", x => x.IdAcademia);
                });

            migrationBuilder.CreateTable(
                name: "Administradores",
                columns: table => new
                {
                    IdFuncionario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cargo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.IdFuncionario);
                });

            migrationBuilder.CreateTable(
                name: "Agendas",
                columns: table => new
                {
                    IdAgenda = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdInstrutor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendas", x => x.IdAgenda);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    IdAluno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdInstrutor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdContrato = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Objetivo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.IdAluno);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacoes",
                columns: table => new
                {
                    IdAvaliacao = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProfessor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAluno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Hora = table.Column<TimeOnly>(type: "time", nullable: false),
                    Peso = table.Column<double>(type: "float", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Altura = table.Column<double>(type: "float", nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    Glicemia = table.Column<double>(type: "float", nullable: false),
                    Pa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Densidade = table.Column<double>(type: "float", nullable: false),
                    Ax = table.Column<double>(type: "float", nullable: false),
                    Pt = table.Column<double>(type: "float", nullable: false),
                    Se = table.Column<double>(type: "float", nullable: false),
                    Tr = table.Column<double>(type: "float", nullable: false),
                    Ab = table.Column<double>(type: "float", nullable: false),
                    Si = table.Column<double>(type: "float", nullable: false),
                    Ub = table.Column<double>(type: "float", nullable: false),
                    Ur = table.Column<double>(type: "float", nullable: false),
                    Rt = table.Column<double>(type: "float", nullable: false),
                    Pr = table.Column<double>(type: "float", nullable: false),
                    Px = table.Column<double>(type: "float", nullable: false),
                    Femur = table.Column<double>(type: "float", nullable: false),
                    Abdomen = table.Column<double>(type: "float", nullable: false),
                    Torax = table.Column<double>(type: "float", nullable: false),
                    Quadril = table.Column<double>(type: "float", nullable: false),
                    BracoD = table.Column<double>(type: "float", nullable: false),
                    BracoE = table.Column<double>(type: "float", nullable: false),
                    CoxaD = table.Column<double>(type: "float", nullable: false),
                    CoxaE = table.Column<double>(type: "float", nullable: false),
                    PernaD = table.Column<double>(type: "float", nullable: false),
                    PernaE = table.Column<double>(type: "float", nullable: false),
                    Deltoide = table.Column<double>(type: "float", nullable: false),
                    Peitoral = table.Column<double>(type: "float", nullable: false),
                    PMagro = table.Column<double>(type: "float", nullable: false),
                    PGordo = table.Column<double>(type: "float", nullable: false),
                    POsseo = table.Column<double>(type: "float", nullable: false),
                    PViscera = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacoes", x => x.IdAvaliacao);
                });

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    IdContrato = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPlanoContrato = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mensalidade = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: false),
                    DataAssinatura = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.IdContrato);
                });

            migrationBuilder.CreateTable(
                name: "Fichas",
                columns: table => new
                {
                    IdFicha = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAvaliacao = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeFicha = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fichas", x => x.IdFicha);
                });

            migrationBuilder.CreateTable(
                name: "FichasMedicas",
                columns: table => new
                {
                    IdFicha = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAluno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Alergia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Restricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Lesao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Cirurgia = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProblemaSaude = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UsoMedicamento = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichasMedicas", x => x.IdFicha);
                });

            migrationBuilder.CreateTable(
                name: "Frequencias",
                columns: table => new
                {
                    IdFrequencia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAluno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TempoTreinoMinutos = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequencias", x => x.IdFrequencia);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    IdFuncionario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cargo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.IdFuncionario);
                });

            migrationBuilder.CreateTable(
                name: "Gestores",
                columns: table => new
                {
                    IdGestor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestores", x => x.IdGestor);
                });

            migrationBuilder.CreateTable(
                name: "Instrutores",
                columns: table => new
                {
                    IdInstrutor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREF = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrutores", x => x.IdInstrutor);
                });

            migrationBuilder.CreateTable(
                name: "Licencas",
                columns: table => new
                {
                    IdLicenca = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPlano = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mensalidade = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: false),
                    DateAssinatura = table.Column<DateOnly>(type: "date", nullable: false),
                    CaminhoPdf = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licencas", x => x.IdLicenca);
                });

            migrationBuilder.CreateTable(
                name: "LogsAtividade",
                columns: table => new
                {
                    IdLog = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Acao = table.Column<int>(type: "int", nullable: false),
                    DispositivoLogado = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Localizacao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsAtividade", x => x.IdLog);
                });

            migrationBuilder.CreateTable(
                name: "PlanosContrato",
                columns: table => new
                {
                    IdPlano = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosContrato", x => x.IdPlano);
                });

            migrationBuilder.CreateTable(
                name: "PlanosLicenca",
                columns: table => new
                {
                    IdPlanoLicenca = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosLicenca", x => x.IdPlanoLicenca);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    IdRefreshToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revogado = table.Column<bool>(type: "bit", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.IdRefreshToken);
                });

            migrationBuilder.CreateTable(
                name: "TelefonesAcademia",
                columns: table => new
                {
                    IdTelefone = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelefonesAcademia", x => x.IdTelefone);
                });

            migrationBuilder.CreateTable(
                name: "TelefonesUsuario",
                columns: table => new
                {
                    IdTelefone = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelefonesUsuario", x => x.IdTelefone);
                });

            migrationBuilder.CreateTable(
                name: "Treinos",
                columns: table => new
                {
                    IdTreino = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFicha = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Exercicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    NomeTreino = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treinos", x => x.IdTreino);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    TipoUsuario = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Flag = table.Column<bool>(type: "bit", nullable: false),
                    Quadra = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "XpHistoricos",
                columns: table => new
                {
                    IdXp = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    XpGanho = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XpHistoricos", x => x.IdXp);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administradores_IdAcademia_IdFuncionario",
                table: "Administradores",
                columns: new[] { "IdAcademia", "IdFuncionario" });

            migrationBuilder.CreateIndex(
                name: "IX_Agendas_IdAcademia_IdAgenda",
                table: "Agendas",
                columns: new[] { "IdAcademia", "IdAgenda" });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_IdAcademia_IdAluno",
                table: "Alunos",
                columns: new[] { "IdAcademia", "IdAluno" });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_IdInstrutor",
                table: "Alunos",
                column: "IdInstrutor");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_IdUsuario",
                table: "Alunos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_IdAcademia_IdAvaliacao",
                table: "Avaliacoes",
                columns: new[] { "IdAcademia", "IdAvaliacao" });

            migrationBuilder.CreateIndex(
                name: "IX_Fichas_IdAcademia_IdFicha",
                table: "Fichas",
                columns: new[] { "IdAcademia", "IdFicha" });

            migrationBuilder.CreateIndex(
                name: "IX_FichasMedicas_IdAcademia_IdFicha",
                table: "FichasMedicas",
                columns: new[] { "IdAcademia", "IdFicha" });

            migrationBuilder.CreateIndex(
                name: "IX_FichasMedicas_IdAluno",
                table: "FichasMedicas",
                column: "IdAluno");

            migrationBuilder.CreateIndex(
                name: "IX_Frequencias_IdAcademia_IdFrequencia",
                table: "Frequencias",
                columns: new[] { "IdAcademia", "IdFrequencia" });

            migrationBuilder.CreateIndex(
                name: "IX_Frequencias_IdAluno",
                table: "Frequencias",
                column: "IdAluno");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_IdAcademia_IdFuncionario",
                table: "Funcionarios",
                columns: new[] { "IdAcademia", "IdFuncionario" });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_IdUsuario",
                table: "Funcionarios",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Gestores_IdAcademia_IdGestor",
                table: "Gestores",
                columns: new[] { "IdAcademia", "IdGestor" });

            migrationBuilder.CreateIndex(
                name: "IX_Gestores_IdUsuario",
                table: "Gestores",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Instrutores_IdAcademia_IdInstrutor",
                table: "Instrutores",
                columns: new[] { "IdAcademia", "IdInstrutor" });

            migrationBuilder.CreateIndex(
                name: "IX_Instrutores_IdUsuario",
                table: "Instrutores",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_LogsAtividade_IdAcademia_IdLog",
                table: "LogsAtividade",
                columns: new[] { "IdAcademia", "IdLog" });

            migrationBuilder.CreateIndex(
                name: "IX_LogsAtividade_IdUsuario",
                table: "LogsAtividade",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_IdAcademia_IdRefreshToken",
                table: "RefreshTokens",
                columns: new[] { "IdAcademia", "IdRefreshToken" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_IdUsuario",
                table: "RefreshTokens",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_TokenHash",
                table: "RefreshTokens",
                column: "TokenHash");

            migrationBuilder.CreateIndex(
                name: "IX_TelefonesUsuario_IdAcademia_IdTelefone",
                table: "TelefonesUsuario",
                columns: new[] { "IdAcademia", "IdTelefone" });

            migrationBuilder.CreateIndex(
                name: "IX_Treinos_IdAcademia_IdTreino",
                table: "Treinos",
                columns: new[] { "IdAcademia", "IdTreino" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CPF",
                table: "Usuarios",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdAcademia_IdUsuario",
                table: "Usuarios",
                columns: new[] { "IdAcademia", "IdUsuario" });

            migrationBuilder.CreateIndex(
                name: "IX_XpHistoricos_IdAcademia_IdXp",
                table: "XpHistoricos",
                columns: new[] { "IdAcademia", "IdXp" });

            migrationBuilder.CreateIndex(
                name: "IX_XpHistoricos_IdUsuario",
                table: "XpHistoricos",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Academias");

            migrationBuilder.DropTable(
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "Agendas");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Fichas");

            migrationBuilder.DropTable(
                name: "FichasMedicas");

            migrationBuilder.DropTable(
                name: "Frequencias");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Gestores");

            migrationBuilder.DropTable(
                name: "Instrutores");

            migrationBuilder.DropTable(
                name: "Licencas");

            migrationBuilder.DropTable(
                name: "LogsAtividade");

            migrationBuilder.DropTable(
                name: "PlanosContrato");

            migrationBuilder.DropTable(
                name: "PlanosLicenca");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "TelefonesAcademia");

            migrationBuilder.DropTable(
                name: "TelefonesUsuario");

            migrationBuilder.DropTable(
                name: "Treinos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "XpHistoricos");
        }
    }
}
