USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CausaPrecio_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CausaPrecio_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[CausaPrecio_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CausaPrecio_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[CausaPrecio_ObtenerPorDescripcion]
@CausaPrecioID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
			cp.CausaPrecioID,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		cs.CausaSalidaID,
		cs.Descripcion AS CausaSalida,
		cp.Precio,		
		cp.Activo
	FROM CausaPrecio cp
	INNER JOIN CausaSalida cs on cp.CausaSalidaID = cs.CausaSalidaID
	INNER JOIN Organizacion o on cp.OrganizacionID = o.OrganizacionID
	WHERE CausaPrecioID = @CausaPrecioID
	SET NOCOUNT OFF;
END

GO
