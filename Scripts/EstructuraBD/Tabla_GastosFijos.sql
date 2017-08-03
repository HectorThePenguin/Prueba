CREATE TABLE [dbo].[GastosFijos](
	GastoFijoID int NOT NULL IDENTITY(1,1)  ,
	Descripcion varchar(250) NOT NULL ,
	Activo bit NOT NULL ,
	Importe decimal(18,2) NULL ,
	FechaCreacion smalldatetime NOT NULL ,
	UsuarioCreacionID int NOT NULL ,
	FechaModificacion smalldatetime NULL ,
	UsuarioModificacionID int NULL 
)
GO
ALTER TABLE [dbo].[GastosFijos] ADD PRIMARY KEY ([GastoFijoID])
GO
ALTER TABLE [dbo].[GastosFijos] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[GastosFijos] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO