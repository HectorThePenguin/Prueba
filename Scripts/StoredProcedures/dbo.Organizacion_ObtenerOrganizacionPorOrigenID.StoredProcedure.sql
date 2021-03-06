USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerOrganizacionPorOrigenID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerOrganizacionPorOrigenID]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerOrganizacionPorOrigenID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Organizacion_ObtenerOrganizacionPorOrigenID]	
	@OrganizacionID INT,
	@OrganizacionOrigenID INT
/* 
=============================================
-- Author: Jorge Velazquez
-- Create date: 28-11-2013
-- Description:	Otiene la Organizaci�n Destino que tenga configurada la Organizaci�n de Origen
-- [Organizacion_ObtenerOrganizacionPorOrigenID] 2,1
=============================================
*/
AS
BEGIN
	SET NOCOUNT ON;
	SELECT O.OrganizacionID,
			O.Descripcion,
			too.TipoOrganizacionID,
			too.Descripcion [TipoOrganizacion],
			O.Activo				
	FROM Organizacion O
	INNER JOIN TipoOrganizacion too on O.TipoOrganizacionID = too.TipoOrganizacionID
	INNER JOIN ConfiguracionEmbarque ce on O.OrganizacionID = ce.OrganizacionDestinoID
	WHERE O.Activo = 1		
		AND O.OrganizacionID = @OrganizacionID 
		AND ce.OrganizacionOrigenID = @OrganizacionOrigenID
	GROUP BY O.OrganizacionID,
			O.Descripcion,
			O.Activo,
			too.TipoOrganizacionID,
			too.Descripcion
	SET NOCOUNT OFF;
END

GO
