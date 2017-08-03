DROP TABLE [dbo].[PedidoGanado]
GO
CREATE TABLE [dbo].[PedidoGanado] (
[PedidoGanadoID] INT NOT NULL IDENTITY(1,1) ,
[OrganizacionID] INT NOT NULL ,
[CabezasPromedio] INT NOT NULL ,
[FechaInicio] smalldatetime NOT NULL ,
[Lunes] INT NOT NULL ,
[Martes] INT NOT NULL ,
[Miercoles] INT NOT NULL ,
[Jueves] INT NOT NULL ,
[Viernes] INT NOT NULL ,
[Sabado] INT NOT NULL ,
[Domingo] INT NOT NULL ,
[FechaCreacion] smalldatetime NOT NULL ,
[UsuarioCreacionID] INT NOT NULL ,
[FechaModificacion] smalldatetime NULL ,
[UsuarioModificacionID] INT NULL 
)


GO
ALTER TABLE [dbo].[PedidoGanado] ADD PRIMARY KEY ([PedidoGanadoID])
GO
ALTER TABLE [dbo].[PedidoGanado] ADD FOREIGN KEY ([OrganizacionID]) REFERENCES [dbo].[Organizacion] ([OrganizacionID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[PedidoGanado] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[PedidoGanado] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

