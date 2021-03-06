USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerFolioPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaProducto_ObtenerFolioPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerFolioPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Roque Solis
-- Create date: 2014/06/19
-- Description: Sp para obtener los folios de las salidas de productos por pagina
-- exec SalidaProducto_ObtenerFolioPorPagina 0,0,'',1,1,15
--=============================================
CREATE PROCEDURE [dbo].[SalidaProducto_ObtenerFolioPorPagina] 
@FolioSalida INT,
@OrganizacionID INT,
@DescripcionMovimiento VARCHAR(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
			ORDER BY SP.FolioSalida ASC
			) AS RowNum, 
		SP.SalidaProductoID,
		SP.OrganizacionID,
		SP.OrganizacionIDDestino,
		SP.TipoMovimientoID,
		SP.FolioSalida,
		SP.AlmacenID,
		SP.AlmacenInventarioLoteID,
		SP.ClienteID,
		SP.CuentaSAPID,
		SP.Observaciones,
		SP.Precio,
		SP.Importe,
		SP.AlmacenMovimientoID,
		SP.PesoTara,
		SP.PesoBruto,
		SP.Piezas,
		SP.FechaSalida,
	    SP.ChoferID,
		SP.CamionID,
		SP.Activo,
		TM.Descripcion
	INTO #DatosSalidaProducto
	FROM SalidaProducto (NOLOCK) SP
	INNER JOIN TipoMovimiento (NOLOCK) TM ON (SP.TipoMovimientoID = TM.TipoMovimientoID) 
	WHERE (@FolioSalida = 0 OR SP.FolioSalida LIKE '%' + CAST(@FolioSalida AS VARCHAR(15)) + '%')
	AND (@DescripcionMovimiento = '' OR TM.Descripcion= @DescripcionMovimiento)
	AND SP.Activo = @Activo
	AND (SP.OrganizacionID = @OrganizacionID OR @OrganizacionID = 0)
	AND SP.AlmacenInventarioLoteID IS NOT NULL
	SELECT SalidaProductoID,
		   OrganizacionID,
		   OrganizacionIDDestino,
		   TipoMovimientoID,
		   FolioSalida,
		   AlmacenID,
		   AlmacenInventarioLoteID,
		   ClienteID,
		   CuentaSAPID,
		   Observaciones,
		   Precio,
		   Importe,
		   AlmacenMovimientoID,
		   PesoTara,
		   PesoBruto,
		   Piezas,
		   FechaSalida,
		   ChoferID,
		   CamionID,
		   Activo,
		   Descripcion
	FROM #DatosSalidaProducto
	WHERE RowNum BETWEEN @Inicio
	AND @Limite
	SELECT COUNT(FolioSalida) AS TotalReg
	FROM #DatosSalidaProducto
	DROP TABLE #DatosSalidaProducto
	SET NOCOUNT OFF;
END

GO
