USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteReimplante_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LoteReimplante_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[LoteReimplante_ObtenerTodos]
@Activo BIT = NULL
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
	SET NOCOUNT OFF;
END

GO
