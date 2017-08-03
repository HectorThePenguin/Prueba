USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[NivelAlerta_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramón Abel Atondo Echavarria
-- Create date: 15/03/2016
-- Description: SP par obtener los registros de nivel alertas
-- SpName     : dbo.NivelAlerta_ObtenerPorPagina
-- --======================================================
CREATE PROCEDURE [dbo].[NivelAlerta_ObtenerPorPagina]
@Descripcion varchar(255),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		Ac.NivelAlertaID,
		Ac.Descripcion,
		Ac.Activo
	INTO #NivelAlerta
	FROM NivelAlerta Ac
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
	AND Activo = @Activo
	SELECT
		NivelAlertaID,
		Descripcion,
		Activo
	FROM #NivelAlerta
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(NivelAlertaID) AS [TotalReg]
	FROM #NivelAlerta
	DROP TABLE #NivelAlerta
	SET NOCOUNT OFF;
END
