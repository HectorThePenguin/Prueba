USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaCosto_ObtenerCostoAnimales]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaCosto_ObtenerCostoAnimales]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaCosto_ObtenerCostoAnimales]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 03/04/2014
-- Description:  Obtener los costos de las entradasa de las tablas de interface
-- Origen: APInterfaces
-- InterfaceSalidaCosto_ObtenerCostoAnimales 748, 0
-- =============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaCosto_ObtenerCostoAnimales]
	@FolioOrigen INT,
	@OrganizacionOrigenID INT
AS
  BEGIN
    SET NOCOUNT ON
		SELECT ISC.OrganizacionID, 
		       ISC.SalidaID,
			   ISC.Arete, 
			   ISC.FechaCompra,
			   ISC.CostoID,
			   ISC.Importe,
			   ISC.FechaRegistro,
			   ISC.UsuarioRegistro
		  FROM InterfaceSalidaCosto ISC
		 INNER JOIN InterfaceSalidaAnimal ISA ON (ISA.SalidaID = ISC.SalidaID AND ISA.OrganizacionID = ISC.OrganizacionID)
		 INNER JOIN InterfaceSalida IntS ON (IntS.SalidaID = ISC.SalidaID AND IntS.OrganizacionID = ISC.OrganizacionID)
		 WHERE ISC.SalidaID = @FolioOrigen
		   AND ISC.OrganizacionID = @OrganizacionOrigenID
		   AND ISC.Arete = ISA.Arete
		 ORDER BY CostoID ASC
	SET NOCOUNT OFF
  END

GO
