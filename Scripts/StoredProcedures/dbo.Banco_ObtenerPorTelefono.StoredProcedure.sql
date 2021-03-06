USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Banco_ObtenerPorTelefono]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Banco_ObtenerPorTelefono]
GO
/****** Object:  StoredProcedure [dbo].[Banco_ObtenerPorTelefono]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================  
-- Author     : Audomar Piña Aguilar
-- Create date: 2015/04/08  
-- SpName     : [Banco_ObtenerPorTelefono] '018002266783'
--=============================================  
CREATE PROCEDURE [dbo].[Banco_ObtenerPorTelefono]  
@Telefono VARCHAR(100)   
AS  
BEGIN  
	SET NOCOUNT ON;  
	
	SELECT BancoID,  
		   Descripcion,  
		   Telefono  
	  FROM Banco
	 WHERE Telefono = @Telefono
	
	SET NOCOUNT OFF;  
END  
GO
