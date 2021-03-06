USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerXmlPoliza]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ObtenerXmlPoliza]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerXmlPoliza]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 16/07/2014
-- Description: 
-- SpName     : Poliza_ObtenerXmlPoliza 4, 1, '20141121', '39136', 'SM', 1
--======================================================
CREATE PROCEDURE [dbo].[Poliza_ObtenerXmlPoliza]
@TipoPolizaID		INT
, @OrganizacionID	INT
, @Fecha			CHAR(8)
, @Clave			VARCHAR(10)
, @Concepto			VARCHAR(10)
, @Estatus			BIT
AS
BEGIN
	SELECT DISTINCT Entrada
		,  Concepto
		,  PolizaID
		,  TipoPolizaID
		,  CAST(XmlPoliza AS VARCHAR(MAX)) AS XmlPoliza
		,  Fecha
	FROM 
	(
		SELECT T.N.value('./texto_asig[1]','BIGINT') AS Entrada
				, T.N.value('./concepto[1]','VARCHAR(100)') AS Concepto
				, T.N.value('./fecha_doc[1]','CHAR(8)') AS Fecha
				, P.PolizaID
				, p.TipoPolizaID
				, P.XmlPoliza
		FROM Poliza P(NOLOCK)
		CROSS APPLY xmlPoliza.nodes('/MT_POLIZA_PERIFERICO/Datos') as T(N)
		WHERE P.OrganizacionID = @OrganizacionID
			AND P.Estatus = @Estatus
			and p.TipoPolizaID = @TipoPolizaID
	) A
	WHERE 
	(A.Concepto LIKE @Concepto + '-' + @Clave + '%' or @Clave = '' or @Clave = '0')
		AND A.Fecha = @Fecha
END

GO
