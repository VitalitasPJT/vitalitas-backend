CREATE DATABASE VITALITAS_DEV;
GO

USE VITALITAS_DEV;
GO

DROP TABLE IF EXISTS fichaMedica;
DROP TABLE IF EXISTS xpAluno;
DROP TABLE IF EXISTS agendaProfessor;
DROP TABLE IF EXISTS usuarioLog;
DROP TABLE IF EXISTS frequencia;
DROP TABLE IF EXISTS telefoneAcademia;
DROP TABLE IF EXISTS telefoneUsuario;
DROP TABLE IF EXISTS video;
DROP TABLE IF EXISTS treino;
DROP TABLE IF EXISTS ficha;
DROP TABLE IF EXISTS avaliacao;
DROP TABLE IF EXISTS funcionario;
DROP TABLE IF EXISTS aluno;
DROP TABLE IF EXISTS contrato;
DROP TABLE IF EXISTS licenca;
DROP TABLE IF EXISTS instrutor;
DROP TABLE IF EXISTS gestor;
DROP TABLE IF EXISTS xpHistorico;
DROP TABLE IF EXISTS logAtividade;
DROP TABLE IF EXISTS agenda;
DROP TABLE IF EXISTS planoContrato;
DROP TABLE IF EXISTS planoLicenca;
DROP TABLE IF EXISTS academia;
DROP TABLE IF EXISTS usuario;

CREATE TABLE usuario (
    idUsuario UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    quadra VARCHAR(100),
    rua VARCHAR(100),
    bairro VARCHAR(100),
    cidade VARCHAR(100),
    estado VARCHAR(100),
    cep VARCHAR(100),
    senha VARCHAR(255) NOT NULL,
    dataNascimento DATE,
    cpf VARCHAR(20) NOT NULL UNIQUE,
    tipoUsuario INT NOT NULL,
    flag BIT NOT NULL DEFAULT 1,
);

CREATE TABLE planoLicenca (
    idPlano INT PRIMARY KEY,
    nome VARCHAR(100),
    descricao VARCHAR(255),
    valor FLOAT
);

CREATE TABLE planoContrato (
    idPlano INT PRIMARY KEY,
    nome VARCHAR(100),
    descricao VARCHAR(255),
    valor FLOAT
);

CREATE TABLE gestor (
    idGestor UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario)
);

CREATE TABLE instrutor (
    idInstrutor UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    cref VARCHAR(30),
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario)
);

CREATE TABLE academia (
    idAcademia UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    nomeAcademia VARCHAR(100),
    cnpj VARCHAR(20),
    emailInstitucional VARCHAR(100),
    tipoAcademia VARCHAR(50),
    cep VARCHAR(15),
    idLicenca INT,
    idGestor UNIQUEIDENTIFIER REFERENCES gestor(idGestor)
);

CREATE TABLE licenca (
    idLicenca INT PRIMARY KEY,
    status VARCHAR(20),
    tipo VARCHAR(50),
    dataFim DATETIME,
    caminhoPdf VARCHAR(255),
    dataAssinatura DATETIME,
    idPlano INT REFERENCES planoLicenca(idPlano),
    mensalidade FLOAT
);

CREATE TABLE contrato (
    idContrato INT PRIMARY KEY,
    status VARCHAR(20),
    dataFim DATETIME,
    dataAssinatura DATETIME,
    idPlano INT REFERENCES planoContrato(idPlano),
    mensalidade FLOAT
);

CREATE TABLE aluno (
    idAluno UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    objetivo VARCHAR(255),
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario),
    idContrato INT REFERENCES contrato(idContrato),
    idAcademia UNIQUEIDENTIFIER REFERENCES academia(idAcademia)
);

CREATE TABLE funcionario (
    idFuncionario UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario),
    cargo VARCHAR(100)
);

CREATE TABLE Agenda (
    idAgenda INT PRIMARY KEY,
    status VARCHAR(20),
    data DATETIME
);

CREATE TABLE logAtividade (
    idLog INT PRIMARY KEY,
    acao VARCHAR(100),
    dataHora DATETIME
);

CREATE TABLE xpHistorico (
    idXp INT PRIMARY KEY,
    motivop VARCHAR(255),
    data DATETIME,
    xpGanho FLOAT
);

CREATE TABLE avaliacao (
    idAvaliacao INT PRIMARY KEY,
    idAluno UNIQUEIDENTIFIER REFERENCES aluno(idAluno),
    sexo VARCHAR(10),
    data DATETIME,
    peso FLOAT,
    altura FLOAT,
    idade INT,
    imc FLOAT,
    glicemia FLOAT,
    pa FLOAT,
    densidade FLOAT,
    tr FLOAT,
    se FLOAT,
    pt FLOAT,
    ax FLOAT,
    si FLOAT,
    ab FLOAT,
    cx FLOAT,
    ptrrlh FLOAT,
    umero FLOAT,
    femur FLOAT,
    pMagro FLOAT,
    pGordo FLOAT,
    pViscera FLOAT,
    pOsseo FLOAT,
    torax FLOAT,
    abdomen FLOAT,
    quadril FLOAT,
    bracoD FLOAT,
    bracoE FLOAT,
    coxaD FLOAT,
    coxaE FLOAT,
    pernaD FLOAT,
    pernaE FLOAT,
    deltoide FLOAT,
    idInstrutor UNIQUEIDENTIFIER REFERENCES instrutor(idInstrutor)
);

CREATE TABLE ficha (
    idFicha INT PRIMARY KEY,
    nomeFicha VARCHAR(100),
    observacoes VARCHAR(255),
    idAvaliacao INT REFERENCES avaliacao(idAvaliacao),
    idAluno UNIQUEIDENTIFIER REFERENCES aluno(idAluno)
);

CREATE TABLE treino (
    idTreino INT PRIMARY KEY,
    nomeTreino VARCHAR(100),
    idFicha INT REFERENCES ficha(idFicha),
    exercicio NVARCHAR(MAX)
);

CREATE TABLE video (
    idVideo INT PRIMARY KEY,
    dataUpload DATETIME,
    titulo VARCHAR(150),
    caminhoArquivo VARCHAR(255),
    idAcademia UNIQUEIDENTIFIER REFERENCES academia(idAcademia)
);

CREATE TABLE telefoneUsuario (
    telefone VARCHAR(20),
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario)
);

CREATE TABLE telefoneAcademia (
    telefone VARCHAR(20),
    idAcademia UNIQUEIDENTIFIER REFERENCES academia(idAcademia)
);

CREATE TABLE frequencia (
    idFrequencia INT PRIMARY KEY,
    idAluno UNIQUEIDENTIFIER REFERENCES aluno(idAluno),
    data DATE,
    tempoTreino INT
);

CREATE TABLE usuarioLog (
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario),
    idLog INT REFERENCES logAtividade(idLog)
);

CREATE TABLE agendaProfessor (
    idAgenda INT REFERENCES agenda(idAgenda),
    idInstrutor UNIQUEIDENTIFIER REFERENCES instrutor(idInstrutor)
);

CREATE TABLE xpAluno (
    idXp INT REFERENCES xpHistorico(idXp),
    idAluno UNIQUEIDENTIFIER REFERENCES aluno(idAluno)
);

CREATE TABLE fichaMedica (
    idFicha INT PRIMARY KEY,
    idAluno UNIQUEIDENTIFIER REFERENCES aluno(idAluno),
    alergia VARCHAR(100),
    restricao VARCHAR(255),
    lesao VARCHAR(255),
    cirurgia VARCHAR(255),
    problemaSaude VARCHAR(255),
    usoMedicamento VARCHAR(255)
);