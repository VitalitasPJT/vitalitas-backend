USE VITALITAS_DEV;
GO

INSERT INTO usuario (
    idUsuario, nome, email, quadra, rua, bairro, cidade, estado,
    cep, senha, dataNascimento, cpf, tipoUsuario, ativo, flag
)
VALUES
('230e0d15-3fe0-4a23-ad1f-562d47de2b12','João Silva','joao@gmail.com','Q1','Rua A','Centro','Brasilia','DF','01001000','123',CAST('1990-05-15' AS DATE),'11111111111',1,1,1),
('de18ef7e-7f09-4c39-a2fb-1ba0ae699efb','Maria Santos','maria@gmail.com','Q2','Rua B','Centro','Brasilia','DF','02002000','123',CAST('1992-08-22' AS DATE),'22222222222',1,1,1),
('5dbe34dc-4d6e-4caf-a2af-eb63cc6bc78e','Ricardo Oliveira','ric@gmail.com','Q3','Rua C','Centro','Brasilia','DF','03003000','123',CAST('1985-03-10' AS DATE),'33333333333',2,1,1),
('0cadf8ca-20d2-4e8e-8f80-de475c099a28','Carlos Eduardo','carlos@gmail.com','Q5','Rua E','Centro','Brasilia','DF','05005000','123',CAST('1980-01-20' AS DATE),'55555555555',3,1,1),
('e6d17569-462f-4b0b-aa05-9f9d88c13ea2','Gabriel Souza','gab@gmail.com','Q7','Rua G','Centro','Brasilia','DF','07007000','123',CAST('1988-11-25' AS DATE),'77777777777',2,1,1),
('11111111-2222-3333-4444-555555555555','Gestor Teste','gestor@gmail.com','Q10','Rua X','Centro','Brasilia','DF','99999999','123',CAST('1990-01-01' AS DATE),'99999999999',3,1,1);

INSERT INTO planoLicenca (
    idPlano, nome, descricao, valor
)
VALUES
(1,'Bronze','Plano básico',500.00),
(2,'Silver','Plano intermediário',800.00);

INSERT INTO gestor (
    idGestor, idUsuario
)
VALUES
('99999999-aaaa-bbbb-cccc-dddddddddddd','11111111-2222-3333-4444-555555555555'),
('8a6f0c2a-5c0f-4e6b-9e2d-6f2c2f4b9c11','0cadf8ca-20d2-4e8e-8f80-de475c099a28');

INSERT INTO instrutor (
    idInstrutor, cref, idUsuario
)
VALUES
('b1f9c2e4-1d6b-4c2e-9c9a-3e3c7d1a1111','123456','5dbe34dc-4d6e-4caf-a2af-eb63cc6bc78e'),
('c2e7d9a1-7a3b-4d1a-b2e4-9a1c7e2b2222','654321','e6d17569-462f-4b0b-aa05-9f9d88c13ea2');

INSERT INTO licenca (
    idLicenca, status, tipo, dataFim, caminhoPdf,
    dataAssinatura, idPlano, mensalidade
)
VALUES
(
'aaaa1111-1111-1111-1111-111111111111',
'Ativa',
'Anual',
CAST('2026-12-31T00:00:00' AS DATETIME),
'licenca1.pdf',
GETDATE(),
1,
500.00
),
(
'bbbb2222-2222-2222-2222-222222222222',
'Ativa',
'Mensal',
CAST('2026-06-30T00:00:00' AS DATETIME),
'licenca2.pdf',
GETDATE(),
2,
800.00
);

INSERT INTO academia (
    idAcademia, nomeAcademia, cnpj, emailInstitucional,
    tipoAcademia, cep, idLicenca, idGestor
)
VALUES
(
'f3c2a111-aaaa-4b2e-9c1a-111111111111',
'Vitalitas Matriz',
'12345678000101',
'matriz@gmail.com',
'Musculacao',
'01001000',
'aaaa1111-1111-1111-1111-111111111111',
'8a6f0c2a-5c0f-4e6b-9e2d-6f2c2f4b9c11'
);

INSERT INTO contrato (
    idContrato, caminhoPdf, dataFim, dataInicio
)
VALUES
(
'aaaa0000-0000-0000-0000-000000000001',
'contrato1.pdf',
CAST('2026-12-31T00:00:00' AS DATETIME),
CAST('2026-01-01T00:00:00' AS DATETIME)
),
(
'bbbb0000-0000-0000-0000-000000000002',
'contrato2.pdf',
CAST('2026-10-01T00:00:00' AS DATETIME),
CAST('2026-02-01T00:00:00' AS DATETIME)
);

INSERT INTO aluno (
    idAluno, objetivo, idUsuario, idContrato, idAcademia
)
VALUES
(
'11111111-aaaa-4c2e-9c1a-aaaaaaaaaaaa',
'Perder peso',
'230e0d15-3fe0-4a23-ad1f-562d47de2b12',
'aaaa0000-0000-0000-0000-000000000001',
'f3c2a111-aaaa-4b2e-9c1a-111111111111'
),
(
'22222222-bbbb-4c2e-9c1a-bbbbbbbbbbbb',
'Hipertrofia',
'de18ef7e-7f09-4c39-a2fb-1ba0ae699efb',
'bbbb0000-0000-0000-0000-000000000002',
'f3c2a111-aaaa-4b2e-9c1a-111111111111'
);

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
