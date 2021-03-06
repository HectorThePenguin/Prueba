USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ActualizaProcesadoSAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ActualizaProcesadoSAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ActualizaProcesadoSAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 26/08/2014
-- Description: 
-- SpName     : Poliza_ActualizaProcesadoSAP
--======================================================
CREATE PROCEDURE [dbo].[Poliza_ActualizaProcesadoSAP]
@PolizaID INT
, @XmlPoliza XML
AS
BEGIN

		UPDATE Poliza
		SET XmlPoliza = @XmlPoliza
			, Procesada = 1
			, FechaModificacion = GETDATE()
		WHERE PolizaID = @PolizaID
END

GO
