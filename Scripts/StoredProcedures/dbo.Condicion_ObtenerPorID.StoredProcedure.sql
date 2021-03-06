USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Condicion_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Condicion_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Condicion_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Condicion_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[Condicion_ObtenerPorID]
@CondicionID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CondicionID,
		Descripcion,
		Activo
	FROM Condicion
	WHERE CondicionID = @CondicionID
	SET NOCOUNT OFF;
END

GO
