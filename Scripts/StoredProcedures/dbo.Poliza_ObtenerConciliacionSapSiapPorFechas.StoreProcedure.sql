USE [SIAP]
GO
DROP PROCEDURE [dbo].[Poliza_ObtenerConciliacionSapSiapPorFechas]
GO
--======================================================
-- Author     : Ramses Santos 
-- Create date: 02/12/2015
-- Description: Obtiene el listado de pólizas para su conciliacion en base al rango de fechas indicado
-- SpName     : Poliza_ObtenerConciliacionSapSiapPorFechas
/*
Poliza_ObtenerConciliacionSapSiapPorFechas 1, '20150822', '20150930',
'<ROOT>
		<Divisiones>
			<Division>CLN</Division>
		</Divisiones>
	 </ROOT>','11514'
*/
--======================================================
CREATE PROCEDURE [dbo].[Poliza_ObtenerConciliacionSapSiapPorFechas]
@OrganizacionID	INT
, @FechaInicio	DATE
, @FechaFin		DATE
, @XmlDivision	XML
,@Prefijo varchar(10)
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @FechaInicial CHAR(8), @FechaFinal CHAR(8)

	SET @FechaInicial = CONVERT(CHAR(8), @FechaInicio, 112)
	SET @FechaFinal = CONVERT(CHAR(8), @FechaFin, 112)
	SELECT A.cuenta
                           , A.importe
                           , A.concepto
                           , A.division
                           , A.ref3
                           , A.clase_doc
                           , A.PolizaID
                           , A.TipoPolizaID
                           , A.Folio
                           , A.FechaDocumento
						   , A.FechaContabilidad
                           , A.ClavePoliza
                           , A.PostFijoRef3
                           , A.DocumentoSAP
                    FROM
                    (
                           SELECT  T.N.value('./cuenta[1]','VARCHAR(100)') AS cuenta
                                        , T.N.value('./importe[1]','VARCHAR(100)') AS importe
                                        , T.N.value('./concepto[1]','VARCHAR(100)') AS concepto
                                        , T.N.value('./division[1]','VARCHAR(100)') AS division
                                        , T.N.value('./ref3[1]','VARCHAR(20)') AS ref3
                                        , T.N.value('./clase_doc[1]','VARCHAR(100)') AS clase_doc
										, T.N.value('./fecha_cont[1]','VARCHAR(8)') AS FechaContabilidad
                                        , LEFT(T.N.value('./DocumentoSAP[1]','VARCHAR(100)'),10) AS documentosap
                                        , P.PolizaID
                                        , P.TipoPolizaID
                                        , P.Folio
                                        , P.FechaDocumento
                                        , TP.ClavePoliza
                                        , TP.PostFijoRef3
                           FROM Poliza P(NOLOCK)
                           CROSS APPLY xmlPoliza.nodes('/MT_POLIZA_PERIFERICO/Datos') as T(N)
                           INNER JOIN CatTipoPoliza TP ON TP.TipoPolizaID = P.TipoPolizaID
                           WHERE 1=1
                                  AND P.OrganizacionID = 1
                                  AND    P.FechaDocumento   BETWEEN @FechaInicio AND @FechaFin
                                  AND P.Procesada = 1
								  AND P.Mensaje = ''
								 
                    ) A
                    WHERE 1=1 
					AND LEFT(cuenta,LEN(RTRIM(@Prefijo))) = RTRIM(@Prefijo)
					AND division  IN (
											SELECT t.item.value('./Division[1]', 'VARCHAR(10)') AS Id
											FROM @XmlDivision.nodes('ROOT/Divisiones') AS T (item)
										)
                    order by CUENTA


	SET NOCOUNT OFF
END
GO


