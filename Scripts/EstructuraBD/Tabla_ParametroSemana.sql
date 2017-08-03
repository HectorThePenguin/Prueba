DROP TABLE [dbo].[ParametroSemana]
GO
CREATE TABLE [dbo].[ParametroSemana] (
[ParametroSemanaID] INT NOT NULL IDENTITY(1,1) ,
[Descripcion] VARCHAR(50) NOT NULL ,
[Lunes] BIT NOT NULL ,
[Martes] BIT NOT NULL ,
[Miercoles] BIT NOT NULL ,
[Jueves] BIT NOT NULL ,
[Viernes] BIT NOT NULL ,
[Sabado] BIT NOT NULL ,
[Domingo] BIT NOT NULL ,
[UsuarioCreacionID] INT NOT NULL ,
[FechaCreacion] smalldatetime NOT NULL ,
[UsuarioModificacionID] INT NULL ,
[FechaModificacion] smalldatetime NULL 
)


GO
ALTER TABLE [dbo].[ParametroSemana] ADD PRIMARY KEY ([ParametroSemanaID])
GO
ALTER TABLE [dbo].[ParametroSemana] ADD FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[ParametroSemana] ADD FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [dbo].[Usuario] ([UsuarioID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

