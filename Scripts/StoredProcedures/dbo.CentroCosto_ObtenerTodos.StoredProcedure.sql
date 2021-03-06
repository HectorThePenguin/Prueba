USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CentroCosto_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CentroCosto_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[CentroCosto_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 28/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CentroCosto_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[CentroCosto_ObtenerTodos]
@Activo BIT = NULL
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
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
