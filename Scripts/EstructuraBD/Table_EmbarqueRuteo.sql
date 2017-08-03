DROP TABLE [dbo].[EmbarqueRuteo]
GO
CREATE TABLE [dbo].[EmbarqueRuteo]
(
	[EmbarqueRuteoID] 		INT IDENTITY(1,1) NOT NULL,
	[EmbarqueID]			INT NULL,
	[OrganizacionID] 		INT NULL,
	[RuteoID] 				INT NULL,
	[Orden]  				INT NULL,
	[Kilometros]  			INT NULL,
	[Horas]  				TIME(0) NULL,
	[Recibido]  			BIT NULL,
	[FechaProgramada]  		SMALLDATETIME NOT NULL,
	[FechaCreacion] 		SMALLDATETIME DEFAULT (GETDATE()),
	[UsuarioCreacionID] 	INT NOT NULL,
	[FechaModificacion] 	SMALLDATETIME DEFAULT '1900-01-01',
	[UsuarioModificacionID]	INT NULL
)
GO
ALTER TABLE [dbo].[EmbarqueRuteo] ADD PRIMARY KEY ([EmbarqueRuteoID])
GO
ALTER TABLE [dbo].[EmbarqueRuteo] ADD FOREIGN KEY ([EmbarqueID]) REFERENCES [dbo].[Embarque] ([EmbarqueID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueRuteo] ADD FOREIGN KEY ([OrganizacionID]) REFERENCES [dbo].[Organizacion] ([OrganizacionID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueRuteo] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueRuteo] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueRuteo] ADD FOREIGN KEY ([RuteoID]) REFERENCES [dbo].[Ruteo] ([RuteoID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO