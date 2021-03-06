USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_ObtenerPorAlmacenIDProductoID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MermaSuperavit_ObtenerPorAlmacenIDProductoID]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_ObtenerPorAlmacenIDProductoID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jesus Alvarez
-- Create date: 28/06/2014
-- Description: Consulta un animal por animalid
-- EXEC MermaSuperavit_ObtenerPorAlmacenIDProductoID
-- =============================================
CREATE PROCEDURE [dbo].[MermaSuperavit_ObtenerPorAlmacenIDProductoID]
@AlmacenID INT,
@Activo INT,
@ProductoID INT
AS
BEGIN
	SELECT 
		MS.MermaSuperavitID,
		MS.AlmacenID,
		MS.ProductoID,
		MS.Merma,
		MS.Superavit,
		MS.Activo,
		MS.FechaCreacion,
		MS.UsuarioCreacionID
	FROM MermaSuperavit (NOLOCK) MS
	WHERE MS.AlmacenID = @AlmacenID
	AND MS.ProductoID = @ProductoID
	AND MS.Activo = @Activo
END

GO
