USE VITALITAS_DEV;
GO

INSERT INTO Usuário (ID_usuario, Nome, Email, Endereço, Senha, Data_nascimento, CPF, Tipo_usuario, Flag) VALUES
(1, 'João Silva', 'joao@email.com', 'Rua das Flores, 123', 'senha123', '1990-05-15', '111.111.111-11', 'Aluno', 1),
(2, 'Maria Santos', 'maria@email.com', 'Av. Central, 456', 'senha456', '1992-08-22', '222.222.222-22', 'Aluno', 1),
(3, 'Ricardo Oliveira', 'ricardo@email.com', 'Rua B, 78', 'prof123', '1985-03-10', '333.333.333-33', 'Professor', 1),
(4, 'Ana Costa', 'ana@email.com', 'Rua C, 90', 'senha789', '1995-12-01', '444.444.444-44', 'Aluno', 1),
(5, 'Carlos Eduardo', 'carlos@email.com', 'Av. Paulista, 1000', 'adm001', '1980-01-20', '555.555.555-55', 'Admin', 1),
(6, 'Fernanda Lima', 'fer@email.com', 'Rua D, 12', 'senha000', '1998-07-11', '666.666.666-66', 'Aluno', 1),
(7, 'Gabriel Souza', 'gabriel@email.com', 'Rua E, 34', 'prof456', '1988-11-25', '777.777.777-77', 'Professor', 1),
(8, 'Juliana Paes', 'ju@email.com', 'Av. Brasil, 500', 'senha555', '1993-02-14', '888.888.888-88', 'Aluno', 1),
(9, 'Roberto Carlos', 'beto@email.com', 'Rua F, 56', 'senha222', '1991-09-05', '999.999.999-99', 'Aluno', 1),
(10, 'Sandra Mello', 'sandra@email.com', 'Rua G, 78', 'senha333', '1994-10-10', '000.000.000-00', 'Aluno', 1);

INSERT INTO Academia (Id_academia, Nome_academia, CNPJ, Email_institucional, Tipo_academia, CEP) VALUES
(1, 'Vitalitas Matriz', '12.345.678/0001-01', 'matriz@vitalitas.com', 'Musculação', '01001-000'),
(2, 'Vitalitas Sul', '12.345.678/0001-02', 'sul@vitalitas.com', 'Crossfit', '04002-000'),
(3, 'Vitalitas Norte', '12.345.678/0001-03', 'norte@vitalitas.com', 'Natação', '02003-000'),
(4, 'Vitalitas Leste', '12.345.678/0001-04', 'leste@vitalitas.com', 'Artes Marciais', '03004-000'),
(5, 'Vitalitas Oeste', '12.345.678/0001-05', 'oeste@vitalitas.com', 'Funcional', '05005-000'),
(6, 'Vitalitas Fit', '12.345.678/0001-06', 'fit@vitalitas.com', 'Musculação', '06006-000'),
(7, 'Vitalitas Prime', '12.345.678/0001-07', 'prime@vitalitas.com', 'Premium', '07007-000'),
(8, 'Vitalitas Beach', '12.345.678/0001-08', 'beach@vitalitas.com', 'Outdoor', '08008-000'),
(9, 'Vitalitas Yoga', '12.345.678/0001-09', 'yoga@vitalitas.com', 'Yoga', '09009-000'),
(10, 'Vitalitas Express', '12.345.678/0001-10', 'express@vitalitas.com', '24 Horas', '10010-000');

INSERT INTO Plano_licenca (Id_plano, Nome, Descricao, Valor) VALUES
(1, 'Bronze', 'Acesso básico', 500.00), (2, 'Silver', 'Acesso intermediário', 800.00),
(3, 'Gold', 'Acesso completo', 1200.00), (4, 'Platinum', 'Rede nacional', 2000.00),
(5, 'Diamond', 'VIP Experience', 5000.00), (6, 'Anual Basic', 'Pagamento único', 4500.00),
(7, 'Semestral', '6 meses de uso', 2500.00), (8, 'Trimestral', '3 meses de uso', 1400.00),
(9, 'Trial', '7 dias teste', 0.00), (10, 'Promocional', 'Black Friday', 350.00);

INSERT INTO Plano_contrato (Id_plano, Nome, Descricao, Valor) VALUES
(1, 'Mensal', 'Cobrança todo mês', 100.00), (2, 'Trimestral', 'Plano 90 dias', 270.00),
(3, 'Semestral', 'Plano 180 dias', 500.00), (4, 'Anual', 'Plano 365 dias', 900.00),
(5, 'Recorrente', 'No cartão', 85.00), (6, 'Fidélidade', '2 anos contrato', 70.00),
(7, 'Estudante', 'Desconto 20%', 80.00), (8, 'Corporativo', 'Grupos 5+', 75.00),
(9, 'Master', 'Acesso livre 24h', 150.00), (10, 'Social', 'Baixa renda', 50.00);

INSERT INTO Exercicios (Id_exercicios, Numero_series, Nome, Numero_repeticoes, Aparelho, Musculo) VALUES
(1, 3, 'Supino Reto', 12, 'Banco Supino', 'Peitoral'), (2, 4, 'Agachamento', 10, 'Gaiola', 'Pernas'),
(3, 3, 'Rosca Direta', 12, 'Barra W', 'Bíceps'), (4, 3, 'Puxada Frente', 15, 'Pulley', 'Costas'),
(5, 4, 'Leg Press', 12, 'Máquina Leg', 'Pernas'), (6, 3, 'Elevação Lateral', 15, 'Halteres', 'Ombros'),
(7, 3, 'Tríceps Corda', 12, 'Polia', 'Tríceps'), (8, 4, 'Levantamento Terra', 8, 'Barra', 'Lombar'),
(9, 3, 'Extensora', 15, 'Cadeira', 'Pernas'), (10, 3, 'Flexora', 15, 'Mesa', 'Pernas');

INSERT INTO Agenda (Id_agenda, Status, Data) VALUES
(1, 'Agendado', '2024-05-01 08:00:00'), (2, 'Concluído', '2024-05-01 09:00:00'),
(3, 'Cancelado', '2024-05-01 10:00:00'), (4, 'Agendado', '2024-05-02 14:00:00'),
(5, 'Agendado', '2024-05-02 15:00:00'), (6, 'Agendado', '2024-05-03 07:00:00'),
(7, 'Pendente', '2024-05-03 08:00:00'), (8, 'Concluído', '2024-05-03 10:30:00'),
(9, 'Agendado', '2024-05-04 16:00:00'), (10, 'Agendado', '2024-05-04 17:00:00');

INSERT INTO Log_atividade (Id_log, Acao, Data_hora) VALUES
(1, 'Login', GETDATE()), (2, 'Logout', GETDATE()), (3, 'Cadastro Aluno', GETDATE()),
(4, 'Edição Ficha', GETDATE()), (5, 'Exclusão Treino', GETDATE()), (6, 'Visualização Avaliação', GETDATE()),
(7, 'Update Perfil', GETDATE()), (8, 'Troca Senha', GETDATE()), (9, 'Nova Matrícula', GETDATE()), (10, 'Inativar Usuário', GETDATE());

INSERT INTO Xp_historico (Id_xp, Motivo, Data, Xp_ganho) VALUES
(1, 'Treino Completo', GETDATE(), 10.0), (2, 'Frequência Semanal', GETDATE(), 50.0),
(3, 'Novo Recorde Supino', GETDATE(), 20.0), (4, 'Indicação Amigo', GETDATE(), 100.0),
(5, 'Avaliação Física', GETDATE(), 30.0), (6, 'Aniversário', GETDATE(), 200.0),
(7, 'Frequência Mensal', GETDATE(), 500.0), (8, 'Update Foto', GETDATE(), 5.0),
(9, 'Compartilhamento', GETDATE(), 15.0), (10, 'Desafio 30 dias', GETDATE(), 1000.0);

INSERT INTO Lembrete (Id_lembrete, Ativo, Horario) VALUES
(1, 'Sim', '2024-01-01 08:00:00'), (2, 'Não', '2024-01-01 12:00:00'),
(3, 'Sim', '2024-01-01 18:00:00'), (4, 'Sim', '2024-01-02 07:00:00'),
(5, 'Não', '2024-01-02 21:00:00'), (6, 'Sim', '2024-01-03 06:00:00'),
(7, 'Sim', '2024-01-03 15:00:00'), (8, 'Sim', '2024-01-04 09:00:00'),
(9, 'Não', '2024-01-05 10:00:00'), (10, 'Sim', '2024-01-05 20:00:00');

INSERT INTO Aluno (Id_aluno, Objetivo, Id_usuario) VALUES 
(1, 1, 1), (2, 2, 2), (3, 1, 4), (4, 3, 6), (5, 2, 8), (6, 1, 9), (7, 3, 10), (8, 1, 5), (9, 2, 1), (10, 1, 2);

INSERT INTO Professor (ID_professor, CREF, Id_usuario) VALUES 
(1, '123456-G/SP', 3), (2, '654321-G/RJ', 7), (3, '999999-G/MG', 1), (4, '888888-G/SC', 2),
(5, '777777-G/PR', 4), (6, '666666-G/RS', 6), (7, '555555-G/MT', 8), (8, '444444-G/BA', 9),
(9, '333333-G/PE', 10), (10, '222222-G/CE', 5);

INSERT INTO Administrador (Id_adm, Id_usuario) VALUES (1, 5), (2, 1), (3, 2), (4, 3), (5, 4), (6, 6), (7, 7), (8, 8), (9, 9), (10, 10);

INSERT INTO Funcionario (Id_funcionario, Id_usuario) VALUES (1, 1), (2, 2), (3, 3), (4, 4), (5, 5), (6, 6), (7, 7), (8, 8), (9, 9), (10, 10);

INSERT INTO Licenca (Id_licenca, Status, Tipo, Data_fim, Caminho_pdf, Data_assinatura, Id_plano) VALUES
(1, 'Ativo', 'Anual', '20250101', 'lic_01.pdf', GETDATE(), 1),
(2, 'Ativo', 'Mensal', '20240601', 'lic_02.pdf', GETDATE(), 2),
(3, 'Inativo', 'Anual', '20240101', 'lic_03.pdf', GETDATE(), 3),
(4, 'Ativo', 'Anual', '20250201', 'lic_04.pdf', GETDATE(), 4),
(5, 'Ativo', 'VIP', '20260101', 'lic_05.pdf', GETDATE(), 5),
(6, 'Ativo', 'Anual', '20250301', 'lic_06.pdf', GETDATE(), 6),
(7, 'Pendente', 'Mensal', '20240515', 'lic_07.pdf', GETDATE(), 7),
(8, 'Ativo', 'Trial', '20240510', 'lic_08.pdf', GETDATE(), 8),
(9, 'Ativo', 'Anual', '20250501', 'lic_09.pdf', GETDATE(), 9),
(10, 'Ativo', 'Promo', '20241201', 'lic_10.pdf', GETDATE(), 10);

INSERT INTO Contrato (Id_contrato, Status, Data_fim, Data_assinatura, Id_plano) VALUES
(1, 'Vigente', '2025-05-01', GETDATE(), 4), (2, 'Vigente', '2024-08-01', GETDATE(), 2),
(3, 'Finalizado', '2024-04-01', GETDATE(), 1), (4, 'Vigente', '2024-11-01', GETDATE(), 3),
(5, 'Suspenso', '2025-05-01', GETDATE(), 5), (6, 'Vigente', '2026-05-01', GETDATE(), 6),
(7, 'Vigente', '2025-01-01', GETDATE(), 7), (8, 'Vigente', '2024-07-01', GETDATE(), 8),
(9, 'Cancelado', '2024-05-01', GETDATE(), 9), (10, 'Vigente', '2024-12-01', GETDATE(), 10);

INSERT INTO Avaliacao (Id_avaliacao, Id_professor, Id_aluno, Sexo, Data, Peso, Altura) VALUES
(1, 1, 1, 'Masculino', GETDATE(), 80.5, 1.80), (2, 2, 2, 'Feminino', GETDATE(), 65.2, 1.65),
(3, 1, 3, 'Feminino', GETDATE(), 70.0, 1.70), (4, 2, 4, 'Feminino', GETDATE(), 58.0, 1.60),
(5, 1, 5, 'Feminino', GETDATE(), 62.5, 1.68), (6, 1, 6, 'Masculino', GETDATE(), 90.1, 1.85),
(7, 2, 7, 'Feminino', GETDATE(), 55.0, 1.62), (8, 1, 8, 'Feminino', GETDATE(), 68.4, 1.71),
(9, 2, 9, 'Masculino', GETDATE(), 85.0, 1.78), (10, 1, 10, 'Feminino', GETDATE(), 63.0, 1.66);

INSERT INTO Ficha (Id_ficha, Nome_ficha, Observacoes, Id_avaliacao) VALUES
(1, 'Adaptação A', 'Treino leve', 1), (2, 'Adaptação B', 'Cuidado ombro', 2),
(3, 'Hipertrofia 1', 'Foco peito', 3), (4, 'Emagrecimento', 'Aerobico', 4),
(5, 'Definição', 'Alta repetição', 5), (6, 'Força', 'Carga máxima', 6),
(7, 'Funcional', 'Mobilidade', 7), (8, 'Pernas Hard', 'Foco quadriceps', 8),
(9, 'Costas V', 'Foco dorsal', 9), (10, 'ABC Completo', 'Geral', 10);

INSERT INTO Treinos (Id_treinos, Tipo, Nome_treino, Id_ficha) VALUES
(1, 'A', 'Peito e Tríceps', 1), (2, 'B', 'Costas e Bíceps', 1),
(3, 'A', 'Pernas', 2), (4, 'B', 'Superior', 2),
(5, 'C', 'Ombros', 3), (6, 'A', 'Peito', 4),
(7, 'B', 'Braços', 5), (8, 'A', 'Geral', 6),
(9, 'B', 'Core', 7), (10, 'C', 'Lombar', 8);

INSERT INTO Video (Id_video, Data_upload, Titulo, Caminho_arquivo, Id_academia) VALUES
(1, GETDATE(), 'Tutorial Supino', 'vid01.mp4', 1), (2, GETDATE(), 'Postura Squat', 'vid02.mp4', 1),
(3, GETDATE(), 'Dicas Dieta', 'vid03.mp4', 2), (4, GETDATE(), 'Uso Pulley', 'vid04.mp4', 2),
(5, GETDATE(), 'Warmup', 'vid05.mp4', 3), (6, GETDATE(), 'Yoga Basic', 'vid06.mp4', 9),
(7, GETDATE(), 'Crossfit 101', 'vid07.mp4', 2), (8, GETDATE(), 'Luta Movimento', 'vid08.mp4', 4),
(9, GETDATE(), 'Funcional em casa', 'vid09.mp4', 5), (10, GETDATE(), 'Tour Academia', 'vid10.mp4', 7);

INSERT INTO Telefone_usuario (Telefone, ID_usuario) VALUES
('11-9999-0001', 1), ('11-9999-0002', 2), ('11-9999-0003', 3), ('11-9999-0004', 4),
('11-9999-0005', 5), ('11-9999-0006', 6), ('11-9999-0007', 7), ('11-9999-0008', 8),
('11-9999-0009', 9), ('11-9999-0010', 10);

INSERT INTO Ficha_medica (Id_ficha, Id_aluno, Alergia, Restricao) VALUES
(1, 1, 'Poeira', 'Asma'), (2, 2, 'Nenhuma', 'Joelho'), (3, 3, 'Iodo', 'Nenhuma'),
(4, 4, 'Nenhuma', 'Coluna'), (5, 5, 'Lactose', 'Nenhuma'), (6, 6, 'Nenhuma', 'Nenhuma'),
(7, 7, 'Nenhuma', 'Grávida'), (8, 8, 'Peixe', 'Hipertensa'), (9, 9, 'Nenhuma', 'Diabetes'),
(10, 10, 'Nenhuma', 'Labirintite');

INSERT INTO Aluno_lembrete (Id_aluno_lembrete, Id_lembrete, Id_aluno, Horario, Ativo, Descricao) VALUES
(1, 1, 1, GETDATE(), 'Sim', 'Beber Água'), (2, 2, 2, GETDATE(), 'Não', 'Tomar Whey'),
(3, 3, 3, GETDATE(), 'Sim', 'Treinar'), (4, 4, 4, GETDATE(), 'Sim', 'Avaliação'),
(5, 5, 5, GETDATE(), 'Não', 'Pagar Mensalidade'), (6, 6, 6, GETDATE(), 'Sim', 'Aeróbico'),
(7, 7, 7, GETDATE(), 'Sim', 'Mobilidade'), (8, 8, 8, GETDATE(), 'Sim', 'Dormir cedo'),
(9, 9, 9, GETDATE(), 'Não', 'Dieta'), (10, 10, 10, GETDATE(), 'Sim', 'Checkup');

INSERT INTO Treino_exercicio (Id_exercicios, Id_treinos) VALUES
(1, 1), (2, 3), (3, 2), (4, 2), (5, 3), (6, 5), (7, 1), (8, 8), (9, 3), (10, 3);

INSERT INTO Academia_licenca (Id_mensalidade, Id_licenca, Id_academia, Status, Valor) VALUES
(1, 1, 1, 'Pago', 1200.00), (2, 2, 2, 'Pendente', 800.00), (3, 3, 3, 'Pago', 1500.00),
(4, 4, 4, 'Pago', 2000.00), (5, 5, 5, 'Atrasado', 500.00), (6, 6, 6, 'Pago', 900.00),
(7, 7, 7, 'Pago', 3000.00), (8, 8, 8, 'Pago', 600.00), (9, 9, 9, 'Pago', 1100.00),
(10, 10, 10, 'Pago', 700.00);