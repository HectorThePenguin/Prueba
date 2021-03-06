USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionAnimal_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionAnimal_Crear]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionAnimal_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 07/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : DeteccionAnimal_Crear
--======================================================
CREATE PROCEDURE [dbo].[DeteccionAnimal_Crear]
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
	INSERT DeteccionAnimal (
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
		NoFierro,
		FechaDeteccion,
		DeteccionAnalista,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@DeteccionAnimalID,
		@AnimalMovimientoID,
		@Arete,
		@AreteMetalico,
		@FotoDeteccion,
		@LoteID,
		@OperadorID,
		@TipoDeteccionID,
		@GradoID,
		@Observaciones,
		@NoFierro,
		@FechaDeteccion,
		@DeteccionAnalista,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
