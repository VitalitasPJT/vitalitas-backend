SELECT *
FROM [VITALITAS_DEV].[dbo].[aluno]

SELECT *
FROM [VITALITAS_DEV].[dbo].[academia]

SELECT *
FROM [VITALITAS_DEV].[dbo].[planoContrato]

SELECT *
FROM [VITALITAS_DEV].[dbo].[contrato]

SELECT *
FROM [VITALITAS_DEV].[dbo].[usuario]

SELECT *
FROM [VITALITAS_DEV].[dbo].[instrutor]

SELECT *
FROM [VITALITAS_DEV].[dbo].[gestor]

SELECT *
FROM [VITALITAS_DEV].[dbo].[fichaMedica]

----
SELECT *
FROM [VITALITAS_DEV].[dbo].[usuario]
WHERE idUsuario = '579c5c4a-67c3-47a1-a0f5-dfc67ad8fe78'

SELECT idAcademia
FROM [VITALITAS_DEV].[dbo].[academia]

USE VITALITAS_DEV;
GO

SELECT * 
FROM dbo.usuario;

SELECT * FROM
usuario as u
join instrutor as i on u.idUsuario = i.idUsuario
WHERE i.idInstrutor = '590cfc8e-8dcd-4e03-9339-41525f9e94a3'


SELECT * FROM usuario
WHERE idAcademia = 'aaaa2222-aaaa-2222-aaaa-222222222222'

SELECT a.idAluno, u.idAcademia, u.idUsuario, u.tipoUsuario, a.objetivo, u.nome, u.email   
                            FROM aluno a
                            JOIN usuario u ON a.idUsuario = u.idUsuario
                            WHERE u.idAcademia = 'aaaa2222-aaaa-2222-aaaa-222222222222'

UPDATE usuario
SET senha = '123456'
WHERE idUsuario = 'DDDD3333-DDDD-3333-DDDD-333333333301';


SELECT * FROM usuario
WHERE senha = '123456'

update usuario
set flag = '0'
where senha = '123456';