USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TiempoMuerto_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TiempoMuerto_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TiempoMuerto_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 24/06/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TiempoMuerto_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[TiempoMuerto_ObtenerPorID]
@TiempoMuertoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TiempoMuertoID,
		ProduccionDiariaID,
		RepartoAlimentoID,
		HoraInicio,
		HoraFin,
		CausaTiempoMuertoID,
		Activo
	FROM TiempoMuerto
	WHERE TiempoMuertoID = @TiempoMuertoID
	SET NOCOUNT OFF;
END

GO
