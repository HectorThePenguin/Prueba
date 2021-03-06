USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Banco_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Banco_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Banco_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================  
-- Author     : Audomar Piña Aguilar
-- Create date: 2015/04/08  
-- SpName     : Banco_ObtenerPorDescripcion 'Banamex'  
--=============================================  
CREATE PROCEDURE [dbo].[Banco_ObtenerPorDescripcion]  
@Descripcion VARCHAR(70)  
AS  
BEGIN  
	SET NOCOUNT ON;  
	SELECT b.BancoID,  
		   b.Descripcion,  
		   b.Telefono,  
		   b.PaisID,
		   p.Descripcion AS Pais,
		   b.Activo  
	  FROM Banco b
	 INNER JOIN Pais p 
	    ON b.PaisID = p.PaisID
	 WHERE b.Descripcion = @Descripcion  
	SET NOCOUNT OFF;  
END  
GO
