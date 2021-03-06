USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerConciliacionPorFechas]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ObtenerConciliacionPorFechas]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerConciliacionPorFechas]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 01/10/2014
-- Description: Obtiene las polizas para su conciliacion
-- SpName     : Poliza_ObtenerConciliacionPorFechas 4, '20150601', '20150620', 'GF'
--======================================================
CREATE PROCEDURE [dbo].[Poliza_ObtenerConciliacionPorFechas]
@OrganizacionID	INT
, @FechaInicio	DATE
, @FechaFin		DATE
, @ClaseDocumento CHAR(2)
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @FechaInicial CHAR(8), @FechaFinal CHAR(8)

	SET @FechaInicial = CONVERT(CHAR(8), @FechaInicio, 112)
	SET @FechaFinal = CONVERT(CHAR(8), @FechaFin, 112)

			SELECT		  A.noref
						, A.fecha_doc
						, A.fecha_cont
						, A.clase_doc
						, A.sociedad
						, A.moneda
						, A.tipocambio
						, A.texto_doc
						, A.mes
						, A.cuenta
						, A.proveedor
						, A.cliente
						, A.indica_cme
						, A.importe
						, A.indica_imp
						, A.centro_cto
						, A.orden_int
						, A.centro_ben
						, A.texto_asig
						, A.concepto
						, A.division
						, A.clase_movt
						, A.bus_act
						, A.periodo
						, A.nolinea
						, A.ref1
						, A.ref2
						, A.ref3
						, A.fecha_imto
						, A.cond_imto
						, A.clave_imto
						, A.tipo_ret
						, A.cod_ret
						, A.imp_ret
						, A.imp_iva
						, A.archifolio
						, A.Texto
						, A.PolizaID
						, A.TipoPolizaID
						, A.OrganizacionID
						, A.Conciliada
			FROM 
			(
				SELECT    T.N.value('./noref[1]','VARCHAR(100)') AS noref
						, T.N.value('./fecha_doc[1]','VARCHAR(100)') AS fecha_doc
						, T.N.value('./fecha_cont[1]','VARCHAR(100)') AS fecha_cont
						, T.N.value('./clase_doc[1]','VARCHAR(100)') AS clase_doc
						, T.N.value('./sociedad[1]','VARCHAR(100)') AS sociedad
						, T.N.value('./moneda[1]','VARCHAR(100)') AS moneda
						, T.N.value('./tipocambio[1]','VARCHAR(100)') AS tipocambio
						, T.N.value('./texto_doc[1]','VARCHAR(100)') AS texto_doc
						, T.N.value('./mes[1]','VARCHAR(100)') AS mes
						, T.N.value('./cuenta[1]','VARCHAR(100)') AS cuenta
						, T.N.value('./proveedor[1]','VARCHAR(100)') AS proveedor
						, T.N.value('./cliente[1]','VARCHAR(100)') AS cliente
						, T.N.value('./indica_cme[1]','VARCHAR(100)') AS indica_cme
						, T.N.value('./importe[1]','VARCHAR(100)') AS importe
						, T.N.value('./indica_imp[1]','VARCHAR(100)') AS indica_imp
						, T.N.value('./centro_cto[1]','VARCHAR(100)') AS centro_cto
						, T.N.value('./orden_int[1]','VARCHAR(100)') AS orden_int
						, T.N.value('./centro_ben[1]','VARCHAR(100)') AS centro_ben
						, T.N.value('./texto_asig[1]','VARCHAR(100)') AS texto_asig
						, T.N.value('./concepto[1]','VARCHAR(100)') AS concepto
						, T.N.value('./division[1]','VARCHAR(100)') AS division
						, T.N.value('./clase_movt[1]','VARCHAR(100)') AS clase_movt
						, T.N.value('./bus_act[1]','VARCHAR(100)') AS bus_act
						, T.N.value('./periodo[1]','VARCHAR(100)') AS periodo
						, T.N.value('./nolinea[1]','VARCHAR(100)') AS nolinea
						, T.N.value('./ref1[1]','VARCHAR(100)') AS ref1
						, T.N.value('./ref2[1]','VARCHAR(100)') AS ref2
						, T.N.value('./ref3[1]','VARCHAR(100)') AS ref3
						, T.N.value('./fecha_imto[1]','VARCHAR(100)') AS fecha_imto
						, T.N.value('./cond_imto[1]','VARCHAR(100)') AS cond_imto
						, T.N.value('./clave_imto[1]','VARCHAR(100)') AS clave_imto
						, T.N.value('./tipo_ret[1]','VARCHAR(100)') AS tipo_ret
						, T.N.value('./cod_ret[1]','VARCHAR(100)') AS cod_ret
						, T.N.value('./imp_ret[1]','VARCHAR(100)') AS imp_ret
						, T.N.value('./imp_iva[1]','VARCHAR(100)') AS imp_iva
						, T.N.value('./archifolio[1]','VARCHAR(100)') AS archifolio
						, T.N.value('./texto_doc[1]','VARCHAR(100)') AS Texto
						, P.PolizaID
						, P.TipoPolizaID
						, P.OrganizacionID
						, P.Conciliada
						, P.Estatus
				FROM Poliza P(NOLOCK)
				CROSS APPLY xmlPoliza.nodes('/MT_POLIZA_PERIFERICO/Datos') as T(N)
				WHERE OrganizacionID = @OrganizacionID
					AND P.Estatus = 1
					AND LEN(Mensaje) = 0
			) A
			WHERE CAST(fecha_doc AS DATE) BETWEEN @FechaInicial AND @FechaFinal
				AND clase_doc = @ClaseDocumento

	SET NOCOUNT OFF

END

GO
