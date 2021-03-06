USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_Cancelar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_Cancelar]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_Cancelar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Pedro Delgado
-- Create date: 2014/12/08
-- Description: Procedimiento que cancela una entrada producto
--=============================================
CREATE PROCEDURE [dbo].[EntradaProducto_Cancelar]
@EntradaProductoID BIGINT,
@EstatusID INT,
@UsuarioModificacionID INT 
AS
BEGIN
	UPDATE EntradaProducto
	SET
		EstatusID = @EstatusID,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE(),
		Activo = 0
	WHERE EntradaProductoID = @EntradaProductoID
END
GO
