USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPendientesPorEnviarCanceladasSAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ObtenerPendientesPorEnviarCanceladasSAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPendientesPorEnviarCanceladasSAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 25/08/2014
-- Description: 
-- SpName     : Poliza_ObtenerPendientesPorEnviarCanceladasSAP
--======================================================
CREATE PROCEDURE [dbo].[Poliza_ObtenerPendientesPorEnviarCanceladasSAP]
AS
BEGIN

	SET NOCOUNT ON

		SELECT CAST(P.XmlPoliza AS VARCHAR(MAX)) AS XmlPoliza
			,  P.OrganizacionID
			,  P.PolizaID
			,  P.TipoPolizaID
		FROM Poliza P
		WHERE Cancelada = 1
			
	SET NOCOUNT OFF

END

GO
