IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'ImportarSaldosSOFOM_Guardar' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[ImportarSaldosSOFOM_Guardar]
END 
GO
CREATE PROCEDURE [dbo].[ImportarSaldosSOFOM_Guardar] 
@XmlSaldos XML  
AS  
BEGIN  
SET NOCOUNT ON
	 
	SELECT
		UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT'),
		CreditoID = t.item.value('./CreditoID[1]', 'INT'),  
		Nombre =  t.item.value('./Nombre[1]', 'VARCHAR(150)'),  
		TipoCredito = t.item.value('./TipoCredito[1]', 'VARCHAR(100)'),		
		FechaAlta = t.item.value('./FechaAlta[1]', 'DATETIME'),  
		FechaVencimiento = t.item.value('./FechaVencimiento[1]', 'DATETIME'), 
		Saldo = t.item.value('./Saldo[1]', 'NUMERIC(18,4)')  
	INTO #Sofom
	FROM @XmlSaldos.nodes('ROOT/Saldos') AS T(item)
	
	IF EXISTS(SELECT CreditoID, COUNT(CreditoID) FROM #Sofom GROUP BY CreditoID HAVING COUNT(CreditoID) > 1)
	BEGIN
		DROP TABLE #Sofom
		RAISERROR ('Existen créditos duplicados en el layout, favor de verificar.',16,1);  
	END
	ELSE
	BEGIN
		UPDATE CreditoSOFOM SET Saldo = 0
		
		SELECT 	
			S.CreditoID,  
			S.Nombre,  
			S.FechaAlta,  
			S.FechaVencimiento, 
			S.Saldo,
			TC.TipoCreditoID, 
			TipoCredito = ISNULL(TC.Descripcion,''),
			S.UsuarioCreacionID    
		INTO #Procesar
		FROM #Sofom S
		INNER JOIN TipoCredito TC
			ON RTRIM(LTRIM(TC.Descripcion)) = S.TipoCredito AND TC.Activo = 1
			
		UPDATE CS
		SET CS.Nombre = P.Nombre, 
			CS.TipoCreditoID = P.TipoCreditoID, 
			CS.FechaAlta = P.FechaAlta, 
			CS.FechaVencimiento = P.FechaVencimiento, 
			CS.Saldo = P.Saldo, 
			CS.FechaModificacion = GETDATE(), 
			CS.UsuarioModificacionID = P.UsuarioCreacionID
		FROM CreditoSOFOM CS
		INNER JOIN #Procesar P
			ON P.CreditoID = CS.CreditoID
				
		INSERT INTO CreditoSOFOM(CreditoID,Nombre,TipoCreditoID,FechaAlta,FechaVencimiento,Saldo,FechaCreacion,UsuarioCreacionID)	
		SELECT	
			P.CreditoID,  
			P.Nombre,  
			P.TipoCreditoID, 
			P.FechaAlta,
			P.FechaVencimiento,
			P.Saldo,
			GETDATE(),
			P.UsuarioCreacionID
		FROM #Procesar P
		LEFT JOIN CreditoSOFOM CS
			ON CS.CreditoID = P.CreditoID
		WHERE CS.CreditoID IS NULL
				
		INSERT INTO	CreditoProveedor(OrganizacionID,ProveedorID,CreditoID,Activo,FechaCreacion,UsuarioCreacionID)
		SELECT DISTINCT 	
			CP.OrganizacionId,  
			CP.ProveedorID,  
			P.CreditoID,  
			1,
			GETDATE(),
			P.UsuarioCreacionID
		FROM #Procesar P
		INNER JOIN Sukarne.dbo.CatProveedor CP
			ON LTRIM(RTRIM(ISNULL(CP.Nombre,'') + ' ' + ISNULL(CP.ApellidoPaterno,'') + ' ' + ISNULL(CP.ApellidoMaterno,''))) COLLATE SQL_Latin1_General_CP1_CI_AS = P.Nombre AND CP.Activo = 1
		INNER JOIN Organizacion O
			ON O.OrganizacionId = CP.OrganizacionId AND O.Activo = 1
		LEFT JOIN CreditoProveedor CR
			ON CR.OrganizacionId = CP.OrganizacionId AND CR.ProveedorID = CP.ProveedorID AND CR.CreditoID = P.CreditoID
		WHERE CR.CreditoID IS NULL
		
		DROP TABLE #Sofom
		DROP TABLE #Procesar
		
		SELECT 	
			S.CreditoID,  
			S.Nombre,  
			S.FechaAlta,  
			S.FechaVencimiento, 
			S.Saldo,
			Existe = CASE WHEN ISNULL(CP.CreditoID,-1) = -1 THEN 0 ELSE 1 END,
			Proveedor = ISNULL(P.Descripcion,''),
			Centro = ISNULL(O.Descripcion,''),
			Ganadera = ISNULL(O2.Descripcion, ''),
			TC.TipoCreditoID, 
			TipoCredito = ISNULL(TC.Descripcion,''),
			S.UsuarioCreacionID    
		FROM CreditoSOFOM S
		INNER JOIN TipoCredito TC
			ON TC.TipoCreditoID = S.TipoCreditoID AND TC.Activo = 1
		LEFT JOIN CreditoProveedor CP
			ON CP.CreditoID = S.CreditoID AND CP.Activo = 1
		LEFT JOIN Organizacion O
			ON O.OrganizacionID = CP.OrganizacionID
		LEFT JOIN Organizacion O2 
			ON O2.Division = O.Division AND O2.TipoOrganizacionID = 2
		LEFT JOIN Sukarne.dbo.CatProveedor P
			ON P.OrganizacionID = CP.OrganizacionID AND P.ProveedorID = CP.ProveedorID
		WHERE S.Saldo > 0
	END
	
SET NOCOUNT OFF
END