USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_ObtenerVentaDetallePorAnimalID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividualVenta_ObtenerVentaDetallePorAnimalID]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_ObtenerVentaDetallePorAnimalID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 07/04/2014
-- Description: Obtiene el animalID de venta Detalle
-- Origen: APInterfaces
-- SalidaIndividualVenta_ObtenerVentaDetallePorAnimalID 1
-- =============================================
CREATE PROCEDURE [dbo].[SalidaIndividualVenta_ObtenerVentaDetallePorAnimalID]
	@AnimalID INT
AS
  BEGIN
    SET NOCOUNT ON
		SELECT VentaGanadoDetalleID, 
		       VentaGanadoID, 
			   AnimalID,
			   FotoVenta,
			   CausaPrecioID, 
			   Activo, 
			   FechaCreacion, 
			   UsuarioCreacion
		  FROM VentaGanadoDetalle (NOLOCK)
		 WHERE AnimalID = @AnimalID
	SET NOCOUNT OFF
  END

GO
