USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorralDetector_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorralDetector_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[CorralDetector_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CorralDetector_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[CorralDetector_ObtenerPorID]
@CorralDetectorID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CorralDetectorID,
		OperadorID,
		CorralID,
		Activo
	FROM CorralDetector
	WHERE CorralDetectorID = @CorralDetectorID
	SET NOCOUNT OFF;
END

GO
