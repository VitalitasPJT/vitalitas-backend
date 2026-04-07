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

<<<<<<< HEAD
CREATE TABLE Plano_licenca (
=======
CREATE TABLE planoLicenca (
>>>>>>> aebb072d482b9fcdd47b0b41ed5ee5152ad93f17
    idPlano INT PRIMARY KEY,
    nome VARCHAR(100),
    descricao VARCHAR(255),
    valor FLOAT
);

<<<<<<< HEAD
CREATE TABLE Plano_contrato (
=======
CREATE TABLE planoContrato (
>>>>>>> aebb072d482b9fcdd47b0b41ed5ee5152ad93f17
    idPlano INT PRIMARY KEY,
    nome VARCHAR(100),
    descricao VARCHAR(255),
    valor FLOAT
);

<<<<<<< HEAD
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
=======
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
>>>>>>> aebb072d482b9fcdd47b0b41ed5ee5152ad93f17
    nomeAcademia VARCHAR(100),
    cnpj VARCHAR(20),
    emailInstitucional VARCHAR(100),
    tipoAcademia VARCHAR(50),
    cep VARCHAR(15),
    idLicenca INT,
<<<<<<< HEAD
    idGestor INT REFERENCES Gestor(idGestor)
);

CREATE TABLE Licenca (
=======
    idGestor UNIQUEIDENTIFIER REFERENCES gestor(idGestor)
);

CREATE TABLE licenca (
>>>>>>> aebb072d482b9fcdd47b0b41ed5ee5152ad93f17
    idLicenca INT PRIMARY KEY,
    status VARCHAR(20),
    tipo VARCHAR(50),
    dataFim DATETIME,
    caminhoPdf VARCHAR(255),
    dataAssinatura DATETIME,
<<<<<<< HEAD
    idPlano INT REFERENCES Plano_licenca(idPlano),
    mensalidade FLOAT
);

CREATE TABLE Contrato (
=======
    idPlano INT REFERENCES planoLicenca(idPlano),
    mensalidade FLOAT
);

CREATE TABLE contrato (
>>>>>>> aebb072d482b9fcdd47b0b41ed5ee5152ad93f17
    idContrato INT PRIMARY KEY,
    status VARCHAR(20),
    dataFim DATETIME,
    dataAssinatura DATETIME,
<<<<<<< HEAD
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

=======
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

>>>>>>> aebb072d482b9fcdd47b0b41ed5ee5152ad93f17
CREATE TABLE Agenda (
    idAgenda INT PRIMARY KEY,
    status VARCHAR(20),
    data DATETIME
);

<<<<<<< HEAD
CREATE TABLE Log_atividade (
=======
CREATE TABLE logAtividade (
>>>>>>> aebb072d482b9fcdd47b0b41ed5ee5152ad93f17
    idLog INT PRIMARY KEY,
    acao VARCHAR(100),
    dataHora DATETIME
);

<<<<<<< HEAD
CREATE TABLE Xp_historico (
=======
CREATE TABLE xpHistorico (
>>>>>>> aebb072d482b9fcdd47b0b41ed5ee5152ad93f17
    idXp INT PRIMARY KEY,
    motivop VARCHAR(255),
    data DATETIME,
    xpGanho FLOAT
);

<<<<<<< HEAD
CREATE TABLE Avaliacao (
    idAvaliacao INT PRIMARY KEY,
    idAluno INT REFERENCES Aluno(idAluno),
=======
CREATE TABLE avaliacao (
    idAvaliacao INT PRIMARY KEY,
    idAluno UNIQUEIDENTIFIER REFERENCES aluno(idAluno),
>>>>>>> aebb072d482b9fcdd47b0b41ed5ee5152ad93f17
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
<<<<<<< HEAD
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
=======
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
>>>>>>> aebb072d482b9fcdd47b0b41ed5ee5152ad93f17
    idVideo INT PRIMARY KEY,
    dataUpload DATETIME,
    titulo VARCHAR(150),
    caminhoArquivo VARCHAR(255),
<<<<<<< HEAD
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
=======
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
>>>>>>> aebb072d482b9fcdd47b0b41ed5ee5152ad93f17
    data DATE,
    tempoTreino INT
);

<<<<<<< HEAD
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
=======
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
>>>>>>> aebb072d482b9fcdd47b0b41ed5ee5152ad93f17
    alergia VARCHAR(100),
    restricao VARCHAR(255),
    lesao VARCHAR(255),
    cirurgia VARCHAR(255),
    problemaSaude VARCHAR(255),
    usoMedicamento VARCHAR(255)
);