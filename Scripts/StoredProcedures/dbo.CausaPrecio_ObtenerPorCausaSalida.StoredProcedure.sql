USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CausaPrecio_ObtenerPorCausaSalida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CausaPrecio_ObtenerPorCausaSalida]
GO
/****** Object:  StoredProcedure [dbo].[CausaPrecio_ObtenerPorCausaSalida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: Obtiene las causas de precio para la causa seleccionada
-- SpName     : CausaPrecio_ObtenerPorCausaSalida 11, 2
--======================================================
CREATE PROCEDURE [dbo].[CausaPrecio_ObtenerPorCausaSalida]
@CausaSalidaID INT
, @OrganizacionID INT
AS
BEGIN
	SELECT CP.CausaPrecioID, CP.Precio AS Precio
	FROM CausaPrecio (NOLOCK) CP
	WHERE CausaSalidaID = @CausaSalidaID
		AND OrganizacionID = @OrganizacionID
END

GO
