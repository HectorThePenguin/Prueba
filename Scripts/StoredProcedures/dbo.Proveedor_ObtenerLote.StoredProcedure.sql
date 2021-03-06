USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerLote]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerLote]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerLote]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Proveedor_ObtenerLote]
	@LoteID INT,
	@OrganizacionID INT,
	@Estatus INT
AS
BEGIN	
SET NOCOUNT ON;	
	SELECT PR.ProveedorID,
		   PR.CodigoSAP,
		   PR.Descripcion,
		   PR.TipoProveedorID,
		   PR.Activo
	  FROM EntradaGanado EG (NOLOCK)
	 INNER JOIN Lote L (NOLOCK) ON EG.LoteID=L.LoteID AND L.Activo=@Estatus
	 INNER JOIN EntradaGanadoCosteo EGCO (NOLOCK) ON EG.EntradaGanadoID = EGCO.EntradaGanadoID
	 INNER JOIN EntradaGanadoCosto EGC (NOLOCK) ON EGC.EntradaGanadoCosteoID= EGCO.EntradaGanadoCosteoID
	 INNER JOIN Proveedor PR ON PR.ProveedorID = EGC.ProveedorID
	 WHERE L.LoteID = @LoteID
	   AND EG.OrganizacionID = @OrganizacionID
SET NOCOUNT OFF;	
END

GO
