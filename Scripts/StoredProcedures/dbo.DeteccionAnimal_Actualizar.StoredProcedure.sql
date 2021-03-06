USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionAnimal_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionAnimal_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionAnimal_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 07/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : DeteccionAnimal_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[DeteccionAnimal_Actualizar]
@DeteccionAnimalID int,
@AnimalMovimientoID bigint,
@Arete varchar(15),
@AreteMetalico varchar(15),
@FotoDeteccion varchar(250),
@LoteID int,
@OperadorID int,
@TipoDeteccionID int,
@GradoID int,
@Observaciones varchar(255),
@NoFierro varchar(10),
@FechaDeteccion smalldatetime,
@DeteccionAnalista bit,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE DeteccionAnimal SET
		AnimalMovimientoID = @AnimalMovimientoID,
		Arete = @Arete,
		AreteMetalico = @AreteMetalico,
		FotoDeteccion = @FotoDeteccion,
		LoteID = @LoteID,
		OperadorID = @OperadorID,
		TipoDeteccionID = @TipoDeteccionID,
		GradoID = @GradoID,
		Observaciones = @Observaciones,
		NoFierro = @NoFierro,
		FechaDeteccion = @FechaDeteccion,
		DeteccionAnalista = @DeteccionAnalista,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioCreacionID,
		FechaModificacion = GETDATE()
	WHERE DeteccionAnimalID = @DeteccionAnimalID
	SET NOCOUNT OFF;
END

GO
