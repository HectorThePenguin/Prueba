USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GenerarTablasTemporalesConsumo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GenerarTablasTemporalesConsumo]
GO
/****** Object:  StoredProcedure [dbo].[GenerarTablasTemporalesConsumo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/05/2015
-- Description: 
-- SpName     : GenerarTablasTemporalesConsumo
--======================================================
CREATE PROCEDURE [dbo].[GenerarTablasTemporalesConsumo]  

AS  
BEGIN --Crea la tabla AnimalCostoTemporal

TRUNCATE TABLE AnimalCostoTemporal

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalCostoTemporal_Animal]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [AnimalCostoTemporal] DROP CONSTRAINT [FK_AnimalCostoTemporal_Animal]


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalCostoTemporal_Costo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [AnimalCostoTemporal] DROP CONSTRAINT [FK_AnimalCostoTemporal_Costo]


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalCostoTemporal_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [AnimalCostoTemporal] DROP CONSTRAINT [FK_AnimalCostoTemporal_Usuario]


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalCostoTemporal_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [AnimalCostoTemporal] DROP CONSTRAINT [FK_AnimalCostoTemporal_UsuarioCreacion]


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AnimalCostoTemporal]') AND OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [AnimalCostoTemporal]


CREATE TABLE [AnimalCostoTemporal]
(
	[AnimalCostoTemporalID] bigint NOT NULL IDENTITY (1, 1),
	[AnimalID] bigint NOT NULL,
	[FechaCosto] smalldatetime NOT NULL,
	[CostoID] int NOT NULL,
	[TipoReferencia] int NOT NULL DEFAULT 0,
	[FolioReferencia] bigint NOT NULL,
	[Importe] decimal(17,2) NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL DEFAULT(GETDATE()),
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int	
)


ALTER TABLE [AnimalCostoTemporal] 
 ADD CONSTRAINT [PK_AnimalCostoTemporal]
	PRIMARY KEY CLUSTERED ([AnimalCostoTemporalID])


ALTER TABLE [AnimalCostoTemporal] ADD CONSTRAINT [FK_AnimalCostoTemporal_Animal]
	FOREIGN KEY ([AnimalID]) REFERENCES [Animal] ([AnimalID]) ON DELETE No Action ON UPDATE No Action


ALTER TABLE [AnimalCostoTemporal] ADD CONSTRAINT [FK_AnimalCostoTemporal_Costo]
	FOREIGN KEY ([CostoID]) REFERENCES [Costo] ([CostoID]) ON DELETE No Action ON UPDATE No Action


ALTER TABLE [AnimalCostoTemporal] ADD CONSTRAINT [FK_AnimalCostoTemporal_Usuario]
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID]) ON DELETE No Action ON UPDATE No Action


ALTER TABLE [AnimalCostoTemporal] ADD CONSTRAINT [FK_AnimalCostoTemporal_UsuarioCreacion]
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID]) ON DELETE No Action ON UPDATE No Action
END

BEGIN --Crea la tabla AnimalConsumoTemporal

TRUNCATE TABLE AnimalConsumoTemporal

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalConsumoTemporal_Animal]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [AnimalConsumoTemporal] DROP CONSTRAINT [FK_AnimalConsumoTemporal_Animal]


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalConsumoTemporal_Formula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [AnimalConsumoTemporal] DROP CONSTRAINT [FK_AnimalConsumoTemporal_Formula]


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalConsumoTemporal_Reparto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [AnimalConsumoTemporal] DROP CONSTRAINT [FK_AnimalConsumoTemporal_Reparto]
 

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalConsumoTemporal_TipoServicio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [AnimalConsumoTemporal] DROP CONSTRAINT [FK_AnimalConsumoTemporal_TipoServicio]
 

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalConsumoTemporal_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [AnimalConsumoTemporal] DROP CONSTRAINT [FK_AnimalConsumoTemporal_UsuarioCreacion]
 

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalConsumoTemporal_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [AnimalConsumoTemporal] DROP CONSTRAINT [FK_AnimalConsumoTemporal_UsuarioModificacion]
 

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AnimalConsumoTemporal]') AND OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [AnimalConsumoTemporal]
 

CREATE TABLE [AnimalConsumoTemporal]
(
	[AnimalConsumoTemporalID] bigint NOT NULL IDENTITY (1, 1),
	[AnimalID] bigint NOT NULL,
	[RepartoID] bigint NOT NULL,
	[FormulaIDServida] int NOT NULL,
	[Cantidad] decimal(12,3) NOT NULL,
	[TipoServicioID] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL DEFAULT(GETDATE()),
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int	
)
 

ALTER TABLE [AnimalConsumoTemporal] 
 ADD CONSTRAINT [PK_AnimalConsumoTemporal]
	PRIMARY KEY CLUSTERED ([AnimalConsumoTemporalID])
 

ALTER TABLE [AnimalConsumoTemporal] ADD CONSTRAINT [FK_AnimalConsumoTemporal_Animal]
	FOREIGN KEY ([AnimalID]) REFERENCES [Animal] ([AnimalID]) ON DELETE No Action ON UPDATE No Action
 

ALTER TABLE [AnimalConsumoTemporal] ADD CONSTRAINT [FK_AnimalConsumoTemporal_Formula]
	FOREIGN KEY ([FormulaIDServida]) REFERENCES [Formula] ([FormulaID]) ON DELETE No Action ON UPDATE No Action
 

ALTER TABLE [AnimalConsumoTemporal] ADD CONSTRAINT [FK_AnimalConsumoTemporal_Reparto]
	FOREIGN KEY ([RepartoID]) REFERENCES [Reparto] ([RepartoID]) ON DELETE No Action ON UPDATE No Action
 

ALTER TABLE [AnimalConsumoTemporal] ADD CONSTRAINT [FK_AnimalConsumoTemporal_TipoServicio]
	FOREIGN KEY ([TipoServicioID]) REFERENCES [TipoServicio] ([TipoServicioID]) ON DELETE No Action ON UPDATE No Action
 

ALTER TABLE [AnimalConsumoTemporal] ADD CONSTRAINT [FK_AnimalConsumoTemporal_UsuarioCreacion]
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID]) ON DELETE No Action ON UPDATE No Action
 

ALTER TABLE [AnimalConsumoTemporal] ADD CONSTRAINT [FK_AnimalConsumoTemporal_UsuarioModificacion]
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID]) ON DELETE No Action ON UPDATE No Action

END

GO
