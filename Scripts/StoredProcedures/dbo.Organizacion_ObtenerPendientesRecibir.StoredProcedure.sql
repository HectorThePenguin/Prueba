USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPendientesRecibir]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPendientesRecibir]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPendientesRecibir]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:  Obtener listado de Organizaciones
-- Organizacion_ObtenerPendientesRecibir 1,1
-- =============================================
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPendientesRecibir]
@OrganizacionID INT,
@Estatus INT
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT 
		o.OrganizacionID,
		o.TipoOrganizacionID,
		ot.Descripcion as [TipoOrganizacion],
		o.Descripcion,
		o.Direccion,
		o.RFC,
		o.IvaID,
		o.Activo,
		i.IvaID,
		i.TasaIva,
		i.IndicadorIvaPagar,
		i.IndicadorIvaRecuperar
      FROM Organizacion O
      INNER JOIN EmbarqueDetalle ed 
		ON ed.OrganizacionOrigenID = o.OrganizacionID AND ed.Orden = 1
      INNER JOIN Embarque e 
		ON e.EmbarqueID = ed.EmbarqueID AND e.Estatus = @Estatus
	  INNER JOIN Iva I
		ON (O.IvaID = I.IvaID)
	  INNER JOIN TipoOrganizacion ot on ot.TipoOrganizacionID = o.TipoOrganizacionID	
      WHERE o.OrganizacionID = @OrganizacionID
      SET NOCOUNT OFF;
  END

GO
