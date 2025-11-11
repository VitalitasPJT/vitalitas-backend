USE VITALITAS_DEV;
GO

-- =========================
-- Inserção de dados fictícios
-- =========================

-- Usuário (12 usuários: 7 Alunos, 3 Professores, 2 Administradores)
INSERT INTO usuario (id_usuario, nome, cpf, email, senha, data_nascimento, rua, bairro, cidade, estado, cep, quadra, tipo_usuario) VALUES
(1,'Carlos Silva','12345678901','carlos@email.com','senha123','1990-01-10','Rua A','Bairro A','Cidade A','SP','01000-000','Q1','Aluno'),
(2,'Ana Souza','23456789012','ana@email.com','senha123','1992-03-15','Rua B','Bairro B','Cidade B','RJ','02000-000','Q2','Aluno'),
(3,'Pedro Lima','34567890123','pedro@email.com','senha123','1988-06-20','Rua C','Bairro C','Cidade C','MG','03000-000','Q3','Professor'),
(4,'Mariana Rocha','45678901234','mariana@email.com','senha123','1995-11-05','Rua D','Bairro D','Cidade D','BA','04000-000','Q4','Administrador'),
(5,'Lucas Martins','56789012345','lucas@email.com','senha123','1993-09-12','Rua E','Bairro E','Cidade E','RS','05000-000','Q5','Aluno'),
(6,'Fernanda Costa','67890123456','fernanda@email.com','senha123','1998-02-25','Rua K','Bairro K','Cidade A','SP','01100-000','Q6','Aluno'),
(7,'Ricardo Alves','78901234567','ricardo@email.com','senha123','2000-07-30','Rua L','Bairro L','Cidade B','RJ','02200-000','Q7','Aluno'),
(8,'Beatriz Santos','89012345678','beatriz@email.com','senha123','1991-04-18','Rua M','Bairro M','Cidade C','MG','03300-000','Q8','Professor'),
(9,'Tiago Pereira','90123456789','tiago@email.com','senha123','1997-12-01','Rua N','Bairro N','Cidade D','BA','04400-000','Q9','Aluno'),
(10,'Julia Ferreira','01234567890','julia@email.com','senha123','1985-08-14','Rua O','Bairro O','Cidade E','RS','05500-000','Q10','Administrador'),
(11,'Roberto Dias','11223344556','roberto@email.com','senha123','1994-05-22','Rua P','Bairro P','Cidade A','SP','01200-000','Q11','Aluno'),
(12,'Sandra Nunes','22334455667','sandra@email.com','senha123','1989-10-03','Rua Q','Bairro Q','Cidade B','RJ','02300-000','Q12','Professor');
GO

-- Usuario_telefone
INSERT INTO usuario_telefone (id_telefone, id_usuario, telefone) VALUES
(1,1,'11999990001'),
(2,2,'21999990002'),
(3,3,'31999990003'),
(4,4,'41999990004'),
(5,5,'51999990005'),
(6,6,'11999990006'),
(7,7,'21999990007'),
(8,8,'31999990008'),
(9,9,'41999990009'),
(10,10,'51999990010'),
(11,11,'11999990011'),
(12,12,'21999990012'),
(13,1,'11988880001'); -- Telefone adicional para Carlos
GO

-- Administrador (Mapeia id_usuario para um id_adm)
INSERT INTO administrador (id_adm, id_usuario) VALUES
(1,4),  -- Mariana Rocha
(2,10); -- Julia Ferreira
GO

-- Aluno (Mapeia id_usuario para um id_aluno)
INSERT INTO aluno (id_aluno, id_usuario, objetivo) VALUES
(1,1,'Perder peso'),
(2,2,'Ganho de massa'),
(3,5,'Melhora de resistência'),
(4,6,'Definição muscular'),
(5,7,'Saúde geral'),
(6,9,'Condicionamento'),
(7,11,'Hipertrofia');
GO

-- Professor (Mapeia id_usuario para um id_professor)
INSERT INTO professor (id_professor, id_usuario, cref) VALUES
(1,3,'CREF12345'),  -- Pedro Lima
(2,8,'CREF67890'),  -- Beatriz Santos
(3,12,'CREF11223'); -- Sandra Nunes
GO

-- Academia (5 academias)
INSERT INTO academia (id_academia, nome_academia, tipo_academia, cnpj, email_institucional, rua, bairro, cidade, estado, cep, quadra) VALUES
(1,'Academia Alpha','Musculação','12345678000199','alpha@email.com','Rua F','Bairro F','Cidade F','SP','06000-000','Q6'),
(2,'Academia Beta','Funcional','22345678000199','beta@email.com','Rua G','Bairro G','Cidade G','RJ','07000-000','Q7'),
(3,'Academia Gamma','Crossfit','32345678000199','gamma@email.com','Rua H','Bairro H','Cidade H','MG','08000-000','Q8'),
(4,'Academia Delta','Pilates','42345678000199','delta@email.com','Rua I','Bairro I','Cidade I','BA','09000-000','Q9'),
(5,'Academia Epsilon','Musculação','52345678000199','epsilon@email.com','Rua J','Bairro J','Cidade J','RS','10000-000','Q10');
GO

-- Academia_telefone
INSERT INTO academia_telefone (id_telefone, id_academia, telefone) VALUES
(1,1,'1133330001'),
(2,2,'2133330002'),
(3,3,'3133330003'),
(4,4,'4133330004'),
(5,5,'5133330005'),
(6,1,'1133331001'), -- Telefone adicional
(7,3,'3133331003'); -- Telefone adicional
GO

-- Administracao (Quem administra qual academia)
INSERT INTO administracao (id_adm, id_academia) VALUES
(1,1), -- Admin 1 (Mariana) cuida da Academia 1
(1,2), -- Admin 1 (Mariana) cuida da Academia 2
(2,3), -- Admin 2 (Julia) cuida da Academia 3
(2,4), -- Admin 2 (Julia) cuida da Academia 4
(2,5); -- Admin 2 (Julia) cuida da Academia 5
GO

-- Contrato (Referencia id_aluno da tabela aluno)
INSERT INTO contrato (id_assinatura, id_aluno, tipo, data_assinatura, status) VALUES
(1,1,'Mensal','2025-01-01','Ativo'),      -- Aluno 1 (Carlos)
(2,2,'Trimestral','2025-02-01','Ativo'),  -- Aluno 2 (Ana)
(3,3,'Semestral','2025-03-01','Inativo'), -- Aluno 3 (Lucas)
(4,4,'Anual','2025-04-01','Ativo'),       -- Aluno 4 (Fernanda)
(5,5,'Mensal','2025-05-01','Ativo'),      -- Aluno 5 (Ricardo)
(6,6,'Mensal','2025-06-01','Ativo'),      -- Aluno 6 (Tiago)
(7,7,'Anual','2025-07-01','Ativo'),       -- Aluno 7 (Roberto)
(8,1,'Trimestral','2025-08-01','Ativo');  -- Aluno 1 (Carlos) tem um segundo contrato
GO

-- Mensalidade (Referencia id_assinatura da tabela contrato)
INSERT INTO mensalidade (id_mensalidade, id_assinatura, valor, status, data_vencimento, data_pagamento, referencia) VALUES
(1,1,150.00,'Paga','2025-02-01','2025-01-31','Janeiro'),
(2,2,450.00,'Paga','2025-05-01','2025-04-30','Fevereiro'),
(3,3,900.00,'Pendente','2025-09-01',NULL,'Março'),
(4,4,1800.00,'Paga','2026-04-01','2025-03-31','Abril'),
(5,5,150.00,'Paga','2025-06-01','2025-05-31','Maio'),
(6,6,160.00,'Paga','2025-07-01','2025-06-30','Junho'),
(7,7,1900.00,'Pendente','2026-07-01',NULL,'Julho'),
(8,8,480.00,'Pendente','2025-11-01',NULL,'Agosto');
GO

-- Agenda (Referencia id_professor da tabela professor)
INSERT INTO agenda (id_agenda, id_professor, data, status) VALUES
(1,1,'2025-10-25T08:00:00','Disponível'), -- Prof 1 (Pedro)
(2,2,'2025-10-25T09:00:00','Agendado'),   -- Prof 2 (Beatriz)
(3,3,'2025-10-25T10:00:00','Disponível'), -- Prof 3 (Sandra)
(4,1,'2025-10-25T11:00:00','Agendado'),   -- Prof 1 (Pedro)
(5,2,'2025-10-25T12:00:00','Disponível'), -- Prof 2 (Beatriz)
(6,3,'2025-10-26T08:00:00','Disponível'), -- Prof 3 (Sandra)
(7,1,'2025-10-26T09:00:00','Disponível'), -- Prof 1 (Pedro)
(8,2,'2025-10-26T10:00:00','Agendado');   -- Prof 2 (Beatriz)
GO

-- Avaliacao (Referencia id_aluno e id_professor)
INSERT INTO avaliacao (id_avaliacao, id_professor, id_aluno, data, hora, peso, sexo, altura, idade, glicemia, pa, densidade, ax, pt, se, tr, ab, si, ub, ur, rt, pr, px, femur, abdomen, torax, quadril, braco_d, braco_e, coxa_d, coxa_e, perna_d, perna_e, deltoide, peitoral, p_magro, p_gordo, p_osseo, p_viscera) VALUES
(1,1,1,'2025-10-20','08:00',70.0,'Masculino',1.75,30,90.5, '120/80',1.05,2.0,3.0,4.0,5.0,6.0,7.0,8.0,9.0,10.0,11.0,12.0,13.0,14.0,15.0,16.0,17.0,18.0,19.0,20.0,21.0,22.0,23.0,24.0,21.0,22.0,23.0,24.0),
(2,2,2,'2025-10-21','09:00',65.0,'Feminino',1.65,28,85.0, '110/70',1.02,2.1,3.1,4.1,5.1,6.1,7.1,8.1,9.1,10.1,11.1,12.1,13.1,14.1,15.1,16.1,17.1,18.1,19.1,20.1,21.1,22.1,23.1,24.1,21.1,22.1,23.1,24.1),
(3,3,3,'2025-10-22','10:00',80.0,'Masculino',1.80,32,95.0,'130/85',1.06,2.2,3.2,4.2,5.2,6.2,7.2,8.2,9.2,10.2,11.2,12.2,13.2,14.2,15.2,16.2,17.2,18.2,19.2,20.2,21.2,22.2,23.2,24.2,21.2,22.2,23.2,24.2),
(4,1,4,'2025-10-23','11:00',75.0,'Feminino',1.70,31,88.0,'115/75',1.04,2.3,3.3,4.3,5.3,6.3,7.3,8.3,9.3,10.3,11.3,12.3,13.3,14.3,15.3,16.3,17.3,18.3,19.3,20.3,21.3,22.3,23.3,24.3,21.3,22.3,23.3,24.3),
(5,2,5,'2025-10-24','12:00',68.0,'Masculino',1.78,29,92.0,'118/78',1.03,2.4,3.4,4.4,5.4,6.4,7.4,8.4,9.4,10.4,11.4,12.4,13.4,14.4,15.4,16.4,17.4,18.4,19.4,20.4,21.4,22.4,23.4,24.4,21.4,22.4,23.4,24.4),
(6,3,6,'2025-10-25','14:00',82.0,'Masculino',1.85,27,80.0,'122/82',1.07,2.5,3.5,4.5,5.5,6.5,7.5,8.5,9.5,10.5,11.5,12.5,13.5,14.5,15.5,16.5,17.5,18.5,19.5,20.5,21.5,22.5,23.5,24.5,21.5,22.5,23.5,24.5),
(7,1,7,'2025-10-25','15:00',63.0,'Feminino',1.60,24,75.0,'108/68',1.01,2.6,3.6,4.6,5.6,6.6,7.6,8.6,9.6,10.6,11.6,12.6,13.6,14.6,15.6,16.6,17.6,18.6,19.6,20.6,21.6,22.6,23.6,24.6,21.6,22.6,23.6,24.6);
GO

-- Ficha (Referencia id_avaliacao)
INSERT INTO ficha (id_ficha, nome_ficha, observacoes, id_avaliacao) VALUES
(1,'Ficha A','Observação A',1), -- Ficha da Avaliação 1 (Aluno 1)
(2,'Ficha B','Observação B',2), -- Ficha da Avaliação 2 (Aluno 2)
(3,'Ficha C','Observação C',3), -- Ficha da Avaliação 3 (Aluno 3)
(4,'Ficha D','Observação D',4), -- Ficha da Avaliação 4 (Aluno 4)
(5,'Ficha E','Observação E',5), -- Ficha da Avaliação 5 (Aluno 5)
(6,'Ficha F','Observação F',6), -- Ficha da Avaliação 6 (Aluno 6)
(7,'Ficha G','Observação G',7); -- Ficha da Avaliação 7 (Aluno 7)
GO

-- Treino (Referencia id_ficha)
-- *** CORRIGIDO: Removido o id_treino = 8, que duplicava o id_ficha = 1 ***
INSERT INTO treino (id_treino, tipo, nome_treino, id_ficha) VALUES
(1,'Musculação','Treino A - Peito/Tríceps',1),
(2,'Funcional','Treino B - Full Body',2),
(3,'Crossfit','Treino C - WOD',3),
(4,'Pilates','Treino D - Solo',4),
(5,'Musculação','Treino E - Perna',5),
(6,'Aeróbico','Treino F - Condicionamento',6),
(7,'Resistência','Treino G - Hipertrofia',7);
GO

-- Exercicio (10 exercícios)
INSERT INTO exercicio (id_exercicio, nome, numero_serie, numero_repeticao, musculo, aparelho) VALUES
(1,'Supino',3,12,'Peito','Banco'),
(2,'Agachamento',4,10,'Perna','Barra'),
(3,'Rosca Bíceps',3,15,'Braço','Halteres'),
(4,'Leg Press',4,12,'Perna','Máquina'),
(5,'Abdominal',3,20,'Abdômen','Colchonete'),
(6,'Puxada Frontal',3,12,'Costas','Máquina'),
(7,'Elevação Lateral',4,15,'Ombro','Halteres'),
(8,'Cadeira Extensora',4,10,'Perna','Máquina'),
(9,'Rosca Martelo',3,12,'Braço','Halteres'),
(10,'Prancha',3,60,'Abdômen','Colchonete'); -- Repetição 60 = 60 segundos
GO

-- Treino_exercicio (Linka treinos e exercícios)
-- *** CORRIGIDO: Removidas as referências ao id_treino = 8, que não existe mais ***
INSERT INTO treino_exercicio (id_treino, id_exercicio) VALUES
(1,1), -- Treino 1 (Peito/Tríceps) -> Supino
(1,7), -- Treino 1 (Peito/Tríceps) -> Elevação Lateral (Ombro)
(2,2), -- Treino 2 (Funcional) -> Agachamento
(2,5), -- Treino 2 (Funcional) -> Abdominal
(2,10),-- Treino 2 (Funcional) -> Prancha
(3,3), -- Treino 3 (Crossfit) -> Rosca Bíceps
(3,6), -- Treino 3 (Crossfit) -> Puxada Frontal
(4,10),-- Treino 4 (Pilates) -> Prancha
(5,2), -- Treino 5 (Perna) -> Agachamento
(5,4), -- Treino 5 (Perna) -> Leg Press
(5,8), -- Treino 5 (Perna) -> Cadeira Extensora
(6,5), -- Treino 6 (Aeróbico) -> Abdominal
(6,10),-- Treino 6 (Aeróbico) -> Prancha
(7,2), -- Treino 7 (Resistência) -> Agachamento
(7,4); -- Treino 7 (Resistência) -> Leg Press
GO
