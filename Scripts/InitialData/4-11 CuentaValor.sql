--Cuenta De Inventario Ganadera E Intensivo
INSERT INTO CuentaValor (CuentaID,OrganizacionID,Valor,Activo,FechaCreacion,UsuarioCreacionID) 
SELECT C.CuentaID, OrganizacionID, CASE C.CuentaID WHEN 1 THEN '1116001' WHEN 3 THEN '1116014' END, 1, GETDATE(), 1
FROM Organizacion O 
JOIN Cuenta C ON C.CuentaID IN (1,3)
WHERE O.TipoOrganizacionID = 2

--Cuenta De Inventario En Transito
INSERT INTO CuentaValor (CuentaID,OrganizacionID,Valor,Activo,FechaCreacion,UsuarioCreacionID) 
SELECT C.CuentaID, OrganizacionID, '1114' + REPLICATE('0',3-LEN(OrganizacionID)) + CAST( OrganizacionID AS VARCHAR), 1, GETDATE(), 1
FROM Organizacion O 
JOIN Cuenta C ON C.CuentaID = 6
WHERE O.TipoOrganizacionID IN (4,5,6,7)

--CUENTAS DE COSTOS
INSERT INTO CuentaValor (CuentaID,OrganizacionID,Valor,Activo,FechaCreacion,UsuarioCreacionID) 
SELECT C.CuentaID, OrganizacionID, '5001' + REPLICATE('0',3-LEN(OrganizacionID)) + CAST( OrganizacionID AS VARCHAR), 1, GETDATE(), 1
FROM Organizacion O 
JOIN Cuenta C ON C.CuentaID = 7
WHERE O.TipoOrganizacionID = 2

--CUENTAS BENEFICIOS
INSERT INTO CuentaValor (CuentaID,OrganizacionID,Valor,Activo,FechaCreacion,UsuarioCreacionID) 
SELECT C.CuentaID, O.OrganizacionID, '4001001002', 1, GETDATE(), 1
FROM Organizacion O 
JOIN Cuenta C ON C.CuentaID IN (8)
WHERE O.TipoOrganizacionID = 2

--CUENTA COSTOS MP
INSERT INTO CuentaValor (CuentaID,OrganizacionID,Valor,Activo,FechaCreacion,UsuarioCreacionID) 
SELECT C.CuentaID, O.OrganizacionID, '5003004', 1, GETDATE(), 1
FROM Organizacion O 
JOIN Cuenta C ON C.CuentaID IN (9)
WHERE O.TipoOrganizacionID = 2
--CUENTA BENEFICIOS MP
INSERT INTO CuentaValor (CuentaID,OrganizacionID,Valor,Activo,FechaCreacion,UsuarioCreacionID) 
SELECT C.CuentaID, O.OrganizacionID, '4003004', 1, GETDATE(), 1
FROM Organizacion O 
JOIN Cuenta C ON C.CuentaID IN (10)
WHERE O.TipoOrganizacionID = 2
