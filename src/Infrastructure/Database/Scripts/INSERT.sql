USE VITALITAS_DEV;
GO

INSERT INTO Usuario VALUES
(1,'João Silva','joao@email.com','Q1','Rua A','Centro','Brasilia','DF','01001000','123','19900515','11111111111',1,1),
(2,'Maria Santos','maria@email.com','Q2','Rua B','Centro','Brasilia','DF','02002000','123','19920822','22222222222',1,1),
(3,'Ricardo Oliveira','ric@email.com','Q3','Rua C','Centro','Brasilia','DF','03003000','123','19850310','33333333333',2,1),
(4,'Ana Costa','ana@email.com','Q4','Rua D','Centro','Brasilia','DF','04004000','123','19951201','44444444444',1,1),
(5,'Carlos Eduardo','carlos@email.com','Q5','Rua E','Centro','Brasilia','DF','05005000','123','19800120','55555555555',3,1),
(6,'Fernanda Lima','fer@email.com','Q6','Rua F','Centro','Brasilia','DF','06006000','123','19980711','66666666666',1,1),
(7,'Gabriel Souza','gab@email.com','Q7','Rua G','Centro','Brasilia','DF','07007000','123','19881125','77777777777',2,1),
(8,'Juliana Paes','ju@email.com','Q8','Rua H','Centro','Brasilia','DF','08008000','123','19930214','88888888888',1,1),
(9,'Roberto Carlos','rob@email.com','Q9','Rua I','Centro','Brasilia','DF','09009000','123','19910905','99999999999',1,1),
(10,'Sandra Mello','sand@email.com','Q10','Rua J','Centro','Brasilia','DF','10010000','123','19941010','00000000000',1,1);

INSERT INTO Gestor VALUES
(1,5);

INSERT INTO Instrutor VALUES
(1,'123456',3),
(2,'654321',7);

INSERT INTO Plano_licenca VALUES
(1,'Bronze','Basico',500),
(2,'Silver','Intermediario',800),
(3,'Gold','Completo',1200);

INSERT INTO Plano_contrato VALUES
(1,'Mensal','Plano mensal',100),
(2,'Trimestral','Plano 3 meses',270),
(3,'Anual','Plano anual',900);

INSERT INTO Licenca VALUES
(1,'Ativo','Anual','20250101','lic1.pdf',GETDATE(),1,500),
(2,'Ativo','Mensal','20240601','lic2.pdf',GETDATE(),2,800),
(3,'Ativo','Anual','20260101','lic3.pdf',GETDATE(),3,1200);

INSERT INTO Academia VALUES
(1,'Vitalitas Matriz','12345678000101','matriz@email.com','Musculação','01001000',1,1),
(2,'Vitalitas Sul','12345678000102','sul@email.com','Crossfit','02002000',2,1);

INSERT INTO Contrato VALUES
(1,'Vigente','20250501',GETDATE(),1,100),
(2,'Vigente','20240801',GETDATE(),2,270),
(3,'Vigente','20260101',GETDATE(),3,900);

INSERT INTO Aluno VALUES
(1,'Perder peso',1,1,1),
(2,'Perder peso',2,2,1),
(3,'Perder peso',4,1,1),
(4,'Perder peso',6,2,1);

INSERT INTO Funcionario VALUES
(1,5,'Gerente'),
(2,3,'Instrutor'),
(3,7,'Instrutor');

INSERT INTO Exercicios VALUES
(1,3,'Supino',12,'Banco','Peito'),
(2,4,'Agachamento',10,'Gaiola','Perna'),
(3,3,'Rosca',12,'Barra','Biceps');

INSERT INTO Agenda VALUES
(1,'Agendado','20240501'),
(2,'Concluido','20240502');

INSERT INTO Log_atividade VALUES
(1,'Login',GETDATE()),
(2,'Cadastro',GETDATE());

INSERT INTO Usuario_log VALUES
(1,1),
(2,2);

INSERT INTO Xp_historico VALUES
(1,'Treino',GETDATE(),10),
(2,'Frequencia',GETDATE(),50);

INSERT INTO Xp_aluno VALUES
(1,1),
(2,2);

INSERT INTO Avaliacao VALUES 
(1,1,'Masculino',GETDATE(),80,1.80,30,24.7,90,12,1.2,1,1,1,1,1,1,1,1,1,1,1,1,70,10,5,15,95,85,90,30,30,50,50,35,1),
(2,2,'Feminino',GETDATE(),65,1.65,28,23.8,85,11,1.1,1,1,1,1,1,1,1,1,1,1,1,1,55,12,4,14,90,80,95,28,28,48,48,34,2);

INSERT INTO Ficha VALUES
(1,'Ficha A','Inicial',1),
(2,'Ficha B','Intermediario',2);

INSERT INTO Treinos VALUES
(1,'A','Peito',1),
(2,'B','Costas',1),
(3,'A','Pernas',2);

INSERT INTO Treino_exercicio VALUES
(1,1),
(2,3),
(3,2);

INSERT INTO Video VALUES
(1,GETDATE(),'Tutorial Supino','supino.mp4',1),
(2,GETDATE(),'Tutorial Agachamento','agachamento.mp4',1);

INSERT INTO Telefone_usuario VALUES
('11999990001',1),
('11999990002',2),
('11999990003',3);

INSERT INTO Telefone_academia VALUES
('1133330001',1),
('1133330002',2);

INSERT INTO Frequencia VALUES
(1,1,'20240501',60),
(2,2,'20240502',55);

INSERT INTO Agenda_professor VALUES
(1,1),
(2,2);

INSERT INTO Ficha_medica VALUES
(1,1,'Poeira','Asma','Nenhuma','Nenhuma','Nenhum','Nenhum'),
(2,2,'Nenhuma','Joelho','Nenhuma','Nenhuma','Nenhum','Nenhum');