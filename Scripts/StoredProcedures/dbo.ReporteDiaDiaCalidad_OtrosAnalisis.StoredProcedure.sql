USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteDiaDiaCalidad_OtrosAnalisis]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteDiaDiaCalidad_OtrosAnalisis]
GO
/****** Object:  StoredProcedure [dbo].[ReporteDiaDiaCalidad_OtrosAnalisis]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gumaro Alberto Lugo
-- Create date: 03/07/2014 12:00:00 p.m.
-- Description:  Procedimiento para generar el reporte de dia a dia calidad
-- SpName     : ReporteDiaDiaCalidad_OtrosAnalisis 1, '20140818'
--======================================================
CREATE PROCEDURE [dbo].[ReporteDiaDiaCalidad_OtrosAnalisis]
@OrganizacionID INT,  @Fecha DATE
AS
BEGIN
SET NOCOUNT ON
DECLARE @EstatusIDCerrado INT
DECLARE @FactorAlmidonHeces decimal(12,2)
DECLARE @TipoParametro INT
SET @TipoParametro = 24
SET @EstatusIDCerrado = 40
CREATE TABLE #OtrosAnalisis
(	
	Clave varchar(250) NOT NULL,
	Descripcion varchar(250) NOT NULL,
	Rango varchar(250) DEFAULT '',
	Resultado decimal(12,2) NOT NULL DEFAULT 0.00,
	Observaciones varchar(250) DEFAULT ''
) 
--Obtenemos la informacion de los tipos parametros de otros analisis del reporte tipoParametro 24
insert into #OtrosAnalisis (Clave, Descripcion, Rango)
SELECT P.Clave, P.Descripcion, PO.Valor
FROM ParametroOrganizacion PO
INNER JOIN Parametro P ON P.ParametroID = PO.ParametroID
INNER JOIN TipoParametro TP ON TP.TipoParametroID = P.TipoParametroID WHERE (TP.TipoParametroID = @TipoParametro)
AND PO.OrganizacionID = @OrganizacionID
/*
 *	Calculo del otros analisis almidon en heces
 */
/*
SET @FactorAlmidonHeces = (
SELECT SUM(TablaFactor.Promedio)/COUNT(1) AS Factor FROM(
SELECT AF.Descripcion, AF.Lectura1, AF.Lectura2, AF.Peso, AF.MateriaSeca,
((AF.Lectura1 + AF.Lectura2)/2) as 'Prom lect',
(AF.Peso * AF.MateriaSeca) as 'Prom de mat seca',
(AF.Peso * AF.MateriaSeca) / ((AF.Lectura1 + AF.Lectura2)/2) as Promedio
FROM AlmidonFactor AF
WHERE OrganizacionID = @OrganizacionID
GROUP BY AF.Descripcion, AF.Lectura1, AF.Lectura2, AF.Peso,
AF.MateriaSeca
) AS TablaFactor);
update #OtrosAnalisis set Resultado = PromedioTotal.PromedioTotal, Observaciones = PromedioTotal.Observaciones
FROM
(SELECT SUM (TablaPromedio.TotalAlmidonPromedio) / COUNT(1) AS 'PromedioTotal',
	(SELECT TOP 1 Observaciones FROM AlmidonHecesDetalle WHERE AlmidonHecesID = TablaPromedio.AlmidonHecesID ORDER BY FechaCreacion DESC) AS Observaciones
	FROM(
	SELECT AHD.Lectura1, AHD.Lectura2, AHD.Peso,
	((@FactorAlmidonHeces * AHD.Lectura1/AHD.Peso) * 100) as '% Almidon1', ((@FactorAlmidonHeces * AHD.Lectura2/AHD.Peso) * 100) as '% Almidon2', 
	((((@FactorAlmidonHeces * AHD.Lectura1/AHD.Peso) * 100) + ((@FactorAlmidonHeces * AHD.Lectura2/AHD.Peso) * 100))/2) as TotalAlmidonPromedio, 
	AHD.AlmidonHecesID
	FROM AlmidonHecesDetalle AHD
	INNER JOIN AlmidonHeces AH on AH.AlmidonHecesID =
	AHD.AlmidonHecesID
	WHERE AH.OrganizacionID = @OrganizacionID and cast(AH.FechaAnalisis as DATE) = @Fecha and AH.EstatusID = @EstatusIDCerrado
		--AND AH.ProductoID = 100
	GROUP BY AHD.Lectura1, AHD.Lectura2, AHD.Peso,
	AHD.AlmidonHecesID
	)AS TablaPromedio
GROUP BY TablaPromedio.AlmidonHecesID) as PromedioTotal
WHERE Clave = 'AnalisisAlmidonHeces'
*/
/*
 *	Calculo del otros analisis Eficiencia mezclado
 */
 DECLARE @Media DECIMAL(12,2)
CREATE TABLE #CalidadMezcladoFormulas(
		CalidadMezcladoID int,
		TipoMuestraID int, 
		Descripcion varchar(50),
		Particulas int, 
		ParticulasEsperadas int, 
		PromedioParticulas int default 0, 
		EficienciaRecuperacion decimal(12,2) default 0.00,
		Varianza decimal(12,2) default 0.00,
		DesviacionEstandar decimal(12,2) default 0.00,
		Coeficiente decimal(12,2) default 0.00
)
insert into #CalidadMezcladoFormulas (CalidadMezcladoID, TipoMuestraID, Descripcion, ParticulasEsperadas, Particulas, PromedioParticulas)
Select CM.CalidadMezcladoID, CMD.TipoMuestraID, TM.Descripcion, (Factor*100) / (CAST(PesoBaseSeca as DECIMAL)/(CAST(PesoBaseHumeda as DECIMAL)) * 100) * CMD.Peso as ParticulasEsperadas,
		CMD.Particulas,
		(SELECT SUM(CMX.Particulas) / count(CMX.Particulas) from CalidadMezcladoDetalle CMX WHERE CMX.CalidadMezcladoID = CM.CalidadMezcladoID) as Promedio
from CalidadMezclado CM
INNER JOIN CalidadMezcladoDetalle CMD ON CM.CalidadMezcladoID = CMD.CalidadMezcladoID
INNER JOIN TipoMuestra TM ON tm.TipoMuestraID = CMD.TipoMuestraID
INNER JOIN CalidadMezcladoFactor CMF ON TM.TipoMuestraID = CMF.TipoMuestraID
WHERE CM.OrganizacionID = @OrganizacionID 
	  AND CAST(CM.Fecha as DATE) = @Fecha
GRoup by CM.CalidadMezcladoID, CMD.TipoMuestraID, TM.Descripcion, CMF.Factor, CMF.PesoBaseSeca, CMF.PesoBaseHumeda, CMD.Peso, CMD.Particulas
/* % de eficiencia de recuperaci�n: Mostrar para cada una de las muestras (M inicial, M media, M final) el % de eficiencia de recuperaci�n aplicando el siguiente 
 c�lculo:(Prom. de part�culas encontradas / part�culas esperadas *100)
 */
 update #CalidadMezcladoFormulas set EficienciaRecuperacion = ((CAST(Particulas AS DECIMAL) / CAST( ParticulasEsperadas AS DECIMAL)) * 100)
 SET @Media = (SELECT SUM(EficienciaRecuperacion) / count(*) from #CalidadMezcladoFormulas)
 /*Varianza = Es la media de las diferencias con la media elevadas al cuadrado.
C�lculo = ((% de eficiencia de recuperaci�n M inicia - Media)2 + (% de eficiencia de recuperaci�n M media � Media)2 + (% de eficiencia de recuperaci�n M final - Media) 2 / n-1.). 
Donde n = a la cantidad de tipos de muestra analizadas. */
update #CalidadMezcladoFormulas set Varianza =  power((EficienciaRecuperacion - @Media), 2)
update #CalidadMezcladoFormulas set DesviacionEstandar = CAST(sqrt(ABS(Varianza)) as DECIMAL(12,2))
update #CalidadMezcladoFormulas set Coeficiente = ((DesviacionEstandar * 100) / @Media)
--Observaci�n: Para este an�lisis el campo Observaciones se mostrar� vac�o.
update #OtrosAnalisis SET Resultado = COALESCE((SELECT SUM(Coeficiente) / Count(*) FROM #CalidadMezcladoFormulas), 0) where Clave = 'AnalisisEficienciaMezclado'
select Clave, Descripcion, Rango, Resultado, Observaciones from #OtrosAnalisis
drop table #OtrosAnalisis
drop table #CalidadMezcladoFormulas
SET NOCOUNT OFF
END

GO
