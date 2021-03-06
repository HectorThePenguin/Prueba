USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PrecioGanado_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PrecioGanado_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[PrecioGanado_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/12/09
-- Description: 
-- 
--=============================================
CREATE PROCEDURE [dbo].[PrecioGanado_ObtenerTodos]
@Activo BIT = null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		PrecioGanadoID,
		OrganizacionID,
		tg.TipoGanadoID,
		tg.Descripcion AS TipoGanado,
		Precio,
		FechaVigencia,
		pg.Activo,
		pg.FechaCreacion,
		pg.UsuarioCreacionID,
		pg.FechaModificacion,
		pg.UsuarioModificacionID	
	FROM PrecioGanado pg
	INNER JOIN TipoGanado tg ON pg.TipoGanadoID = tg.TipoGanadoID
	WHERE pg.Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
