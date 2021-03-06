USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Rol_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Rol_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Rol_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Modificado :	Torres Lugo Manuel
--		se incluyeron los campos de NivelAlertaID y el 
-- 		campo Descripcion de la tabla NivelAlerta
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Rol_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[Rol_ObtenerPorPagina]
@RolID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY R.Descripcion ASC) AS [RowNum],
		R.RolID,
		R.Descripcion,
		R.Activo,
		ISNULL(R.NivelAlertaID,0) AS NivelAlertaID,
		NA.Descripcion AS DescripcionNivel
	INTO #Rol
	FROM Rol AS R
	LEFT JOIN NivelAlerta AS NA ON NA.NivelAlertaID = R.NivelAlertaID
	WHERE (R.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND R.Activo = @Activo
	SELECT
		RolID,
		Descripcion,
		Activo,
		NivelAlertaID,
		DescripcionNivel
	FROM #Rol
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(RolID) AS [TotalReg]
	FROM #Rol
	DROP TABLE #Rol
	SET NOCOUNT OFF;
END
GO