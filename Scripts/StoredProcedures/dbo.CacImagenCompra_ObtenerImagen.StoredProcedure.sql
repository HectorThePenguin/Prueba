USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CacImagenCompra_ObtenerImagen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CacImagenCompra_ObtenerImagen]
GO
/****** Object:  StoredProcedure [dbo].[CacImagenCompra_ObtenerImagen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : R�ben Guzman
-- Create date: 19/08/2015
-- Description: Obtiene la imagen de una compra del sistema de centros
-- SpName     : CacImagenCompra_ObtenerImagen 1,1
--====================================================== 
CREATE PROCEDURE [dbo].[CacImagenCompra_ObtenerImagen]
(
	@OrganizacionId INT,
	@FolioCompra INT
)  
AS
BEGIN
	SELECT 
		IC.Imagen
	FROM sukarne.dbo.CacImagenCompra AS  IC (NOLOCK)
	INNER JOIN sukarne.dbo.CacImportarRecibosCompra AS IRC(NOLOCK)
		ON IC.OrganizacionId = IRC.OrganizacionId AND IC.FolioCompra = IRC.FolioCompra
	WHERE IRC.OrganizacionId = @OrganizacionId AND IRC.FolioCompra = @FolioCompra 
END

GO
