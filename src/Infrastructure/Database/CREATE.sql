CREATE DATABASE VITALITAS_DEV;
GO
USE VITALITAS_DEV;
GO

-- Tabelas principais (Nível 0 - Sem FKs)
CREATE TABLE Usuário ( 
    ID_usuario INT PRIMARY KEY,
    Nome VARCHAR(100),  
    Email VARCHAR(100),  
    Endereço VARCHAR(255),  
    Senha VARCHAR(50),  
    Data_nascimento DATE,  
    CPF VARCHAR(20),  
    Tipo_usuario VARCHAR(20),  
    Flag INT
); 

CREATE TABLE Academia ( 
    Id_academia INT PRIMARY KEY,
    Nome_academia VARCHAR(100),  
    CNPJ VARCHAR(20),  
    Email_institucional VARCHAR(100),  
    Tipo_academia VARCHAR(50),  
    CEP VARCHAR(15)
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

CREATE TABLE Lembrete ( 
    Id_lembrete INT PRIMARY KEY,
    Ativo VARCHAR(10),  
    Horario DATETIME
); 

-- Tabelas de Perfil (Dependem de Usuário)
CREATE TABLE Aluno ( 
    Id_aluno INT PRIMARY KEY,  
    Objetivo INT,  
    Id_usuario INT REFERENCES Usuário(ID_usuario)
); 

CREATE TABLE Administrador ( 
    Id_adm INT PRIMARY KEY,  
    Id_usuario INT REFERENCES Usuário(ID_usuario)
); 

CREATE TABLE Professor ( 
    ID_professor INT PRIMARY KEY,  
    CREF VARCHAR(30),  
    Id_usuario INT REFERENCES Usuário(ID_usuario)
); 

CREATE TABLE Funcionario ( 
    Id_funcionario INT PRIMARY KEY,  
    Id_usuario INT REFERENCES Usuário(ID_usuario)
); 

-- Tabelas Dependentes (Contratos, Licenças e Avaliações)
CREATE TABLE Licenca ( 
    Id_licenca INT PRIMARY KEY,
    Status VARCHAR(20),  
    Tipo VARCHAR(50),  
    Data_fim DATETIME,  
    Caminho_pdf VARCHAR(255),  
    Data_assinatura DATETIME,  
    Id_plano INT REFERENCES Plano_licenca(Id_plano)
); 

CREATE TABLE Contrato ( 
    Id_contrato INT PRIMARY KEY,  
    Status VARCHAR(20),  
    Data_fim DATETIME,  
    Data_assinatura DATETIME,  
    Id_plano INT REFERENCES Plano_contrato(Id_plano)
); 

CREATE TABLE Avaliacao ( 
    Id_avaliacao INT PRIMARY KEY,
    Id_professor INT REFERENCES Professor(ID_professor),  
    Id_aluno INT REFERENCES Aluno(Id_aluno),  
    Sexo VARCHAR(10),  
    Data DATETIME,  
    Peso FLOAT, Altura FLOAT, Idade INT, IMC FLOAT, Glicemia FLOAT, PA FLOAT,
    Densidade FLOAT, TR FLOAT, SE FLOAT, PT FLOAT, AX FLOAT, SI FLOAT, AB FLOAT,
    CX FLOAT, PTrrlh FLOAT, Umero FLOAT, Femur FLOAT, P_magro FLOAT, P_gordo FLOAT,
    P_viscera FLOAT, P_osseo FLOAT, Torax FLOAT, Abdomen FLOAT, Quadril FLOAT,
    Braco_D FLOAT, Braco_E FLOAT, Coxa_D FLOAT, Coxa_E FLOAT, Perna_D FLOAT,
    Perna_E FLOAT, Deltoide FLOAT
); 

-- Tabelas de Fichas e Exercícios
CREATE TABLE Ficha ( 
    Id_ficha INT PRIMARY KEY,  
    Nome_ficha VARCHAR(100),  
    Observacoes VARCHAR(255),  
    Id_avaliacao INT REFERENCES Avaliacao(Id_avaliacao)
); 

CREATE TABLE Treinos ( 
    Id_treinos INT PRIMARY KEY,  
    Tipo VARCHAR(10),  
    Nome_treino VARCHAR(100),  
    Id_ficha INT REFERENCES Ficha(Id_ficha)
); 

-- Tabelas de Relacionamento e Atributos
CREATE TABLE Video ( 
    Id_video INT PRIMARY KEY,
    Data_upload DATETIME,  
    Titulo VARCHAR(150),  
    Caminho_arquivo VARCHAR(255),  
    Id_academia INT REFERENCES Academia(Id_academia)
); 

CREATE TABLE Telefone_usuario ( 
    Telefone VARCHAR(20),  
    ID_usuario INT REFERENCES Usuário(ID_usuario)
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

CREATE TABLE Aluno_lembrete ( 
    Id_aluno_lembrete INT PRIMARY KEY,
    Id_lembrete INT REFERENCES Lembrete(Id_lembrete),  
    Id_aluno INT REFERENCES Aluno(Id_aluno),  
    Horario DATETIME,  
    Ativo VARCHAR(10),  
    Descricao VARCHAR(255)
); 

CREATE TABLE Treino_exercicio ( 
    Id_exercicios INT REFERENCES Exercicios(Id_exercicios),  
    Id_treinos INT REFERENCES Treinos(Id_treinos)
); 

-- Outras tabelas de ligação (Reduzido para brevidade, mas com VARCHAR corrigido)
CREATE TABLE Academia_licenca ( 
    Id_mensalidade INT PRIMARY KEY,
    Id_licenca INT REFERENCES Licenca(Id_licenca),
    Id_academia INT REFERENCES Academia(Id_academia),
    Status VARCHAR(20), Valor FLOAT, Data_pagamento DATETIME, Data_vencimento DATETIME
);