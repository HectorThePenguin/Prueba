IF OBJECT_ID('[dbo].[Animal_PlancharAreteLote]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[Animal_PlancharAreteLote]
END
GO
--======================================================      
-- Author     : Ernesto Cardenas Llanes
-- Create date: 29/10/2015  
-- Description: Planchar el arete en un animal que no este programado pero que pertenezca al mismo lote que el arete ficticio
-- SpName     : Animal_PlancharAreteLote
--			   '<ROOT>
--					<Relacion>
--						<Planchar><Arete>1231231231</Arete><AnimalId>12345</AnimalId><LoteId>13122</LoteId></Planchar>
--						<Planchar><Arete>45453454</Arete><AnimalId>858823</AnimalId><LoteId>656645</LoteId></Planchar>
--						<Planchar><Arete>462423423</Arete><AnimalId>43523</AnimalId><LoteId>656645</LoteId></Planchar>
--					</Relacion>
--				</ROOT>',
--              '<ROOT>
--					<Animal>
--						<Arete>131331231</Arete><LoteId>52379</LoteId>
--					</Animal>
--					<Animal>
--						<Arete>56323123131111</Arete><LoteId>52379</LoteId>
--					</Animal>
--					<Animal>
--						<Arete>563231231313123</Arete><LoteId>52379</LoteId>
--					</Animal>
--				</ROOT>'
--======================================================   
CREATE PROCEDURE [dbo].[Animal_PlancharAreteLote] (  
	@xmlSacrificio    XML,
	@xmlPlanchadoLote XML,
	@FechaSacrificio  DATETIME
)
AS
BEGIN
	SET NOCOUNT ON
 
	DECLARE @Sacrificio TABLE(Arete VARCHAR(14), AnimalId BIGINT, LoteId INT)
	DECLARE @PlanchadoLote TABLE(Arete VARCHAR(14), LoteId INT)
	DECLARE @AretesNoConsiderados TABLE(Arete VARCHAR(14), AnimalId BIGINT, LoteId INT)

	INSERT INTO @Sacrificio(Arete, AnimalId, LoteId)
	SELECT
		t.item.value('./Arete[1]', 'VARCHAR(14)') as Arete,
		t.item.value('./AnimalId[1]', 'BIGINT') as AnimalID,
		t.item.value('./LoteId[1]', 'INT') as Corral
	FROM 
		@xmlSacrificio.nodes('ROOT/Relacion') AS t(item)

	INSERT INTO @PlanchadoLote(Arete, LoteId)
	SELECT
		t.item.value('./Arete[1]', 'VARCHAR(14)') as Arete,
		t.item.value('./LoteId[1]', 'INT') as Corral
	FROM 
		@xmlPlanchadoLote.nodes('ROOT/Animal') AS t(item)


	--Aretes no considerados en en sacrificio por lote
	INSERT INTO @AretesNoConsiderados(Arete, AnimalId, LoteId)
	SELECT a.Arete, a.AnimalId, a.LoteId
	FROM (SELECT a.Arete, a.AnimalID, l.LoteID
				FROM OrdenSacrificio os(NOLOCK)
				INNER JOIN OrdenSacrificioDetalle osd(NOLOCK) ON
					os.OrdenSacrificioID = osd.OrdenSacrificioID
				INNER JOIN Lote l (NOLOCK) ON
					osd.LoteID = l.LoteID
				INNER JOIN animalMovimiento am(NOLOCK) ON
					osd.LoteID = am.LoteID
				INNER JOIN Animal a(NOLOCK) ON
					am.AnimalID= a.AnimalID
				WHERE os.OrganizacionID = 5
				  AND Convert(date,os.FechaOrden,112) = @FechaSacrificio
				  AND l.Activo  = 1
				  AND am.Activo = 1)A 
	LEFT JOIN @Sacrificio B On a.AnimalId = b.AnimalID
							AND a.LoteID = b.LoteID
	WHERE B.AnimalID iS nuLL

--	--INSERT INTO @AretesNoConsiderados(Arete, AnimalId, LoteId)
--	SELECT B.Arete, B.AnimalId, B.LoteId
--	FROM  @Sacrificio B 
--	LEFT JOIN (SELECT a.Arete, a.AnimalID, l.LoteID
--				FROM OrdenSacrificio os(NOLOCK)
--				INNER JOIN OrdenSacrificioDetalle osd(NOLOCK) ON
--					os.OrdenSacrificioID = osd.OrdenSacrificioID
--				INNER JOIN Lote l (NOLOCK) ON
--					osd.LoteID = l.LoteID
--				INNER JOIN animalMovimiento am(NOLOCK) ON
--					osd.LoteID = am.LoteID
--				INNER JOIN Animal a(NOLOCK) ON
--					am.AnimalID= a.AnimalID
--				WHERE os.OrganizacionID = 5
--				  AND Convert(date,os.FechaOrden,112) = @FechaSacrificio
--				  AND l.Activo  = 1
--				  AND am.Activo = 1)A On a.AnimalId = b.AnimalID
--							AND a.LoteID = b.LoteID
--	--WHERE B.AnimalID =0


--SELECT 'b', *
--	FROM  @Sacrificio B 


--SELECT a.Arete, a.AnimalID, l.LoteID
--				FROM OrdenSacrificio os(NOLOCK)
--				INNER JOIN OrdenSacrificioDetalle osd(NOLOCK) ON
--					os.OrdenSacrificioID = osd.OrdenSacrificioID
--				INNER JOIN Lote l (NOLOCK) ON
--					osd.LoteID = l.LoteID
--				INNER JOIN animalMovimiento am(NOLOCK) ON
--					osd.LoteID = am.LoteID
--				INNER JOIN Animal a(NOLOCK) ON
--					am.AnimalID= a.AnimalID
--				WHERE os.OrganizacionID = 5
--				  AND Convert(date,os.FechaOrden,112) = @FechaSacrificio
--				  AND l.Activo  = 1
--				  AND am.Activo = 1
--				    anD aM.AnimalID = 1116072

	SELECT pl.LoteId
		 , anc.AnimalId
		 , pl.Arete
	FROM (
		SELECT
			LoteId
		  , ROW_NUMBER() OVER(PARTITION BY LoteId ORDER BY Arete) Consecutivo	
		  , Arete
		FROM @PlanchadoLote ) pl
	LEFT JOIN (SELECT
			LoteId
		  , ROW_NUMBER() OVER(PARTITION BY LoteId ORDER BY Arete) Consecutivo
		  , Arete
		  , AnimalId
		FROM @AretesNoConsiderados) anc ON ANC.LoteId = PL.LoteId
								AND ANC.Consecutivo = PL.Consecutivo

	SET NOCOUNT OFF
 END