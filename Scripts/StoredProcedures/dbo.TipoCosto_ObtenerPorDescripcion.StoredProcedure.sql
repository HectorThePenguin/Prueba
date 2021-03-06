USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCosto_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCosto_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[TipoCosto_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoCosto_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[TipoCosto_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoCostoID,
		Descripcion,
		Activo
	FROM TipoCosto
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
