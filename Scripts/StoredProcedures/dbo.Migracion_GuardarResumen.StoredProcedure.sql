USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarResumen]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Migracion_GuardarResumen]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarResumen]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    César Valdez Figueroa
-- Create date: 30/01/2015
-- Description: Guarda informacion en la tabla resumen
-- Origen: APInterfaces
-- Migracion_GuardarResumen 1
-- =============================================
CREATE PROCEDURE [dbo].[Migracion_GuardarResumen]
	@ResumenXML XML,
	@OrganizacionID INT
AS
  BEGIN
    SET NOCOUNT ON
	DECLARE @TotalCabezas BIGINT;
	DECLARE @TotalCostos DOUBLE PRECISION;
	/* Se elimina la informacion de la tabla resumen para la organizacion a trabajar */
	DELETE FROM RESUMEN WHERE Organizacion = @OrganizacionID;
	/* Se inserta la informacion en la tabla resumen*/
	INSERT INTO RESUMEN 
	SELECT
		[ORGANIZACION] = t.item.value('./Organizacion[1]', 'INT'),
		[FECHA INICIO] = t.item.value('./FechaInicio[1]', 'DATETIME'),
		[FECHA DISPONIBILIDAD] = t.item.value('./FechaDisponibilidad[1]', 'DATETIME'),
		[CORRAL] = t.item.value('./Corral[1]', 'VARCHAR(255)'),
		[LOTE] = t.item.value('./Lote[1]', 'VARCHAR(255)'),
		[TIPO DE GANADO] = t.item.value('./TipoGanado[1]', 'VARCHAR(255)'),
		[CABEZAS] = t.item.value('./Cabezas[1]', 'FLOAT(53)'),
		[DIASENG] = t.item.value('./DiasEng[1]', 'FLOAT(53)'),
		[GANANCIA DIARIA] = t.item.value('./GananciaDiaria[1]', 'FLOAT(53)'),
		[PESO ORIGEN] = t.item.value('./PesoOrigen[1]', 'FLOAT(53)'),
		[PESO PROYECTADO] = t.item.value('./PesoProyectado[1]', 'FLOAT(53)'),
		[FORMULA ACTUAL] = t.item.value('./FormulaActual[1]', 'VARCHAR(255)'),
		[CONSUMO] = t.item.value('./Consumo[1]', 'FLOAT(53)'),
		[COSTO DE GANADO] = t.item.value('./Ganado[1]', 'FLOAT(53)'),
		[ALIMENTO] = t.item.value('./Alimento[1]', 'FLOAT(53)'),
		[COMISION] = t.item.value('./Comision[1]', 'FLOAT(53)'),
		[FLETES] = t.item.value('./Fletes[1]', 'FLOAT(53)'),
		[IMPUESTO PREDIAL] = t.item.value('./ImpPred[1]', 'FLOAT(53)'),
		[GUIAS DE TRANSITO] = t.item.value('./GuiaTrans[1]', 'FLOAT(53)'),
		[SEGURO DE MORTANDAD CORRALES] = t.item.value('./SeguroDeCorrales[1]', 'FLOAT(53)'),
		[SEGURO DE TRANSPORTES] = t.item.value('./SeguroDeTransporte[1]', 'FLOAT(53)'),
		[GASTOS ENGORDA FIJOS] = t.item.value('./GastoDeEngorda[1]', 'FLOAT(53)'),
		[GASTOS CENTROS FIJOS] = t.item.value('./GastoDeCentro[1]', 'FLOAT(53)'),
		[GASTOS PLANTA ALIMENTO FIJOS] = t.item.value('./GastoDePlanta[1]', 'FLOAT(53)'),
		[BAÑOS] = t.item.value('./CertificadoyBanos[1]', 'FLOAT(53)'),
		[ALIMENTO EN CENTRO] = t.item.value('./AlimentoCentros[1]', 'FLOAT(53)'),
		[PRUEBAS TB Y BR] = t.item.value('./Pruebas[1]', 'FLOAT(53)'),
		[MEDICAMENTO EN CENTRO] = t.item.value('./MedicamentoEnCentros[1]', 'FLOAT(53)'),
		[RENTA] = t.item.value('./Renta[1]', 'FLOAT(53)'),
		[SEGURO DE ENFERMEDADES EXOTICAS] = t.item.value('./SeguroDeEnfermedadesExoticas[1]', 'FLOAT(53)'),
		[MEDICAMENTO ENFERMERIA] = t.item.value('./MedicamentoDeEnfermerias[1]', 'FLOAT(53)'),
		[MEDICAMENTO DE IMPLANTE] = t.item.value('./MedicamentoDeImpolante[1]', 'FLOAT(53)'),
		[MEDICAMENTO DE REIMPLANTE] = t.item.value('./MedicamentoDeReimplante[1]', 'FLOAT(53)'),
		[GASTOS INDIRECTOS] = t.item.value('./GastosIndirectos[1]', 'FLOAT(53)'),
		[SEGURO DE GANADO EN CENTRO] = t.item.value('./SeguroDeGanadoEnCentro[1]', 'FLOAT(53)'),
		[MEDICAMENTO EN PRADERA] = t.item.value('./MedicamentoEnPradera[1]', 'FLOAT(53)'),
		[ALIMENTO EN CADIS] = t.item.value('./AlimentacionEnCadis[1]', 'FLOAT(53)'),
		[MEDICAMENTO EN CADIS] = t.item.value('./MedicamentoEnCadis[1]', 'FLOAT(53)'),
		[GASTOS MATERIAS PRIMAS] = t.item.value('./GastosMateriasPrimas[1]', 'FLOAT(53)'),
		[GASTOS FINANCIEROS] = t.item.value('./GastosFinancieros[1]', 'FLOAT(53)'),
		[GASTOS PRADERAS FIJOS] = t.item.value('./GastosPraderasFijos[1]', 'FLOAT(53)'),
		[SEGURO ALTA MORTANDAD PRADERA] = t.item.value('./SeguroAltaMortandadPradera[1]', 'FLOAT(53)'),
		[ALIMENTO EN DESCANSO] = t.item.value('./AlimentoEnDescanso[1]', 'FLOAT(53)'),
		[MEDICAMENTO EN DESCANSO] = t.item.value('./MedicamentoEnDescanso[1]', 'FLOAT(53)'),
		[MANEJO DE GANADO] = t.item.value('./ManejoDeGanado[1]', 'FLOAT(53)'),
		[COSTO DE PRADERA] = t.item.value('./CostoDePradera[1]', 'FLOAT(53)'),
		[DEMORAS] = t.item.value('./Demoras[1]', 'FLOAT(53)'),
		[MANIOBRAS] = t.item.value('./Maniobras[1]', 'FLOAT(53)')
	FROM @ResumenXML.nodes('ROOT/ResumenInfo') AS T(item)
	WHERE t.item.value('./Organizacion[1]', 'INT') = @OrganizacionID;
	SELECT @TotalCabezas = SUM([CABEZAS]), 
		   @TotalCostos = SUM(
							(
								ROUND(COALESCE([COSTO DE GANADO],0),2)+
								ROUND(COALESCE([ALIMENTO],0),2)+
								ROUND(COALESCE([COMISION],0),2)+
								ROUND(COALESCE([FLETES],0),2)+
								ROUND(COALESCE([IMPUESTO PREDIAL],0),2)+
								ROUND(COALESCE([GUIAS DE TRANSITO],0),2)+
								ROUND(COALESCE([PRUEBAS TB Y BR],0),2)+
								ROUND(COALESCE([GASTOS INDIRECTOS],0),2)+
								ROUND(COALESCE([GASTOS ENGORDA FIJOS],0),2)+
								ROUND(COALESCE([GASTOS PLANTA ALIMENTO FIJOS],0),2)+
								ROUND(COALESCE([GASTOS CENTROS FIJOS],0),2)+
								ROUND(COALESCE([ALIMENTO EN CENTRO],0),2)+
								ROUND(COALESCE([MEDICAMENTO EN CENTRO],0),2)+
								ROUND(COALESCE([MEDICAMENTO DE IMPLANTE],0),2)+
								ROUND(COALESCE([MEDICAMENTO DE REIMPLANTE],0),2)+
								ROUND(COALESCE([MEDICAMENTO ENFERMERIA],0),2)+
								ROUND(COALESCE([SEGURO DE MORTANDAD CORRALES],0),2)+
								ROUND(COALESCE([SEGURO DE TRANSPORTES],0),2)+
								ROUND(COALESCE([SEGURO DE GANADO EN CENTRO],0),2)+
								ROUND(COALESCE([SEGURO DE ENFERMEDADES EXOTICAS],0),2)+
								ROUND(COALESCE([RENTA],0),2)+
								ROUND(COALESCE([MEDICAMENTO EN PRADERA],0),2)+
								ROUND(COALESCE([BAÑOS],0),2)+
								ROUND(COALESCE([ALIMENTO EN CADIS],0),2)+
								ROUND(COALESCE([MEDICAMENTO EN CADIS],0),2)+
								ROUND(COALESCE([GASTOS MATERIAS PRIMAS],0),2)+
								ROUND(COALESCE([GASTOS FINANCIEROS],0),2)+
								ROUND(COALESCE([GASTOS PRADERAS FIJOS],0),2)+
								ROUND(COALESCE([SEGURO ALTA MORTANDAD PRADERA],0),2)+
								ROUND(COALESCE([ALIMENTO EN DESCANSO],0),2)+
								ROUND(COALESCE([MEDICAMENTO EN DESCANSO],0),2)+
								ROUND(COALESCE([MANEJO DE GANADO],0),2)+
								ROUND(COALESCE([COSTO DE PRADERA],0),2)+
								ROUND(COALESCE([DEMORAS],0),2)+
								ROUND(COALESCE([MANIOBRAS],0),2)
							)* [CABEZAS])
	FROM RESUMEN
	WHERE [ORGANIZACION] = @OrganizacionID;
	SELECT @TotalCabezas AS TotalCabezas, @TotalCostos AS TotalCostos;
	SET NOCOUNT OFF
  END

GO
