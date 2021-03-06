USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanado_ExisteVentaEnSalidaGanado]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaGanado_ExisteVentaEnSalidaGanado]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanado_ExisteVentaEnSalidaGanado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 07/04/2014
-- Description: Obtiene la salida ganado en base a la ventaGanadoID
-- Origen: APInterfaces
-- SalidaGanado_ExisteVentaEnSalidaGanado 1
-- =============================================
CREATE PROCEDURE [dbo].[SalidaGanado_ExisteVentaEnSalidaGanado]
	@VentaGanadoID INT
AS
  BEGIN
    SET NOCOUNT ON
		SELECT TOP 1 SalidaGanadoID,
					 OrganizacionID,
					 TipoMovimientoID,
					 Fecha,
					 Folio,
					 VentaGanadoID,
					 Activo,
					 FechaCreacion,
					 UsuarioCreacionID
		FROM SalidaGanado
		WHERE VentaGanadoID = @VentaGanadoID
	SET NOCOUNT OFF
  END

GO
