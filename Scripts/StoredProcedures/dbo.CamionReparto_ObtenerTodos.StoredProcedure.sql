USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CamionReparto_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CamionReparto_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[CamionReparto_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 28/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CamionReparto_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[CamionReparto_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CamionRepartoID,
		OrganizacionID,
		CentroCostoID,
		NumeroEconomico,
		Activo
	FROM CamionReparto
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
