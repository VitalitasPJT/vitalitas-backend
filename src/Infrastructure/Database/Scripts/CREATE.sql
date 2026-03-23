CREATE DATABASE VITALITAS_DEV;
GO

USE VITALITAS_DEV;
GO

DROP TABLE IF EXISTS Ficha_medica;
DROP TABLE IF EXISTS Xp_aluno;
DROP TABLE IF EXISTS Agenda_professor;
DROP TABLE IF EXISTS Usuario_log;
DROP TABLE IF EXISTS Frequencia;
DROP TABLE IF EXISTS Telefone_academia;
DROP TABLE IF EXISTS Telefone_usuario;
DROP TABLE IF EXISTS Video;
DROP TABLE IF EXISTS Treino_exercicio;
DROP TABLE IF EXISTS Treinos;
DROP TABLE IF EXISTS Ficha;
DROP TABLE IF EXISTS Avaliacao;
DROP TABLE IF EXISTS Funcionario;
DROP TABLE IF EXISTS Aluno;
DROP TABLE IF EXISTS Contrato;
DROP TABLE IF EXISTS Licenca;
DROP TABLE IF EXISTS Instrutor;
DROP TABLE IF EXISTS Gestor;
DROP TABLE IF EXISTS Xp_historico;
DROP TABLE IF EXISTS Log_atividade;
DROP TABLE IF EXISTS Agenda;
DROP TABLE IF EXISTS Exercicios;
DROP TABLE IF EXISTS Plano_contrato;
DROP TABLE IF EXISTS Plano_licenca;
DROP TABLE IF EXISTS Academia;
DROP TABLE IF EXISTS Usuario;

CREATE TABLE Usuario (
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

CREATE TABLE Plano_licenca (
    idPlano INT PRIMARY KEY,
    nome VARCHAR(100),
    descricao VARCHAR(255),
    valor FLOAT
);

CREATE TABLE Plano_contrato (
    idPlano INT PRIMARY KEY,
    nome VARCHAR(100),
    descricao VARCHAR(255),
    valor FLOAT
);

CREATE TABLE Gestor (
    idGestor INT PRIMARY KEY,
    idUsuario INT REFERENCES Usuario(idUsuario)
);

CREATE TABLE Instrutor (
    idInstrutor INT PRIMARY KEY,
    cref VARCHAR(30),
    idUsuario INT REFERENCES Usuario(idUsuario)
);

CREATE TABLE Academia (
    idAcademia INT PRIMARY KEY,
    nomeAcademia VARCHAR(100),
    cnpj VARCHAR(20),
    emailInstitucional VARCHAR(100),
    tipoAcademia VARCHAR(50),
    cep VARCHAR(15),
    idLicenca INT,
    idGestor INT REFERENCES Gestor(idGestor)
);

CREATE TABLE Licenca (
    idLicenca INT PRIMARY KEY,
    status VARCHAR(20),
    tipo VARCHAR(50),
    dataFim DATETIME,
    caminhoPdf VARCHAR(255),
    dataAssinatura DATETIME,
    idPlano INT REFERENCES Plano_licenca(idPlano),
    mensalidade FLOAT
);

CREATE TABLE Contrato (
    idContrato INT PRIMARY KEY,
    status VARCHAR(20),
    dataFim DATETIME,
    dataAssinatura DATETIME,
    idPlano INT REFERENCES Plano_contrato(idPlano),
    mensalidade FLOAT
);

CREATE TABLE Aluno (
    idAluno INT PRIMARY KEY,
    objetivo VARCHAR(255),
    idUsuario INT REFERENCES Usuario(idUsuario),
    idContrato INT REFERENCES Contrato(idContrato),
    idAcademia INT REFERENCES Academia(idAcademia)
);

CREATE TABLE Funcionario (
    idFuncionario INT PRIMARY KEY,
    idUsuario INT REFERENCES Usuario(idUsuario),
    cargo VARCHAR(100)
);

CREATE TABLE Exercicios (
    idExercicio INT PRIMARY KEY,
    numerioSeries INT,
    nome VARCHAR(100),
    numeroRepeticoes INT,
    aparelho VARCHAR(50),
    musculo VARCHAR(50)
);

CREATE TABLE Agenda (
    idAgenda INT PRIMARY KEY,
    status VARCHAR(20),
    data DATETIME
);

CREATE TABLE Log_atividade (
    idLog INT PRIMARY KEY,
    acao VARCHAR(100),
    dataHora DATETIME
);

CREATE TABLE Xp_historico (
    idXp INT PRIMARY KEY,
    motivop VARCHAR(255),
    data DATETIME,
    xpGanho FLOAT
);

CREATE TABLE Avaliacao (
    idAvaliacao INT PRIMARY KEY,
    idAluno INT REFERENCES Aluno(idAluno),
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
    idInstrutor INT REFERENCES Instrutor(idInstrutor)
);

CREATE TABLE Ficha (
    idFicha INT PRIMARY KEY,
    nomeFicha VARCHAR(100),
    observacoes VARCHAR(255),
    idAvaliacao INT REFERENCES Avaliacao(idAvaliacao)
);

CREATE TABLE Treinos (
    idTreino INT PRIMARY KEY,
    tipo VARCHAR(20),
    nomeTreino VARCHAR(100),
    idFicha INT REFERENCES Ficha(idFicha)
);

CREATE TABLE Treino_exercicio (
    idExercicio INT REFERENCES Exercicios(idExercicio),
    idTreino INT REFERENCES Treinos(idTreino)
);

CREATE TABLE Video (
    idVideo INT PRIMARY KEY,
    dataUpload DATETIME,
    titulo VARCHAR(150),
    caminhoArquivo VARCHAR(255),
    idAcademia INT REFERENCES Academia(idAcademia)
);

CREATE TABLE Telefone_usuario (
    telefone VARCHAR(20),
    idUsuario INT REFERENCES Usuario(idUsuario)
);

CREATE TABLE Telefone_academia (
    telefone VARCHAR(20),
    idAcademia INT REFERENCES Academia(idAcademia)
);

CREATE TABLE Frequencia (
    idFrequencia INT PRIMARY KEY,
    idAluno INT REFERENCES Aluno(idAluno),
    data DATE,
    tempoTreino INT
);

CREATE TABLE Usuario_log (
    idUsuario INT REFERENCES Usuario(idUsuario),
    idLog INT REFERENCES Log_atividade(idLog)
);

CREATE TABLE Agenda_professor (
    idAgenda INT REFERENCES Agenda(idAgenda),
    idInstrutor INT REFERENCES Instrutor(idInstrutor)
);

CREATE TABLE Xp_aluno (
    idXp INT REFERENCES Xp_historico(idXp),
    idAluno INT REFERENCES Aluno(idAluno)
);

CREATE TABLE Ficha_medica (
    idFicha INT PRIMARY KEY,
    idAluno INT REFERENCES Aluno(idAluno),
    alergia VARCHAR(100),
    restricao VARCHAR(255),
    lesao VARCHAR(255),
    cirurgia VARCHAR(255),
    problemaSaude VARCHAR(255),
    usoMedicamento VARCHAR(255)
);