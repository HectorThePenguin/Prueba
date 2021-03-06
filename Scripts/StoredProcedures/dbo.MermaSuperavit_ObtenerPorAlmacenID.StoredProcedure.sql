USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_ObtenerPorAlmacenID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MermaSuperavit_ObtenerPorAlmacenID]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_ObtenerPorAlmacenID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jorge Luis Velazquez	Araujo
-- Create date: 01/07/2014
-- Description: Consulta la configuracion de la merma y/o superavit de un almacen
-- MermaSuperavit_ObtenerPorAlmacenID
-- =============================================
CREATE PROCEDURE [dbo].[MermaSuperavit_ObtenerPorAlmacenID]
@AlmacenID INT,
@Activo INT
AS
BEGIN
	SELECT 
		MermaSuperavitID,
		AlmacenID,
		ProductoID,
		Merma,
		Superavit,
		Activo		
	FROM MermaSuperavit 
	WHERE AlmacenID = @AlmacenID
	AND Activo = @Activo
END

GO
