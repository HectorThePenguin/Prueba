USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerProvedoresPorRuta]    Script Date: 26/06/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerProvedoresPorRuta]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerProvedoresPorRuta]    Script Date: 26/06/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Lorenzo Antonio Villaseñor Martínez
-- Create date: 26-06-2017
-- Description: sp para obtener proveedores en base a una ruta
-- SpName     : Proveedor_ObtenerProvedoresPorRuta 2, 31, 1
--======================================================  
CREATE PROCEDURE Proveedor_ObtenerProvedoresPorRuta
@TipoProveedorID INT,
@ConfiguracionEmbarqueDetalleID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		P.ProveedorID,
		P.CodigoSAP,
		P.Descripcion,
		P.TipoProveedorID,
		P.CorreoElectronico,
		P.Activo
	FROM Proveedor AS P (NOLOCK)
	INNER JOIN EmbarqueTarifa AS ET (NOLOCK) ON P.ProveedorID = ET.ProveedorID
	INNER JOIN ConfiguracionEmbarqueDetalle AS CED (NOLOCK) ON ET.ConfiguracionEmbarqueDetalleID = CED.ConfiguracionEmbarqueDetalleID
	INNER JOIN ConfiguracionEmbarque AS CE (NOLOCK) ON CED.ConfiguracionEmbarqueId = CE.ConfiguracionEmbarqueID
		WHERE P.Activo = @Activo AND P.TipoProveedorID = @TipoProveedorID AND CED.ConfiguracionEmbarqueDetalleID = @ConfiguracionEmbarqueDetalleID
	SET NOCOUNT OFF;
END