USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CentroCosto_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CentroCosto_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[CentroCosto_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 28/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CentroCosto_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[CentroCosto_ObtenerPorID]
@CentroCostoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CentroCostoID,
		CentroCostoSAP,
		Descripcion,
		AreaDepartamento,
		Activo
	FROM CentroCosto
	WHERE CentroCostoID = @CentroCostoID
	SET NOCOUNT OFF;
END

GO
