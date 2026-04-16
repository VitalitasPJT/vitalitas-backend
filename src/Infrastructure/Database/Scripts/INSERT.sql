USE VITALITAS_DEV;
GO

INSERT INTO usuario VALUES
('230e0d15-3fe0-4a23-ad1f-562d47de2b12','João Silva','joao@gmail.com','Q1','Rua A','Centro','Brasilia','DF','01001000','123','19900515','11111111111',1,1),
('de18ef7e-7f09-4c39-a2fb-1ba0ae699efb','Maria Santos','maria@gmail.com','Q2','Rua B','Centro','Brasilia','DF','02002000','123','19920822','22222222222',1,1),
('5dbe34dc-4d6e-4caf-a2af-eb63cc6bc78e','Ricardo Oliveira','ric@gmail.com','Q3','Rua C','Centro','Brasilia','DF','03003000','123','19850310','33333333333',2,1),
('0cadf8ca-20d2-4e8e-8f80-de475c099a28','Carlos Eduardo','carlos@gmail.com','Q5','Rua E','Centro','Brasilia','DF','05005000','123','19800120','55555555555',3,1),
('e6d17569-462f-4b0b-aa05-9f9d88c13ea2','Gabriel Souza','gab@gmail.com','Q7','Rua G','Centro','Brasilia','DF','07007000','123','19881125','77777777777',2,1);

INSERT INTO usuario VALUES
('11111111-2222-3333-4444-555555555555','Gestor Teste','gestor@gmail.com','Q10','Rua X','Centro','Brasilia','DF','99999999','123','19900101','99999999999',3,1);

INSERT INTO gestor VALUES
('99999999-aaaa-bbbb-cccc-dddddddddddd','11111111-2222-3333-4444-555555555555');

INSERT INTO gestor VALUES
('8a6f0c2a-5c0f-4e6b-9e2d-6f2c2f4b9c11','0cadf8ca-20d2-4e8e-8f80-de475c099a28');

INSERT INTO instrutor VALUES
('b1f9c2e4-1d6b-4c2e-9c9a-3e3c7d1a1111','123456','5dbe34dc-4d6e-4caf-a2af-eb63cc6bc78e'),
('c2e7d9a1-7a3b-4d1a-b2e4-9a1c7e2b2222','654321','e6d17569-462f-4b0b-aa05-9f9d88c13ea2');

INSERT INTO planoLicenca VALUES
(1,'Bronze','Basico',500),
(2,'Silver','Intermediario',800);

INSERT INTO planoContrato VALUES
(1,'Mensal','Plano mensal',100),
(2,'Trimestral','Plano 3 meses',270);

INSERT INTO licenca VALUES
(1,'Ativo','Anual','20250101','lic1.pdf',GETDATE(),1,500),
(2,'Ativo','Mensal','20240601','lic2.pdf',GETDATE(),2,800);

INSERT INTO academia VALUES
('f3c2a111-aaaa-4b2e-9c1a-111111111111','Vitalitas Matriz','12345678000101','matriz@gmail.com','Musculação','01001000',1,'8a6f0c2a-5c0f-4e6b-9e2d-6f2c2f4b9c11');

INSERT INTO contrato VALUES
(1,'Vigente','20250501',GETDATE(),1,100),
(2,'Vigente','20240801',GETDATE(),2,270);

INSERT INTO aluno VALUES
('11111111-aaaa-4c2e-9c1a-aaaaaaaaaaaa','Perder peso','230e0d15-3fe0-4a23-ad1f-562d47de2b12',1,'f3c2a111-aaaa-4b2e-9c1a-111111111111'),
('22222222-bbbb-4c2e-9c1a-bbbbbbbbbbbb','Hipertrofia','de18ef7e-7f09-4c39-a2fb-1ba0ae699efb',2,'f3c2a111-aaaa-4b2e-9c1a-111111111111');

INSERT INTO funcionario VALUES
('aaaa1111-aaaa-4c2e-9c1a-aaaaaaaaaaaa','0cadf8ca-20d2-4e8e-8f80-de475c099a28','Gerente');

INSERT INTO logAtividade VALUES
(1,'Login',GETDATE()),
(2,'Cadastro',GETDATE());

INSERT INTO usuarioLog VALUES
('230e0d15-3fe0-4a23-ad1f-562d47de2b12',1),
('de18ef7e-7f09-4c39-a2fb-1ba0ae699efb',2);

INSERT INTO xpHistorico VALUES
(1,'Treino',GETDATE(),10),
(2,'Frequencia',GETDATE(),50);

INSERT INTO xpAluno VALUES
(1,'11111111-aaaa-4c2e-9c1a-aaaaaaaaaaaa'),
(2,'22222222-bbbb-4c2e-9c1a-bbbbbbbbbbbb');

INSERT INTO avaliacao VALUES 
(1,'11111111-aaaa-4c2e-9c1a-aaaaaaaaaaaa','Masculino',GETDATE(),80,1.80,30,24.7,90,12,1.2,1,1,1,1,1,1,1,1,1,1,1,1,70,10,5,15,95,85,90,30,30,50,50,35,'b1f9c2e4-1d6b-4c2e-9c9a-3e3c7d1a1111'),
(2,'22222222-bbbb-4c2e-9c1a-bbbbbbbbbbbb','Feminino',GETDATE(),65,1.65,28,23.8,85,11,1.1,1,1,1,1,1,1,1,1,1,1,1,1,55,12,4,14,90,80,95,28,28,48,48,34,'c2e7d9a1-7a3b-4d1a-b2e4-9a1c7e2b2222');

INSERT INTO ficha VALUES
(1,'Ficha A','Inicial',1,'11111111-aaaa-4c2e-9c1a-aaaaaaaaaaaa'),
(2,'Ficha B','Intermediario',2,'22222222-bbbb-4c2e-9c1a-bbbbbbbbbbbb');

INSERT INTO treino VALUES
(1,'Treino ABC',1,
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
}'),

(2,'Treino AB',2,
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
}');

INSERT INTO video VALUES
(1,GETDATE(),'Tutorial Supino','supino.mp4','f3c2a111-aaaa-4b2e-9c1a-111111111111');

INSERT INTO telefoneUsuario VALUES
('11999990001','230e0d15-3fe0-4a23-ad1f-562d47de2b12');

INSERT INTO telefoneAcademia VALUES
('1133330001','f3c2a111-aaaa-4b2e-9c1a-111111111111');

INSERT INTO frequencia VALUES
(1,'11111111-aaaa-4c2e-9c1a-aaaaaaaaaaaa','20240501',60);

INSERT INTO agenda VALUES
(1,'Agendado','20240501');

INSERT INTO agendaProfessor VALUES
(1,'b1f9c2e4-1d6b-4c2e-9c9a-3e3c7d1a1111');

INSERT INTO fichaMedica VALUES
(1,'11111111-aaaa-4c2e-9c1a-aaaaaaaaaaaa','Poeira','Asma','Nenhuma','Nenhuma','Nenhum','Nenhum');