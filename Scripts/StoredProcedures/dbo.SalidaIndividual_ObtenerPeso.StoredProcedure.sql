USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividual_ObtenerPeso]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividual_ObtenerPeso]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividual_ObtenerPeso]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Pedro Delgado
-- Create date: 03/03/2014
-- Description: Obtiene el peso proyectado de un animal
-- Origen     : APInterfaces
-- Modificaciones : 21-05-2014
--					Gilberto Carranza
--					Se agrego consulta para obtener
--					el ultimo corral de produccion
--					en el que se encontraba el animal
--					para obtener el peso correcto					
-- SalidaIndividual_ObtenerPeso '300338' , 1
-- =============================================
CREATE PROCEDURE [dbo].[SalidaIndividual_ObtenerPeso]
@Arete VARCHAR(15),
@OrganizacionID INT
AS
BEGIN
	DECLARE @FechaInicio DATETIME
	DECLARE @Peso INT
	DECLARE @PesoProyectado INT
	DECLARE @GananciaDiaria DECIMAL
	DECLARE @DiasDiferencia INT
	DECLARE @LoteID INT
	DECLARE @Cabezas INT
	------ Gilberto Carranza 21-05-2014 ------
	DECLARE @CorralProduccion INT	
	DECLARE @tMovimientoProduccion TABLE
	(
		AnimalMovimientoID	BIGINT
		, LoteID			INT
		, Peso				INT
		, AnimalID			BIGINT
	)

	SET @CorralProduccion = 2
	INSERT INTO @tMovimientoProduccion
	SELECT MAX(AM.AnimalMovimientoID) AS AnimalMovimientoID
		,  AM.LoteID AS LoteProduccion
		,  AM.Peso
		,  AM.AnimalID
	FROM AnimalMovimiento AM(NOLOCK)
	INNER JOIN Corral C
		ON (AM.CorralID = C.CorralID
			AND C.TipoCorralID = @CorralProduccion)
	INNER JOIN Animal A(NOLOCK)
		ON (AM.AnimalID = A.AnimalID
			AND AM.OrganizacionID = @OrganizacionID
			AND A.Arete = @Arete)
	GROUP BY AM.LoteID
		,	 AM.Peso
		,	 AM.AnimalID

	SELECT TOP 1 @Peso = AM.Peso
				,@LoteID = AM.LoteID
	FROM @tMovimientoProduccion AM
	------ Gilberto Carranza 21-05-2014 ------
	/*
	SELECT @Peso = AM.Peso,@LoteID = AM.LoteID
	FROM AnimalMovimiento (NOLOCK) AM
	INNER JOIN Animal (NOLOCK) A
	ON (AM.AnimalID = A.AnimalID)
	WHERE A.Activo = 1 AND AM.Activo = 1 AND AM.OrganizacionID = @OrganizacionID AND A.Arete = @Arete
	*/

	SELECT @Peso = ISNULL(SUM(AM.Peso), 0)
	FROM AnimalMovimiento (NOLOCK) AM
	WHERE AM.Activo = 1 AND AM.LoteID = @LoteID

	IF (@Peso = 0)
	BEGIN

		DECLARE @AnimalID BIGINT
		SET @AnimalID = (SELECT AnimalID FROM Animal(NOLOCK) WHERE RTRIM(LTRIM(Arete)) = RTRIM(LTRIM(@Arete)))	

		SET @Peso = (SELECT Peso FROM AnimalMovimiento(NOLOCK) WHERE AnimalID = @AnimalID AND Activo = 1)

	END
	
	SELECT @FechaInicio = FechaInicio, @Cabezas = Cabezas
	FROM Lote (NOLOCK) L
	WHERE LoteID = @LoteID AND Activo = 1 AND OrganizacionID = @OrganizacionID
	
	SELECT @GananciaDiaria = GananciaDiaria 
	FROM LoteProyeccion (NOLOCK) LP
	WHERE LoteID = @LoteID AND OrganizacionID = @OrganizacionID

	SELECT @DiasDiferencia = DATEDIFF(Day, @FechaInicio, GETDATE())  

	SELECT @PesoProyectado = (@Peso / @Cabezas)  + ISNULL(CAST((@DiasDiferencia * @GananciaDiaria) AS INT),0)
	 
	SELECT @FechaInicio AS FechaInicio,ISNULL(@Peso,0) AS Peso,@GananciaDiaria AS GananciaDiaria,
				 ISNULL(@DiasDiferencia,0) AS DiasDiferencia,ISNULL(@PesoProyectado,@Peso) AS PesoProyectado, @Cabezas AS Cabezas
END
GO
