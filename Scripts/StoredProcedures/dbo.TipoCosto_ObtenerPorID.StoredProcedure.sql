USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCosto_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCosto_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoCosto_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para obtener un Tipo de Costo por ID
-- 
--=============================================
CREATE PROCEDURE [dbo].[TipoCosto_ObtenerPorID]
@TipoCostoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		TipoCostoID,
		Descripcion,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID	
	FROM TipoCosto
	WHERE TipoCostoID = @TipoCostoID
	SET NOCOUNT OFF;
END

GO
