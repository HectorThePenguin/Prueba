DROP TABLE [dbo].[Ruteo]
GO
CREATE TABLE [dbo].[Ruteo] (
[RuteoID] int NOT NULL IDENTITY(1,1) ,
[OrganizacionOrigenID] int NOT NULL ,
[OrganizacionDestinoID] int NOT NULL ,
[NombreRuteo] VARCHAR(255) NOT NULL ,
[Activo] bit NOT NULL DEFAULT ((1)) ,
[FechaCreacion] smalldatetime NOT NULL ,
[UsuarioCreacionID] int NOT NULL ,
[FechaModificacion] smalldatetime NULL,
[UsuarioModificacionID] int NULL
)


GO
ALTER TABLE [dbo].[Ruteo] ADD PRIMARY KEY ([RuteoID])
GO
ALTER TABLE [dbo].[Ruteo] ADD FOREIGN KEY ([OrganizacionOrigenID]) REFERENCES [dbo].[Organizacion] ([OrganizacionID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Ruteo] ADD FOREIGN KEY ([OrganizacionDestinoID]) REFERENCES [dbo].[Organizacion] ([OrganizacionID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Ruteo] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Ruteo] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO