USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionProducto_Guardar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RecepcionProducto_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionProducto_Guardar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pedro Delgado>
-- Create date: <23/07/2014>
-- Description:	Guarda la entrada de un producto.
/*RecepcionProducto_Guardar */
-- =============================================
CREATE PROCEDURE [dbo].[RecepcionProducto_Guardar]
@OrganizacionID INT,
@AlmacenID INT,
@FolioOrdenCompra VARCHAR(6),
@FechaSolicitud SMALLDATETIME,
@ProveedorID INT,
@AlmacenMovimientoID BIGINT,
@Factura VARCHAR(50),
@UsuarioCreacionID INT,
@TipoFolio INT,
@Activo BIT
AS
BEGIN
	DECLARE @FolioRecepcion INT
	DECLARE @RecepcionProductoID INT
	EXEC Folio_Obtener @OrganizacionID,@TipoFolio,@FolioRecepcion OUTPUT;
	INSERT INTO RecepcionProducto
	(AlmacenID,FolioRecepcion,FolioOrdenCompra,FechaSolicitud,
	ProveedorID,FechaRecepcion,AlmacenMovimientoID,
	Factura,Activo,FechaCreacion,UsuarioCreacionID)
	VALUES
	(@AlmacenID,@FolioRecepcion,@FolioOrdenCompra,@FechaSolicitud,
	@ProveedorID,GETDATE(),@AlmacenMovimientoID,
	@Factura,@Activo,GETDATE(),@UsuarioCreacionID)
	SET @RecepcionProductoID = @@IDENTITY
	SELECT
		RecepcionProductoID,
		AlmacenID,
		FolioRecepcion,
		FolioOrdenCompra,
		FechaSolicitud,
		ProveedorID,
		FechaRecepcion,
		AlmacenMovimientoID,
		Factura,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM RecepcionProducto (NOLOCK)
	WHERE RecepcionProductoID = @RecepcionProductoID
END

GO
