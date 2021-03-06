USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_GrabarDeteccionGenerica]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_GrabarDeteccionGenerica]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_GrabarDeteccionGenerica]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Cesar Valdez
-- Create date: 07/11/2014
-- Description:	Guarda una deteccion generica
-- DeteccionGanado_GrabarDeteccionGenerica 1 
--======================================================
CREATE PROCEDURE [dbo].[DeteccionGanado_GrabarDeteccionGenerica]
	@DeteccionID INT
AS
BEGIN
	DECLARE @LoteActual INT;
	DECLARE @IdentityID INT;
	/* Guarda una deteccion generica */
	INSERT INTO Deteccion (
		Arete,
		AreteMetalico,
		FotoDeteccion,
		LoteID,
		OperadorID,
		TipoDeteccionID,
		GradoID,
		Observaciones,
		NoFierro,
		FechaDeteccion,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID,
		DescripcionGanadoID  )
	SELECT TOP 1
		Arete = A.Arete,
		AreteMetalico = A.AreteMetalico,
		FotoDeteccion,
		LoteID = AM.LoteID,
		DA.OperadorID,
		DA.TipoDeteccionID,
		DA.GradoID,
		DA.Observaciones,
		DA.NoFierro,
		FechaDeteccion = GETDATE(),
		Activo = 1,
		FechaCreacion = GETDATE(),
		DA.UsuarioCreacionID,
		FechaModificacion = NULL,
		UsuarioModificacionID = NULL,
		DA.DescripcionGanadoID  
	 FROM Animal A
	INNER JOIN AnimalMovimiento AM ON A.AnimalID = AM.AnimalID
	INNER JOIN DeteccionAnimal DA ON (DA.Arete = A.Arete OR DA.AreteMetalico = A.AreteMetalico)
	WHERE AM.Activo = 1
	AND DA.DeteccionAnimalID = @DeteccionID;
	/*Se obtiene el id Insertado */
	SET @IdentityID = (SELECT @@IDENTITY)
	/*Se crea el detalle de la deteccion */
	INSERT INTO DeteccionSintoma
    SELECT 
		DeteccionID = @IdentityID,
		SintomaID,
		Activo,
		FechaCreacion = GETDATE(),
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID  
	  FROM DeteccionSintomaAnimal 
	 WHERE DeteccionAnimalID = @DeteccionID;
	SELECT @IdentityID;
END

GO
