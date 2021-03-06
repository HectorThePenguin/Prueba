USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[BoletaRecepcionForraje_CrearNuevoRegistro]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[BoletaRecepcionForraje_CrearNuevoRegistro]
GO
/****** Object:  StoredProcedure [dbo].[BoletaRecepcionForraje_CrearNuevoRegistro]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Eduardo Cota
-- Create date: 18/12/2014
-- Origen     : Apinterfaces
-- Description: Inserta un registro en la tabla 
-- SpName     : BoletaRecepcionForraje_CrearNuevoRegistro 1, 1, 1, 0, 1, 5, 1
--======================================================
CREATE PROCEDURE [dbo].[BoletaRecepcionForraje_CrearNuevoRegistro]
	@EntradaProductoDetalleID int,
	@Porcentaje decimal(10,2),
	@Descuento decimal(10,2),
	@Rechazo bit,
	@Activo bit,
	@UsuarioCreacionID int,
	@EsOrigen bit 
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO EntradaProductoMuestra
		(
			EntradaProductoDetalleID,
			Porcentaje,
			Descuento,
			Rechazo,
			Activo,
			FechaCreacion,
			UsuarioCreacionID,
			EsOrigen
		)
	VALUES
		(
			@EntradaProductoDetalleID,
			@Porcentaje,
			@Descuento,
			@Rechazo,
			@Activo,
			getdate (),
			@UsuarioCreacionID,
			@EsOrigen	
		)
END

GO
