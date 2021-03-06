USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoMuerto_ObtenerFolio]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaGanadoMuerto_ObtenerFolio]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoMuerto_ObtenerFolio]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Alejandro Quiroz
-- Create date: 04-08-2014
-- Description: Obtiene el folio para la creacion del reporte diario de salida por muerte
-- SalidaGanadoMuerto_ObtenerFolio 1,16
-- =============================================
CREATE PROCEDURE [dbo].[SalidaGanadoMuerto_ObtenerFolio]
	@OrganizacionID INT,
	@TipoFolioID INT
AS
  BEGIN
    SET NOCOUNT ON
	DECLARE @FolioSalida INT;
	EXEC Folio_Obtener @OrganizacionID, @TipoFolioID, @Folio = @FolioSalida OUTPUT
	SELECT @FolioSalida AS Folio
	SET NOCOUNT OFF
  END

GO
