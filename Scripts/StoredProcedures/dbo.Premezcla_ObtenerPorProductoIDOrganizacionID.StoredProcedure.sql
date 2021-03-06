USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Premezcla_ObtenerPorProductoIDOrganizacionID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Premezcla_ObtenerPorProductoIDOrganizacionID]
GO
/****** Object:  StoredProcedure [dbo].[Premezcla_ObtenerPorProductoIDOrganizacionID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesus Alvarez
-- Create date: 14-07-2014
-- Description:	Otiene una premezcla por organizacion y productoid
-- Premezcla_ObtenerPorProductoIDOrganizacionID 4,1
-- =============================================
CREATE PROCEDURE [dbo].[Premezcla_ObtenerPorProductoIDOrganizacionID]
@ProductoID INT,
@OrganizacionID INT,
@Activo INT
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
	WHERE O.Activo = @Activo 
		  AND O.OrganizacionID = @OrganizacionID
		  AND P.ProductoID = @ProductoID
	      AND P.Activo = @Activo
END

GO
