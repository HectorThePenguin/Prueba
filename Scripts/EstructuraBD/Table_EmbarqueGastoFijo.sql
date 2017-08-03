DROP TABLE [dbo].[EmbarqueGastoFijo]
GO
CREATE TABLE [dbo].[EmbarqueGastoFijo]
(
	[EmbarqueGastoFijoID] 	INT IDENTITY(1,1) NOT NULL,
	[EmbarqueID] 			INT NOT NULL,
	[GastoFijoID] 			INT NOT NULL,
	[Importe] 				DECIMAL(10,2) NOT NULL,
	[Activo] 				BIT NOT NULL,
	[FechaCreacion] 		SMALLDATETIME DEFAULT (GETDATE()),
	[UsuarioCreacionID] 	INT NULL,
	[FechaModificacion] 	SMALLDATETIME DEFAULT '1900-01-01',
	[UsuarioModificacionID]	INT NULL
)

GO
ALTER TABLE [dbo].[EmbarqueGastoFijo] ADD PRIMARY KEY ([EmbarqueGastoFijoID])
GO
ALTER TABLE [dbo].[EmbarqueGastoFijo] ADD FOREIGN KEY ([EmbarqueID]) REFERENCES [dbo].[Embarque] ([EmbarqueID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueGastoFijo] ADD FOREIGN KEY ([GastoFijoID]) REFERENCES [dbo].[GastosFijos] ([GastoFijoID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueGastoFijo] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueGastoFijo] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO