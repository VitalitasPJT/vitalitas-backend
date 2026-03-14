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
    Id_usuario INT PRIMARY KEY,
    Nome VARCHAR(100),
    Email VARCHAR(100),
    Endereco VARCHAR(255),
    Senha VARCHAR(50),
    Data_nascimento DATETIME,
    CPF VARCHAR(20),
    Tipo_usuario VARCHAR(20),
    Flag INT
);

CREATE TABLE Plano_licenca (
    Id_plano INT PRIMARY KEY,
    Nome VARCHAR(100),
    Descricao VARCHAR(255),
    Valor FLOAT
);

CREATE TABLE Plano_contrato (
    Id_plano INT PRIMARY KEY,
    Nome VARCHAR(100),
    Descricao VARCHAR(255),
    Valor FLOAT
);

CREATE TABLE Gestor (
    Id_gestor INT PRIMARY KEY,
    Id_usuario INT REFERENCES Usuario(Id_usuario)
);

CREATE TABLE Instrutor (
    Id_instrutor INT PRIMARY KEY,
    CREF VARCHAR(30),
    Id_usuario INT REFERENCES Usuario(Id_usuario)
);

CREATE TABLE Academia (
    Id_academia INT PRIMARY KEY,
    Nome_academia VARCHAR(100),
    CNPJ VARCHAR(20),
    Email_institucional VARCHAR(100),
    Tipo_academia VARCHAR(50),
    CEP VARCHAR(15),
    Id_licenca INT,
    Id_gestor INT REFERENCES Gestor(Id_gestor)
);

CREATE TABLE Licenca (
    Id_licenca INT PRIMARY KEY,
    Status VARCHAR(20),
    Tipo VARCHAR(50),
    Data_fim DATETIME,
    Caminho_pdf VARCHAR(255),
    Data_assinatura DATETIME,
    Id_plano INT REFERENCES Plano_licenca(Id_plano),
    Mensalidade FLOAT
);

CREATE TABLE Contrato (
    Id_contrato INT PRIMARY KEY,
    Status VARCHAR(20),
    Data_fim DATETIME,
    Data_assinatura DATETIME,
    Id_plano INT REFERENCES Plano_contrato(Id_plano),
    Mensalidade FLOAT
);

CREATE TABLE Aluno (
    Id_aluno INT PRIMARY KEY,
    Objetivo INT,
    Id_usuario INT REFERENCES Usuario(Id_usuario),
    Id_contrato INT REFERENCES Contrato(Id_contrato),
    Id_academia INT REFERENCES Academia(Id_academia)
);

CREATE TABLE Funcionario (
    Id_funcionario INT PRIMARY KEY,
    Id_usuario INT REFERENCES Usuario(Id_usuario),
    Cargo VARCHAR(100)
);

CREATE TABLE Exercicios (
    Id_exercicios INT PRIMARY KEY,
    Numero_series INT,
    Nome VARCHAR(100),
    Numero_repeticoes INT,
    Aparelho VARCHAR(50),
    Musculo VARCHAR(50)
);

CREATE TABLE Agenda (
    Id_agenda INT PRIMARY KEY,
    Status VARCHAR(20),
    Data DATETIME
);

CREATE TABLE Log_atividade (
    Id_log INT PRIMARY KEY,
    Acao VARCHAR(100),
    Data_hora DATETIME
);

CREATE TABLE Xp_historico (
    Id_xp INT PRIMARY KEY,
    Motivo VARCHAR(255),
    Data DATETIME,
    Xp_ganho FLOAT
);

CREATE TABLE Avaliacao (
    Id_avaliacao INT PRIMARY KEY,
    Id_aluno INT REFERENCES Aluno(Id_aluno),
    Sexo VARCHAR(10),
    Data DATETIME,
    Peso FLOAT,
    Altura FLOAT,
    Idade INT,
    IMC FLOAT,
    Glicemia FLOAT,
    PA FLOAT,
    Densidade FLOAT,
    TR FLOAT,
    SE FLOAT,
    PT FLOAT,
    AX FLOAT,
    SI FLOAT,
    AB FLOAT,
    CX FLOAT,
    PTrrlh FLOAT,
    Umero FLOAT,
    Femur FLOAT,
    P_magro FLOAT,
    P_gordo FLOAT,
    P_viscera FLOAT,
    P_osseo FLOAT,
    Torax FLOAT,
    Abdomen FLOAT,
    Quadril FLOAT,
    Braco_D FLOAT,
    Braco_E FLOAT,
    Coxa_D FLOAT,
    Coxa_E FLOAT,
    Perna_D FLOAT,
    Perna_E FLOAT,
    Deltoide FLOAT,
    Id_instrutor INT REFERENCES Instrutor(Id_instrutor)
);

CREATE TABLE Ficha (
    Id_ficha INT PRIMARY KEY,
    Nome_ficha VARCHAR(100),
    Observacoes VARCHAR(255),
    Id_avaliacao INT REFERENCES Avaliacao(Id_avaliacao)
);

CREATE TABLE Treinos (
    Id_treinos INT PRIMARY KEY,
    Tipo VARCHAR(20),
    Nome_treino VARCHAR(100),
    Id_ficha INT REFERENCES Ficha(Id_ficha)
);

CREATE TABLE Treino_exercicio (
    Id_exercicios INT REFERENCES Exercicios(Id_exercicios),
    Id_treinos INT REFERENCES Treinos(Id_treinos)
);

CREATE TABLE Video (
    Id_video INT PRIMARY KEY,
    Data_upload DATETIME,
    Titulo VARCHAR(150),
    Caminho_arquivo VARCHAR(255),
    Id_academia INT REFERENCES Academia(Id_academia)
);

CREATE TABLE Telefone_usuario (
    Telefone VARCHAR(20),
    Id_usuario INT REFERENCES Usuario(Id_usuario)
);

CREATE TABLE Telefone_academia (
    Telefone VARCHAR(20),
    Id_academia INT REFERENCES Academia(Id_academia)
);

CREATE TABLE Frequencia (
    Id_frequencia INT PRIMARY KEY,
    Id_aluno INT REFERENCES Aluno(Id_aluno),
    Data DATE,
    Tempo_treino INT
);

CREATE TABLE Usuario_log (
    Id_usuario INT REFERENCES Usuario(Id_usuario),
    Id_log INT REFERENCES Log_atividade(Id_log)
);

CREATE TABLE Agenda_professor (
    Id_agenda INT REFERENCES Agenda(Id_agenda),
    Id_instrutor INT REFERENCES Instrutor(Id_instrutor)
);

CREATE TABLE Xp_aluno (
    Id_xp INT REFERENCES Xp_historico(Id_xp),
    Id_aluno INT REFERENCES Aluno(Id_aluno)
);

CREATE TABLE Ficha_medica (
    Id_ficha INT PRIMARY KEY,
    Id_aluno INT REFERENCES Aluno(Id_aluno),
    Alergia VARCHAR(100),
    Restricao VARCHAR(255),
    Lesao VARCHAR(255),
    Cirurgia VARCHAR(255),
    Problema_saude VARCHAR(255),
    Uso_medicamento VARCHAR(255)
);