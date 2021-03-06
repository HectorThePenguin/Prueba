USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerHumedadOrigenPorEntradaProductoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerHumedadOrigenPorEntradaProductoID]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerHumedadOrigenPorEntradaProductoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Edgar Villarreal
-- Create date: 28/07/2014
-- Description: Obtiene la humedad origen por EntradaProductoID
-- 				
-- SpName     : exec EntradaProducto_ObtenerHumedadOrigenPorEntradaProductoID 2430, 1, 1
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerHumedadOrigenPorEntradaProductoID]
@EntradaProductoID INT,
@OrganizacionID INT,
@Activo INT = 2
AS 
BEGIN
SELECT
	EPM.Porcentaje AS HumedadOrigen
FROM EntradaProducto(NOLOCK) EP
INNER JOIN EntradaProductoDetalle EPD
	ON EPD.EntradaProductoID = EP.EntradaProductoID
INNER JOIN EntradaProductoMuestra EPM
	ON EPM.EntradaProductoDetalleID = EPD.EntradaProductoDetalleID
WHERE EP.OrganizacionID = @OrganizacionID
AND EP.Activo = @Activo
AND EPM.esOrigen = 1
AND EP.EntradaProductoID = @EntradaProductoID;

END
GO
