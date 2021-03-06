USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoProrrateo_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoProrrateo_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoProrrateo_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para obtener un Tipo de Prorrateo por ID
-- 
--=============================================
CREATE PROCEDURE [dbo].[TipoProrrateo_ObtenerPorID] @TipoProrrateoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TipoProrrateoID
		,Descripcion
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		,FechaModificacion
		,UsuarioModificacionID
	FROM TipoProrrateo
	WHERE TipoProrrateoID = @TipoProrrateoID
	SET NOCOUNT OFF;
END

GO
