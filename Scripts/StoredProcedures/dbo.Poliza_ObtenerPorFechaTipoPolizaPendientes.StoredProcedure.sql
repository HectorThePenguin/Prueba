USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPorFechaTipoPolizaPendientes]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ObtenerPorFechaTipoPolizaPendientes]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPorFechaTipoPolizaPendientes]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 16/07/2014
-- Description: 
-- SpName     : Poliza_ObtenerPorFechaTipoPolizaPendientes 5, 25, '20150608'
--======================================================
CREATE PROCEDURE [dbo].[Poliza_ObtenerPorFechaTipoPolizaPendientes]
@OrganizacionID		INT
, @TipoPolizaID		INT
, @Fecha			CHAR(8)
AS
BEGIN

	SELECT DISTINCT PolizaID
		,  TipoPolizaID
		,  CAST(XmlPoliza AS VARCHAR(MAX)) AS XmlPoliza
		,  Fecha
	FROM 
	(
		SELECT T.N.value('./texto_asig[1]','INT') AS Entrada
				, T.N.value('./concepto[1]','VARCHAR(100)') AS Concepto
				, T.N.value('./fecha_doc[1]','CHAR(8)') AS Fecha
				, T.N.value('./DocumentoSAP[1]','VARCHAR(100)') AS DocumentoSAP
				, P.PolizaID
				, p.TipoPolizaID
				, P.XmlPoliza
		FROM Poliza P(NOLOCK)
		CROSS APPLY xmlPoliza.nodes('/MT_POLIZA_PERIFERICO/Datos') as T(N)
		WHERE P.OrganizacionID = @OrganizacionID
			AND P.TipoPolizaID = @TipoPolizaID
			AND P.Estatus = 1
			AND P.Procesada = 1
	) A
	WHERE A.Fecha = @Fecha
	ORDER BY PolizaID DESC
END

GO
