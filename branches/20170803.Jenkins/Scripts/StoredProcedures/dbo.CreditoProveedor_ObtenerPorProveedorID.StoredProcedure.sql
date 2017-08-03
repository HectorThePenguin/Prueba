IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'CreditoProveedor_ObtenerPorProveedorID' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[CreditoProveedor_ObtenerPorProveedorID]
END 
GO
--======================================================        
-- Author     : Sergio Alberto Gamez Gomez      
-- Create date: 21/06/2016      
-- Description:         
-- SpName     : CreditoProveedor_ObtenerPorProveedorID 3,19         
--======================================================        
CREATE PROCEDURE [dbo].[CreditoProveedor_ObtenerPorProveedorID]       
@ProveedorID INT,      
@OrganizacionID INT        
AS    
BEGIN        
SET NOCOUNT ON  

	SELECT   
		CreditoID = CP.CreditoID,   
		ProveedorID = CP.ProveedorID,   
		Proveedor = P.Descripcion,  
		TipoCreditoID = TP.TipoCreditoID,  
		TipoCredito = TP.Descripcion,   
		Saldo = CAST(CS.Saldo AS NUMERIC(18,2)),   
		CentroID = O.OrganizacionID,   
		Centro = O.Descripcion,  
		GanaderaID = O2.OrganizacionID,  
		Ganadera = O2.Descripcion    
	FROM CreditoProveedor CP  
	INNER JOIN CreditoSOFOM CS  
	ON CS.CreditoID = CP.CreditoID  
	INNER JOIN Organizacion O  
	ON O.OrganizacionID = CP.OrganizacionID   
	INNER JOIN Organizacion O2  
	ON O2.Division = O.Division AND O2.TipoOrganizacionID = 2  
	INNER JOIN TipoCredito TP  
	ON TP.TipoCreditoID = CS.TipoCreditoID  
	INNER JOIN Sukarne.dbo.CatProveedor P  
	ON P.OrganizacionID = CP.OrganizacionID AND P.ProveedorID = CP.ProveedorID  
	WHERE CP.ProveedorID = @ProveedorID   
	AND CP.OrganizacionID = @OrganizacionID  
	AND CS.Saldo > 0    

SET NOCOUNT OFF      
END