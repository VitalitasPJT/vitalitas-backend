/* =====================================================================================
   CREATE.sql
   Script de criação de schema, gerado a partir do modelo de domínio ATUAL
   (src/Domain/Features/**/Entities), refletindo nomes de tabela/coluna conforme
   as IEntityTypeConfiguration (src/Infrastructure/Database/Configurations/**).

   Entidades PROPOSITALMENTE EXCLUÍDAS: Avaliacao, Funcionario.
   Observação: como "Avaliacao" foi removida, a coluna Ficha.IdAvaliacao continua
   existindo no domínio, mas não há mais tabela de avaliação para referenciar —
   o valor gravado (ver INSERT.sql) é apenas um GUID fictício de preenchimento.

   Não há FOREIGN KEY físicas neste script porque o schema atual gerado pelo EF Core
   (Migrations/20260807112226_InitialCreate.cs) também não define nenhuma — os
   relacionamentos são apenas lógicos (Ids soltos).
   ===================================================================================== */

SET NOCOUNT ON;
GO

-- =====================================================================================
-- 1. LIMPEZA
-- =====================================================================================
DROP TABLE IF EXISTS RefreshToken;
DROP TABLE IF EXISTS Treino;
DROP TABLE IF EXISTS FichaMedica;
DROP TABLE IF EXISTS Ficha;
DROP TABLE IF EXISTS Agenda;
DROP TABLE IF EXISTS LogAtividade;
DROP TABLE IF EXISTS XpHistorico;
DROP TABLE IF EXISTS Frequencia;
DROP TABLE IF EXISTS TelefoneAcademia;
DROP TABLE IF EXISTS TelefoneUsuario;
DROP TABLE IF EXISTS Aluno;
DROP TABLE IF EXISTS Administrador;
DROP TABLE IF EXISTS Instrutor;
DROP TABLE IF EXISTS Gestor;
DROP TABLE IF EXISTS Usuario;
DROP TABLE IF EXISTS Academia;
DROP TABLE IF EXISTS Contrato;
DROP TABLE IF EXISTS PlanoContrato;
DROP TABLE IF EXISTS Licenca;
DROP TABLE IF EXISTS PlanoLicenca;
GO

-- =====================================================================================
-- 2. CRIAÇÃO DAS TABELAS
-- =====================================================================================

CREATE TABLE PlanoLicenca (
    IdPlanoLicenca UNIQUEIDENTIFIER PRIMARY KEY,
    Nome           NVARCHAR(200) NOT NULL,
    Descricao      NVARCHAR(1000) NULL,
    Valor          NVARCHAR(20) NOT NULL
);

CREATE TABLE PlanoContrato (
    IdPlano   UNIQUEIDENTIFIER PRIMARY KEY,
    Nome      NVARCHAR(200) NOT NULL,
    Descricao NVARCHAR(1000) NULL,
    Valor     NVARCHAR(20) NOT NULL
);

CREATE TABLE Licenca (
    IdLicenca      UNIQUEIDENTIFIER PRIMARY KEY,
    IdPlano        UNIQUEIDENTIFIER NOT NULL, -- PlanoLicenca.IdPlanoLicenca
    Mensalidade    NVARCHAR(20) NOT NULL,
    Status         INT NOT NULL,              -- StatusLicenca
    Tipo           INT NOT NULL,              -- TipoLicenca
    DataFim        DATE NOT NULL,
    DataAssinatura DATE NOT NULL,
    CaminhoPdf     NVARCHAR(500) NULL
);

CREATE TABLE Contrato (
    IdContrato      UNIQUEIDENTIFIER PRIMARY KEY,
    IdPlanoContrato UNIQUEIDENTIFIER NOT NULL, -- PlanoContrato.IdPlano
    Mensalidade     NVARCHAR(20) NOT NULL,
    Status          INT NOT NULL,              -- StatusContrato
    DataFim         DATE NOT NULL,
    DataAssinatura  DATE NOT NULL
);

CREATE TABLE Academia (
    IdAcadenia          UNIQUEIDENTIFIER PRIMARY KEY, -- (nome mantido conforme entidade, com o typo original)
    IdLicenca           UNIQUEIDENTIFIER NOT NULL,    -- Licenca.IdLicenca
    IdGestor            UNIQUEIDENTIFIER NOT NULL,    -- Gestor.IdGestor
    NomeAcademia        NVARCHAR(200) NOT NULL,
    CNPJ                NVARCHAR(18) NOT NULL,
    Quadra              NVARCHAR(MAX) NOT NULL,
    Rua                 NVARCHAR(MAX) NOT NULL,
    Bairro              NVARCHAR(MAX) NOT NULL,
    Cidade              NVARCHAR(MAX) NOT NULL,
    Estado              NVARCHAR(MAX) NOT NULL,
    CEP                 NVARCHAR(MAX) NOT NULL,
    TipoAcademia        INT NOT NULL,              -- TipoAcademia
    EmailInstitucional  NVARCHAR(200) NOT NULL
);

CREATE TABLE Usuario (
    IdUsuario       UNIQUEIDENTIFIER PRIMARY KEY,
    IdAcademia      UNIQUEIDENTIFIER NOT NULL, -- Academia.IdAcadenia
    Nome            NVARCHAR(200) NOT NULL,
    Email           NVARCHAR(200) NOT NULL,
    Senha           NVARCHAR(MAX) NOT NULL,
    DataNascimento  DATE NOT NULL,
    CPF             NVARCHAR(14) NOT NULL,
    TipoUsuario     INT NOT NULL,              -- TipoUsuario
    Ativo           BIT NOT NULL,
    Flag            BIT NOT NULL,
    Quadra          NVARCHAR(MAX) NOT NULL,
    Rua             NVARCHAR(MAX) NOT NULL,
    Bairro          NVARCHAR(MAX) NOT NULL,
    Cidade          NVARCHAR(MAX) NOT NULL,
    Estado          NVARCHAR(MAX) NOT NULL,
    CEP             NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Gestor (
    IdGestor  UNIQUEIDENTIFIER PRIMARY KEY,
    IdUsuario UNIQUEIDENTIFIER NOT NULL -- Usuario.IdUsuario
);

CREATE TABLE Instrutor (
    IdInstrutor UNIQUEIDENTIFIER PRIMARY KEY,
    IdUsuario   UNIQUEIDENTIFIER NOT NULL, -- Usuario.IdUsuario
    CREF        NVARCHAR(20) NOT NULL
);

CREATE TABLE Administrador (
    IdFuncionario UNIQUEIDENTIFIER PRIMARY KEY,
    IdUsuario     UNIQUEIDENTIFIER NOT NULL, -- Usuario.IdUsuario
    Cargo         INT NOT NULL               -- Cargo
);

CREATE TABLE Aluno (
    IdAluno     UNIQUEIDENTIFIER PRIMARY KEY,
    IdUsuario   UNIQUEIDENTIFIER NOT NULL, -- Usuario.IdUsuario
    IdInstrutor UNIQUEIDENTIFIER NOT NULL, -- Instrutor.IdInstrutor
    IdContrato  UNIQUEIDENTIFIER NOT NULL, -- Contrato.IdContrato
    Objetivo    NVARCHAR(500) NOT NULL
);

CREATE TABLE TelefoneUsuario (
    IdTelefone UNIQUEIDENTIFIER PRIMARY KEY,
    IdUsuario  UNIQUEIDENTIFIER NOT NULL, -- Usuario.IdUsuario
    Telefone   NVARCHAR(20) NOT NULL
);

CREATE TABLE TelefoneAcademia (
    IdTelefone UNIQUEIDENTIFIER PRIMARY KEY,
    IdAcademia UNIQUEIDENTIFIER NOT NULL, -- Academia.IdAcadenia
    Telefone   NVARCHAR(20) NOT NULL
);

CREATE TABLE Frequencia (
    IdFrequencia       UNIQUEIDENTIFIER PRIMARY KEY,
    IdAluno            UNIQUEIDENTIFIER NOT NULL, -- Aluno.IdAluno
    TempoTreinoMinutos INT NOT NULL,
    Data               DATETIME2 NOT NULL
);

CREATE TABLE XpHistorico (
    IdXp      UNIQUEIDENTIFIER PRIMARY KEY,
    IdUsuario UNIQUEIDENTIFIER NOT NULL, -- Usuario.IdUsuario
    XpGanho   INT NOT NULL,
    Data      DATETIME2 NOT NULL,
    Motivo    NVARCHAR(500) NULL
);

CREATE TABLE LogAtividade (
    IdLog             UNIQUEIDENTIFIER PRIMARY KEY,
    IdUsuario         UNIQUEIDENTIFIER NOT NULL, -- Usuario.IdUsuario
    DataHora          DATETIME2 NOT NULL,
    Acao              INT NOT NULL,              -- AcaoLog (placeholder — enum ainda sem membros no domínio)
    DispositivoLogado NVARCHAR(300) NULL,
    Localizacao       NVARCHAR(300) NULL
);

CREATE TABLE Agenda (
    IdAgenda    INT IDENTITY(1,1) PRIMARY KEY,
    IdInstrutor UNIQUEIDENTIFIER NOT NULL, -- Instrutor.IdInstrutor
    IdAcademia  UNIQUEIDENTIFIER NOT NULL, -- Academia.IdAcadenia
    Status      INT NOT NULL,              -- StatusAgenda
    Data        DATETIME2 NOT NULL
);

CREATE TABLE Ficha (
    IdFicha      UNIQUEIDENTIFIER PRIMARY KEY,
    IdAvaliacao  UNIQUEIDENTIFIER NOT NULL, -- sem tabela de origem (Avaliacao foi removida); GUID fictício
    NomeFicha    NVARCHAR(200) NOT NULL,
    Observacoes  NVARCHAR(1000) NULL
);

CREATE TABLE FichaMedica (
    IdFicha         UNIQUEIDENTIFIER PRIMARY KEY,
    IdAluno         UNIQUEIDENTIFIER NOT NULL, -- Aluno.IdAluno
    Alergia         NVARCHAR(500) NULL,
    Restricao       NVARCHAR(500) NULL,
    Lesao           NVARCHAR(500) NULL,
    Cirurgia        NVARCHAR(500) NULL,
    ProblemaSaude   NVARCHAR(500) NULL,
    UsoMedicamento  NVARCHAR(500) NULL
);

CREATE TABLE Treino (
    IdTreino   UNIQUEIDENTIFIER PRIMARY KEY,
    IdFicha    UNIQUEIDENTIFIER NOT NULL, -- Ficha.IdFicha
    Exercicio  NVARCHAR(MAX) NOT NULL,    -- JSON: Dictionary<string, List<Exercicio>>
    Tipo       INT NOT NULL,              -- TipoTreino
    NomeTreino NVARCHAR(200) NOT NULL
);

CREATE TABLE RefreshToken (
    IdRefreshToken UNIQUEIDENTIFIER PRIMARY KEY,
    TokenHash      NVARCHAR(500) NOT NULL,
    DataExpiracao  DATETIME2 NOT NULL,
    Revogado       BIT NOT NULL,
    IdUsuario      UNIQUEIDENTIFIER NOT NULL -- Usuario.IdUsuario
);
GO
