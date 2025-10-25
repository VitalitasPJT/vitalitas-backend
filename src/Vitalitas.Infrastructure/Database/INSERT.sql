USE VITALITAS_DEV;
GO

-- =========================
-- Inserção de dados fictícios
-- =========================

-- Usuário
INSERT INTO usuario VALUES
(1,'Carlos Silva','12345678901','carlos@email.com','senha123','1990-01-10','Rua A','Bairro A','Cidade A','SP','01000-000','Q1','Aluno'),
(2,'Ana Souza','23456789012','ana@email.com','senha123','1992-03-15','Rua B','Bairro B','Cidade B','RJ','02000-000','Q2','Aluno'),
(3,'Pedro Lima','34567890123','pedro@email.com','senha123','1988-06-20','Rua C','Bairro C','Cidade C','MG','03000-000','Q3','Professor'),
(4,'Mariana Rocha','45678901234','mariana@email.com','senha123','1995-11-05','Rua D','Bairro D','Cidade D','BA','04000-000','Q4','Administrador'),
(5,'Lucas Martins','56789012345','lucas@email.com','senha123','1993-09-12','Rua E','Bairro E','Cidade E','RS','05000-000','Q5','Aluno');
GO

-- Usuario_telefone
INSERT INTO usuario_telefone VALUES
(1,1,'11999990001'),
(2,2,'21999990002'),
(3,3,'31999990003'),
(4,4,'41999990004'),
(5,5,'51999990005');
GO

-- Administrador
INSERT INTO administrador VALUES
(1,4);
GO

-- Academia
INSERT INTO academia VALUES
(1,'Academia Alpha','Musculação','12345678000199','alpha@email.com','Rua F','Bairro F','Cidade F','SP','06000-000','Q6'),
(2,'Academia Beta','Funcional','22345678000199','beta@email.com','Rua G','Bairro G','Cidade G','RJ','07000-000','Q7'),
(3,'Academia Gamma','Crossfit','32345678000199','gamma@email.com','Rua H','Bairro H','Cidade H','MG','08000-000','Q8'),
(4,'Academia Delta','Pilates','42345678000199','delta@email.com','Rua I','Bairro I','Cidade I','BA','09000-000','Q9'),
(5,'Academia Epsilon','Musculação','52345678000199','epsilon@email.com','Rua J','Bairro J','Cidade J','RS','10000-000','Q10');
GO

-- Academia_telefone
INSERT INTO academia_telefone VALUES
(1,1,'1133330001'),
(2,2,'2133330002'),
(3,3,'3133330003'),
(4,4,'4133330004'),
(5,5,'5133330005');
GO

-- Administracao
INSERT INTO administracao VALUES
(1,1),
(1,2),
(1,3),
(1,4),
(1,5);
GO

-- Aluno
INSERT INTO aluno VALUES
(1,1,'Perder peso'),
(2,2,'Ganho de massa'),
(3,5,'Melhora de resistência'),
(4,1,'Definição muscular'),
(5,2,'Saúde geral');
GO

-- Contrato
INSERT INTO contrato VALUES
(1,1,'Mensal','2025-01-01','Ativo'),
(2,2,'Trimestral','2025-02-01','Ativo'),
(3,3,'Semestral','2025-03-01','Inativo'),
(4,4,'Anual','2025-04-01','Ativo'),
(5,5,'Mensal','2025-05-01','Ativo');
GO

-- Mensalidade
INSERT INTO mensalidade VALUES
(1,1,150.00,'Paga','2025-02-01','2025-01-31','Janeiro'),
(2,2,450.00,'Paga','2025-05-01','2025-04-30','Fevereiro'),
(3,3,900.00,'Pendente','2025-09-01','2025-02-20','Março'),
(4,4,1800.00,'Paga','2026-04-01','2026-03-31','Abril'),
(5,5,150.00,'Paga','2025-06-01','2025-05-31','Maio');
GO

-- Professor
INSERT INTO professor VALUES
(1,3,'CREF12345'),
(2,3,'CREF23456'),
(3,3,'CREF34567'),
(4,3,'CREF45678'),
(5,3,'CREF56789');
GO

-- Agenda
INSERT INTO agenda VALUES
(1,1,'2025-10-25T08:00:00','Disponível'),
(2,2,'2025-10-25T09:00:00','Agendado'),
(3,3,'2025-10-25T10:00:00','Disponível'),
(4,4,'2025-10-25T11:00:00','Agendado'),
(5,5,'2025-10-25T12:00:00','Disponível');
GO

-- Avaliacao
INSERT INTO avaliacao VALUES
(1,1,1,'2025-10-20','08:00',70,'Masculino',1.75,30,90, '120/80',1.0,2.0,3.0,4.0,5.0,6.0,7.0,8.0,9.0,10.0,11.0,12.0,13.0,14.0,15.0,16.0,17.0,18.0,19.0,20.0,21.0,22.0,23.0,24.0,21.0,22.0,23.0,24.0),
(2,2,2,'2025-10-21','09:00',65,'Feminino',1.65,28,85, '110/70',1.1,2.1,3.1,4.1,5.1,6.1,7.1,8.1,9.1,10.1,11.1,12.1,13.1,14.1,15.1,16.1,17.1,18.1,19.1,20.1,21.1,22.1,23.1,24.1,21.0,22.0,23.0,24.0),
(3,3,3,'2025-10-22','10:00',80,'Masculino',1.80,32,95,'130/85',1.2,2.2,3.2,4.2,5.2,6.2,7.2,8.2,9.2,10.2,11.2,12.2,13.2,14.2,15.2,16.2,17.2,18.2,19.2,20.2,21.2,22.2,23.2,24.2,21.0,22.0,23.0,24.0),
(4,4,4,'2025-10-23','11:00',75,'Feminino',1.70,31,88,'115/75',1.3,2.3,3.3,4.3,5.3,6.3,7.3,8.3,9.3,10.3,11.3,12.3,13.3,14.3,15.3,16.3,17.3,18.3,19.3,20.3,21.3,22.3,23.3,24.3,21.0,22.0,23.0,24.0),
(5,5,5,'2025-10-24','12:00',68,'Masculino',1.78,29,92,'118/78',1.4,2.4,3.4,4.4,5.4,6.4,7.4,8.4,9.4,10.4,11.4,12.4,13.4,14.4,15.4,16.4,17.4,18.4,19.4,20.4,21.4,22.4,23.4,24.4,21.0,22.0,23.0,24.0);
GO

-- Ficha
INSERT INTO ficha VALUES
(1,'Ficha A','Observação A',1),
(2,'Ficha B','Observação B',2),
(3,'Ficha C','Observação C',3),
(4,'Ficha D','Observação D',4),
(5,'Ficha E','Observação E',5);
GO

-- Treino
INSERT INTO treino VALUES
(1,'Musculação','Treino A',1),
(2,'Funcional','Treino B',2),
(3,'Crossfit','Treino C',3),
(4,'Pilates','Treino D',4),
(5,'Musculação','Treino E',5);
GO

-- Exercicio
INSERT INTO exercicio VALUES
(1,'Supino',3,12,'Peito','Banco'),
(2,'Agachamento',4,10,'Perna','Barra'),
(3,'Rosca Bíceps',3,15,'Braço','Halteres'),
(4,'Leg Press',4,12,'Perna','Máquina'),
(5,'Abdominal',3,20,'Abdômen','Colchonete');
GO

-- Treino_exercicio
INSERT INTO treino_exercicio VALUES
(1,1),
(1,5),
(2,2),
(3,3),
(4,4),
(5,1);
GO