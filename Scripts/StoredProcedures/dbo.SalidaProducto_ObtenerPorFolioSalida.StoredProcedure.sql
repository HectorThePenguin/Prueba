USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerPorFolioSalida]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaProducto_ObtenerPorFolioSalida]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerPorFolioSalida]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Roque Solis
-- Create date: 2014/06/20
-- Description: Sp para obtener un folio de las salidas de productos
-- exec SalidaProducto_ObtenerPorFolioSalida 1,1,1
--=============================================
CREATE PROCEDURE [dbo].[SalidaProducto_ObtenerPorFolioSalida] 
@FolioSalida INT,
@OrganizacionID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
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
	FROM SalidaProducto (NOLOCK) SP
	INNER JOIN TipoMovimiento (NOLOCK) TM ON (SP.TipoMovimientoID = TM.TipoMovimientoID) 
	WHERE SP.FolioSalida = @FolioSalida
	AND SP.OrganizacionID = @OrganizacionID
	AND SP.Activo = @Activo
	SET NOCOUNT OFF;
END

GO
