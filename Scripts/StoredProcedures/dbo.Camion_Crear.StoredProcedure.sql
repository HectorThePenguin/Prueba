USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Camion_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Camion_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Camion_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Camion_Crear
--======================================================
--======================================================
-- Author     : Guillermo Osuna Covarrubias
-- Create date: 23/may/2017
-- Description: Guarda un nuevo tracto, se agregaron las culumnas NumEconomico, MarcaID, Modelo, Color, Boletinado, Observaciones
-- SpName     : Camion_Crear 
--======================================================
CREATE PROCEDURE [dbo].[Camion_Crear]
@ProveedorID int,
@NumEconomico		   VARCHAR(10),
@MarcaID			   INT,
@Modelo				   INT,
@Color				   VARCHAR(255),
@Boletinado			   BIT,
@Observaciones		   VARCHAR(255),
@PlacaCamion varchar(10),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Camion (
		ProveedorID,
		PlacaCamion,
		Activo,
		UsuarioCreacionID,
		FechaCreacion,
		NumEconomico,
		MarcaID,
		Modelo,
		Color,
		Boletinado,
		Observaciones
	)
	VALUES(
		@ProveedorID,
		@PlacaCamion,
		@Activo,
		@UsuarioCreacionID,
		GETDATE(),
		@NumEconomico,
		@MarcaID,
		@Modelo,
		@Color,
		@Boletinado,
		@Observaciones
	)
	SET NOCOUNT OFF;
END

GO
