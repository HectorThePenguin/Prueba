USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_ObtenerContratoPorTipoPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionFletes_ObtenerContratoPorTipoPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_ObtenerContratoPorTipoPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Roque Solis
-- Create date: 18/06/2014 
-- Description: 
-- SpName     : exec ProgramacionFletes_ObtenerContratoPorTipoPorPagina 0,0,2,1,1,15
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionFletes_ObtenerContratoPorTipoPorPagina]
@FolioContrato int,
@OrganizacionID int, 
@TipoFleteID int,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 	
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		F.ContratoID 		AS ContratoID,
		C.Folio 		 	AS Folio,
		O.Descripcion 		AS Descripcion,
		F.Activo 			AS Activo,
		F.OrganizacionID 	AS OrganizacionID
	INTO #Contratos
	FROM Flete (NOLOCK) F
	INNER JOIN Contrato (NOLOCK) 		AS C ON C.ContratoID=F.ContratoID
	INNER JOIN Organizacion (NOLOCK) 	AS O ON O.OrganizacionID=F.OrganizacionID
	WHERE (@FolioContrato = 0 OR  C.Folio = 	@FolioContrato)
	  AND (@OrganizacionID = 0 OR F.OrganizacionID = @OrganizacionID)
	  AND   F.Activo = @Activo
	  AND 	C.Activo = @Activo
	  AND 	O.Activo = @Activo
	  AND 	C.TipofleteID = @TipoFleteID
	GROUP BY F.ContratoID,C.Folio,F.OrganizacionID,O.Descripcion,F.Activo
	SELECT
		ContratoID,
		Folio,
		Descripcion,
		Activo, 
		OrganizacionID
	FROM #Contratos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ContratoID) AS [TotalReg]
	FROM #Contratos
	DROP TABLE #Contratos
	SET NOCOUNT OFF;
END

GO
