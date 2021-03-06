USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoContrato_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoContrato_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoContrato_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Jesus Alvarez
-- Create date: 17/05/2014
-- Description:	Obtiene un tipo de contrato por id
-- TipoContrato_ObtenerPorID 1 
--======================================================
CREATE PROCEDURE [dbo].[TipoContrato_ObtenerPorID]
@TipoContratoID INT
AS
BEGIN
	SELECT  
		TipoContratoID,
		Descripcion,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	FROM TipoContrato (NOLOCK)
	WHERE TipoContratoID = @TipoContratoID
END

GO
