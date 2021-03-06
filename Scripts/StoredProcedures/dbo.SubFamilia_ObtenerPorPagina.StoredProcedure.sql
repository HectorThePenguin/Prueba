USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SubFamilia_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/01/14
-- Description: 
-- SubFamilia_ObtenerPorPagina 1, '', 1, 1, 15
--=============================================
CREATE PROCEDURE [dbo].[SubFamilia_ObtenerPorPagina]
@FamiliaID INT,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY SB.Descripcion ASC) AS RowNum
		, SB.FamiliaID
		, SB.SubFamiliaID
		, SB.Descripcion AS DescripcionSubFamilia
		, SB.Activo
		, F.Descripcion AS DescripcionFamilia
	INTO #Datos
	FROM SubFamilia SB
	INNER JOIN Familia F
		ON (SB.FamiliaID = F.FamiliaID)
	WHERE (SB.Descripcion LIKE '%' + @Descripcion + '%' OR @Descripcion = '')
		AND @FamiliaID IN (SB.FamiliaID, 0)
		AND SB.Activo = @Activo
	SELECT
		FamiliaID
		, SubFamiliaID
		, DescripcionSubFamilia
		, Activo
		, DescripcionFamilia
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(SubFamiliaID)AS TotalReg
	From #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO
