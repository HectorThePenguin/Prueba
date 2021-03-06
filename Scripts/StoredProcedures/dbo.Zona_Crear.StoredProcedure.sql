USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Zona_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Zona_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Zona_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================  
-- Author     : Audomar Piña Aguilar
-- Create date: 2015/04/08  
-- Description: Banco_Crear 
--=============================================  
CREATE PROCEDURE [dbo].[Zona_Crear]  
@Descripcion varchar(100),  
@PaisID		INT,
@Activo BIT,  
@UsuarioCreacionID INT  
AS  
BEGIN  
	SET NOCOUNT ON;  
	INSERT Zona
	(  
		Descripcion,  
		PaisID,
		Activo,  
		UsuarioCreacionID
	)  
	VALUES
	(  
		@Descripcion,
		@PaisID,  
		@Activo,
		@UsuarioCreacionID  
	)  
	SELECT SCOPE_IDENTITY()  
	SET NOCOUNT OFF;  
END  
GO
