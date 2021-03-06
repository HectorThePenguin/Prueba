USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaPremezcla_ObtenerPorMovimientoEntrada]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaPremezcla_ObtenerPorMovimientoEntrada]
GO
/****** Object:  StoredProcedure [dbo].[EntradaPremezcla_ObtenerPorMovimientoEntrada]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 09/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : EntradaPremezcla_ObtenerPorMovimientoEntrada
--======================================================
CREATE PROCEDURE [dbo].[EntradaPremezcla_ObtenerPorMovimientoEntrada]
@AlmacenMovimientoIDEntrada BIGINT
AS
BEGIN
	SELECT 
		EntradaPremezclaID,
		AlmacenMovimientoIDEntrada,
		AlmacenMovimientoIDSalida,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	FROM EntradaPremezcla 
	WHERE AlmacenMovimientoIDEntrada = @AlmacenMovimientoIDEntrada
END

GO
