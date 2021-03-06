USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerFolioPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pedido_ObtenerFolioPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerFolioPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/06/16
-- Description: Procedimiento almacenado para obtener folios de pase a proceso
-- Pedido_ObtenerFolioPorPagina 1, '', 1, 1, 150
--=============================================
CREATE PROCEDURE [dbo].[Pedido_ObtenerFolioPorPagina]
@OrganizacionID INT,
@DescripcionAlmacen VARCHAR(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @EstatusRecibido INT, @TipoEstatusRecibido INT
	SET @EstatusRecibido = 30
	SET @TipoEstatusRecibido = 8
	SELECT ROW_NUMBER() OVER (
			ORDER BY A.Descripcion ASC
			) AS RowNum, 
		   P.FolioPedido
		,  A.Descripcion
		,  P.PedidoID
	INTO #DatosCalidadPaseProceso
	FROM Pedido P
	INNER JOIN Estatus E
		ON (P.EstatusID = E.EstatusID
			AND P.OrganizacionID = @OrganizacionID
			AND P.Activo = 1
			AND E.EstatusID = @EstatusRecibido)
	INNER JOIN TipoEstatus TE
		ON (E.TipoEstatus = TE.TipoEstatusID
			AND TE.TipoEstatusID = @TipoEstatusRecibido)
	INNER JOIN Almacen A
		ON (P.AlmacenID = A.AlmacenID
			AND (@DescripcionAlmacen = '' OR A.Descripcion LIKE '%' + @DescripcionAlmacen + '%'))
	SELECT FolioPedido
		,  Descripcion
		,  PedidoID
	FROM #DatosCalidadPaseProceso
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT COUNT(FolioPedido) AS TotalReg
	FROM #DatosCalidadPaseProceso
	DROP TABLE #DatosCalidadPaseProceso
	SET NOCOUNT OFF;
END

GO
