USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_CrearTodosParametros]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_CrearTodosParametros]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_CrearTodosParametros]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 04/02/2015
-- Description: Crea un nuevo almacen inventario lote, mandandole el Lote y la fecha de inicio
-- AlmacenInventarioLote_CrearTodosParametros
--=============================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_CrearTodosParametros]
	@AlmacenInventarioID INT,
	@Cantidad DECIMAL(18,2),
	@Lote INT,
	@PrecioPromedio DECIMAL(18,4),
	@Piezas INT,
	@Importe DECIMAL(24,2),
	@FechaInicio SMALLDATETIME,
	@Activo INT,
	@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
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
		@Lote,
		@Cantidad,
		@PrecioPromedio,
		@Piezas,
		@Importe,
		@FechaInicio,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
