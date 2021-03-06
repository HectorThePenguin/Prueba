USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoContrato_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoContrato_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoContrato_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 16/05/2014
-- Description:	Obtiene los tipos de contrato activos
-- [TipoContrato_ObtenerTodos] 
--======================================================
CREATE PROCEDURE [dbo].[TipoContrato_ObtenerTodos]
AS
BEGIN
	SELECT  
		TipoContratoID,
		Descripcion,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaCreacion,
		UsuarioCreacionID
	FROM TipoContrato (NOLOCK)
	WHERE Activo = 1
END

GO
