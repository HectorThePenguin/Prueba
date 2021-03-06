USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventario_Crear]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 23/05/2014
-- Description: Crea un nuevo almacen inventario
-- AlmacenInventario_Crear
--=============================================
CREATE PROCEDURE [dbo].[AlmacenInventario_Crear]
	@AlmacenID INT,
	@ProductoID INT,
	@Minimo INT,
	@Maximo INT,
	@PrecioPromedio DECIMAL(18,4),
	@Cantidad DECIMAL(18,2),
	@Importe DECIMAL(24,2),
	@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT AlmacenInventario(
		AlmacenID,
		ProductoID,
		Minimo,
		Maximo,
		PrecioPromedio,
		Cantidad,
		Importe,
		FechaCreacion,
		UsuarioCreacionID
	)
	VALUES(
		@AlmacenID,
		@ProductoID,
		@Minimo,
		@Maximo,
		@PrecioPromedio,
		@Cantidad,
		@Importe,
		GETDATE(),
		@UsuarioCreacionID
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
