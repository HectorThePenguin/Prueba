USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Banco_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Banco_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Banco_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---=============================================  
-- Author     : Audomar Piña Aguilar
-- Create date: 2015/04/08  
-- SpName     : Banco_Actualizar  
--=============================================  
CREATE PROCEDURE [dbo].[Banco_Actualizar]  
@BancoID int,  
@Descripcion varchar(70),  
@PaisID int,
@Telefono varchar(50),  
@Activo BIT,  
@UsuarioCreacionID INT  
AS  
BEGIN  
	SET NOCOUNT ON;  
	
	UPDATE Banco 
	   SET Descripcion = @Descripcion,  
		   Telefono = @Telefono,
		   PaisID = @PaisID,
		   UsuarioModificacionID = @UsuarioCreacionID,
		   FechaModificacion = GetDate(),
		   Activo = @Activo  
	 WHERE BancoID = @BancoID  
	 
	SET NOCOUNT OFF;  
END  

GO
