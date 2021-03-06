USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ActualizaMensajeSAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ActualizaMensajeSAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ActualizaMensajeSAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 26/08/2014
-- Description: 
-- SpName     : Poliza_ActualizaMensajeSAP
--======================================================
CREATE PROCEDURE [dbo].[Poliza_ActualizaMensajeSAP]
@PolizaID INT
, @Mensaje VARCHAR(2000)
AS
BEGIN

		UPDATE Poliza
		SET Mensaje = @Mensaje
			, FechaModificacion = GETDATE()
		WHERE PolizaID = @PolizaID
END

GO
