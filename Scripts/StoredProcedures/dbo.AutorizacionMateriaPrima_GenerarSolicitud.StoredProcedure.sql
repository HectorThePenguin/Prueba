USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_GenerarSolicitud]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AutorizacionMateriaPrima_GenerarSolicitud]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_GenerarSolicitud]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Emir Lezama
-- Create date: 24/11/2014
-- Description: Sp para generar la solicitu de autorizacion 
--=============================================
CREATE PROCEDURE [dbo].[AutorizacionMateriaPrima_GenerarSolicitud] 
@OrganizacionID INT,
@FolioSalida INT,
@Justificacion VARCHAR(255),
@Precio DECIMAL(18,4),
@ProductoID INT,
@AlmacenID INT,
@UsuarioCreacionID INT,
@TipoAutorizacionID INT,
@EstatusID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Lote INT
	/*		Lote	*/
	SELECT @Lote = Lote 
	FROM AlmacenInventarioLote (NOLOCK) AS AIL
	INNER JOIN SalidaProducto (NOLOCK) AS SP
	ON AIL.AlmacenInventarioLoteID = SP.AlmacenInventarioLoteID 
	WHERE SP.FolioSalida = @FolioSalida AND SP.OrganizacionID = @OrganizacionID AND SP.Activo = @Activo
	INSERT INTO AutorizacionMateriaPrima
	(
		OrganizacionID, 
		TipoAutorizacionID, 
		Folio, 
		Justificacion, 
		Lote, 
		Precio, 
		Cantidad, 
		ProductoID, 
		AlmacenID, 
		EstatusID,
		Activo, 
		FechaCreacion, 
		UsuarioCreacionID
	)
	VALUES 
	(
		@OrganizacionID, 
		@TipoAutorizacionID, 
		@FolioSalida, 
		@Justificacion, 
		@Lote, 
		@Precio, 
		0, 
		@ProductoID, 
		@AlmacenID, 
		@EstatusID,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
	)
	UPDATE SalidaProducto SET AutorizacionMateriaPrimaID = @@IDENTITY
	WHERE FolioSalida = @FolioSalida AND OrganizacionID = @OrganizacionID AND Activo = @Activo
	SELECT @@IDENTITY
	SET NOCOUNT OFF;
END

GO
