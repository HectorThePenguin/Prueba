USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Rendimiento_EliminarTransaccionales_Lucero]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Rendimiento_EliminarTransaccionales_Lucero]
GO
/****** Object:  StoredProcedure [dbo].[Rendimiento_EliminarTransaccionales_Lucero]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Rendimiento_EliminarTransaccionales_Lucero 1, '2015-04-06'

CREATE PROCEDURE [dbo].[Rendimiento_EliminarTransaccionales_Lucero]
	@organizacion int
	, @fecha datetime
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		ls.LoteSacrificioID
		, l.LoteID
		, lsd.AnimalID 
	INTO 
		#animales
	FROM 
		LoteSacrificioLucero ls
		INNER JOIN LoteSacrificioLuceroDetalle lsd on
			ls.LoteSacrificioID = lsd.LoteSacrificioID
		INNER JOIN Lote l on
			ls.LoteID = l.LoteID
		INNER JOIN InterfaceSalidaTraspasoDetalle istd on
			ls.InterfaceSalidaTraspasoDetalleID = istd.InterfaceSalidaTraspasoDetalleID
		INNER JOIN InterfaceSalidaTraspaso ist on
			istd.InterfaceSalidaTraspasoID = ist.InterfaceSalidaTraspasoID
	WHERE
		DATEADD(d,0,DATEDIFF(d,0,ls.Fecha)) = @fecha
		AND ist.OrganizacionIDDestino = @organizacion
	
	DECLARE @am int, @ac int, @an int, @a int
	
	SELECT
		@am = Count(1)
	FROM
		AnimalMovimientoHistorico a
		INNER JOIN #animales t ON a.AnimalID = t.AnimalID

	SELECT
		@ac = Count(1)
	FROM
		AnimalCostoHistorico a
		INNER JOIN #animales t ON a.AnimalID = t.AnimalID

	SELECT
		@an = Count(1)
	FROM
		AnimalConsumoHistorico a
		INNER JOIN #animales t ON a.AnimalID = t.AnimalID

	SELECT
		@a = Count(1)
	FROM
		AnimalHistorico a
		INNER JOIN #animales t ON a.AnimalID = t.AnimalID

	IF @am > 0 and @ac > 0 and @an > 0 and @a > 0
	BEGIN

		DELETE a
		FROM
			AnimalMovimiento a
			INNER JOIN #animales t ON a.AnimalID = t.AnimalID

		DELETE a
		FROM
			AnimalCosto a
			INNER JOIN #animales t ON a.AnimalID = t.AnimalID

		DELETE a
		FROM
			AnimalConsumo a
			INNER JOIN #animales t ON a.AnimalID = t.AnimalID

		DELETE a
		FROM
			Animal a
			INNER JOIN #animales t ON a.AnimalID = t.AnimalID


		SELECT 'OK'
	END
	ELSE
	BEGIN
		RAISERROR ('NO SE PUEDE REALIZAR LA ELIMINACION DE LAS TABLAS TRANSACCIONALES POR QUE UNA O MAS TABLAS NO SE HAN PASADO A LAS HISTORICO', 16, 1)
	END

	SET NOCOUNT OFF;
END




GO
