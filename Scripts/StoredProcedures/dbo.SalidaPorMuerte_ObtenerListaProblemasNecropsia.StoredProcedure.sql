USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaPorMuerte_ObtenerListaProblemasNecropsia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaPorMuerte_ObtenerListaProblemasNecropsia]
GO
/****** Object:  StoredProcedure [dbo].[SalidaPorMuerte_ObtenerListaProblemasNecropsia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Andres Vejar
-- Create date: 12/02/2013
-- Description: Obtiene la lista de problemas para salida en necropsia
-- Empresa: Apinterfaces
-- =============================================
CREATE PROCEDURE [dbo].[SalidaPorMuerte_ObtenerListaProblemasNecropsia]
AS
BEGIN
	Select ProblemaId, Descripcion, TipoProblemaID, Activo, 
	FechaCreacion, UsuarioCreacionID, 
	ISNULL(FechaModificacion, '1900-01-01') as FechaModificacion, 
	ISNULL(UsuarioModificacionID, 0) as UsuarioModificacionID
	from Problema where Activo = 1
END

GO
