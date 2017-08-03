USE [SIAP]
--========================01_CatConfiguracionParametroBanco_ObtenerPorBanco========================
GO
IF EXISTS(SELECT object_id FROM sys.objects WHERE name = 'Banco_ObtenerPorID' AND TYPE = 'P')
BEGIN
	DROP PROCEDURE Banco_ObtenerPorID
END
GO
--=============================================  
-- Author     : Audomar Piña Aguilar
-- Create date: 2015/04/08  
-- Description: Banco_ObtenerPorID 5  
-- Modificado : Roque Solis
-- Fecha	  :	24/09/2016
-- Modificacion : Se agrega los campos del pais
--=============================================  
CREATE PROCEDURE [dbo].[Banco_ObtenerPorID]  
@BancoID INT  
AS  
BEGIN  
	SET NOCOUNT ON;  

	SELECT B.BancoID,  
		   B.Descripcion,  
		   B.Telefono,
		   b.PaisID,
		   p.Descripcion AS Pais,
		   b.Telefono,  
		   b.Activo 
	  FROM Banco B  
	  INNER JOIN Pais p 
	    ON b.PaisID = p.PaisID
	 WHERE B.BancoID = @BancoID  

	SET NOCOUNT OFF;  
END  