USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiariaDetalle_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionDiariaDetalle_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiariaDetalle_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 24/06/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProduccionDiariaDetalle_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[ProduccionDiariaDetalle_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ProduccionDiariaDetalleID,
		ProduccionDiariaID,
		ProductoID,
		PesajeMateriaPrimaID,
		EspecificacionForraje,
		HoraInicial,
		HoraFinal,
		Activo
	FROM ProduccionDiariaDetalle
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
