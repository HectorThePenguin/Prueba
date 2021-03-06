USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalConsumo_MoverTablaTemporal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalConsumo_MoverTablaTemporal]
GO
/****** Object:  StoredProcedure [dbo].[AnimalConsumo_MoverTablaTemporal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 16/07/2015
-- Description: Mueve la tabla de AnimalConsumoTemporal a la tabla AnimalConsumo
-- SpName     : EXEC AnimalConsumo_MoverTablaTemporal 
--======================================================
CREATE PROCEDURE [dbo].[AnimalConsumo_MoverTablaTemporal]

AS

SET NOCOUNT ON

INSERT INTO AnimalConsumo
(
AnimalID
,RepartoID
,FormulaIDServida
,Cantidad
,TipoServicioID
,Fecha
,Activo
,FechaCreacion
,UsuarioCreacionID
,FechaModificacion
,UsuarioModificacionID)

SELECT 
AnimalID
,RepartoID
,FormulaIDServida
,Cantidad
,TipoServicioID
,Fecha
,Activo
,FechaCreacion
,UsuarioCreacionID
,FechaModificacion
,UsuarioModificacionID
FROM AnimalConsumoTemporal

SET NOCOUNT OFF
	  




GO
