DROP TABLE USUARIO;

CREATE TABLE USUARIO
(
ID_USUARIO      VARCHAR(100),
DATA_NASCIMENTO DATE,
SENHA           VARCHAR(20),
CPF             VARCHAR(14),
NOME            VARCHAR(100),
TELEFONE        VARCHAR(11),
EMAIL           VARCHAR (60),
);

INSERT INTO USUARIO (ID_USUARIO, DATA_NASCIMENTO, SENHA, CPF, NOME, TELEFONE, EMAIL)
VALUES 
('U001', '1995-04-15', 'Senha@123', '123.456.789-00', 'Ana Clara Silva', '61999998888', 'ana.silva@example.com'),
('U002', '1988-09-22', 'Teste#456', '987.654.321-00', 'Bruno Oliveira Santos', '11988887777', 'bruno.oliveira@example.com'),
('U003', '2000-12-05', 'Abc@2024', '321.654.987-00', 'Carla Mendes', '21977776666', 'carla.mendes@example.com'),
('U004', '1992-06-30', 'Senha$789', '456.789.123-00', 'Diego Pereira Costa', '31966665555', 'diego.costa@example.com'),
('U005', '1998-11-10', 'User123!', '654.321.987-00', 'Elisa Rocha', '71955554444', 'elisa.rocha@example.com');
