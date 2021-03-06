USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiariaDetalle_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionDiariaDetalle_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiariaDetalle_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 24/06/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProduccionDiariaDetalle_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[ProduccionDiariaDetalle_ObtenerPorID]
@ProduccionDiariaDetalleID int
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
	WHERE ProduccionDiariaDetalleID = @ProduccionDiariaDetalleID
	SET NOCOUNT OFF;
END

GO
