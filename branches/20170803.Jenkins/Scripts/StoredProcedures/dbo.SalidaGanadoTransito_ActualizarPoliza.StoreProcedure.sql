USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_ActualizarPoliza]    Script Date: 15/04/2016 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[SalidaGanadoTransito_ActualizarPoliza]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_ActualizarPoliza]    Script Date: 15/04/2016 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Franco Jesus Inzunza Martinez
-- Create date: 15/04/2016 4:55:00 a.m.
-- Description: SP para asignar la poliza generada a la salida de ganado en transito correspondiente
-- SpName     : SalidaGanadoTransito_ActualizarPoliza
--======================================================
CREATE PROCEDURE [dbo].[SalidaGanadoTransito_ActualizarPoliza]
@Folio INT,
@PolizaID INT,
@EsMuerte BIT
AS
BEGIN
	UPDATE SalidaGanadoTransito
		SET PolizaID = @PolizaID
	WHERE Folio = @Folio
	AND Muerte = @EsMuerte
END