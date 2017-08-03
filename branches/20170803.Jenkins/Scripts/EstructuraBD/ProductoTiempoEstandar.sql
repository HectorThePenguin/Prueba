
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProductoTiempoEstandar]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)DROP TABLE [ProductoTiempoEstandar]
GO

CREATE TABLE [dbo].[ProductoTiempoEstandar] (
[ProductoTiempoEstandarID] int NOT NULL IDENTITY(1,1) ,
[ProductoID] int NOT NULL ,
[Tiempo] time(7) NOT NULL ,
[Activo] bit NOT NULL DEFAULT (1),
[UsuarioCreacionID] int NOT NULL ,
[FechaCreacion] datetime NOT NULL ,
[UsuarioModificacionID] int NULL ,
[FechaModificacion] datetime NULL ,
PRIMARY KEY (ProductoTiempoEstandarID) 
)
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[ProductoTiempoEstandar]
-- ----------------------------
ALTER TABLE [dbo].[ProductoTiempoEstandar] ADD FOREIGN KEY ([ProductoID]) REFERENCES [dbo].[Producto] ([ProductoID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[ProductoTiempoEstandar] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[ProductoTiempoEstandar] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO