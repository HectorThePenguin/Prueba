DROP TABLE [dbo].[PedidoGanadoEspejo]
GO
CREATE TABLE [dbo].[PedidoGanadoEspejo] (
[PedidoGanadoEspejoID] int NOT NULL IDENTITY(1,1) ,
[PedidoGanadoID] int NOT NULL ,
[OrganizacionID] int NOT NULL ,
[CabezasPromedio] int NOT NULL ,
[FechaInicio] smalldatetime NOT NULL ,
[Lunes] int NOT NULL ,
[Martes] int NOT NULL ,
[Miercoles] int NOT NULL ,
[Jueves] int NOT NULL ,
[Viernes] int NOT NULL ,
[Sabado] int NOT NULL ,
[Domingo] int NOT NULL ,
[Justificacion] VARCHAR(255) NOT NULL ,
[Estatus] bit  NULL ,
[UsuarioSolicitanteID] int NOT NULL ,
[UsuarioAproboID] int  NULL ,
[Activo] bit NOT NULL DEFAULT ((1)) ,
[FechaCreacion] smalldatetime NOT NULL ,
[UsuarioCreacionID] int NOT NULL ,
[FechaModificacion] smalldatetime NULL ,
[UsuarioModificacionID] int NULL 
)


GO
ALTER TABLE [dbo].[PedidoGanadoEspejo] ADD PRIMARY KEY ([PedidoGanadoEspejoID])
GO
ALTER TABLE [dbo].[PedidoGanadoEspejo] ADD FOREIGN KEY ([OrganizacionID]) REFERENCES [dbo].[Organizacion] ([OrganizacionID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[PedidoGanadoEspejo] ADD FOREIGN KEY ([PedidoGanadoID]) REFERENCES [dbo].[PedidoGanado] ([PedidoGanadoID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[PedidoGanadoEspejo] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[PedidoGanadoEspejo] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[PedidoGanadoEspejo] ADD FOREIGN KEY ([UsuarioAproboID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[PedidoGanadoEspejo] ADD FOREIGN KEY ([UsuarioSolicitanteID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

