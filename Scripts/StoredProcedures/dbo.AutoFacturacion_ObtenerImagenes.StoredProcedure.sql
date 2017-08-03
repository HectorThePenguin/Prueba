IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'AutoFacturacion_ObtenerImagenes' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[AutoFacturacion_ObtenerImagenes]
END 
GO
CREATE PROCEDURE [dbo].[AutoFacturacion_ObtenerImagenes]  
@OrganizacionId INT,  
@FolioId INT,  
@Estatus INT,  
@FormaPAgo INT,
@ChequeId INT,
@FechaInicio DATETIME,
@FechaFin DATETIME
AS  
BEGIN
	
	DECLARE @FormaPagoDesc VARCHAR(30)  
	SET @FormaPagoDesc = CASE WHEN @FormaPAgo = 1 THEN 'CHEQUE' ELSE CASE WHEN @FormaPAgo = 2 THEN 'TRANSFERENCIA' ELSE '%%' END END    
	CREATE TABLE #Registros(  
		OrganizacionId INT,  
		Organizacion VARCHAR(50),  
		FolioCompra INT,
		ProveedorId INT
	)
	  
	INSERT INTO #Registros(OrganizacionId,Organizacion,FolioCompra,ProveedorId)  
	SELECT   
		OrganizacionId = C.OrganizacionId,  
		Organizacion = O.Descripcion,  
		FolioCompra = C.Folio,  
		ProveedorId = C.ProveedorCompraId
	FROM Sukarne.dbo.CacCompra C (NOLOCK)  
	INNER JOIN Sukarne.dbo.CacPago P (NOLOCK)  
	ON C.OrganizacionId = P.OrganizacionId AND C.Folio = P.Folio  
	INNER JOIN Organizacion O (NOLOCK)  
	ON O.OrganizacionId = C.OrganizacionId  
	INNER JOIN Sukarne.dbo.CatProveedor CP (NOLOCK)  
	ON CP.ProveedorId = C.ProveedorCompraId AND CP.OrganizacionId = C.OrganizacionId  
	LEFT OUTER JOIN Sukarne.dbo.CacCompraGanadoFactura CGF (NOLOCK)  
	ON CGF.OrganizacionId = C.OrganizacionId AND CGF.Folio = C.Folio
	INNER JOIN Sukarne.dbo.CacCheque CC (NOLOCK)
	ON CC.OrganizacionId = C.OrganizacionId
	WHERE @OrganizacionId IN (0,C.OrganizacionId)   
	AND @FolioId IN(0,C.Folio)   
	AND CASE WHEN P.Transferencia = 0 THEN 'CHEQUE' ELSE 'TRANSFERENCIA' END LIKE @FormaPagoDesc  
	AND @Estatus IN(0, CASE WHEN CGF.Folio IS NULL THEN 1 ELSE 2 END) 
	AND @ChequeId In(0,CC.ChequeId)
	AND CONVERT(VARCHAR(8),C.FechaCompra,112) BETWEEN CONVERT(VARCHAR(8),@FechaInicio,112) AND CONVERT(VARCHAR(8),@FechaFin,112)	 
	GROUP BY C.OrganizacionId,  
			O.Descripcion,  
			C.Folio,  
			C.ProveedorCompraId
			   
	UNION ALL
	  
	SELECT  
		OrganizacionId = C.OrganizacionId,  
		Organizacion = O.Descripcion,  
		FolioCompra = C.Folio,  
		ProveedorId = C.ProveedorCompraId
	FROM Sukarne.dbo.CacCompraDirecta C (NOLOCK)  
	INNER JOIN Sukarne.dbo.CacPago P (NOLOCK)  
	ON C.OrganizacionId = P.OrganizacionId AND C.Folio = P.Folio  
	INNER JOIN Organizacion O (NOLOCK)  
	ON O.OrganizacionId = C.OrganizacionId  
	INNER JOIN Sukarne.dbo.CatProveedor CP (NOLOCK)  
	ON CP.ProveedorId = C.ProveedorCompraId AND CP.OrganizacionId = C.OrganizacionId  
	LEFT OUTER JOIN Sukarne.dbo.CacCompraGanadoFactura CGF (NOLOCK)  
	ON CGF.OrganizacionId = C.OrganizacionId AND CGF.Folio = C.Folio
	INNER JOIN Sukarne.dbo.CacCheque CC (NOLOCK)
	ON CC.OrganizacionId = C.OrganizacionId   
	WHERE @OrganizacionId IN (0,C.OrganizacionId)   
	AND @FolioId IN(0,C.Folio)   
	AND CASE WHEN P.Transferencia = 0 THEN 'CHEQUE' ELSE 'TRANSFERENCIA' END LIKE @FormaPagoDesc  
	AND @Estatus IN(0, CASE WHEN CGF.Folio IS NULL THEN 1 ELSE 2 END)
	AND @ChequeId In(0,CC.ChequeId) 
	AND CONVERT(VARCHAR(8),C.FechaCompra,112) BETWEEN CONVERT(VARCHAR(8),@FechaInicio,112) AND CONVERT(VARCHAR(8),@FechaFin,112)	  
	GROUP BY C.OrganizacionId,  
			O.Descripcion,  
			C.Folio,  
			C.ProveedorCompraId
			   
	UNION ALL  
	
	SELECT  
		OrganizacionId = C.OrganizacionId,  
		Organizacion = O.Descripcion,  
		FolioCompra = C.CompraIndividualId,  
		ProveedorId = C.ProveedorId
	FROM Sukarne.dbo.CacCompraIndividualEnc C (NOLOCK)  
	INNER JOIN Sukarne.dbo.CacPago P (NOLOCK)  
	ON C.OrganizacionId = P.OrganizacionId AND C.CompraIndividualId = P.Folio  
	INNER JOIN Organizacion O (NOLOCK)  
	ON O.OrganizacionId = C.OrganizacionId  
	INNER JOIN Sukarne.dbo.CatProveedor CP (NOLOCK)  
	ON CP.ProveedorId = C.ProveedorId AND CP.OrganizacionId = C.OrganizacionId  
	LEFT OUTER JOIN Sukarne.dbo.CacCompraGanadoFactura CGF (NOLOCK)  
	ON CGF.OrganizacionId = C.OrganizacionId AND CGF.Folio = C.CompraIndividualId
	INNER JOIN Sukarne.dbo.CacCheque CC (NOLOCK)
	ON CC.OrganizacionId = C.OrganizacionId   
	WHERE @OrganizacionId IN (0,C.OrganizacionId)   
	AND @FolioId IN(0,C.CompraIndividualId)   
	AND CASE WHEN P.Transferencia = 0 THEN 'CHEQUE' ELSE 'TRANSFERENCIA' END LIKE @FormaPagoDesc  
	AND @Estatus IN(0, CASE WHEN CGF.Folio IS NULL THEN 1 ELSE 2 END)
	AND @ChequeId In(0,CC.ChequeId)
	AND CONVERT(VARCHAR(8),C.FechaCompra,112) BETWEEN CONVERT(VARCHAR(8),@FechaInicio,112) AND CONVERT(VARCHAR(8),@FechaFin,112)    
	GROUP BY C.OrganizacionId,  
			O.Descripcion,  
			C.CompraIndividualId,  
			C.ProveedorId
			
	-- DOCUMENTOS DE COMPRA		
	SELECT   
		IC.Imagen,
		R.OrganizacionId,
		R.FolioCompra, 
		R.Organizacion 
	FROM Sukarne.dbo.CacImagenCompra AS  IC (NOLOCK)  
	INNER JOIN #Registros R
	ON R.OrganizacionId = IC.OrganizacionId AND R.FolioCompra = IC.FolioCompra
	UNION ALL
	-- CREDENCIAL DE ELECTOR  
	SELECT       
		I.Imagen,
		R.OrganizacionId,
		R.FolioCompra, 
		R.Organizacion     
	FROM Sukarne.dbo.CacProveedorImagen P (NOLOCK)      
	INNER JOIN Sukarne.dbo.CatImagen I (NOLOCK)      
	ON P.ImagenID = I.ImagenID AND P.OrganizacionId = I.OrganizacionID
	INNER JOIN #Registros R
	ON R.OrganizacionId = P.OrganizacionId AND R.ProveedorId = P.ProveedorID      
	WHERE CASE WHEN ISNULL(P.INE,0) = 0 THEN 0 ELSE P.INE END IN(1,2) AND P.Activo = 1  
	UNION ALL
	-- CLAVE ÚNICA DE REGISTRO DE POBLACIÓN  
	SELECT       
		I.Imagen,
		R.OrganizacionId,
		R.FolioCompra, 
		R.Organizacion       
	FROM sukarne.dbo.CacProveedorImagen P (NOLOCK)      
	INNER JOIN sukarne.dbo.CatImagen I (NOLOCK)      
	ON P.ImagenID = I.ImagenID AND P.OrganizacionId = I.OrganizacionID
	INNER JOIN #Registros R
	ON R.OrganizacionId = P.OrganizacionId AND R.ProveedorId = P.ProveedorID            
	WHERE CASE WHEN RTRIM(LTRIM(ISNULL(P.CURP,''))) = '' THEN '' ELSE P.CURP END  <> '' AND P.Activo = 1      
 
	DROP TABLE #Registros
	
END