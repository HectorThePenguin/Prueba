DROP TABLE [dbo].[EmbarqueSeguimiento]
GO
CREATE TABLE [dbo].[EmbarqueSeguimiento]
(
	[EmbarqueSeguimientoID] INT IDENTITY(1,1) NOT NULL,
	[EmbarqueID]			INT NULL,
	[ObservacionesTransito] VARCHAR(255) NULL,
	[Activo]  				BIT NULL,
	[FechaCreacion] 		SMALLDATETIME DEFAULT (GETDATE()),
	[UsuarioCreacionID] 	INT NOT NULL,
	[FechaModificacion] 	SMALLDATETIME NULL,
	[UsuarioModificacionID]	INT NULL
)
GO
ALTER TABLE [dbo].[EmbarqueSeguimiento] ADD PRIMARY KEY ([EmbarqueSeguimientoID])
GO
ALTER TABLE [dbo].[EmbarqueSeguimiento] ADD FOREIGN KEY ([EmbarqueID]) REFERENCES [dbo].[Embarque] ([EmbarqueID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueSeguimiento] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueSeguimiento] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO