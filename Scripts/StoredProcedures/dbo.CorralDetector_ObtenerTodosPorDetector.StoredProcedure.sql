USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorralDetector_ObtenerTodosPorDetector]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorralDetector_ObtenerTodosPorDetector]
GO
/****** Object:  StoredProcedure [dbo].[CorralDetector_ObtenerTodosPorDetector]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : C�sar Valdez
-- Create date: 16/08/2014
-- Description: 
-- SpName     : CorralDetector_ObtenerTodosPorDetector 1
--======================================================
CREATE PROCEDURE [dbo].[CorralDetector_ObtenerTodosPorDetector]
	@OperadorID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CD.CorralDetectorID,
		CD.OperadorID,
		CD.CorralID,
		C.Codigo,
		TC.TipoCorralID,
		TC.Descripcion,
		CD.Activo
	 FROM CorralDetector CD
	INNER JOIN Corral C ON C.CorralID = CD.CorralID
	INNER JOIN TipoCorral TC ON TC.TipoCorralID = C.TipoCorralID
	WHERE CD.Activo = 1
	  AND CD.OperadorID = @OperadorID
	SET NOCOUNT OFF;
END

GO
