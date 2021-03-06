USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Premezcla_ObtenerPorOrganizacionID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Premezcla_ObtenerPorOrganizacionID]
GO
/****** Object:  StoredProcedure [dbo].[Premezcla_ObtenerPorOrganizacionID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pedro Delgado
-- Create date: 10-07-2014
-- Description:	Otiene un listado de premezclas de una organizacion
-- Premezcla_ObtenerPorOrganizacionID 4,1
-- =============================================
CREATE PROCEDURE [dbo].[Premezcla_ObtenerPorOrganizacionID]
@OrganizacionID INT,
@Activo BIT = 1
AS
BEGIN
	SELECT 
		P.PremezclaID,
		P.OrganizacionID,
		P.Descripcion,
		P.ProductoID,
		P.Activo,
		P.FechaCreacion,
		P.UsuarioCreacionID,
		P.FechaCreacion,
		P.UsuarioModificacionID
	FROM Premezcla (NOLOCK) P
	INNER JOIN Organizacion (NOLOCK) O ON (O.OrganizacionID = P.OrganizacionID)
	WHERE O.Activo = @Activo AND O.OrganizacionID = @OrganizacionID AND P.Activo = @Activo
END

GO
