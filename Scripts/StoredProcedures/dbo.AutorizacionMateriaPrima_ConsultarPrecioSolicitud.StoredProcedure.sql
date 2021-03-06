USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_ConsultarPrecioSolicitud]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AutorizacionMateriaPrima_ConsultarPrecioSolicitud]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_ConsultarPrecioSolicitud]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Emir Lezama
-- Create date: 24/11/2014
-- Description: Sp para obtener 
-- exec AutorizacionMateriaPrima_ConsultarPrecioSolicitud 1,2.1,1,49,1
--=============================================
CREATE PROCEDURE [dbo].[AutorizacionMateriaPrima_ConsultarPrecioSolicitud]
@FolioSalida INT,
@PrecioVenta DECIMAL(18,4),
@OrganizacionID INT,
@EstatusRechazadoID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	COALESCE(MAX(AutorizacionMateriaPrimaID), 0) AS AutorizacionMateriaPrimaID
	FROM AutorizacionMateriaPrima (NOLOCK)
	WHERE Folio = @FolioSalida
	AND OrganizacionID = @OrganizacionID
	AND Precio = @PrecioVenta
	AND EstatusID = @EstatusRechazadoID
	AND Activo = @Activo
	SET NOCOUNT OFF;
END

GO
