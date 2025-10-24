-- =========================
-- Criação do Banco de Dados
-- =========================
--CREATE DATABASE Vitalitas;
--GO

USE VITALITAS_DEV;
--GO

-- =========================
-- Script de Criação das Tabelas
-- =========================
DROP TABLE treino_exercicio;
DROP TABLE exercicio;
DROP TABLE treino;
DROP TABLE ficha;
DROP TABLE avaliacao;
DROP TABLE agenda;
DROP TABLE professor;
DROP TABLE mensalidade;
DROP TABLE contrato;
DROP TABLE aluno;
DROP TABLE administracao;
DROP TABLE academia_telefone;
DROP TABLE academia;
DROP TABLE administrador;
DROP TABLE usuario_telefone;
DROP TABLE usuario;
GO

CREATE TABLE [usuario] (
  [id_usuario] INT PRIMARY KEY,
  [nome] NVARCHAR(255),
  [cpf] NVARCHAR(255),
  [email] NVARCHAR(255),
  [senha] NVARCHAR(255),
  [data_nascimento] DATE,
  [rua] NVARCHAR(255),
  [bairro] NVARCHAR(255),
  [cidade] NVARCHAR(255),
  [estado] NVARCHAR(255),
  [cep] NVARCHAR(255),
  [quadra] NVARCHAR(255)
);
GO

CREATE TABLE [usuario_telefone] (
  [id_telefone] INT PRIMARY KEY,
  [id_usuario] INT,
  [telefone] NVARCHAR(255),
  FOREIGN KEY ([id_usuario]) REFERENCES [usuario]([id_usuario])
);
GO

CREATE TABLE [administrador] (
  [id_adm] INT PRIMARY KEY,
  [id_usuario] INT,
  FOREIGN KEY ([id_usuario]) REFERENCES [usuario]([id_usuario])
);
GO

CREATE TABLE [academia] (
  [id_academia] INT PRIMARY KEY,
  [nome_academia] NVARCHAR(255),
  [tipo_academia] NVARCHAR(255),
  [cnpj] NVARCHAR(255),
  [email_institucional] NVARCHAR(255),
  [rua] NVARCHAR(255),
  [bairro] NVARCHAR(255),
  [cidade] NVARCHAR(255),
  [estado] NVARCHAR(255),
  [cep] NVARCHAR(255),
  [quadra] NVARCHAR(255)
);
GO

CREATE TABLE [academia_telefone] (
  [id_telefone] INT PRIMARY KEY,
  [id_academia] INT,
  [telefone] NVARCHAR(255),
  FOREIGN KEY ([id_academia]) REFERENCES [academia]([id_academia])
);
GO

CREATE TABLE [administracao] (
  [id_adm] INT,
  [id_academia] INT,
  PRIMARY KEY (id_adm, id_academia),
  FOREIGN KEY ([id_adm]) REFERENCES [administrador]([id_adm]),
  FOREIGN KEY ([id_academia]) REFERENCES [academia]([id_academia])
);
GO

--Aqui precisa colocar os atributos no aluno: 
	--objetivo
	--professor_responsavel
	--sexo

CREATE TABLE [aluno] (
  [id_aluno] INT PRIMARY KEY,
  [id_usuario] INT,
  FOREIGN KEY ([id_usuario]) REFERENCES [usuario]([id_usuario])
);
GO

CREATE TABLE [contrato] (
  [id_assinatura] INT PRIMARY KEY,
  [id_aluno] INT,
  [tipo] NVARCHAR(255),
  [data_assinatura] DATE,
  [status] NVARCHAR(255),
  FOREIGN KEY ([id_aluno]) REFERENCES [aluno]([id_aluno])
);
GO

CREATE TABLE [mensalidade] (
  [id_mensalidade] INT PRIMARY KEY,
  [id_assinatura] INT UNIQUE,
  [valor] DECIMAL(10,2),
  [status] NVARCHAR(255),
  [data_vencimento] DATE,
  [data_pagamento] DATE,
  [referencia] NVARCHAR(255),
  FOREIGN KEY ([id_assinatura]) REFERENCES [contrato]([id_assinatura])
);
GO

CREATE TABLE [professor] (
  [id_professor] INT PRIMARY KEY,
  [id_usuario] INT,
  [cref] NVARCHAR(255),
  FOREIGN KEY ([id_usuario]) REFERENCES [usuario]([id_usuario])
);
GO

--Aqui precisa colocar os atributos no agenda: 
	--id_aluno
	--status
	--data
	-- *Retirar dia da semana e colcocar data do tipo dd/mm/aaaa

CREATE TABLE [agenda] (
  [id_agenda] INT PRIMARY KEY,
  [id_professor] INT,
  [hora] TIME,
  [dia_semana] NVARCHAR(255),
  [status] NVARCHAR(255),
  FOREIGN KEY ([id_professor]) REFERENCES [professor]([id_professor])
);
GO

CREATE TABLE [avaliacao] (
  [id_avaliacao] INT PRIMARY KEY,
  [id_professor] INT,
  [id_aluno] INT,
  [data] DATE,
  [hora] TIME,
  [peso] DECIMAL(10,2),
  [altura] DECIMAL(10,2),
  [idade] INT,
  [glicemia] DECIMAL(10,2),
  [pa] NVARCHAR(255),
  [densidade] DECIMAL(10,2),
  [ax] DECIMAL(10,2),
  [pt] DECIMAL(10,2),
  [se] DECIMAL(10,2),
  [tr] DECIMAL(10,2),
  [ab] DECIMAL(10,2),
  [si] DECIMAL(10,2),
  [ub] DECIMAL(10,2),
  [ur] DECIMAL(10,2),
  [rt] DECIMAL(10,2),
  [pr] DECIMAL(10,2),
  [px] DECIMAL(10,2),
  [femur] DECIMAL(10,2),
  [abdomen] DECIMAL(10,2),
  [torax] DECIMAL(10,2),
  [quadril] DECIMAL(10,2),
  [braco_d] DECIMAL(10,2),
  [braco_e] DECIMAL(10,2),
  [coxa_d] DECIMAL(10,2),
  [coxa_e] DECIMAL(10,2),
  [perna_d] DECIMAL(10,2),
  [perna_e] DECIMAL(10,2),
  [deltoide] DECIMAL(10,2),
  [peitoral] DECIMAL(10,2),
  [p_magro] DECIMAL(10,2),
  [p_gordo] DECIMAL(10,2),
  [p_osseo] DECIMAL(10,2),
  [p_viscera] DECIMAL(10,2),
  FOREIGN KEY ([id_professor]) REFERENCES [professor]([id_professor]),
  FOREIGN KEY ([id_aluno]) REFERENCES [aluno]([id_aluno])
);
GO

--Aqui precisa colocar os atributos na ficha: 
	--data de validade
	--nome da ficha*
	--observacoes

CREATE TABLE [ficha] (
  [id_ficha] INT PRIMARY KEY,
  [nome_treino] NVARCHAR(255),
  [id_avaliacao] INT,
  FOREIGN KEY ([id_avaliacao]) REFERENCES [avaliacao]([id_avaliacao])
);
GO

CREATE TABLE [treino] (
  [id_treino] INT PRIMARY KEY,
  [tipo] NVARCHAR(255),
  [id_ficha] INT UNIQUE NOT NULL,
  FOREIGN KEY ([id_ficha]) REFERENCES [ficha]([id_ficha])
);
GO

CREATE TABLE [exercicio] (
  [id_exercicio] INT PRIMARY KEY,
  [nome] NVARCHAR(255),
  [numero_serie] INT,
  [numero_repeticao] INT,
  [musculo] NVARCHAR(255),
  [aparelho] NVARCHAR(255)
);
GO

CREATE TABLE [treino_exercicio] (
  [id_treino] INT,
  [id_exercicio] INT,
  PRIMARY KEY (id_treino, id_exercicio),
  FOREIGN KEY ([id_treino]) REFERENCES [treino]([id_treino]),
  FOREIGN KEY ([id_exercicio]) REFERENCES [exercicio]([id_exercicio])
);
GO