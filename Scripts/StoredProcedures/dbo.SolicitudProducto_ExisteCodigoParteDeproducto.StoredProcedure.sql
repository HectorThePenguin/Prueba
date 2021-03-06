USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProducto_ExisteCodigoParteDeproducto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudProducto_ExisteCodigoParteDeproducto]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProducto_ExisteCodigoParteDeproducto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/08/09
-- Description: SP para validar si el producto se encuenta como relacionado con un codigo de parte
-- Origen     : APInterfaces
-- EXEC SolicitudProducto_ExisteCodigoParteDeproducto 1
-- =============================================
CREATE PROCEDURE [dbo].[SolicitudProducto_ExisteCodigoParteDeproducto]
	@ProductoID INT
AS
BEGIN
	SELECT ProductoCodigoParteID,
       ProductoID,
       CodigoParte,
       Activo,
       FechaCreacion,
       UsuarioCreacionID,
       FechaModificacion,
       UsuarioModificacionID 
	FROM ProductoCodigoParte
	WHERE ProductoID = @ProductoID
END

GO
