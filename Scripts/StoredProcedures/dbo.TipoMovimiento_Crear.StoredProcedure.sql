USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoMovimiento_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoMovimiento_Crear
--======================================================
CREATE PROCEDURE [dbo].[TipoMovimiento_Crear]
@Descripcion varchar(50),
@EsGanado bit,
@EsProducto bit,
@EsEntrada bit,
@EsSalida bit,
@ClaveCodigo char(2),
@TipoPolizaID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT TipoMovimiento (
		Descripcion,
		EsGanado,
		EsProducto,
		EsEntrada,
		EsSalida,
		ClaveCodigo,
		TipoPolizaID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@Descripcion,
		@EsGanado,
		@EsProducto,
		@EsEntrada,
		@EsSalida,
		@ClaveCodigo,
		@TipoPolizaID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
