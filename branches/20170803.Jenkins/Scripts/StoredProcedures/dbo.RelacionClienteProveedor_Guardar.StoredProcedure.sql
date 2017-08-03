IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'RelacionClienteProveedor_Guardar' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[RelacionClienteProveedor_Guardar]
END 
GO
CREATE PROCEDURE [dbo].[RelacionClienteProveedor_Guardar] 
@Xml XML  
AS  
BEGIN  
SET NOCOUNT ON
	 
	SELECT   
		CreditoID = t.item.value('./CreditoID[1]', 'INT'),  
		OrganizacionID =  t.item.value('./OrganizacionID[1]', 'INT'),  
		ProveedorID = t.item.value('./ProveedorID[1]', 'INT'),		
		UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT') 
	INTO #RCP
	FROM @Xml.nodes('ROOT/Creditos') AS T(item)
	
	SELECT   
		R.CreditoID,  
		R.OrganizacionID,  
		R.ProveedorID,		
		R.UsuarioCreacionID,
		Existe =  CASE WHEN ISNULL(CP.ProveedorID,0) = 0 THEN 0 ELSE 1 END
	INTO #Procesar
	FROM #RCP R
	LEFT JOIN CreditoProveedor CP
		ON CP.CreditoID = R.CreditoID AND CP.OrganizacionID = R.OrganizacionID
		
	UPDATE CP
	SET CP.ProveedorID = P.ProveedorID, CP.FechaModificacion = GETDATE(), CP.UsuarioModificacionID = P.UsuarioCreacionID 
	FROM CreditoProveedor CP
	INNER JOIN #Procesar P
		ON P.CreditoID = CP.CreditoID AND P.OrganizacionID = CP.OrganizacionID AND P.Existe = 1 
	
	INSERT INTO CreditoProveedor(OrganizacionID,ProveedorID,CreditoID,Activo,FechaCreacion,UsuarioCreacionID)
	SELECT OrganizacionID, ProveedorID, CreditoID, 1, GETDATE(), UsuarioCreacionID FROM #Procesar WHERE Existe = 0

	DROP TABLE #RCP
	DROP TABLE #Procesar

SET NOCOUNT OFF
END