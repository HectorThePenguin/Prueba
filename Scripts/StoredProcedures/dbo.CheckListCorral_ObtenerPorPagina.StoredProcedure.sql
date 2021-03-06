USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListCorral_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListCorral_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[CheckListCorral_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListCorral_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[CheckListCorral_ObtenerPorPagina]
@CheckListCorralID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY CheckListCorralID ASC) AS [RowNum],
		CheckListCorralID,
		OrganizacionID,
		LoteID,
		PDF
		INTO #Datos
	FROM CheckListCorral
	WHERE Activo = @Activo
	SELECT
		CheckListCorralID,
		OrganizacionID,
		LoteID,
		PDF
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(CheckListCorralID) AS [TotalReg]
	FROM #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO
