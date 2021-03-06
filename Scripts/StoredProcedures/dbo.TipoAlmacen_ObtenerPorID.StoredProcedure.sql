USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoAlmacen_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoAlmacen_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoAlmacen_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoAlmacen_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[TipoAlmacen_ObtenerPorID]
@TipoAlmacenID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoAlmacenID,
		Descripcion,
		Activo
	FROM TipoAlmacen
	WHERE TipoAlmacenID = @TipoAlmacenID
	SET NOCOUNT OFF;
END

GO
