USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorPaginaRecibido]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerPorPaginaRecibido]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorPaginaRecibido]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Jorge Luis Velazquez
-- Create date: 12/11/2013
-- Description:	Obtener listado de Entradas de Ganado Recibidos.
-- EntradaGanado_ObtenerPorPagina 1, 1, 1, 10 
--=============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerPorPaginaRecibido] @FolioEntrada NVARCHAR(50)
	,@OrganizacionID INT
	,@Inicio INT
	,@Limite INT
	,@Observacion NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
			ORDER BY EG.FolioEntrada ASC
			) AS RowNum
		,EG.FolioEntrada
		,O.Descripcion
		,EG.EntradaGanadoID
	INTO #EntradaGanado
	FROM EntradaGanado EG
	INNER JOIN Organizacion O ON (EG.OrganizacionOrigenID = O.OrganizacionID)
	INNER JOIN Embarque em ON eg.EmbarqueID = em.EmbarqueID
	WHERE @FolioEntrada IN (
			EG.FolioEntrada
			,0
			)
		AND EG.OrganizacionID = @OrganizacionID
		AND EG.ImpresionTicket = 1
		AND EG.Activo = 1		
		AND EG.LoteID > 0
		AND em.Estatus = 2
		AND NOT EXISTS (
			SELECT ''
			FROM EntradaGanadoCosteo egc
			WHERE egc.EntradaGanadoID = EG.EntradaGanadoID
				AND egc.Activo = 1
			)
			AND (O.descripcion LIKE '%'+@Observacion+'%' OR @Observacion = '')
	SELECT FolioEntrada
		,Descripcion
		,EntradaGanadoID
	FROM #EntradaGanado
	WHERE RowNum BETWEEN @Inicio
			AND @Limite
	SELECT COUNT(FolioEntrada) AS TotalReg
	FROM #EntradaGanado
	DROP TABLE #EntradaGanado
END

GO
