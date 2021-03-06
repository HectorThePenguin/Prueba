USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerCentroCostoSAPPorCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Costo_ObtenerCentroCostoSAPPorCosto]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerCentroCostoSAPPorCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Andres Vejar
-- Create date: 17/07/2014
-- Description: Obtiene el centro de costo sap para un costoid
-- Costo_ObtenerPorID 1
--=============================================
CREATE PROCEDURE [dbo].[Costo_ObtenerCentroCostoSAPPorCosto] @CostoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT C.CentroCostoID
		,C.CentroCostoSAP
		,C.Descripcion
		,C.AreaDepartamento
		,C.Activo
		,C.FechaCreacion
		,C.UsuarioCreacionID
		,C.FechaModificacion
		,C.UsuarioModificacionID
	FROM
	GastoCentroCosto G 
	INNER JOIN CentroCosto C on G.CentroCostoID = C.CentroCostoID
	WHERE CostoID = @CostoID AND G.Activo = 1 AND C.Activo = 1
	SET NOCOUNT OFF;
END

GO
