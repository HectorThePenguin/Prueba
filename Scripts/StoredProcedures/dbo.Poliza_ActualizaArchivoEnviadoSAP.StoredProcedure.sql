USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ActualizaArchivoEnviadoSAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ActualizaArchivoEnviadoSAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ActualizaArchivoEnviadoSAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 26/08/2014
-- Description: 
-- SpName     : Poliza_ActualizaArchivoEnviadoSAP
--======================================================
CREATE PROCEDURE [dbo].[Poliza_ActualizaArchivoEnviadoSAP]
@PolizaID INT
AS
BEGIN
		UPDATE Poliza
		SET ArchivoEnviadoServidor = 1
			, FechaModificacion = GETDATE()
		WHERE PolizaID = @PolizaID
END

GO
