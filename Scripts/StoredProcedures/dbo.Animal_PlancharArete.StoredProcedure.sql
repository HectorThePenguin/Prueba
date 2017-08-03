IF OBJECT_ID('[dbo].[Animal_PlancharArete]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[Animal_PlancharArete]
END
GO
--======================================================      
-- Author     : Lic. Sergio Alberto Gamez Gomez     
-- Create date: 01/10/2015  
-- Description: Intercambiar los aretes entre 2 animales  
--======================================================   
--              Modificaciones
--======================================================      
-- Author     : Erenesto Cardenas Llanes
-- Create date: 29/10/2015  
-- Description: Reemplazar aretes de un conjunto de animales por correspondencia y regrese la relacion arete/animal Id
-- SpName     : Animal_PlancharArete '<ROOT><Relacion><Arete>1231231231</Arete><AnimalID>12345</AnimalID></Relacion></ROOT>', 1
--======================================================  
CREATE PROCEDURE [dbo].[Animal_PlancharArete] (  
	@Relacion  XML,
	@UsuarioID INT
)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @RETURN TABLE (Arete Varchar(20), AnimalId BIGINT)

	SELECT
		Arete = t.item.value('./Arete[1]', 'VARCHAR(14)'),
		AnimalId = t.item.value('./AnimalId[1]', 'BIGINT')
	INTO 
		#AretesSCP
	FROM 
		@Relacion.nodes('ROOT/Relacion') AS t(item)

	UPDATE
		a
	SET
		Arete = b.Arete
	OutPut inserted.Arete, inserted.AnimalID INTO @RETURN
	FROM
		Animal a
		INNER JOIN #AretesSCP b ON
			a.AnimalID = b.AnimalId

	SELECT * FROM @RETURN
	
	SET NOCOUNT OFF
 END