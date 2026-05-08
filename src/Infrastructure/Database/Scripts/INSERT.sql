USE VITALITAS_DEV;
GO

INSERT INTO planoLicenca (idPlanoLicenca, nome, descricao, valor) VALUES
(1, 'Plano Starter', 'Ideal para estúdios e pequenas academias. Limite de 200 alunos.', 149.90),
(2, 'Plano Pro', 'Para academias em crescimento. Limite de 500 alunos e suporte prioritário.', 299.90),
(3, 'Plano Premium', 'Acesso a todos os módulos, alunos ilimitados e integrações exclusivas.', 499.90);

INSERT INTO licenca (idLicenca, idPlano, mensalidade, status, tipo, dataFim, dataAssinatura, caminhoPdf) VALUES
-- Licença vinculada ao Plano Starter (ID 1)
('11111111-1111-1111-1111-111111111111', 1, 149.90, 'Ativa', 'Mensal', '2026-12-31', '2025-01-15', '/docs/licencas/licenca_starter_1.pdf'),
-- Licença vinculada ao Plano Pro (ID 2)
('22222222-2222-2222-2222-222222222222', 2, 299.90, 'Ativa', 'Anual', '2027-05-10', '2026-05-10', '/docs/licencas/licenca_pro_1.pdf'),
-- Licença vinculada ao Plano Premium (ID 3), mas simulando uma que já venceu
('33333333-3333-3333-3333-333333333333', 3, 499.90, 'Vencida', 'Mensal', '2025-12-31', '2024-01-20', '/docs/licencas/licenca_premium_old.pdf');

INSERT INTO academia (idAcademia, idLicenca, idGestor, nomeAcademia, cnpj, quadra, rua, bairro, cidade, estado, cep, tipoAcademia, emailInstitucional) VALUES
-- Academia 1 (Vinculada à Licença Starter: 111...)
('AAAA1111-AAAA-1111-AAAA-111111111111', '11111111-1111-1111-1111-111111111111', NULL, 'FitZone Centro', '12345678000190', 'Quadra 10', 'Av. Principal', 'Centro', 'São Paulo', 'SP', '01001000', 'Musculação e Funcional', 'contato@fitzone.com.br'),
-- Academia 2 (Vinculada à Licença Pro: 222...)
('AAAA2222-AAAA-2222-AAAA-222222222222', '22222222-2222-2222-2222-222222222222', NULL, 'IronGym Cross', '98765432000180', 'Lote 5', 'Rua das Pedras', 'Bela Vista', 'Rio de Janeiro', 'RJ', '20001000', 'Crossfit', 'admin@irongym.com.br'),
-- Academia 3 (Vinculada à Licença Premium vencida: 333...)
('AAAA3333-AAAA-3333-AAAA-333333333333', '33333333-3333-3333-3333-333333333333', NULL, 'Acqua Life Natação', '45678912000170', 'Bloco C', 'Av. Atlântica', 'Copacabana', 'Rio de Janeiro', 'RJ', '22001000', 'Natação e Hidro', 'gerencia@acqualife.com.br');

INSERT INTO usuario (idUsuario, idAcademia, nome, email, senha, dataNascimento, cpf, tipoUsuario, ativo, flag, quadra, rua, bairro, cidade, estado, cep) VALUES
-- ADMINISTRADORES (Tipo 4) - Sistema geral (Vinculados à primeira academia de forma administrativa)
('DDDD4444-DDDD-4444-DDDD-444444444401', 'AAAA1111-AAAA-1111-AAAA-111111111111', 'Admin Master', 'admin@sistema.com', 'hash_senha', '1985-01-10', '00000000001', 4, 1, 1, 'Q 1', 'Rua Alfa', 'Centro', 'São Paulo', 'SP', '01000000'),
('DDDD4444-DDDD-4444-DDDD-444444444402', 'AAAA1111-AAAA-1111-AAAA-111111111111', 'Suporte Técnico', 'suporte@sistema.com', 'hash_senha', '1990-05-20', '00000000002', 4, 1, 1, 'Q 2', 'Rua Beta', 'Centro', 'São Paulo', 'SP', '01000000'),
-- GESTORES (Tipo 3) - Um para cada academia (Vamos usar esses IDs depois para atualizar a academia)
-- Gestor da Academia 1 (FitZone)
('DDDD3333-DDDD-3333-DDDD-333333333301', 'AAAA1111-AAAA-1111-AAAA-111111111111', 'Carlos Silva', 'carlos@fitzone.com', 'hash_senha', '1980-03-15', '00000000003', 3, 1, 1, 'Q 10', 'Av. Principal', 'Centro', 'São Paulo', 'SP', '01001000'),
-- Gestor da Academia 2 (IronGym)
('DDDD3333-DDDD-3333-DDDD-333333333302', 'AAAA2222-AAAA-2222-AAAA-222222222222', 'Mariana Souza', 'mariana@irongym.com', 'hash_senha', '1982-07-22', '00000000004', 3, 1, 1, 'Lote 5', 'Rua das Pedras', 'Bela Vista', 'Rio de Janeiro', 'RJ', '20001000'),
-- Gestor da Academia 3 (Acqua Life)
('DDDD3333-DDDD-3333-DDDD-333333333303', 'AAAA3333-AAAA-3333-AAAA-333333333333', 'Roberto Alves', 'roberto@acqualife.com', 'hash_senha', '1975-11-30', '00000000005', 3, 1, 1, 'Bloco C', 'Av. Atlântica', 'Copacabana', 'Rio de Janeiro', 'RJ', '22001000'),
-- INSTRUTORES (Tipo 1) - Divididos entre as academias
-- Instrutores Academia 1
('DDDD1111-DDDD-1111-DDDD-111111111101', 'AAAA1111-AAAA-1111-AAAA-111111111111', 'Fernando Personal', 'fernando@fitzone.com', 'hash_senha', '1992-04-10', '00000000006', 1, 1, 1, 'Q 12', 'Rua X', 'Jardins', 'São Paulo', 'SP', '01002000'),
('DDDD1111-DDDD-1111-DDDD-111111111102', 'AAAA1111-AAAA-1111-AAAA-111111111111', 'Juliana Cross', 'juliana@fitzone.com', 'hash_senha', '1995-09-05', '00000000007', 1, 1, 1, 'Q 14', 'Rua Y', 'Pinheiros', 'São Paulo', 'SP', '01003000'),
-- Instrutores Academia 2
('DDDD1111-DDDD-1111-DDDD-111111111103', 'AAAA2222-AAAA-2222-AAAA-222222222222', 'Thiago Monstro', 'thiago@irongym.com', 'hash_senha', '1990-12-12', '00000000008', 1, 1, 1, 'Lote 1', 'Rua Z', 'Centro', 'Rio de Janeiro', 'RJ', '20002000'),
('DDDD1111-DDDD-1111-DDDD-111111111104', 'AAAA2222-AAAA-2222-AAAA-222222222222', 'Paula Coach', 'paula@irongym.com', 'hash_senha', '1994-02-28', '00000000009', 1, 1, 1, 'Lote 2', 'Rua W', 'Lapa', 'Rio de Janeiro', 'RJ', '20003000'),
-- Instrutor Academia 3
('DDDD1111-DDDD-1111-DDDD-111111111105', 'AAAA3333-AAAA-3333-AAAA-333333333333', 'Lucas Nadador', 'lucas@acqualife.com', 'hash_senha', '1988-06-14', '00000000010', 1, 1, 1, 'Bloco A', 'Rua V', 'Ipanema', 'Rio de Janeiro', 'RJ', '22002000'),
-- ALUNOS (Tipo 2) - Frequência variada nas academias
-- Alunos Academia 1
('DDDD2222-DDDD-2222-DDDD-222222222201', 'AAAA1111-AAAA-1111-AAAA-111111111111', 'João Pereira', 'joao@email.com', 'hash_senha', '2000-01-20', '00000000011', 2, 1, 1, 'Q 1', 'Rua 1', 'Bairro A', 'São Paulo', 'SP', '01004000'),
('DDDD2222-DDDD-2222-DDDD-222222222202', 'AAAA1111-AAAA-1111-AAAA-111111111111', 'Maria Oliveira', 'maria@email.com', 'hash_senha', '1998-05-15', '00000000012', 2, 1, 1, 'Q 2', 'Rua 2', 'Bairro B', 'São Paulo', 'SP', '01005000'),
('DDDD2222-DDDD-2222-DDDD-222222222203', 'AAAA1111-AAAA-1111-AAAA-111111111111', 'Pedro Santos', 'pedro@email.com', 'hash_senha', '1995-10-10', '00000000013', 2, 1, 1, 'Q 3', 'Rua 3', 'Bairro C', 'São Paulo', 'SP', '01006000'),
('DDDD2222-DDDD-2222-DDDD-222222222204', 'AAAA1111-AAAA-1111-AAAA-111111111111', 'Ana Costa', 'ana@email.com', 'hash_senha', '2002-12-05', '00000000014', 2, 1, 1, 'Q 4', 'Rua 4', 'Bairro D', 'São Paulo', 'SP', '01007000'),
-- Alunos Academia 2
('DDDD2222-DDDD-2222-DDDD-222222222205', 'AAAA2222-AAAA-2222-AAAA-222222222222', 'Felipe Almeida', 'felipe@email.com', 'hash_senha', '1993-08-30', '00000000015', 2, 1, 1, 'Lote 1', 'Rua 5', 'Bairro E', 'Rio de Janeiro', 'RJ', '20004000'),
('DDDD2222-DDDD-2222-DDDD-222222222206', 'AAAA2222-AAAA-2222-AAAA-222222222222', 'Camila Rocha', 'camila@email.com', 'hash_senha', '1997-04-25', '00000000016', 2, 1, 1, 'Lote 2', 'Rua 6', 'Bairro F', 'Rio de Janeiro', 'RJ', '20005000'),
('DDDD2222-DDDD-2222-DDDD-222222222207', 'AAAA2222-AAAA-2222-AAAA-222222222222', 'Diego Martins', 'diego@email.com', 'hash_senha', '1990-02-14', '00000000017', 2, 1, 1, 'Lote 3', 'Rua 7', 'Bairro G', 'Rio de Janeiro', 'RJ', '20006000'),
('DDDD2222-DDDD-2222-DDDD-222222222208', 'AAAA2222-AAAA-2222-AAAA-222222222222', 'Letícia Gomes', 'leticia@email.com', 'hash_senha', '1999-11-18', '00000000018', 2, 1, 1, 'Lote 4', 'Rua 8', 'Bairro H', 'Rio de Janeiro', 'RJ', '20007000'),
-- Alunos Academia 3
('DDDD2222-DDDD-2222-DDDD-222222222209', 'AAAA3333-AAAA-3333-AAAA-333333333333', 'Bruno Lima', 'bruno@email.com', 'hash_senha', '2005-03-08', '00000000019', 2, 1, 1, 'Bloco B', 'Rua 9', 'Bairro I', 'Rio de Janeiro', 'RJ', '22003000'),
('DDDD2222-DDDD-2222-DDDD-222222222210', 'AAAA3333-AAAA-3333-AAAA-333333333333', 'Sofia Mendes', 'sofia@email.com', 'hash_senha', '2003-09-02', '00000000020', 2, 1, 1, 'Bloco D', 'Rua 10', 'Bairro J', 'Rio de Janeiro', 'RJ', '22004000');


INSERT INTO gestor (idGestor, idUsuario) VALUES
-- Gestor da Academia 1 (Usuário Carlos Silva)
('EEEE3333-EEEE-3333-EEEE-333333333301', 'DDDD3333-DDDD-3333-DDDD-333333333301'),
-- Gestor da Academia 2 (Usuária Mariana Souza)
('EEEE3333-EEEE-3333-EEEE-333333333302', 'DDDD3333-DDDD-3333-DDDD-333333333302'),
-- Gestor da Academia 3 (Usuário Roberto Alves)
('EEEE3333-EEEE-3333-EEEE-333333333303', 'DDDD3333-DDDD-3333-DDDD-333333333303');
-- Vinculando o Carlos à FitZone Centro
UPDATE academia 
SET idGestor = 'EEEE3333-EEEE-3333-EEEE-333333333301' 
WHERE idAcademia = 'AAAA1111-AAAA-1111-AAAA-111111111111';
-- Vinculando a Mariana à IronGym Cross
UPDATE academia 
SET idGestor = 'EEEE3333-EEEE-3333-EEEE-333333333302' 
WHERE idAcademia = 'AAAA2222-AAAA-2222-AAAA-222222222222';
-- Vinculando o Roberto à Acqua Life Natação
UPDATE academia 
SET idGestor = 'EEEE3333-EEEE-3333-EEEE-333333333303' 
WHERE idAcademia = 'AAAA3333-AAAA-3333-AAAA-333333333333';

INSERT INTO instrutor (idInstrutor, idUsuario, cref) VALUES
-- Instrutores da Academia 1 (São Paulo - SP)
-- Vinculado ao Fernando Personal
('FFFF1111-FFFF-1111-FFFF-111111111101', 'DDDD1111-DDDD-1111-DDDD-111111111101', '123456-G/SP'),
-- Vinculado à Juliana Cross
('FFFF1111-FFFF-1111-FFFF-111111111102', 'DDDD1111-DDDD-1111-DDDD-111111111102', '654321-G/SP'),
-- Instrutores da Academia 2 (Rio de Janeiro - RJ)
-- Vinculado ao Thiago Monstro
('FFFF1111-FFFF-1111-FFFF-111111111103', 'DDDD1111-DDDD-1111-DDDD-111111111103', '987654-G/RJ'),
-- Vinculado à Paula Coach
('FFFF1111-FFFF-1111-FFFF-111111111104', 'DDDD1111-DDDD-1111-DDDD-111111111104', '456789-G/RJ'),
-- Instrutor da Academia 3 (Rio de Janeiro - RJ)
-- Vinculado ao Lucas Nadador
('FFFF1111-FFFF-1111-FFFF-111111111105', 'DDDD1111-DDDD-1111-DDDD-111111111105', '321987-G/RJ');

INSERT INTO planoContrato (idPlanoContrato, nome, descricao, valor) VALUES
(1, 'Mensal FitZone', 'Acesso livre área de musculação', 120.00),
(2, 'Trimestral Iron', 'Acesso Crossfit e LPO', 300.00),
(3, 'Semestral Acqua', 'Natação 3x na semana', 500.00);

INSERT INTO contrato (idContrato, idPlanoContrato, mensalidade, status, caminhoPdf, dataFim, dataAssinatura) VALUES
-- Contratos da Academia 1 (Plano 1)
('CCCC1111-CCCC-1111-CCCC-111111111101', 1, 120.00, 1, '/docs/contratos/aluno_01.pdf', '2026-12-31', '2026-01-10'),
('CCCC1111-CCCC-1111-CCCC-111111111102', 1, 120.00, 1, '/docs/contratos/aluno_02.pdf', '2026-12-31', '2026-02-15'),
('CCCC1111-CCCC-1111-CCCC-111111111103', 1, 120.00, 1, '/docs/contratos/aluno_03.pdf', '2026-12-31', '2026-03-20'),
('CCCC1111-CCCC-1111-CCCC-111111111104', 1, 120.00, 1, '/docs/contratos/aluno_04.pdf', '2026-12-31', '2026-04-05'),
-- Contratos da Academia 2 (Plano 2)
('CCCC1111-CCCC-1111-CCCC-111111111105', 2, 300.00, 1, '/docs/contratos/aluno_05.pdf', '2026-08-10', '2026-05-10'),
('CCCC1111-CCCC-1111-CCCC-111111111106', 2, 300.00, 1, '/docs/contratos/aluno_06.pdf', '2026-09-12', '2026-06-12'),
('CCCC1111-CCCC-1111-CCCC-111111111107', 2, 300.00, 1, '/docs/contratos/aluno_07.pdf', '2026-10-01', '2026-07-01'),
('CCCC1111-CCCC-1111-CCCC-111111111108', 2, 300.00, 1, '/docs/contratos/aluno_08.pdf', '2026-11-20', '2026-08-20'),
-- Contratos da Academia 3 (Plano 3)
('CCCC1111-CCCC-1111-CCCC-111111111109', 3, 500.00, 1, '/docs/contratos/aluno_09.pdf', '2026-11-05', '2026-05-05'),
('CCCC1111-CCCC-1111-CCCC-111111111110', 3, 500.00, 1, '/docs/contratos/aluno_10.pdf', '2027-01-15', '2026-07-15');

INSERT INTO aluno (idAluno, idUsuario, IdInstrutor, idContrato, objetivo) VALUES
-- Alunos da Academia 1 (FitZone) -> Treinando com Fernando (FFFF...01) e Juliana (FFFF...02)
('BBBB2222-BBBB-2222-BBBB-222222222201', 'DDDD2222-DDDD-2222-DDDD-222222222201', 'FFFF1111-FFFF-1111-FFFF-111111111101', 'CCCC1111-CCCC-1111-CCCC-111111111101', 'Hipertrofia'),
('BBBB2222-BBBB-2222-BBBB-222222222202', 'DDDD2222-DDDD-2222-DDDD-222222222202', 'FFFF1111-FFFF-1111-FFFF-111111111101', 'CCCC1111-CCCC-1111-CCCC-111111111102', 'Emagrecimento'),
('BBBB2222-BBBB-2222-BBBB-222222222203', 'DDDD2222-DDDD-2222-DDDD-222222222203', 'FFFF1111-FFFF-1111-FFFF-111111111102', 'CCCC1111-CCCC-1111-CCCC-111111111103', 'Condicionamento Físico'),
('BBBB2222-BBBB-2222-BBBB-222222222204', 'DDDD2222-DDDD-2222-DDDD-222222222204', 'FFFF1111-FFFF-1111-FFFF-111111111102', 'CCCC1111-CCCC-1111-CCCC-111111111104', 'Saúde e Bem-estar'),
-- Alunos da Academia 2 (IronGym) -> Treinando com Thiago (FFFF...03) e Paula (FFFF...04)
('BBBB2222-BBBB-2222-BBBB-222222222205', 'DDDD2222-DDDD-2222-DDDD-222222222205', 'FFFF1111-FFFF-1111-FFFF-111111111103', 'CCCC1111-CCCC-1111-CCCC-111111111105', 'Ganho de Força'),
('BBBB2222-BBBB-2222-BBBB-222222222206', 'DDDD2222-DDDD-2222-DDDD-222222222206', 'FFFF1111-FFFF-1111-FFFF-111111111103', 'CCCC1111-CCCC-1111-CCCC-111111111106', 'Resistência Muscular'),
('BBBB2222-BBBB-2222-BBBB-222222222207', 'DDDD2222-DDDD-2222-DDDD-222222222207', 'FFFF1111-FFFF-1111-FFFF-111111111104', 'CCCC1111-CCCC-1111-CCCC-111111111107', 'Competição Crossfit'),
('BBBB2222-BBBB-2222-BBBB-222222222208', 'DDDD2222-DDDD-2222-DDDD-222222222208', 'FFFF1111-FFFF-1111-FFFF-111111111104', 'CCCC1111-CCCC-1111-CCCC-111111111108', 'Mobilidade e Flexibilidade'),
-- Alunos da Academia 3 (Acqua Life) -> Treinando com Lucas (FFFF...05)
('BBBB2222-BBBB-2222-BBBB-222222222209', 'DDDD2222-DDDD-2222-DDDD-222222222209', 'FFFF1111-FFFF-1111-FFFF-111111111105', 'CCCC1111-CCCC-1111-CCCC-111111111109', 'Aprender a nadar'),
('BBBB2222-BBBB-2222-BBBB-222222222210', 'DDDD2222-DDDD-2222-DDDD-222222222210', 'FFFF1111-FFFF-1111-FFFF-111111111105', 'CCCC1111-CCCC-1111-CCCC-111111111110', 'Treino para Triatlo');

---------------------------------------------------------------------------------------------------------
-- Parei aqui
---------------------------------------------------------------------------------------------------------

INSERT INTO funcionario (
    idFuncionario, idUsuario, cargo
)
VALUES
(
'aaaa1111-aaaa-4c2e-9c1a-aaaaaaaaaaaa',
'0cadf8ca-20d2-4e8e-8f80-de475c099a28',
'Gerente'
);

INSERT INTO agenda (
    idAgenda, status, data
)
VALUES
(
1,
'Agendado',
CAST('2026-05-10T08:00:00' AS DATETIME)
),
(
2,
'Confirmado',
CAST('2026-05-12T10:00:00' AS DATETIME)
);

INSERT INTO logAtividade (
    idLog, acao, dataHora
)
VALUES
('10000000-0000-0000-0000-000000000001','Login',GETDATE()),
('20000000-0000-0000-0000-000000000002','Cadastro',GETDATE());

INSERT INTO xpHistorico (
    idXp, motivo, data, xpGanho
)
VALUES
('30000000-0000-0000-0000-000000000003','Treino completo',GETDATE(),10),
('40000000-0000-0000-0000-000000000004','Frequência semanal',GETDATE(),50);

INSERT INTO avaliacao (
    idAvaliacao, idAluno, sexo, data, peso, altura,
    idade, imc, glicemia, pa, densidade,
    tr, se, pt, ax, si, ab, cx, ptrrlh,
    umero, femur, pMagro, pGordo, pViscera,
    pOsseo, torax, abdomen, quadril,
    bracoD, bracoE, coxaD, coxaE,
    pernaD, pernaE, deltoide, idInstrutor
)
VALUES
(
'50000000-0000-0000-0000-000000000005',
'11111111-aaaa-4c2e-9c1a-aaaaaaaaaaaa',
'Masculino',
GETDATE(),
80,1.80,30,24.7,90,12,1.2,
1,1,1,1,1,1,1,1,
1,1,70,10,5,
15,95,85,90,
30,30,50,50,
35,35,12,
'b1f9c2e4-1d6b-4c2e-9c9a-3e3c7d1a1111'
),
(
'60000000-0000-0000-0000-000000000006',
'22222222-bbbb-4c2e-9c1a-bbbbbbbbbbbb',
'Feminino',
GETDATE(),
65,1.65,28,23.8,85,11,1.1,
1,1,1,1,1,1,1,1,
1,1,55,12,4,
14,90,80,95,
28,28,48,48,
34,34,10,
'c2e7d9a1-7a3b-4d1a-b2e4-9a1c7e2b2222'
);

INSERT INTO ficha (
    idFicha, nomeFicha, observacoes, idAvaliacao, idAluno
)
VALUES
(
'70000000-0000-0000-0000-000000000007',
'Ficha A',
'Ficha inicial',
'50000000-0000-0000-0000-000000000005',
'11111111-aaaa-4c2e-9c1a-aaaaaaaaaaaa'
),
(
'80000000-0000-0000-0000-000000000008',
'Ficha B',
'Ficha intermediária',
'60000000-0000-0000-0000-000000000006',
'22222222-bbbb-4c2e-9c1a-bbbbbbbbbbbb'
);

INSERT INTO treino (
    idTreino,
    nomeTreino,
    idFicha,
    exercicio
)
VALUES
(
    1,
    'Treino ABC',
    '70000000-0000-0000-0000-000000000007',
    '{
        "A": [
            {"nome": "Supino Reto", "series": 4, "reps": 10},
            {"nome": "Supino Inclinado", "series": 3, "reps": 10},
            {"nome": "Crucifixo", "series": 3, "reps": 12},
            {"nome": "Triceps Pulley", "series": 3, "reps": 12}
        ],
        "B": [
            {"nome": "Puxada Frontal", "series": 4, "reps": 10},
            {"nome": "Remada Curvada", "series": 3, "reps": 10},
            {"nome": "Rosca Direta", "series": 3, "reps": 12},
            {"nome": "Rosca Alternada", "series": 3, "reps": 12}
        ],
        "C": [
            {"nome": "Agachamento", "series": 4, "reps": 10},
            {"nome": "Leg Press", "series": 3, "reps": 12},
            {"nome": "Cadeira Extensora", "series": 3, "reps": 12},
            {"nome": "Panturrilha", "series": 4, "reps": 15}
        ]
    }'
),
(
    2,
    'Treino AB',
    '80000000-0000-0000-0000-000000000008',
    '{
        "A": [
            {"nome": "Supino Reto", "series": 4, "reps": 10},
            {"nome": "Supino Inclinado", "series": 3, "reps": 10},
            {"nome": "Triceps Testa", "series": 3, "reps": 12},
            {"nome": "Triceps Corda", "series": 3, "reps": 12}
        ],
        "B": [
            {"nome": "Puxada Frontal", "series": 4, "reps": 10},
            {"nome": "Remada Baixa", "series": 3, "reps": 10},
            {"nome": "Rosca Direta", "series": 3, "reps": 12},
            {"nome": "Rosca Martelo", "series": 3, "reps": 12},
            {"nome": "Agachamento", "series": 4, "reps": 10},
            {"nome": "Leg Press", "series": 3, "reps": 12}
        ]
    }'
);

INSERT INTO video (
    idVideo, dataUpload, titulo, caminhoArquivo, idAcademia
)
VALUES
(
1,
GETDATE(),
'Tutorial Supino',
'supino.mp4',
'f3c2a111-aaaa-4b2e-9c1a-111111111111'
);

INSERT INTO telefoneUsuario (
    telefone, idUsuario
)
VALUES
('61999990001','230e0d15-3fe0-4a23-ad1f-562d47de2b12'),
('61999990002','de18ef7e-7f09-4c39-a2fb-1ba0ae699efb');

INSERT INTO telefoneAcademia (
    telefone, idAcademia
)
VALUES
('6133330001','f3c2a111-aaaa-4b2e-9c1a-111111111111');

INSERT INTO frequencia (
    idFrequencia, idAluno, data, tempoTreino
)
VALUES
(
'90000000-0000-0000-0000-000000000009',
'11111111-aaaa-4c2e-9c1a-aaaaaaaaaaaa',
CAST('2026-05-01' AS DATE),
60
);

INSERT INTO usuarioLog (
    idUsuario, idLog
)
VALUES
('230e0d15-3fe0-4a23-ad1f-562d47de2b12','10000000-0000-0000-0000-000000000001'),
('de18ef7e-7f09-4c39-a2fb-1ba0ae699efb','20000000-0000-0000-0000-000000000002');

INSERT INTO agendaProfessor (
    idAgenda, idInstrutor
)
VALUES
(1,'b1f9c2e4-1d6b-4c2e-9c9a-3e3c7d1a1111');

INSERT INTO usuarioAcademia (
    idAcademia, idUsuario
)
VALUES
('f3c2a111-aaaa-4b2e-9c1a-111111111111','230e0d15-3fe0-4a23-ad1f-562d47de2b12'),
('f3c2a111-aaaa-4b2e-9c1a-111111111111','de18ef7e-7f09-4c39-a2fb-1ba0ae699efb');

INSERT INTO xpAluno (
    idXp, idAluno
)
VALUES
('30000000-0000-0000-0000-000000000003','11111111-aaaa-4c2e-9c1a-aaaaaaaaaaaa'),
('40000000-0000-0000-0000-000000000004','22222222-bbbb-4c2e-9c1a-bbbbbbbbbbbb');

INSERT INTO fichaMedica (
    idFicha, idAluno, alergia, restricao,
    lesao, cirurgia, problemaSaude, usoMedicamento
)
VALUES
(
'a0000000-0000-0000-0000-000000000010',
'11111111-aaaa-4c2e-9c1a-aaaaaaaaaaaa',
'Poeira',
'Nenhuma',
'Nenhuma',
'Nenhuma',
'Asma',
'Nenhum'
);
