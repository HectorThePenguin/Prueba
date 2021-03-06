USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[VentaGanado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[VentaGanado_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[VentaGanado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 14/07/2014
-- Description: 
-- SpName     : VentaGanado_ObtenerPorPagina 2 , '', 0 , 1, 15
--======================================================
CREATE PROCEDURE [dbo].[VentaGanado_ObtenerPorPagina]
@OrganizacionID INT,
@Descripcion VARCHAR(50),
@Activo BIT,
@Inicio INT,
@Limite INT 

AS
BEGIN
	SET NOCOUNT ON;

	SELECT ROW_NUMBER() OVER (ORDER BY C.Descripcion ASC) AS [RowNum]
			,  VG.FolioTicket
			,  C.CodigoSAP
			,  C.Descripcion
			,  VG.FechaVenta
			,  VG.VentaGanadoID
		INTO #Venta
		FROM VentaGanado VG
		INNER JOIN Cliente C
			ON (VG.ClienteID = C.ClienteID)		
		INNER JOIN Lote L
			ON (VG.LoteID = L.LoteID)
		WHERE (C.Descripcion LIKE '%' + @Descripcion + '%' OR @Descripcion = '') 
			AND VG.Activo = @Activo
			AND VG.OrganizacionID = @OrganizacionID

	SELECT
		FolioTicket
		, CodigoSAP
		, Descripcion
		, VentaGanadoID
		, FechaVenta
	FROM #Venta a
	WHERE RowNum BETWEEN @Inicio AND @Limite

	SELECT
	COUNT(VentaGanadoID) AS [TotalReg]
	FROM #Venta

	DROP TABLE #Venta

	SET NOCOUNT OFF;
END

GO
