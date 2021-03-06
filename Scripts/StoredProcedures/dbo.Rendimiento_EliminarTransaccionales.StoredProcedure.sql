USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Rendimiento_EliminarTransaccionales]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Rendimiento_EliminarTransaccionales]
GO
/****** Object:  StoredProcedure [dbo].[Rendimiento_EliminarTransaccionales]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Cesar Fernando Vega Vazquez
-- Create date: <01 Abr 2015>
-- Description: Elimina las tablas transaccionales que fueron sacrificadas
-- Rendimiento_EliminarTransaccionales 1, '2015-04-07'
-- =============================================

CREATE PROCEDURE [dbo].[Rendimiento_EliminarTransaccionales]
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
		LoteSacrificio ls
		INNER JOIN LoteSacrificioDetalle lsd on
			ls.LoteSacrificioID = lsd.LoteSacrificioID
		INNER JOIN Lote l on
			ls.LoteID = l.LoteID
	WHERE
		l.OrganizacionID = @organizacion
		AND DATEADD(d,0,DATEDIFF(d,0,ls.Fecha)) = @fecha

	
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
