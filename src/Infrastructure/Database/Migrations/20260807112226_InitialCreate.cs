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
                name: "Academia",
                columns: table => new
                {
                    IdAcadenia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdLicenca = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdGestor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeAcademia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    Quadra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoAcademia = table.Column<int>(type: "int", nullable: false),
                    EmailInstitucional = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academia", x => x.IdAcadenia);
                });

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    IdFuncionario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cargo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.IdFuncionario);
                });

            migrationBuilder.CreateTable(
                name: "Agenda",
                columns: table => new
                {
                    IdAgenda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdInstrutor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agenda", x => x.IdAgenda);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    IdAluno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdInstrutor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdContrato = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Objetivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.IdAluno);
                });

            migrationBuilder.CreateTable(
                name: "avaliacao",
                columns: table => new
                {
                    id_avaliacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_professor = table.Column<int>(type: "int", nullable: false),
                    id_aluno = table.Column<int>(type: "int", nullable: false),
                    data = table.Column<DateOnly>(type: "date", nullable: false),
                    hora = table.Column<TimeOnly>(type: "time", nullable: false),
                    peso = table.Column<double>(type: "float", nullable: false),
                    sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    altura = table.Column<double>(type: "float", nullable: false),
                    idade = table.Column<int>(type: "int", nullable: false),
                    glicemia = table.Column<double>(type: "float", nullable: false),
                    pa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    densidade = table.Column<double>(type: "float", nullable: false),
                    ax = table.Column<double>(type: "float", nullable: false),
                    pt = table.Column<double>(type: "float", nullable: false),
                    se = table.Column<double>(type: "float", nullable: false),
                    tr = table.Column<double>(type: "float", nullable: false),
                    ab = table.Column<double>(type: "float", nullable: false),
                    si = table.Column<double>(type: "float", nullable: false),
                    ub = table.Column<double>(type: "float", nullable: false),
                    ur = table.Column<double>(type: "float", nullable: false),
                    rt = table.Column<double>(type: "float", nullable: false),
                    pr = table.Column<double>(type: "float", nullable: false),
                    px = table.Column<double>(type: "float", nullable: false),
                    femur = table.Column<double>(type: "float", nullable: false),
                    abdomen = table.Column<double>(type: "float", nullable: false),
                    torax = table.Column<double>(type: "float", nullable: false),
                    quadril = table.Column<double>(type: "float", nullable: false),
                    braco_d = table.Column<double>(type: "float", nullable: false),
                    braco_e = table.Column<double>(type: "float", nullable: false),
                    coxa_d = table.Column<double>(type: "float", nullable: false),
                    coxa_e = table.Column<double>(type: "float", nullable: false),
                    perna_d = table.Column<double>(type: "float", nullable: false),
                    perna_e = table.Column<double>(type: "float", nullable: false),
                    deltoide = table.Column<double>(type: "float", nullable: false),
                    peitoral = table.Column<double>(type: "float", nullable: false),
                    p_magro = table.Column<double>(type: "float", nullable: false),
                    p_gordo = table.Column<double>(type: "float", nullable: false),
                    p_osseo = table.Column<double>(type: "float", nullable: false),
                    p_viscera = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_avaliacao", x => x.id_avaliacao);
                });

            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    IdContrato = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPlanoContrato = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mensalidade = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: false),
                    DataAssinatura = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.IdContrato);
                });

            migrationBuilder.CreateTable(
                name: "Ficha",
                columns: table => new
                {
                    IdFicha = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAvaliacao = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeFicha = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ficha", x => x.IdFicha);
                });

            migrationBuilder.CreateTable(
                name: "FichaMedica",
                columns: table => new
                {
                    IdFicha = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAluno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Alergia = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Restricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Lesao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cirurgia = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ProblemaSaude = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UsoMedicamento = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaMedica", x => x.IdFicha);
                });

            migrationBuilder.CreateTable(
                name: "Frequencia",
                columns: table => new
                {
                    IdFrequencia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAluno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TempoTreinoMinutos = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequencia", x => x.IdFrequencia);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    IdFuncionario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cargo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.IdFuncionario);
                });

            migrationBuilder.CreateTable(
                name: "Gestor",
                columns: table => new
                {
                    IdGestor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestor", x => x.IdGestor);
                });

            migrationBuilder.CreateTable(
                name: "Instrutor",
                columns: table => new
                {
                    IdInstrutor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREF = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrutor", x => x.IdInstrutor);
                });

            migrationBuilder.CreateTable(
                name: "Licenca",
                columns: table => new
                {
                    IdLicenca = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPlano = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mensalidade = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: false),
                    DataAssinatura = table.Column<DateOnly>(type: "date", nullable: false),
                    CaminhoPdf = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenca", x => x.IdLicenca);
                });

            migrationBuilder.CreateTable(
                name: "LogAtividade",
                columns: table => new
                {
                    IdLog = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Acao = table.Column<int>(type: "int", nullable: false),
                    DispositivoLogado = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Localizacao = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAtividade", x => x.IdLog);
                });

            migrationBuilder.CreateTable(
                name: "PlanoContrato",
                columns: table => new
                {
                    IdPlano = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoContrato", x => x.IdPlano);
                });

            migrationBuilder.CreateTable(
                name: "PlanoLicenca",
                columns: table => new
                {
                    IdPlanoLicenca = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoLicenca", x => x.IdPlanoLicenca);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    IdRefreshToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revogado = table.Column<bool>(type: "bit", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.IdRefreshToken);
                });

            migrationBuilder.CreateTable(
                name: "TelefoneAcademia",
                columns: table => new
                {
                    IdTelefone = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelefoneAcademia", x => x.IdTelefone);
                });

            migrationBuilder.CreateTable(
                name: "TelefoneUsuario",
                columns: table => new
                {
                    IdTelefone = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelefoneUsuario", x => x.IdTelefone);
                });

            migrationBuilder.CreateTable(
                name: "Treino",
                columns: table => new
                {
                    IdTreino = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFicha = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Exercicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    NomeTreino = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treino", x => x.IdTreino);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAcademia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    TipoUsuario = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Flag = table.Column<bool>(type: "bit", nullable: false),
                    Quadra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "XpHistorico",
                columns: table => new
                {
                    IdXp = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    XpGanho = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XpHistorico", x => x.IdXp);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Academia");

            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "Agenda");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "avaliacao");

            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.DropTable(
                name: "Ficha");

            migrationBuilder.DropTable(
                name: "FichaMedica");

            migrationBuilder.DropTable(
                name: "Frequencia");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Gestor");

            migrationBuilder.DropTable(
                name: "Instrutor");

            migrationBuilder.DropTable(
                name: "Licenca");

            migrationBuilder.DropTable(
                name: "LogAtividade");

            migrationBuilder.DropTable(
                name: "PlanoContrato");

            migrationBuilder.DropTable(
                name: "PlanoLicenca");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "TelefoneAcademia");

            migrationBuilder.DropTable(
                name: "TelefoneUsuario");

            migrationBuilder.DropTable(
                name: "Treino");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "XpHistorico");
        }
    }
}
