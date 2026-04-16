USE VITALITAS_DEV;
GO

SELECT * FROM usuario;
SELECT * FROM gestor;

UPDATE Usuario
SET senha = 123456789
WHERE idUsuario = '0CADF8CA-20D2-4E8E-8F80-DE475C099A28';

SELECT 
    g.idGestor,
    u.idUsuario,
    u.nome,
    u.email,
    u.tipoUsuario,
    u.senha,
    u.flag
FROM gestor g
INNER JOIN usuario u ON u.idUsuario = g.idUsuario;

