USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_ObtenerCostosPorProveedorIDOrigenIDDestinoIDCostoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoEmbarqueDetalle_ObtenerCostosPorProveedorIDOrigenIDDestinoIDCostoID]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_ObtenerCostosPorProveedorIDOrigenIDDestinoIDCostoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Alejandro Quiroz
-- Create date: 22/06/2015
-- Description: Sp para obtener los costos del ultimo embarque con el mismo origen,destino, proveedor y costo.
-- exec CostoEmbarqueDetalle_ObtenerCostosPorProveedorIDOrigenIDDestinoIDCostoID 4524,115,1,4
--=============================================
CREATE PROCEDURE [dbo].[CostoEmbarqueDetalle_ObtenerCostosPorProveedorIDOrigenIDDestinoIDCostoID]
@TipoEmbarqueID INT,
@ProveedorID INT,
@OrganizacionOrigenID INT,
@OrganizacionDestinoID INT,
@CostoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT e.EmbarqueID, ed.FechaCreacion, ced.Importe
	FROM Embarque(NOLOCK) e 
    INNER JOIN EmbarqueDetalle(NOLOCK) ed ON e.EmbarqueID = ed.EmbarqueID
	INNER JOIN CostoEmbarqueDetalle(NOLOCK) ced ON ced.EmbarqueDetalleID = ed.EmbarqueDetalleID
	INNER JOIN Costo(NOLOCK) c ON ced.CostoID = c.CostoID
	WHERE e.TipoEmbarqueID = @TipoEmbarqueID
    AND ed.ProveedorID = @ProveedorID
	AND ed.OrganizacionOrigenID = @OrganizacionOrigenID
	AND ed.OrganizacionDestinoID = @OrganizacionDestinoID
	AND c.CostoID = @CostoID
	SET NOCOUNT OFF;
END

GO
