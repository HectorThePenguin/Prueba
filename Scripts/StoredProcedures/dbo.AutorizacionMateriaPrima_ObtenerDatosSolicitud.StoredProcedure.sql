USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_ObtenerDatosSolicitud]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AutorizacionMateriaPrima_ObtenerDatosSolicitud]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_ObtenerDatosSolicitud]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Emir Lezama
-- Create date: 24/11/2014
-- Description: Sp para obtener datos de la solicitud
-- exec AutorizacionMateriaPrima_ObtenerDatosSolicitud 1,1,1
--=============================================
CREATE PROCEDURE [dbo].[AutorizacionMateriaPrima_ObtenerDatosSolicitud] 
@FolioSalida INT,
@OrganizacionID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	COALESCE(AMP.AutorizacionMateriaPrimaID,0) AS SolicitudID,
	COALESCE(AMP.EstatusID,0) AS EstatusSolicitud,
	COALESCE(AMP.Precio,0) AS Precio
	FROM SalidaProducto (NOLOCK) SP
	LEFT JOIN AutorizacionMateriaPrima (NOLOCK) AMP ON (AMP.AutorizacionMateriaPrimaID = SP.AutorizacionMateriaPrimaID)
	WHERE SP.FolioSalida = @FolioSalida
	AND SP.OrganizacionID = @OrganizacionID
	AND SP.Activo = @Activo
	SET NOCOUNT OFF;
END

GO
