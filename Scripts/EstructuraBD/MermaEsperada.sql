DROP TABLE [dbo].[MermaEsperada]
GO
CREATE TABLE [dbo].[MermaEsperada] (
[MermaEsperadaID] int NOT NULL IDENTITY(1,1) ,
[OrganizacionOrigenID] int NOT NULL ,
[OrganizacionDestinoID] int NOT NULL ,
[Merma] decimal(12,2) NOT NULL ,
[Activo] BIT DEFAULT(1),
[UsuarioCreacionID] int NOT NULL ,
[FechaCreacion] DATETIME NOT NULL ,
[UsuarioModificacionID] int NULL ,
[FechaModificacion] DATETIME NULL 
)
GO

ALTER TABLE [dbo].[MermaEsperada] ADD PRIMARY KEY ([MermaEsperadaID])
GO
ALTER TABLE [dbo].[MermaEsperada] ADD FOREIGN KEY ([OrganizacionDestinoID]) REFERENCES [dbo].[Organizacion] ([OrganizacionID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[MermaEsperada] ADD FOREIGN KEY ([OrganizacionOrigenID]) REFERENCES [dbo].[Organizacion] ([OrganizacionID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[MermaEsperada] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[MermaEsperada] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO