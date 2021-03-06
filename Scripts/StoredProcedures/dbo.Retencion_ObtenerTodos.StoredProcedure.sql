USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Retencion_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Retencion_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Retencion_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/12/02
-- Description: 
-- Retencion_ObtenerTodos
--=============================================
CREATE PROCEDURE [dbo].[Retencion_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		RetencionID,
		Descripcion,
		Activo,
		IndicadorImpuesto,
		IndicadorRetencion,
		TipoRetencion
	FROM Retencion
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
