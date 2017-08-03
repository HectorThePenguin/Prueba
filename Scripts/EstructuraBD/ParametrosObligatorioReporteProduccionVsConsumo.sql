IF NOT EXISTS (SELECT * FROM dbo.TipoParametro WHERE Descripcion = 'Parametro para Reporte Produccion Vs Consumo')
	INSERT INTO TipoParametro(Descripcion,Activo,FechaCreacion,UsuarioCreacionID)
	VALUES ('Parametro para Reporte Produccion Vs Consumo',1,GETDATE(),1)
GO

IF NOT EXISTS (SELECT * FROM dbo.Parametro WHERE Descripcion = 'Parametro para los idProducto para reporte')
	INSERT INTO Parametro(TipoParametroID,Descripcion,Clave,Activo,FechaCreacion,UsuarioCreacionID)
	VALUES((SELECT TipoParametroID FROM dbo.TipoParametro WHERE Descripcion = 'Parametro para Reporte Produccion Vs Consumo'),
         'Parametro para los idProducto para reporte','ProducReportPvC',1,GETDATE(),1)
GO

IF NOT EXISTS (SELECT * FROM dbo.ParametroOrganizacion WHERE ParametroID = (SELECT ParametroID FROM dbo.Parametro WHERE Descripcion = 'Parametro para los idProducto para reporte'))
	INSERT INTO ParametroOrganizacion(ParametroID,OrganizacionID,Valor,Activo,FechaCreacion,UsuarioCreacionID)
	VALUES((SELECT ParametroID FROM dbo.Parametro WHERE Descripcion = 'Parametro para los idProducto para reporte'),
         1,'1|2|3|6|7|9|8|318|319|320|321|327|368|369|370|373|374|375|376',1,GETDATE(),1)
GO