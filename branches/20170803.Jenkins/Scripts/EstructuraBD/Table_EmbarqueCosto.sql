DROP TABLE [dbo].[EmbarqueCosto]
GO
CREATE TABLE [dbo].[EmbarqueCosto]
(
	[EmbarqueCostoID] 		INT IDENTITY(1,1) NOT NULL,
	[EmbarqueID] 			INT NOT NULL,
	[CostoID] 				INT NOT NULL,
	[Importe] 				DECIMAL(10,2) NOT NULL,
	[Activo] 				BIT NOT NULL,
	[FechaCreacion] 		SMALLDATETIME DEFAULT (GETDATE()),
	[UsuarioCreacionID] 	INT NULL,
	[FechaModificacion] 	SMALLDATETIME DEFAULT '1900-01-01',
	[UsuarioModificacionID]	INT NULL
)

GO
ALTER TABLE [dbo].[EmbarqueCosto] ADD PRIMARY KEY ([EmbarqueCostoID])
GO
ALTER TABLE [dbo].[EmbarqueCosto] ADD FOREIGN KEY ([EmbarqueID]) REFERENCES [dbo].[Embarque] ([EmbarqueID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueCosto] ADD FOREIGN KEY ([CostoID]) REFERENCES [dbo].[Costo] ([CostoID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueCosto] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueCosto] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO