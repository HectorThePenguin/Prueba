USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionAnimal_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionAnimal_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionAnimal_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 07/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : DeteccionAnimal_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[DeteccionAnimal_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		DeteccionAnimalID,
		AnimalMovimientoID,
		Arete,
		AreteMetalico,
		FotoDeteccion,
		LoteID,
		OperadorID,
		TipoDeteccionID,
		GradoID,
		Observaciones,
		DescripcionGanadoID,
		NoFierro,
		FechaDeteccion,
		DeteccionAnalista,
		Activo
	FROM DeteccionAnimal(NOLOCK)
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
