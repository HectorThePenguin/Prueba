USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerProveedorPorConfiguracionOrigenDestino]    Script Date: 20/06/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerProveedorPorConfiguracionOrigenDestino]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerProveedorPorConfiguracionOrigenDestino]    Script Date: 20/06/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Lorenzo Antonio Villaseñor Martínez
-- Create date: 20-06-2017
-- Description: sp para regresar el proveedor que tiene configurado Origen-Destino
-- SpName     : Proveedor_ObtenerProveedorPorConfiguracionOrigenDestino 375, 74, 82, 1
--======================================================  
CREATE PROCEDURE Proveedor_ObtenerProveedorPorConfiguracionOrigenDestino
@ProveedorID INT,
@OrigenID INT,
@DestinoID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		P.ProveedorID,
		P.CodigoSAP,
		p.Descripcion
	FROM Proveedor AS P
	INNER JOIN EmbarqueTarifa AS ET ON P.ProveedorID = ET.ProveedorID
	INNER JOIN ConfiguracionEmbarqueDetalle AS CED ON ET.ConfiguracionEmbarqueDetalleID = CED.ConfiguracionEmbarqueDetalleID
	INNER JOIN ConfiguracionEmbarque AS CE ON CED.ConfiguracionEmbarqueId = CE.ConfiguracionEmbarqueID
	WHERE P.ProveedorID = @ProveedorID AND ET.Activo = @Activo AND (CE.OrganizacionOrigenID = @OrigenID AND CE.OrganizacionDestinoID = @DestinoID)	
	SET NOCOUNT OFF;
END