USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CentroCosto_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CentroCosto_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[CentroCosto_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 28/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CentroCosto_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[CentroCosto_ObtenerPorDescripcion]
@Descripcion varchar(100)
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
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
