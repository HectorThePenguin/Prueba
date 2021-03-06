USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CentroCosto_ObtenerPorAutorizador]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CentroCosto_ObtenerPorAutorizador]
GO
/****** Object:  StoredProcedure [dbo].[CentroCosto_ObtenerPorAutorizador]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CentroCosto_ObtenerPorAutorizador 0,2 
--======================================================
CREATE PROCEDURE [dbo].[CentroCosto_ObtenerPorAutorizador] @CentroCostoID INT
	,@AutorizadorID INT
AS
BEGIN
	SELECT CentroCostoID
		,CentroCostoSAP
		,Descripcion
		,AreaDepartamento
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		,FechaModificacion
		,UsuarioModificacionID
	FROM CentroCosto cc
	WHERE @CentroCostoID in (cc.CentroCostoID, 0)
	And 
	EXISTS (
			SELECT ''
			FROM CentroCostoAutorizador
			WHERE CentroCostoID = cc.CentroCostoId
				AND @AutorizadorId IN (
					UsuarioIDAutorizador
					,0
					)
			)
END

GO
