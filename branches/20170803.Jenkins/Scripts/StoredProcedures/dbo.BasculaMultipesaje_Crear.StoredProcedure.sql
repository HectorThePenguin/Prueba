USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[BasculaMultipesaje_Crear]     ******/
DROP PROCEDURE [dbo].[BasculaMultipesaje_Crear]
GO
/****** Object:  StoredProcedure [dbo].[BasculaMultipesaje_Crear]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Pablo Bórquez 
-- Create date: 30/11/2015
-- Description: Crea un registro de Báscula multipesaje
--=============================================
CREATE PROCEDURE [dbo].[BasculaMultipesaje_Crear]
	@OrganizacionID INT, 
	@TipoFolio INT,
	@Chofer VARCHAR(100),
	@Placas VARCHAR(100),
	@PesoBruto INT,
	@PesoTara INT,
	@FechaPesoBruto DATETIME,
	@FechaPesoTara DATETIME,
	@UsuarioCreacionID INT,
	@Producto VARCHAR(100),
	@Observacion VARCHAR(255),
	@EnvioSAP BIT,
	@OperadorID INT
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @ValorFolio int
	EXEC Folio_Obtener @OrganizacionID, @TipoFolio, @Folio = @ValorFolio output
	INSERT BasculaMultipesaje(
		Folio, 
		Chofer, 
		Placas,
		PesoBruto,
		PesoTara,
		Fecha,
		FechaPesoBruto,
		FechaPesoTara,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		Producto,
		Observacion,
		OrganizacionID,
		EnvioSAP,
		OperadorID
	)
	VALUES(
		@ValorFolio,
		@Chofer,
		@Placas,
		@PesoBruto,
		@PesoTara,
		CURRENT_TIMESTAMP,
		@FechaPesoBruto,
		@FechaPesoTara,
		1,
		CURRENT_TIMESTAMP,
		@UsuarioCreacionID,
		@Producto,
		@Observacion,
		@OrganizacionID,
		@EnvioSAP, 
		@OperadorID
	);

	SELECT @ValorFolio as Folio;

	SET NOCOUNT OFF;
END