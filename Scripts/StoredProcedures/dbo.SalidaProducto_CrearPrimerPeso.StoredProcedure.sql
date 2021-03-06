USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_CrearPrimerPeso]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaProducto_CrearPrimerPeso]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_CrearPrimerPeso]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Roque Solis
-- Create date: 23/06/2014
-- Description: Crea un nuevo registro con folio en la tabla "Salida Producto." con el peso tara
-- EXEC SalidaProducto_CrearPrimerPeso 
--=============================================
CREATE PROCEDURE [dbo].[SalidaProducto_CrearPrimerPeso]
    @TipoFolio INT,
	@OrganizacionID INT,
	@TipoMovimientoID INT,
	@Cliente INT,
	@Division INT,
	@PesoTara INT,
	@FechaSalida DATETIME,
	@ChoferID INT,
	@CamionID INT,
	@Activo INT,
	@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @ValorFolio INT
	EXEC Folio_Obtener @OrganizacionID, @TipoFolio, @Folio = @ValorFolio output
	IF @Cliente = 0
	BEGIN
	   SET @Cliente = NULL;
	END 
	IF @Division = 0
	BEGIN
		SET @Division = NULL;
	END
	IF @ChoferID = 0
	BEGIN
		SET @ChoferID = NULL;
	END
	IF @CamionID = 0
	BEGIN
		SET @CamionID = NULL;
	END
	INSERT SalidaProducto(
        OrganizacionID,
        TipoMovimientoID,
        FolioSalida,
        ClienteID,
		OrganizacionIDDestino,
        PesoTara,
        FechaSalida,
		ChoferID,
		CamionID,
        Activo,
        FechaCreacion,
        UsuarioCreacionID
		)
		VALUES(
		@OrganizacionID,
		@TipoMovimientoID,
		@ValorFolio,
		@Cliente,
		@Division,
		@PesoTara,
		@FechaSalida,
		@ChoferID,
		@CamionID,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
		)
	SELECT @ValorFolio;
	SET NOCOUNT OFF;
END

GO
