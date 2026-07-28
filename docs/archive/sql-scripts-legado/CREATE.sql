
USE master;
GO

ALTER DATABASE VITALITAS_DEV
SET SINGLE_USER
WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE VITALITAS_DEV;
GO


USE VITALITAS_DEV;
GO

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
DROP TABLE IF EXISTS planoLicenca;
DROP TABLE IF EXISTS planoContrato;
DROP TABLE IF EXISTS plano;
DROP TABLE IF EXISTS academia;
DROP TABLE IF EXISTS usuario;



CREATE TABLE planoLicenca (
    idPlanoLicenca INT PRIMARY KEY,
    nome VARCHAR(100),
    descricao VARCHAR(255),
    valor DECIMAL(10,2)
);

CREATE TABLE licenca (
    idLicenca UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    idPlano INT REFERENCES planoLicenca(idPlanoLicenca),
    mensalidade DECIMAL(10,2),
    status VARCHAR(20),
    tipo VARCHAR(50),
    dataFim DATETIME,
    dataAssinatura DATETIME,
    caminhoPdf VARCHAR(255) 
);

CREATE TABLE academia (
    idAcademia UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    idLicenca UNIQUEIDENTIFIER REFERENCES licenca(idLicenca),
    idGestor UNIQUEIDENTIFIER, -- Sem o REFERENCES aqui por enquanto!
    nomeAcademia VARCHAR(100),
    cnpj VARCHAR(14),
    quadra VARCHAR(100),
    rua VARCHAR(100),
    bairro VARCHAR(100),
    cidade VARCHAR(100),
    estado VARCHAR(100),
    cep VARCHAR(8),
    tipoAcademia VARCHAR(50),
    emailInstitucional VARCHAR(100)
);

CREATE TABLE usuario (
    idUsuario UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    idAcademia UNIQUEIDENTIFIER REFERENCES academia(idAcademia),
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    senha VARCHAR(255) NOT NULL,
    dataNascimento DATE,
    cpf VARCHAR(11) NOT NULL UNIQUE,
    tipoUsuario INT NOT NULL,
    ativo BIT NOT NULL,
    flag BIT NOT NULL DEFAULT 1,
    quadra VARCHAR(100),
    rua VARCHAR(100),
    bairro VARCHAR(100),
    cidade VARCHAR(100),
    estado VARCHAR(100),
    cep VARCHAR(8)
);

CREATE TABLE refreshToken (
    idRefreshToken UNIQUEIDENTIFIER PRIMARY KEY,
    tokenHash VARCHAR(255) NOT NULL,
    dataExpiracao DATETIME NOT NULL,
    revogado BIT NOT NULL DEFAULT 0,
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario)
);

CREATE TABLE gestor (
    idGestor UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario)
);

ALTER TABLE academia 
ADD CONSTRAINT FK_Academia_Gestor 
FOREIGN KEY (idGestor) REFERENCES gestor(idGestor);

CREATE TABLE instrutor (
    idInstrutor UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario),
    cref VARCHAR(30)
);


CREATE TABLE contrato (
    idContrato UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    idPlanoContrato INT,
    mensalidade DECIMAL(10,2),
    status INT  NOT NULL,
    caminhoPdf VARCHAR(255),
    dataFim DATETIME,
    dataAssinatura DATETIME
);

CREATE TABLE planoContrato (
    idPlanoContrato INT PRIMARY KEY,
    nome VARCHAR(100),
    descricao VARCHAR(255),
    valor DECIMAL(10,2)
);

ALTER TABLE contrato
ADD CONSTRAINT FK_Contrato_PlanoContrato  
FOREIGN KEY (idPlanoContrato) REFERENCES planoContrato(idPlanoContrato);

CREATE TABLE aluno (
    idAluno UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario),
    IdInstrutor UNIQUEIDENTIFIER REFERENCES instrutor(idInstrutor),
    idContrato UNIQUEIDENTIFIER REFERENCES contrato(idContrato),
    objetivo VARCHAR(255)
);

CREATE TABLE funcionario (
    idFuncionario UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario),
    cargo VARCHAR(100)
);

CREATE TABLE agenda (
    idAgenda INT PRIMARY KEY,
    idInstrutor UNIQUEIDENTIFIER REFERENCES instrutor(idInstrutor),
    idAcademia UNIQUEIDENTIFIER REFERENCES academia(idAcademia),
    status int NOT NULL,
    data DATETIME
);

CREATE TABLE logAtividade (
    idLog UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario),
    dataHora DATETIME,
    acao int NOT NULL,
    dispositivoLogado VARCHAR(255),
    localizacao VARCHAR(255)
);


CREATE TABLE xpHistorico (
    idXp UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    motivo VARCHAR(255),
    data DATETIME,
    xpGanho FLOAT
);

CREATE TABLE avaliacao (
    idAvaliacao UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
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
    idFicha UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    nomeFicha VARCHAR(100),
    observacoes VARCHAR(255),
    idAvaliacao UNIQUEIDENTIFIER REFERENCES avaliacao(idAvaliacao),
    idAluno UNIQUEIDENTIFIER REFERENCES aluno(idAluno)
);

CREATE TABLE treino (
    idTreino INT PRIMARY KEY,
    nomeTreino VARCHAR(100),
    idFicha UNIQUEIDENTIFIER REFERENCES ficha(idFicha),
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
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario),
    PRIMARY KEY (telefone, idUsuario)
);

CREATE TABLE telefoneAcademia (
    telefone VARCHAR(20),
    idAcademia UNIQUEIDENTIFIER REFERENCES academia(idAcademia),
    PRIMARY KEY (telefone, idAcademia)
);

CREATE TABLE frequencia (
    idFrequencia UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    idAluno UNIQUEIDENTIFIER REFERENCES aluno(idAluno),
    data DATE,
    tempoTreino INT
);

CREATE TABLE usuarioLog (
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario),
    idLog UNIQUEIDENTIFIER REFERENCES logAtividade(idLog),
    PRIMARY KEY (idUsuario, idLog)
);

CREATE TABLE agendaProfessor (
    idAgenda INT REFERENCES agenda(idAgenda),
    idInstrutor UNIQUEIDENTIFIER REFERENCES instrutor(idInstrutor),
    PRIMARY KEY (idAgenda, idInstrutor)
);

CREATE TABLE usuarioAcademia (
    idAcademia UNIQUEIDENTIFIER REFERENCES academia(idAcademia),
    idUsuario UNIQUEIDENTIFIER REFERENCES usuario(idUsuario),
    PRIMARY KEY (idAcademia, idUsuario)
);

CREATE TABLE xpAluno (
    idXp UNIQUEIDENTIFIER REFERENCES xpHistorico(idXp),
    idAluno UNIQUEIDENTIFIER REFERENCES aluno(idAluno),
    PRIMARY KEY (idXp, idAluno)
);

CREATE TABLE fichaMedica (
    idFicha UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    idAluno UNIQUEIDENTIFIER REFERENCES aluno(idAluno),
    alergia VARCHAR(100),
    restricao VARCHAR(255),
    lesao VARCHAR(255),
    cirurgia VARCHAR(255),
    problemaSaude VARCHAR(255),
    usoMedicamento VARCHAR(255)
);