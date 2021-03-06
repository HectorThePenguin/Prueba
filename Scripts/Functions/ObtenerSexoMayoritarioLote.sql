IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerSexoMayoritarioLote]'))
		DROP FUNCTION [dbo].[ObtenerSexoMayoritarioLote]
GO
--=============================================
-- Author     : Francisco Alfonso Mendez Padilla
-- Create date: 2014/10/12
-- Description: Funcion para obtener el sexo del lote en base a si hay mas machos o mas hembras
--=============================================
CREATE FUNCTION [dbo].[ObtenerSexoMayoritarioLote]
(
	@LoteID INT
)
RETURNS CHAR (1)
AS
BEGIN
	DECLARE @Sexo AS CHAR(1)
	DECLARE @Macho AS BIGINT
	DECLARE @Hembra AS BIGINT
	SET @Macho=0;
	SET @Hembra=0;
    Select @Macho=Count(TG.Sexo) FROM Animal AN (NOLOCK)
	INNER JOIN
	(Select AM.LoteID,AM.AnimalID 
		From AnimalMovimiento AM (NOLOCK)
		Where AM.LoteID=@LoteID AND AM.Activo=1     
		Group by AM.LoteID,AM.AnimalID
	) AS AG ON AN.AnimalID=AG.AnimalID
	INNER JOIN TipoGanado TG (NOLOCK) ON AN.TipoGanadoID=TG.TipoGanadoID AND TG.Activo=1
    Where TG.Sexo='M'
	Group by AG.LoteID,TG.Sexo

	Select @Hembra=Count(TG.Sexo) FROM Animal AN (NOLOCK)
	INNER JOIN
	(Select AM.LoteID,AM.AnimalID 
		From AnimalMovimiento AM (NOLOCK)
		Where AM.LoteID=@LoteID AND AM.Activo=1     
		Group by AM.LoteID,AM.AnimalID
	) AS AG ON AN.AnimalID=AG.AnimalID
	INNER JOIN TipoGanado TG (NOLOCK) ON AN.TipoGanadoID=TG.TipoGanadoID AND TG.Activo=1
    Where TG.Sexo='H'
	Group by AG.LoteID,TG.Sexo
	IF @Macho>@Hembra
	BEGIN
   	    SET  @Sexo='M'
	END
	ELSE
	BEGIN
	    IF @Hembra>@Macho
		BEGIN
   			SET @Sexo='H'
        END 
	END
	RETURN @Sexo
END

