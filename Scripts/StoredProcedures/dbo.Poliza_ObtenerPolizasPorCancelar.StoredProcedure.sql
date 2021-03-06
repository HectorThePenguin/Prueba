USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPolizasPorCancelar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ObtenerPolizasPorCancelar]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPolizasPorCancelar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 16/07/2014
-- Description: 
-- SpName     : Poliza_ObtenerPolizasPorCancelar
--======================================================
CREATE PROCEDURE [dbo].[Poliza_ObtenerPolizasPorCancelar]
AS
BEGIN

	SELECT  DISTINCT  PolizaID
		,  TipoPolizaID
		,  CAST(XmlPoliza AS VARCHAR(MAX)) AS XmlPoliza
		,  Fecha
		,  OrganizacionID
	FROM 
	(
		SELECT T.N.value('./texto_asig[1]','INT') AS Entrada
				, T.N.value('./concepto[1]','VARCHAR(100)') AS Concepto
				, T.N.value('./fecha_doc[1]','CHAR(8)') AS Fecha
				, T.N.value('./DocumentoSAP[1]','VARCHAR(100)') AS DocumentoSAP
				, T.N.value('./DocumentoCancelacionSAP[1]','VARCHAR(100)') AS DocumentoCancelacionSAP 
				, P.PolizaID
				, p.TipoPolizaID
				, P.XmlPoliza
				, P.OrganizacionID
		FROM Poliza P(NOLOCK)
		CROSS APPLY xmlPoliza.nodes('/MT_POLIZA_PERIFERICO/Datos') as T(N)
		WHERE P.Estatus = 1
			AND P.Procesada = 1
			AND P.Cancelada = 1
	) A
	WHERE LEN(A.DocumentoSAP) > 0
		AND LEN(A.DocumentoCancelacionSAP) = 0
	ORDER BY PolizaID DESC
END

GO
