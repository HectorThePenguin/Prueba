DROP TABLE [dbo].[EmbarqueGastoTarifa]
GO
CREATE TABLE [dbo].[EmbarqueGastoTarifa] (
[EmbarqueGastoTarifaID] int NOT NULL IDENTITY(1,1) ,
[EmbarqueTarifaID] int NOT NULL ,
[GastoFijoID] int NOT NULL ,
[Activo] bit NOT NULL ,
[FechaCreacion] smalldatetime NOT NULL ,
[UsuarioCreacionID] int NOT NULL ,
[FechaModificacion] smalldatetime NULL ,
[UsuarioModificacionID] int NULL
) 

GO
ALTER TABLE [dbo].[EmbarqueGastoTarifa] ADD PRIMARY KEY ([EmbarqueGastoTarifaID])
GO
ALTER TABLE [dbo].[EmbarqueGastoTarifa] ADD FOREIGN KEY ([EmbarqueTarifaID]) REFERENCES [dbo].[EmbarqueTarifa] ([EmbarqueTarifaID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueGastoTarifa] ADD FOREIGN KEY ([GastoFijoID]) REFERENCES [dbo].[GastosFijos] ([GastoFijoID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueGastoTarifa] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[EmbarqueGastoTarifa] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO