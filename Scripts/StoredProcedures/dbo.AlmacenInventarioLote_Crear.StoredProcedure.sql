USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_Crear]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 24/05/2014
-- Description: Crea un nuevo almacen inventario lote
-- AlmacenInventarioLote_Crear
--=============================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_Crear]
	@AlmacenInventarioID INT,
	@Cantidad DECIMAL(18,2),
	@PrecioPromedio DECIMAL(18,4),
	@Piezas INT,
	@Importe DECIMAL(24,2),
	@Activo INT,
	@UsuarioCreacionID INT,
	@AlmacenID INT,
	@ProductoID INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @ValorLote int 
	EXEC FolioLoteProducto_Obtener @AlmacenID, @ProductoID, @Folio = @ValorLote output
	INSERT AlmacenInventarioLote(
		AlmacenInventarioID,
		Lote,
		Cantidad,
		PrecioPromedio,
		Piezas,
		Importe,
		FechaInicio,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	)
	VALUES(
		@AlmacenInventarioID,
		@ValorLote,
		@Cantidad,
		@PrecioPromedio,
		@Piezas,
		@Importe,
		GETDATE(),
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
