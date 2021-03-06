USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_ObtenerPorPaginaPorFamilia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SubFamilia_ObtenerPorPaginaPorFamilia]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_ObtenerPorPaginaPorFamilia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/01/14
-- Description: 
-- SubFamilia_ObtenerPorPaginaPorFamilia 1, '', 1, 1, 15
--=============================================
CREATE PROCEDURE [dbo].[SubFamilia_ObtenerPorPaginaPorFamilia]
@Descripcion varchar(50),
@XmlFamilias XML,
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY SF.Descripcion ASC) AS RowNum
		, SF.FamiliaID
		, SF.SubFamiliaID
		, SF.Descripcion AS DescripcionSubFamilia
		, SF.Activo
		, F.Descripcion AS DescripcionFamilia
	INTO #Datos
	FROM SubFamilia SF
	INNER JOIN
	(
		SELECT FamiliaID = t.item.value('./FamiliaID[1]', 'INT')
		FROM @XmlFamilias.nodes('ROOT/Familia') AS T(item)
	) xF ON (SF.FamiliaID = xF.FamiliaID)
	INNER JOIN Familia F
		ON (xF.FamiliaID = F.FamiliaID)
	WHERE (SF.Descripcion LIKE '%' + @Descripcion + '%' OR @Descripcion = '')
		AND SF.Activo = @Activo
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
