USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalCosto_MoverTablaTemporal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalCosto_MoverTablaTemporal]
GO
/****** Object:  StoredProcedure [dbo].[AnimalCosto_MoverTablaTemporal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 16/07/2015
-- Description: Mueve la tabla de AnimalCostoTemporal a la tabla AnimalCosto
-- SpName     : EXEC AnimalCosto_MoverTablaTemporal 
--======================================================
CREATE PROCEDURE [dbo].[AnimalCosto_MoverTablaTemporal]

AS

SET NOCOUNT ON

INSERT INTO AnimalCosto
(
AnimalID
,FechaCosto
,CostoID
,TipoReferencia
,FolioReferencia
,Importe
,FechaCreacion
,UsuarioCreacionID
,FechaModificacion
,UsuarioModificacionID)

SELECT 
AnimalID
,FechaCosto
,CostoID
,TipoReferencia
,FolioReferencia
,Importe
,FechaCreacion
,UsuarioCreacionID
,FechaModificacion
,UsuarioModificacionID
FROM AnimalCostoTemporal

SET NOCOUNT OFF
	  




GO
