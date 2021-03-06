USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Premezcla_ObtenerOrganizacionesPorProductoId]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Premezcla_ObtenerOrganizacionesPorProductoId]
GO
/****** Object:  StoredProcedure [dbo].[Premezcla_ObtenerOrganizacionesPorProductoId]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ramses Santos
-- Create date: 10-07-2014
-- Description:	Otiene un listado de las organizaciones premezclas
-- Premezcla_ObtenerOrganizacionesPorProductoId 168
-- =============================================
CREATE PROCEDURE [dbo].[Premezcla_ObtenerOrganizacionesPorProductoId]
@ProductoId INT,
@Activo BIT = 1
AS
BEGIN
	SELECT DISTINCT
	  O.OrganizacionID,
	  O.TipoOrganizacionID,
	  TI.Descripcion AS TipoOrganizacion,
	  O.Descripcion,
	  O.Direccion,
	  O.Rfc,
	  O.IvaID,
	  i.Descripcion as [Iva],
	  O.Activo
	  FROM Organizacion (NOLOCK) O
	  INNER JOIN TipoOrganizacion (NOLOCK) TI ON (O.TipoOrganizacionID = TI.TipoOrganizacionID) 
	  INNER JOIN Iva I ON (I.IvaID = O.IvaID)
	  INNER JOIN Premezcla (NOLOCK) P ON (O.OrganizacionID = P.OrganizacionID)
	  INNER JOIN PremezclaDetalle (NOLOCK) PD ON (P.PremezclaID = PD.PremezclaID AND PD.ProductoID = @ProductoId)
    AND O.Activo = @Activo AND P.Activo = @Activo
END

GO
