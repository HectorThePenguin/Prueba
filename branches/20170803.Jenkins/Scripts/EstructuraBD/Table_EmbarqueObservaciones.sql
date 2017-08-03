DROP TABLE [dbo].[EmbarqueObservaciones]
GO
CREATE TABLE [dbo].[EmbarqueObservaciones]
(
	[EmbarqueObservacionesID] 	INT IDENTITY(1,1),
	[EmbarqueID] 				INT NOT NULL,
	[Observacion]  				VARCHAR (255) NULL,
	[Activo]					BIT NOT NULL,
	[FechaCreacion] 			SMALLDATETIME NOT NULL DEFAULT (GETDATE()),
	[UsuarioCreacionID] 		INT NOT NULL,
	[FechaModificacion] 		SMALLDATETIME DEFAULT '1900-01-01',
	[UsuarioModificacionID]		INT NULL
)
GO
ALTER TABLE [dbo].[EmbarqueObservaciones] ADD PRIMARY KEY ([EmbarqueObservacionesID])
GO
ALTER TABLE [dbo].[EmbarqueObservaciones] ADD FOREIGN KEY ([EmbarqueID]) REFERENCES [dbo].[Embarque] ([EmbarqueID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueObservaciones] ADD FOREIGN KEY ([OrganizacionID]) REFERENCES [dbo].[Organizacion] ([OrganizacionID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueObservaciones] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueObservaciones] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO