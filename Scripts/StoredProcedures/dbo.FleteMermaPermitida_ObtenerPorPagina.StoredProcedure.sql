USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteMermaPermitida_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteMermaPermitida_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[FleteMermaPermitida_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 09/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : FleteMermaPermitida_ObtenerPorPagina 0, 1, 1, 15
--======================================================
CREATE PROCEDURE [dbo].[FleteMermaPermitida_ObtenerPorPagina]
@OrganizacionID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY O.Descripcion ASC) AS [RowNum],
		FMP.FleteMermaPermitidaID,
		FMP.OrganizacionID,
		FMP.SubFamiliaID,
		FMP.MermaPermitida,
		FMP.Activo
		, O.Descripcion				AS Organizacion
		, SF.Descripcion			AS SubFamilia
		, F.FamiliaID
		, F.Descripcion				AS Familia
	INTO #FleteMermaPermitida
	FROM FleteMermaPermitida FMP
	INNER JOIN Organizacion O
		ON (FMP.OrganizacionID = O.OrganizacionID
			AND @OrganizacionID IN (O.OrganizacionID, 0))
	INNER JOIN SubFamilia SF
		ON (FMP.SubFamiliaID = SF.SubFamiliaID)
	INNER JOIN Familia F
		ON (SF.FamiliaID = F.FamiliaID)
	WHERE FMP.Activo = @Activo
	SELECT
		FleteMermaPermitidaID,
		OrganizacionID,
		SubFamiliaID,
		MermaPermitida,
		Activo
		, Organizacion
		, SubFamilia
		, FamiliaID
		, Familia
	FROM #FleteMermaPermitida
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(FleteMermaPermitidaID) AS [TotalReg]
	FROM #FleteMermaPermitida
	DROP TABLE #FleteMermaPermitida
	SET NOCOUNT OFF;
END

GO
