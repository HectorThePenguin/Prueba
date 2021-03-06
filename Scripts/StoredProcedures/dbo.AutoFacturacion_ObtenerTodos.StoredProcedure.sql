USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AutoFacturacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AutoFacturacion_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[AutoFacturacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sergio Gamez
-- Create date: 012/08/2015
-- Description: Obtener todos los movimientos de autofacturaci�n
-- SpName     : AutoFacturacion_ObtenerTodos 0,0,1,20,0,0
--====================================================== 
CREATE PROCEDURE AutoFacturacion_ObtenerTodos  
@OrganizacionId INT,  
@FolioId INT,  
@Inicio INT,  
@Limite INT,  
@Estatus INT,  
@FormaPAgo INT,
@ChequeId INT,
@FechaInicio DATETIME,
@FechaFin DATETIME
AS      
BEGIN      
	SET NOCOUNT ON
	  
	DECLARE @FormaPagoDesc VARCHAR(30)  
	SET @FormaPagoDesc = CASE WHEN @FormaPAgo = 1 THEN 'CHEQUE' ELSE CASE WHEN @FormaPAgo = 2 THEN 'TRANSFERENCIA' ELSE '%%' END END    
	CREATE TABLE #Registros(  
		OrganizacionId INT,  
		Organizacion VARCHAR(50),  
		FolioCompra INT,  
		ProveedorId INT,  
		Proveedor VARCHAR(255),  
		Factura VARCHAR(15),  
		FechaCompra DATETIME,   
		Cantidad Decimal(18,2),  
		FormaPago VARCHAR(30),  
		PagoId INT,  
		ProductoCabezas INT,  
		TipoCompra VARCHAR(50),  
		Estatus INT  
	)  
	INSERT INTO #Registros(OrganizacionId,Organizacion,FolioCompra,ProveedorId,Proveedor,Factura,FechaCompra,Cantidad,FormaPago, PagoId, ProductoCabezas,TipoCompra,Estatus)  
	SELECT   
		OrganizacionId = C.OrganizacionId,  
		Organizacion = O.Descripcion,  
		FolioCompra = C.Folio,  
		ProveedorId = C.ProveedorCompraId,  
		Proveedor = CP.Descripcion,  
		Factura = ISNULL(CGF.Factura,ISNULL(C.Factura,'')),  
		FechaCompra = C.FechaCompra,   
		Cantidad = P.Cantidad,   
		FormaPago = CASE WHEN P.Transferencia = 0 THEN 'CHEQUE' ELSE 'TRANSFERENCIA' END,  
		PagoId = P.PagoId,  
		ProductoCabezas = COUNT(CD.Folio),  
		'COMPRA GRUPAL',  
		Estatus = CASE WHEN CGF.Folio IS NULL THEN 1 ELSE 2 END  
	FROM Sukarne.dbo.CacCompra C (NOLOCK)  
	INNER JOIN Sukarne.dbo.CacPago P (NOLOCK)  
	ON C.OrganizacionId = P.OrganizacionId AND C.Folio = P.Folio  
	INNER JOIN Organizacion O (NOLOCK)  
	ON O.OrganizacionId = C.OrganizacionId  
	INNER JOIN Sukarne.dbo.CatProveedor CP (NOLOCK)  
	ON CP.ProveedorId = C.ProveedorCompraId AND CP.OrganizacionId = C.OrganizacionId  
	INNER JOIN Sukarne.dbo.CacCompraDetalle CD (NOLOCK)  
	ON C.OrganizacionId = CD.OrganizacionId AND C.Folio = CD.Folio  
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
			C.ProveedorCompraId,  
			CP.Descripcion,  
			C.Factura,  
			C.FechaCompra,   
			P.Cantidad,   
			P.Transferencia,  
			P.PagoId,  
			CGF.Folio,  
			CGF.Factura
			   
	UNION ALL
	  
	SELECT  
		OrganizacionId = C.OrganizacionId,  
		Organizacion = O.Descripcion,  
		FolioCompra = C.Folio,  
		ProveedorId = C.ProveedorCompraId,  
		Proveedor = CP.Descripcion,  
		Factura = ISNULL(CGF.Factura,ISNULL(C.Factura,'')),  
		FechaCompra = C.FechaCompra,   
		Cantidad = P.Cantidad,   
		FormaPago = CASE WHEN P.Transferencia = 0 THEN 'CHEQUE' ELSE 'TRANSFERENCIA' END,  
		PagoId = P.PagoId,  
		ProductoCabezas = COUNT(CD.Folio),  
		'COMPRA DIRECTA',  
		Estatus = CASE WHEN CGF.Folio IS NULL THEN 1 ELSE 2 END  
	FROM Sukarne.dbo.CacCompraDirecta C (NOLOCK)  
	INNER JOIN Sukarne.dbo.CacPago P (NOLOCK)  
	ON C.OrganizacionId = P.OrganizacionId AND C.Folio = P.Folio  
	INNER JOIN Organizacion O (NOLOCK)  
	ON O.OrganizacionId = C.OrganizacionId  
	INNER JOIN Sukarne.dbo.CatProveedor CP (NOLOCK)  
	ON CP.ProveedorId = C.ProveedorCompraId AND CP.OrganizacionId = C.OrganizacionId  
	INNER JOIN Sukarne.dbo.CacCompraDirectaDetalle CD (NOLOCK)  
	ON C.OrganizacionId = CD.OrganizacionId AND C.Folio = CD.Folio  
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
			C.ProveedorCompraId,  
			CP.Descripcion,  
			C.Factura,  
			C.FechaCompra,   
			P.Cantidad,   
			P.Transferencia,  
			P.PagoId,  
			CGF.Folio,  
			CGF.Factura
			   
	UNION ALL  
	
	SELECT  
		OrganizacionId = C.OrganizacionId,  
		Organizacion = O.Descripcion,  
		FolioCompra = C.CompraIndividualId,  
		ProveedorId = C.ProveedorId,  
		Proveedor = CP.Descripcion,  
		Factura = ISNULL(CGF.Factura,ISNULL(C.Factura,'')),  
		FechaCompra = C.FechaCompra,   
		Cantidad = P.Cantidad,   
		FormaPago = CASE WHEN P.Transferencia = 0 THEN 'CHEQUE' ELSE 'TRANSFERENCIA' END,  
		PagoId = P.PagoId,  
		ProductoCabezas = COUNT(CD.CompraIndividualId),  
		'COMPRA INDIVIDUAL',  
		Estatus = CASE WHEN CGF.Folio IS NULL THEN 1 ELSE 2 END  
	FROM Sukarne.dbo.CacCompraIndividualEnc C (NOLOCK)  
	INNER JOIN Sukarne.dbo.CacPago P (NOLOCK)  
	ON C.OrganizacionId = P.OrganizacionId AND C.CompraIndividualId = P.Folio  
	INNER JOIN Organizacion O (NOLOCK)  
	ON O.OrganizacionId = C.OrganizacionId  
	INNER JOIN Sukarne.dbo.CatProveedor CP (NOLOCK)  
	ON CP.ProveedorId = C.ProveedorId AND CP.OrganizacionId = C.OrganizacionId  
	INNER JOIN Sukarne.dbo.CacCompraIndividualDet CD (NOLOCK)  
	ON C.OrganizacionId = CD.OrganizacionId AND C.CompraIndividualId = CD.CompraIndividualId  
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
			C.ProveedorId,  
			CP.Descripcion,  
			C.Factura,  
			C.FechaCompra,   
			P.Cantidad,   
			P.Transferencia,  
			P.PagoId,  
			CGF.Folio,  
			CGF.Factura  
			 
	SELECT  
		ROW_NUMBER() OVER (ORDER BY OrganizacionId,FolioCompra ASC) AS [RowNum],   
		OrganizacionId,  
		Organizacion,  
		FolioCompra,  
		ProveedorId,  
		Proveedor,  
		Factura,  
		FechaCompra,  
		Cantidad,  
		FormaPago,  
		PagoID,  
		ProductoCabezas,  
		TipoCompra,  
		Estatus  
	INTO #Resultado   
	FROM #Registros
	  
	SELECT     
		OrganizacionId,
		Organizacion,
		FolioCompra,
		ProveedorId,
		Proveedor,
		Factura,
		FechaCompra,
		Cantidad,
		FormaPago,
		PagoId,
		ProductoCabezas,
		TipoCompra,
		Estatus  
	FROM #Resultado WHERE RowNum BETWEEN @Inicio AND @Limite
	  
	SELECT    
		COUNT(*) AS [TotalReg]    
	FROM #Resultado  
	
	DROP TABLE #Resultado    
	DROP TABLE #Registros 
	  
	SET NOCOUNT OFF        
END