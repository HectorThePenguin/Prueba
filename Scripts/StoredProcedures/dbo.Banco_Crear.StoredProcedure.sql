USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Banco_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Banco_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Banco_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================  
-- Author     : Audomar Piña Aguilar
-- Create date: 2015/04/08  
-- Description: Banco_Crear 
--=============================================  
CREATE PROCEDURE [dbo].[Banco_Crear]  
@Descripcion varchar(70),  
@PaisID		INT,
@Telefono varchar(50),  
@Activo BIT,  
@UsuarioCreacionID INT  
AS  
BEGIN  
	SET NOCOUNT ON;  
	INSERT Banco 
	(  
		Descripcion,  
		PaisID,
		Telefono,  
		Activo,  
		UsuarioCreacionID
	)  
	VALUES
	(  
		@Descripcion,
		@PaisID,  
		@Telefono,  
		@Activo,
		@UsuarioCreacionID  
	)  
	SELECT SCOPE_IDENTITY()  
	SET NOCOUNT OFF;  
END  
GO
