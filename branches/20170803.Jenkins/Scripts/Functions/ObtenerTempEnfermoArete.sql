IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerTempEnfermoArete]'))
		DROP FUNCTION [dbo].[ObtenerTempEnfermoArete]
GO
CREATE FUNCTION ObtenerTempEnfermoArete(@Arete varchar(15))
Returns VARCHAR(1000)
AS
BEGIN
	DECLARE @Temperatura BIGINT
	DECLARE @AnimalID BIGINT
	
	SET @AnimalID = (select top 1 AnimalID from Animal where Arete = @Arete)
	
	IF ISNULL(@AnimalID,0) = 0 BEGIN
		SET @AnimalID = (select top 1 AnimalID from AnimalHistorico where Arete = @Arete)
	END
	
	IF ISNULL(@AnimalID,0) = 0 BEGIN
		SET @Temperatura = 0
	END
	ELSE BEGIN
		SET @Temperatura = (select top 1 Temperatura from AnimalMovimiento where AnimalID = @AnimalID and Temperatura > 0)
		IF ISNULL(@Temperatura,0) = 0 BEGIN
			SET @Temperatura = (select top 1 Temperatura from AnimalMovimientoHistorico where AnimalID = @AnimalID and Temperatura > 0)
		END
	END

	RETURN ISNULL(@Temperatura,0)
END
GO