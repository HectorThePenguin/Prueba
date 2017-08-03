DROP TABLE [dbo].[CuentaGasto]
GO
CREATE TABLE [dbo].[CuentaGasto] (
[CuentaGastoID] bigint NOT NULL IDENTITY(1,1) ,
[OrganizacionID] int NULL ,
[CuentaSAPID] int NOT NULL ,
[CostoID] int NOT NULL ,
[Activo] bit NOT NULL DEFAULT ((1)) ,
[UsuarioCreacionID] int NOT NULL ,
[FechaCreacion] datetime NOT NULL ,
[UsuarioModificacionID] int NULL ,
[FechaModificacion] datetime NULL 
)


GO
ALTER TABLE [dbo].[CuentaGasto] ADD PRIMARY KEY ([CuentaGastoID])
GO
ALTER TABLE [dbo].[CuentaGasto] ADD FOREIGN KEY ([OrganizacionID]) REFERENCES [dbo].[Organizacion] ([OrganizacionID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[CuentaGasto] ADD FOREIGN KEY ([CuentaSAPID]) REFERENCES [dbo].[CuentaSAP] ([CuentaSAPID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[CuentaGasto] ADD FOREIGN KEY ([CostoID]) REFERENCES [dbo].[Costo] ([CostoID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[CuentaGasto] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[CuentaGasto] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

