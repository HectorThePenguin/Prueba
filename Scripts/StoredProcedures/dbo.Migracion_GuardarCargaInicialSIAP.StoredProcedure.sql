USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarCargaInicialSIAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Migracion_GuardarCargaInicialSIAP]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarCargaInicialSIAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:    César Valdez Figueroa
-- Create date: 30/01/2015
-- Description: Elimina el Animal de Animal
-- Origen: APInterfaces
-- Migracion_GuardarCargaInicialSIAP 
-- '<ROOT>
--   <CtrManiInfo>
--     <Arete>000000000000001</Arete>
--     <FechaComp>2013-10-06T00:00:00</FechaComp>
--     <FechaCorte>2013-10-06T00:00:00</FechaCorte>
--     <NumCorr>772</NumCorr>
--     <CalEng></CalEng>
--     <PesoCorte>433</PesoCorte>
--     <Paletas>0</Paletas>
--     <TipoGan>1</TipoGan>
--     <Temperatura>0.00</Temperatura>
--   </CtrManiInfo>
-- </ROOT>',
-- '<ROOT>
--   <CtrReimInfo>
--     <Arete>000000000000001</Arete>
--     <FechaComp>2013-04-09T00:00:00</FechaComp>
--     <FechaReim>2013-10-25T00:00:00</FechaReim>
--     <PesoReimp>383</PesoReimp>
--   </CtrReimInfo>
-- </ROOT>', 2
-- =============================================
CREATE PROCEDURE [dbo].[Migracion_GuardarCargaInicialSIAP]
/*	@CtrManiXML XML,
	@CtrReimXML XML,*/
	@OrganizacionID INT
AS
  BEGIN
    SET NOCOUNT ON

	/* Se las tablas temporales de control individual */
/*	IF OBJECT_ID('CtrManiTMP') IS NOT NULL
		DROP TABLE CtrManiTMP
	IF OBJECT_ID('CtrReimTMP') IS NOT NULL
		DROP TABLE CtrReimTMP
*/	/* Se elimina las tablas de carga inicial */
	IF OBJECT_ID('EntradaGanadoCargaInicial') IS NOT NULL
		DROP TABLE EntradaGanadoCargaInicial
	IF OBJECT_ID('LoteCargaInicial') IS NOT NULL
		DROP TABLE LoteCargaInicial
	IF OBJECT_ID('AnimalCargaInicial') IS NOT NULL
		DROP TABLE AnimalCargaInicial
	IF OBJECT_ID('AnimalMovimientoCargaInicial') IS NOT NULL
		DROP TABLE AnimalMovimientoCargaInicial
	IF OBJECT_ID('AnimalCostoCargaInicial') IS NOT NULL
		DROP TABLE AnimalCostoCargaInicial
		
	/* Se Crea temporal CtrMani */
/*	SELECT
		[Arete] = t.item.value('./Arete[1]', 'VARCHAR(15)'),
		[FechaComp] = t.item.value('./FechaComp[1]', 'DATETIME'),
		[FechaCorte] = t.item.value('./FechaCorte[1]', 'DATETIME'),
		[NumCorr] = t.item.value('./NumCorr[1]', 'VARCHAR(3)'),
		[CalEng] = t.item.value('./CalEng[1]', 'VARCHAR(2)'),
		[PesoCorte] = t.item.value('./PesoCorte[1]', 'INT'),
		[Paletas] = t.item.value('./Paletas[1]', 'INT'),
		[TipoGan] = t.item.value('./TipoGan[1]', 'VARCHAR(1)'),
		[Temperatura] = t.item.value('./Temperatura[1]', 'DECIMAL(4,2)')
	INTO CtrManiTMP
	FROM @CtrManiXML.nodes('ROOT/CtrManiInfo') AS T(item);
*/
	/* Se Crea temporal CtrReim */
/*	SELECT
		[Arete] = t.item.value('./Arete[1]', 'VARCHAR(15)'),
		[FechaComp] = t.item.value('./FechaComp[1]', 'DATETIME'),
		[FechaReim] = t.item.value('./FechaReim[1]', 'DATETIME'),
		[PesoReimp] = t.item.value('./PesoReimp[1]', 'INT')
	INTO CtrReimTMP
	FROM @CtrReimXML.nodes('ROOT/CtrReimInfo') AS T(item);
*/

	/* Cargar las Entradas de Ganado Carga Inicial */
	EXEC Migracion_GuardarEntradaGanadoCargaInicial @OrganizacionID;
	
	/* Cargar los Lotes Carga Inicial */
	EXEC Migracion_GuardarLoteCargaInicial @OrganizacionID;

	/* Cargar los Animales Carga Inicial */
	EXEC Migracion_GuardarAnimalCargaInicial @OrganizacionID;

	/* Cargar los Movimientos de los Animales Carga Inicial */
	EXEC Migracion_GuardarAnimalMovimientoCargaInicial @OrganizacionID;
	
	/* Cargar los Costos de los Animales Carga Inicial */
	EXEC Migracion_GuardarAnimalCostoCargaInicial @OrganizacionID;
	
	/* Se eliminan las tablas temporales */
	-- DROP TABLE CtrManiTMP
	-- DROP TABLE CtrReimTMP
	
	SELECT CAST(SUM(1) AS BIGINT) TotalCabezas , SUM(COALESCE(Importe,0)) TotalCostos
	  FROM AnimalCargaInicial AC
	  LEFT JOIN (SELECT Arete, SUM(COALESCE(Importe,0)) Importe
				   FROM AnimalCostoCargaInicial
				  GROUP BY Arete) AS Costo ON Costo.Arete = AC.Arete
		   
	SET NOCOUNT OFF
  END


GO
