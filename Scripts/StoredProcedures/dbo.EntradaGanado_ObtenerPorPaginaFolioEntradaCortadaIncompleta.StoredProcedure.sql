USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorPaginaFolioEntradaCortadaIncompleta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerPorPaginaFolioEntradaCortadaIncompleta]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorPaginaFolioEntradaCortadaIncompleta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author:		Jorge Luis Velazquez
-- Create date: 16/06/2015
-- Description:	Obtener listado de Entradas de Ganado Recibidos.
-- EntradaGanado_ObtenerPorPaginaFolioEntradaCortadaIncompleta 0, 5, 1, 15,'' 
--=============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerPorPaginaFolioEntradaCortadaIncompleta] @FolioEntrada INT
	,@OrganizacionID INT
	,@Inicio INT
	,@Limite INT
	,@OrganizacionOrigen NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT ROW_NUMBER() OVER (
			ORDER BY EG.FolioEntrada ASC
			) AS RowNum
		,EG.FolioEntrada
		,O.Descripcion
		,EG.EntradaGanadoID
		,EG.OrganizacionID
		,EG.FechaEntrada
		,eg.FolioOrigen
		,EG.OrganizacionOrigenID
		,EG.PesoBruto
		,EG.PesoTara
		,co.Codigo [Corral]
		,lo.Lote
	INTO #EntradaGanado
	FROM EntradaGanado EG (nolock)
	INNER JOIN Organizacion O (nolock) ON (EG.OrganizacionOrigenID = O.OrganizacionID)
	INNER JOIN Corral co (nolock) ON eg.CorralID = co.CorralID
	INNER JOIN Lote lo (nolock) ON eg.LoteID = lo.LoteID
	WHERE @FolioEntrada IN (EG.FolioEntrada,0)
		AND EG.OrganizacionID = @OrganizacionID
		AND EG.ImpresionTicket = 1
		AND EG.Activo = 1
		AND EG.Costeado = 1
		AND EG.Manejado = 0
		AND (O.descripcion LIKE '%'+@OrganizacionOrigen+'%' OR @OrganizacionOrigen = '')

	SELECT FolioEntrada
		,Descripcion AS OrganizacionOrigen
		,EntradaGanadoID
		,FechaEntrada
		,FolioOrigen
		,OrganizacionOrigenID
		,OrganizacionID
		,PesoBruto
		,PesoTara
		,Corral
		,Lote
	FROM #EntradaGanado
	WHERE RowNum BETWEEN @Inicio
			AND @Limite

	SELECT COUNT(FolioEntrada) AS TotalReg
	FROM #EntradaGanado

	DROP TABLE #EntradaGanado
END

GO
