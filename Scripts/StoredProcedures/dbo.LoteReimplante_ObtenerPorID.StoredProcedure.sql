USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteReimplante_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LoteReimplante_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[LoteReimplante_ObtenerPorID]
@LoteReimplanteID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		LoteReimplanteID,
		LoteProyeccionID,
		LoteProyeccionID,
		NumeroReimplante,
		FechaProyectada,
		PesoProyectado,
		FechaReal,
		PesoReal
	FROM LoteReimplante
	WHERE LoteReimplanteID = @LoteReimplanteID
	SET NOCOUNT OFF;
END

GO
