USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Zona_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Zona_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Zona_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================  
-- Author     : Audomar Piña Aguilar
-- Create date: 2015/04/08  
-- SpName     : Zona_Actualizar  
--=============================================  
CREATE PROCEDURE [dbo].[Zona_Actualizar]  
@ZonaID int,  
@Descripcion varchar(70),  
@PaisID int,
@Activo BIT,  
@UsuarioCreacionID INT  
AS  
BEGIN  
	SET NOCOUNT ON;  
	
	UPDATE Zona
	   SET Descripcion = @Descripcion,  
		   PaisID = @PaisID,
		   UsuarioModificacionID = @UsuarioCreacionID,
		   FechaModificacion = GetDate(),
		   Activo = @Activo  
	 WHERE ZonaID = @ZonaID  
	 
	SET NOCOUNT OFF;  
END  

GO
