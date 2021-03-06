USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPendientesPorEnviarSAP2]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ObtenerPendientesPorEnviarSAP2]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPendientesPorEnviarSAP2]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 25/08/2014
-- Description: 
-- SpName     : Poliza_ObtenerPendientesPorEnviarSAP2
--======================================================
CREATE PROCEDURE [dbo].[Poliza_ObtenerPendientesPorEnviarSAP2]
AS
BEGIN

	SET NOCOUNT ON

		SELECT CAST(P.XmlPoliza AS VARCHAR(MAX)) AS XmlPoliza
			,  P.OrganizacionID
			,  P.PolizaID
			,  P.TipoPolizaID
			,  P.Mensaje
		FROM Poliza P
		WHERE --Procesada = 0
			--AND Estatus = 1
			--AND OrganizacionID = 0
			--AND LEN(Mensaje) = 0
			--AND UsuarioCreacionID = 3
			--AND (Mensaje NOT LIKE '%no%abierto%')
			--AND 
			PolizaID IN (87034, 87035, 86954, 86959)
			--AND CAST(FechaCreacion AS DATE) = CAST(GETDATE() AS DATE)
		ORDER BY PolizaID

	SET NOCOUNT OFF

END

GO
