USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteDistribucionAlimento_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteDistribucionAlimento_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[LoteDistribucionAlimento_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 18/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LoteDistribucionAlimento_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[LoteDistribucionAlimento_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		LoteDistribucionAlimentoID,
		LoteID,
		TipoServicioID,
		EstatusDistribucionID,
		Fecha,
		Activo
	FROM LoteDistribucionAlimento
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
