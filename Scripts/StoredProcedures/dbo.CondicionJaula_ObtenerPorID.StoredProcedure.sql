USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CondicionJaula_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CondicionJaula_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[CondicionJaula_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 22/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CondicionJaula_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[CondicionJaula_ObtenerPorID]
@CondicionJaulaID int
AS 
BEGIN
	SET NOCOUNT ON;
	SELECT
		CondicionJaulaID,
		Descripcion,
		Activo
	FROM CondicionJaula
	WHERE CondicionJaulaID = @CondicionJaulaID
	SET NOCOUNT OFF;
END

GO
