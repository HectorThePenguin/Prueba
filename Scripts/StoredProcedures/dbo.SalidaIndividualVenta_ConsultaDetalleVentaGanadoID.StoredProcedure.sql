USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_ConsultaDetalleVentaGanadoID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividualVenta_ConsultaDetalleVentaGanadoID]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_ConsultaDetalleVentaGanadoID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/02/28
-- Description: 
--  exec SalidaIndividualVenta_ConsultaDetalleVentaGanadoID 8
--=============================================
CREATE PROCEDURE [dbo].[SalidaIndividualVenta_ConsultaDetalleVentaGanadoID]
	@VentaGanadoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT vdet.VentaGanadoDetalleID, vdet.VentaGanadoID, vdet.Arete, a.AreteMetalico, vdet.FotoVenta, vdet.CausaPrecioID, vdet.Activo 
	FROM VentaGanadoDetalle vdet left join animal a on (vdet.animalid = a.animalid)
	WHERE vdet.VentaGanadoID = @VentaGanadoID AND vdet.Activo = 1
	SET NOCOUNT OFF;
END

GO
