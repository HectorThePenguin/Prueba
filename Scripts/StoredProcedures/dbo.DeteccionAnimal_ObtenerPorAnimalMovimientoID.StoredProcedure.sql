USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionAnimal_ObtenerPorAnimalMovimientoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionAnimal_ObtenerPorAnimalMovimientoID]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionAnimal_ObtenerPorAnimalMovimientoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 07/11/2014 12:00:00 a.m.
-- Description: Obtiene la deteccion animal en base al animal movimientp
-- SpName     : DeteccionAnimal_ObtenerPorAnimalMovimientoID
--======================================================
CREATE PROCEDURE [dbo].[DeteccionAnimal_ObtenerPorAnimalMovimientoID]
@AnimalMovimientoID bigint
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		DeteccionAnimalID,
		AnimalMovimientoID,
		Arete,
		AreteMetalico,
		FotoDeteccion,
		lo.LoteID,
		lo.Lote,
		lo.TipoCorralID,
		op.OperadorID,
		op.Nombre AS Operador,
		td.TipoDeteccionID,
		td.Descripcion AS TipoDeteccion,
		gr.GradoID,
		gr.Descripcion AS Grado,
		Observaciones,		
		NoFierro,
		FechaDeteccion,
		DeteccionAnalista,
		da.Activo,
		gc.GrupoCorralID
	FROM DeteccionAnimal da(NOLOCK)
	INNER JOIN Lote lo on da.LoteID = lo.LoteID
	inner join Operador op on da.OperadorID = op.OperadorID
	INNER JOIN TipoCorral tc on lo.TipoCorralID = tc.TipoCorralID
	INNER JOIN GrupoCorral gc on tc.GrupoCorralID = gc.GrupoCorralID
	INNER JOIN TipoDeteccion td on da.TipoDeteccionID = td.TipoDeteccionID
	INNER JOIN Grado gr on da.GradoID = gr.GradoID
	WHERE da.AnimalMovimientoID = @AnimalMovimientoID
	and da.Activo = 1
	SET NOCOUNT OFF;
END

GO
