DROP TABLE [dbo].[RuteoDetalle]
GO
CREATE TABLE [dbo].[RuteoDetalle] (
[RuteoDetalleID] int NOT NULL IDENTITY(1,1) ,
[RuteoID] int NOT NULL,
[OrganizacionOrigenID] int NOT NULL ,
[OrganizacionDestinoID] int NOT NULL ,
[Kilometros] DECIMAL(10,2) NOT NULL ,
[Horas] DECIMAL(4,1) NOT NULL ,
[Activo] bit NOT NULL DEFAULT ((1)) ,
[FechaCreacion] smalldatetime NOT NULL ,
[UsuarioCreacionID] int NOT NULL ,
[FechaModificacion] smalldatetime NULL ,
[UsuarioModificacionID] int NULL 
)


GO
ALTER TABLE [dbo].[RuteoDetalle] ADD PRIMARY KEY ([RuteoDetalleID])
GO
ALTER TABLE [dbo].[RuteoDetalle] ADD FOREIGN KEY ([RuteoID]) REFERENCES [dbo].[Ruteo] ([RuteoID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[RuteoDetalle] ADD FOREIGN KEY ([OrganizacionOrigenID]) REFERENCES [dbo].[Organizacion] ([OrganizacionID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[RuteoDetalle] ADD FOREIGN KEY ([OrganizacionDestinoID]) REFERENCES [dbo].[Organizacion] ([OrganizacionID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[RuteoDetalle] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[RuteoDetalle] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO