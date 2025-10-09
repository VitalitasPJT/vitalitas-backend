-- =========================
-- Script de Criação do Banco de Dados
-- =========================

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
  [id_usuario] INT PRIMARY KEY,
  [telefone] NVARCHAR(255)
);
GO

CREATE TABLE [administrador] (
  [id_adm] INT PRIMARY KEY,
  [id_usuario] INT
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
  [id_academia] INT PRIMARY KEY,
  [telefone] NVARCHAR(255)
);
GO

CREATE TABLE [administra] (
  [id_adm] INT,
  [id_academia] INT,
  PRIMARY KEY (id_adm, id_academia)
);
GO

CREATE TABLE [aluno] (
  [id_aluno] INT PRIMARY KEY,
  [id_usuario] INT
);
GO

CREATE TABLE [contrato] (
  [id_assinatura] INT PRIMARY KEY,
  [id_aluno] INT,
  [tipo] NVARCHAR(255),
  [data_assinatura] DATE,
  [status] NVARCHAR(255)
);
GO

CREATE TABLE [mensalidade] (
  [id_mensalidade] INT PRIMARY KEY,
  [id_assinatura] INT UNIQUE,
  [valor] DECIMAL(10,2),
  [status] NVARCHAR(255),
  [data_vencimento] DATE,
  [data_pagamento] DATE,
  [referencia] NVARCHAR(255)
);
GO

CREATE TABLE [professor] (
  [id_professor] INT PRIMARY KEY,
  [id_usuario] INT,
  [cref] NVARCHAR(255)
);
GO

CREATE TABLE [agenda] (
  [id_agenda] INT PRIMARY KEY,
  [id_professor] INT,
  [hora] TIME,
  [dia_semana] NVARCHAR(255),
  [status] NVARCHAR(255)
);
GO

CREATE TABLE [avalia] (
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
  [p_viscera] DECIMAL(10,2)
);
GO

CREATE TABLE [ficha] (
  [id_ficha] INT PRIMARY KEY,
  [nome_treino] NVARCHAR(255),
  [id_avaliacao] INT
);
GO

CREATE TABLE [treinos] (
  [id_treinos] INT PRIMARY KEY,
  [tipo] NVARCHAR(255),
  [id_ficha] INT UNIQUE NOT NULL
);
GO

CREATE TABLE [exercicios] (
  [id_exercicios] INT PRIMARY KEY,
  [nome] NVARCHAR(255),
  [numero_series] INT,
  [numero_repeticoes] INT,
  [musculo] NVARCHAR(255),
  [aparelho] NVARCHAR(255)
);
GO

CREATE TABLE [treinos_exercicios] (
  [id_treinos] INT,
  [id_exercicios] INT,
  PRIMARY KEY (id_treinos, id_exercicios)
);
GO

-- =========================
-- FOREIGN KEYS
-- =========================
ALTER TABLE [usuario_telefone] ADD FOREIGN KEY ([id_usuario]) REFERENCES [usuario] ([id_usuario]);
ALTER TABLE [administrador] ADD FOREIGN KEY ([id_usuario]) REFERENCES [usuario] ([id_usuario]);
ALTER TABLE [academia_telefone] ADD FOREIGN KEY ([id_academia]) REFERENCES [academia] ([id_academia]);
ALTER TABLE [administra] ADD FOREIGN KEY ([id_adm]) REFERENCES [administrador] ([id_adm]);
ALTER TABLE [administra] ADD FOREIGN KEY ([id_academia]) REFERENCES [academia] ([id_academia]);
ALTER TABLE [aluno] ADD FOREIGN KEY ([id_usuario]) REFERENCES [usuario] ([id_usuario]);
ALTER TABLE [contrato] ADD FOREIGN KEY ([id_aluno]) REFERENCES [aluno] ([id_aluno]);
ALTER TABLE [mensalidade] ADD FOREIGN KEY ([id_assinatura]) REFERENCES [contrato] ([id_assinatura]);
ALTER TABLE [professor] ADD FOREIGN KEY ([id_usuario]) REFERENCES [usuario] ([id_usuario]);
ALTER TABLE [agenda] ADD FOREIGN KEY ([id_professor]) REFERENCES [professor] ([id_professor]);
ALTER TABLE [avalia] ADD FOREIGN KEY ([id_professor]) REFERENCES [professor] ([id_professor]);
ALTER TABLE [avalia] ADD FOREIGN KEY ([id_aluno]) REFERENCES [aluno] ([id_aluno]);
ALTER TABLE [ficha] ADD FOREIGN KEY ([id_avaliacao]) REFERENCES [avalia] ([id_avaliacao]);
ALTER TABLE [treinos] ADD FOREIGN KEY ([id_ficha]) REFERENCES [ficha] ([id_ficha]);
ALTER TABLE [treinos_exercicios] ADD FOREIGN KEY ([id_treinos]) REFERENCES [treinos] ([id_treinos]);
ALTER TABLE [treinos_exercicios] ADD FOREIGN KEY ([id_exercicios]) REFERENCES [exercicios] ([id_exercicios]);
GO
