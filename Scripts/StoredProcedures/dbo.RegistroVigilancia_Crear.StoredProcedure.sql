USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilancia_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RegistroVigilancia_Crear]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilancia_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Eduardo COta
-- Create date: 23/05/2014
-- Description: Crea un nuevo registro con folio en la tabla "Registro Vigilancia"
-- RegistroVigilancia_Crear 82, 4842, 9, 43, 'DODGE', 'CAFE', 1, 1, 1, 10
--=============================================
CREATE PROCEDURE [dbo].[RegistroVigilancia_Crear]
	@ProductoID INT,
	@ProveedorMateriasPrimas INT,
	@ContratoID INT,
	@ProveedorChoferID INT,
	@Transportista VARCHAR(50),
	@Chofer VARCHAR(50),
	@CamionID INT,
	@Camion VARCHAR(50),
	@Marca VARCHAR(50),
	@Color VARCHAR(50),
	@Activo INT,
	@OrganizacionID INT,
	@UsuarioCreacionID INT,
	@TipoFolio INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @ValorFolio INT
	EXEC Folio_Obtener @OrganizacionID, @TipoFolio, @Folio = @ValorFolio output
	IF (@ProveedorChoferID = 0)
	BEGIN
		SET @ProveedorChoferID = NULL
	END
	IF (@CamionID = 0)
	BEGIN
		SET @CamionID = NULL
	END
	INSERT RegistroVigilancia(
		OrganizacionID,
		ProveedorIDMateriasPrimas,
		ContratoID,
		ProveedorChoferID,
		Transportista,
		Chofer,
		ProductoID,
		CamionID,
		Camion,
		Marca,
		Color,
		FolioTurno,
		FechaLlegada,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
		)
		VALUES(
		@OrganizacionID,
		@ProveedorMateriasPrimas,
		CASE WHEN @ContratoID > 0 
			THEN @ContratoID
		ELSE NULL END,
		@ProveedorChoferID,
		@Transportista,
		@Chofer,
		@ProductoID,
		@CamionID,
		@Camion,
		@Marca,
		@Color,
		@ValorFolio,
		GETDATE(),
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
		)
	SELECT @ValorFolio;
	SET NOCOUNT OFF;
END

GO
