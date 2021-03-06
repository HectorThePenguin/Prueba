USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EstadoComedero_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EstadoComedero_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[EstadoComedero_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : EstadoComedero_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[EstadoComedero_ObtenerTodos]
@Activo BIT = NULL
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
		Activo,
		Activo as [Status]
	FROM EstadoComedero
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
