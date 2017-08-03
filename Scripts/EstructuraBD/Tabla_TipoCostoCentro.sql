DROP TABLE [dbo].[TipoCostoCentro]
GO
CREATE TABLE [dbo].[TipoCostoCentro] (
[TipoCostoCentroID] INT NOT NULL IDENTITY(1,1) ,
[Descripcion] VARCHAR(50) NULL ,
[Activo] BIT NOT NULL ,
[UsuarioCreacionID] int NOT NULL ,
[FechaCreacion] datetime NOT NULL ,
[UsuarioModificacionID] int NULL ,
[FechaModificacion] datetime NULL ,
)


GO
ALTER TABLE [dbo].[TipoCostoCentro] ADD PRIMARY KEY ([TipoCostoCentroID])
GO
ALTER TABLE [dbo].[TipoCostoCentro] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[TipoCostoCentro] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


