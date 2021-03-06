USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CriteriosSupervision_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CriteriosSupervision_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[CriteriosSupervision_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Ricardo Lopez
-- Create date: 18/02/2014
-- Description:  Obtiene Nombre del Detector Evaluado.
-- EXEC CriteriosSupervision_ObtenerTodos
-- =============================================
CREATE PROCEDURE [dbo].[CriteriosSupervision_ObtenerTodos]	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT CriterioSupervisionId, Descripcion, ValorInicial, ValorFinal, CodigoColor, Activo
	FROM CriterioSupervision
	WHERE Activo = 1
SET NOCOUNT OFF;
END

GO
