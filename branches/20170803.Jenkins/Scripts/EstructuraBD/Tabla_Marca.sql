DROP TABLE [dbo].[Marca]
GO
CREATE TABLE [dbo].[Marca](
	[MarcaID] int NOT NULL IDENTITY(1,1),
	[Descripcion] VARCHAR(255) NOT NULL,
	[Tracto] bit NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID]	int NOT NULL,
	[FechaModificacion]	smalldatetime NULL,
	[UsuarioModificacionID]	int NULL
)

GO
ALTER TABLE [dbo].[Marca] ADD PRIMARY KEY ([MarcaID])
GO
ALTER TABLE [dbo].[Marca] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Marca] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO