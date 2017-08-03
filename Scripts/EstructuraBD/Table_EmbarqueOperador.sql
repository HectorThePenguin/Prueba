DROP TABLE [dbo].[EmbarqueOperador]
GO
CREATE TABLE [dbo].[EmbarqueOperador]
(
	[EmbarqueOperadorID] 	INT IDENTITY(1,1) NOT NULL,
	[EmbarqueID] 			INT NOT NULL,
	[ProveedorChoferID] 	INT NOT NULL,
	[NumOperador]  			SMALLINT NOT NULL,
	[NumTelefonico]   		INT NULL,
	[Activo] 				BIT NOT NULL,	
	[FechaCreacion] 		SMALLDATETIME DEFAULT (GETDATE()),
	[UsuarioCreacionID] 	INT NOT NULL,
	[FechaModificacion] 	SMALLDATETIME DEFAULT '1900-01-01',
	[UsuarioModificacionID]	INT NULL
)

GO
ALTER TABLE [dbo].[EmbarqueOperador] ADD PRIMARY KEY ([EmbarqueOperadorID])
GO
ALTER TABLE [dbo].[EmbarqueOperador] ADD FOREIGN KEY ([EmbarqueID]) REFERENCES [dbo].[Embarque] ([EmbarqueID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueOperador] ADD FOREIGN KEY ([ProveedorChoferID]) REFERENCES [dbo].[ProveedorChofer] ([ProveedorChoferID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueOperador] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueOperador] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO