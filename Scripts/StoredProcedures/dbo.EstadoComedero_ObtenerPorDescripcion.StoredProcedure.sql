USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EstadoComedero_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EstadoComedero_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[EstadoComedero_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : EstadoComedero_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[EstadoComedero_ObtenerPorDescripcion]
@Descripcion varchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		EstadoComederoID,
		Descripcion,
		DescripcionCorta,
		NoServir,
		AjusteBase,
		Tendencia,
		Activo
	FROM EstadoComedero
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
