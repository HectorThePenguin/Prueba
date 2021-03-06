USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoAutorizacion_ObtenerMovimientos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoAutorizacion_ObtenerMovimientos]
GO
/****** Object:  StoredProcedure [dbo].[TipoAutorizacion_ObtenerMovimientos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Emir Lezama
-- Create Date: 28/11/2014
-- Description: Obtiene los diferentes Movimientos para Autorizar
-- TipoAutorizacion_ObtenerMovimientos 1
-- =============================================
CREATE PROCEDURE [dbo].[TipoAutorizacion_ObtenerMovimientos]
	@Activo BIT
AS
BEGIN
	SELECT 
	TipoAutorizacionID, 
	Descripcion 
	FROM TipoAutorizacion 
	WHERE Activo = @Activo
	ORDER BY Descripcion
END	

GO
