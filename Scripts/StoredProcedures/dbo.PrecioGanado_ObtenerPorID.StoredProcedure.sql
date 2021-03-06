USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PrecioGanado_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PrecioGanado_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[PrecioGanado_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
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
CREATE PROCEDURE [dbo].[PrecioGanado_ObtenerPorID]
@PrecioGanadoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		PG.PrecioGanadoID,
		PG.OrganizacionID,
		PG.TipoGanadoID,
		PG.Precio,
		PG.FechaVigencia,
		PG.Activo,
		PG.FechaCreacion,
		PG.UsuarioCreacionID,
		PG.FechaModificacion,
		PG.UsuarioModificacionID
		, O.Descripcion AS Organizacion
		, TG.Descripcion AS TipoGanado
	FROM PrecioGanado PG
	INNER JOIN Organizacion O
		ON (PG.OrganizacionID = O.OrganizacionID)
	INNER JOIN TipoGanado TG
		ON (PG.TipoGanadoID = TG.TipoGanadoID)
	WHERE PG.PrecioGanadoID = @PrecioGanadoID
	SET NOCOUNT OFF;
END

GO
