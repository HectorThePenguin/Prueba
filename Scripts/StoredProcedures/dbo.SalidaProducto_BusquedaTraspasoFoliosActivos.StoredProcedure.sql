USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_BusquedaTraspasoFoliosActivos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaProducto_BusquedaTraspasoFoliosActivos]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_BusquedaTraspasoFoliosActivos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Octavio Quintero>
-- Create date: <08/07/2014>
-- Description:	Consulta los folios activos que tengan capturado el peso tara.
--
-- =============================================
CREATE PROCEDURE [dbo].[SalidaProducto_BusquedaTraspasoFoliosActivos]
	@FolioSalida INT,
	@OrganizacionID INT,
	@Activo BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
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
		  TM.Descripcion,
		  O.Descripcion as DescripcionOrganizacionDestino
		 FROM SalidaProducto (NOLOCK) SP
			LEFT JOIN Organizacion (NOLOCK) O ON (SP.OrganizacionIDDestino = O.OrganizacionID  )
			INNER JOIN TipoMovimiento (NOLOCK) TM ON (SP.TipoMovimientoID = TM.TipoMovimientoID)
		 WHERE (SP.AlmacenID is null OR SP.AlmacenID = 0)
		   AND (SP.AlmacenInventarioLoteID is null OR SP.AlmacenInventarioLoteID = 0)
		   AND (SP.FolioSalida = @FolioSalida or 0 = @FolioSalida) 
		   AND (SP.OrganizacionID = @OrganizacionID or 0 = @OrganizacionID) 
		   AND (SP.Activo = @Activo or 0 = @Activo)
	  SET NOCOUNT OFF;
END

GO
