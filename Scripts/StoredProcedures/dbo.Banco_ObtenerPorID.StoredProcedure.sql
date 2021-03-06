USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Banco_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Banco_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Banco_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================  
-- Author     : Audomar Piña Aguilar
-- Create date: 2015/04/08  
-- Description: Banco_ObtenerPorID 5  
--=============================================  
CREATE PROCEDURE [dbo].[Banco_ObtenerPorID]  
@BancoID INT  
AS  
BEGIN  
	SET NOCOUNT ON;  

	SELECT B.BancoID,  
		   B.Descripcion,  
		   B.Telefono
	  FROM Banco B  
	 WHERE B.BancoID = @BancoID  

	SET NOCOUNT OFF;  
END  
GO
