

IF NOT EXISTS (SELECT * FROM dbo.Parametro WHERE Descripcion = 'Horas Carga CA')
	INSERT INTO Parametro(TipoParametroID,Descripcion,Clave,Activo,FechaCreacion,UsuarioCreacionID)
	VALUES((SELECT TipoParametroID FROM dbo.TipoParametro WHERE Descripcion = 'Parametros Generales'),
         'Horas Carga CA','HorasCargaCA',1,GETDATE(),1)
GO

IF NOT EXISTS (SELECT * FROM dbo.ParametroGeneral WHERE ParametroID = (SELECT ParametroID FROM dbo.Parametro WHERE Descripcion = 'Horas Carga CA'))
	INSERT INTO ParametroOrganizacion(ParametroID,Valor,Activo,FechaCreacion,UsuarioCreacionID)
	VALUES((SELECT ParametroID FROM dbo.Parametro WHERE Descripcion = 'Horas Carga CA'),
         '3',1,GETDATE(),1)
GO



IF NOT EXISTS (SELECT * FROM dbo.Parametro WHERE Descripcion = 'Horas Carga CD')
	INSERT INTO Parametro(TipoParametroID,Descripcion,Clave,Activo,FechaCreacion,UsuarioCreacionID)
	VALUES((SELECT TipoParametroID FROM dbo.TipoParametro WHERE Descripcion = 'Parametros Generales'),
         'Horas Carga CD','HorasCargaCD',1,GETDATE(),1)
GO

IF NOT EXISTS (SELECT * FROM dbo.ParametroGeneral WHERE ParametroID = (SELECT ParametroID FROM dbo.Parametro WHERE Descripcion = 'Horas Carga CD'))
	INSERT INTO ParametroOrganizacion(ParametroID,Valor,Activo,FechaCreacion,UsuarioCreacionID)
	VALUES((SELECT ParametroID FROM dbo.Parametro WHERE Descripcion = 'Horas Carga CD'),
         '5',1,GETDATE(),1)
GO


