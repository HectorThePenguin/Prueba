USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Gilberto Carranza
-- Create date: 12/11/2013
-- Description:	Obtener listado de Entradas de Ganado.
-- EntradaGanado_ObtenerPorPagina 0, 1, 1, 10 
--=============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerPorPagina]
	@FolioEntrada NVARCHAR(50),
	@OrganizacionID INT,
	@Inicio INT, 
	@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	    ROW_NUMBER() OVER ( ORDER BY EG.FolioEntrada ASC) AS RowNum,
			EG.FolioEntrada
			, O.Descripcion
			, EG.EntradaGanadoId
		INTO #EntradaGanado
		FROM EntradaGanado EG
		INNER JOIN Organizacion O
			ON (EG.OrganizacionOrigenID = O.OrganizacionID)
		INNER JOIN EmbarqueDetalle ED
			ON ED.EmbarqueID = EG.EmbarqueID 
		   AND ED.OrganizacionOrigenID = EG.OrganizacionOrigenID
		   AND ED.Recibido = 0
		WHERE @FolioEntrada  IN (EG.FolioEntrada,0)
		  AND EG.OrganizacionID = @OrganizacionID
		  AND EG.Activo = 1
	SELECT 
		FolioEntrada
		, Descripcion
		, EntradaGanadoId
	FROM    #EntradaGanado	
	WHERE   RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(FolioEntrada)AS TotalReg 
	FROM #EntradaGanado
	DROP TABLE #EntradaGanado
END

GO
