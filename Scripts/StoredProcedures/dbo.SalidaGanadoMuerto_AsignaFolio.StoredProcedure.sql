USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoMuerto_AsignaFolio]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaGanadoMuerto_AsignaFolio]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoMuerto_AsignaFolio]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Alejandro Quiroz
-- Create date: 04-08-2014
-- Description: Obtiene el folio para la creacion del reporte diario de salida por muerte
-- SalidaGanadoMuerto_AsignaFolio 2,2,'<ROOT><Muertes><Muerte>5</Muerte></Muertes></ROOT>'
-- =============================================
CREATE PROCEDURE [dbo].[SalidaGanadoMuerto_AsignaFolio]
	@Folio INT,
	@UsuarioModificacionID INT,
	@MuertesXML XML
AS
  BEGIN
	CREATE TABLE #MuertesAfectadas
	(
		MuerteID INT
	)
	INSERT INTO #MuertesAfectadas
	SELECT DISTINCT T.ITEM.value('./Muerte[1]','INT') AS MuerteID
	FROM @MuertesXML.nodes('/ROOT/Muertes') as T(ITEM)
    UPDATE Muertes
	SET FolioSalida = @Folio,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE MuerteID IN (SELECT MuerteID FROM #MuertesAfectadas)
  END

GO
