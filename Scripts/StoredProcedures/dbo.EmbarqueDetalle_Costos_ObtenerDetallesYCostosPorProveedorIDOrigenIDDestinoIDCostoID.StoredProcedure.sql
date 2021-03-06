USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EmbarqueDetalle_Costos_ObtenerDetallesYCostosPorProveedorIDOrigenIDDestinoIDCostoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EmbarqueDetalle_Costos_ObtenerDetallesYCostosPorProveedorIDOrigenIDDestinoIDCostoID]
GO
/****** Object:  StoredProcedure [dbo].[EmbarqueDetalle_Costos_ObtenerDetallesYCostosPorProveedorIDOrigenIDDestinoIDCostoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Alejandro Quiroz
-- Create date: 22/06/2015
-- Description: Sp para obtener los detalles del embarque y los costos posibles con la misma escala, tipo de embarque, proveedor y costo.
-- exec EmbarqueDetalle_Costos_ObtenerDetallesYCostosPorProveedorIDOrigenIDDestinoIDCostoID 3,4373,5,1,2,8
--=============================================
CREATE PROCEDURE [dbo].[EmbarqueDetalle_Costos_ObtenerDetallesYCostosPorProveedorIDOrigenIDDestinoIDCostoID]
@TipoEmbarqueID INT,
@ProveedorID INT,
@OrganizacionOrigenID INT,
@OrganizacionDestinoID INT,
@OrdenDestino INT,
@CostoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT e.EmbarqueID, ed.EmbarqueDetalleID
	INTO #Posibles 
	FROM (Embarque(NOLOCK) e 
	INNER JOIN EmbarqueDetalle(NOLOCK) ed ON e.EmbarqueID = ed.EmbarqueID)
	LEFT JOIN CostoEmbarqueDetalle(NOLOCK) ced ON ced.EmbarqueDetalleID = ed.EmbarqueDetalleID
	WHERE e.TipoEmbarqueID = @TipoEmbarqueID
	AND ed.ProveedorID = @ProveedorID
	AND (ed.OrganizacionOrigenID = @OrganizacionOrigenID  AND ed.Orden = 1 AND ced.CostoID = @CostoID 
	  OR ed.OrganizacionDestinoID = @OrganizacionDestinoID AND ed.Orden = @OrdenDestino)
	SELECT ed.EmbarqueID, ed.EmbarqueDetalleID, ed.Orden, ed.OrganizacionOrigenID, ed.OrganizacionDestinoID, ed.FechaCreacion
	FROM EmbarqueDetalle(NOLOCK) ed
	WHERE ed.EmbarqueID IN (
		SELECT EmbarqueID
		FROM #Posibles
		GROUP BY EmbarqueID
		HAVING COUNT(1) > 1 )
	SELECT p.EmbarqueDetalleID, ced.Importe
	FROM #Posibles p
	INNER JOIN CostoEmbarqueDetalle(NOLOCK) ced ON ced.EmbarqueDetalleID = p.EmbarqueDetalleID
	WHERE ced.CostoID = @CostoID
	DROP TABLE #Posibles
	SET NOCOUNT OFF;
END

GO
