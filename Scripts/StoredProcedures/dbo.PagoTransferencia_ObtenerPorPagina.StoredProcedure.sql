USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PagoTransferencia_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PagoTransferencia_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[PagoTransferencia_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sergio Alberto Gamez Gomez
-- Create date: 04/06/2016
-- Description: Obtener los pagos por transferencia de forma paginada  
-- SpName     : PagoTransferencia_ObtenerPorPagina 0,0,1,15
--======================================================  
CREATE PROCEDURE [dbo].[PagoTransferencia_ObtenerPorPagina]
	@CentroId INT,
	@FolioId INT,
	@Inicio INT,
	@Limite INT
AS  
BEGIN  
	SET NOCOUNT ON; 
	CREATE TABLE #Pago(
		RowNum INT,
		PagoID INT,  
		CentroAcopioID INT,
		CentroAcopioDescripcion VARCHAR(100),
		BancoID INT,  
		BancoDescripcion VARCHAR(100),
		FolioEntrada INT,
		Proveedor INT,
		ProveedorDescripcion VARCHAR(100),
		Fecha SMALLDATETIME,
		Importe NUMERIC(18,2)
	)
	IF @CentroId = 0 AND @FolioId = 0
	BEGIN
		INSERT INTO #Pago(RowNum,PagoID,CentroAcopioID,CentroAcopioDescripcion,BancoID,BancoDescripcion,FolioEntrada,Proveedor,ProveedorDescripcion,Fecha,Importe)
		SELECT  
			ROW_NUMBER() OVER (ORDER BY P.Folio ASC) AS [RowNum],
			P.PagoId,  
			CentroAcopioID = P.OrganizacionID,
			CentroAcopioDescripcion = UPPER(O.Descripcion),
			BA.BancoID,  
			BancoDescripcion = UPPER(ISNULL(BA.Descripcion,'')),
			P.Folio,
			ProveedorId = ISNULL(I.ProveedorId,0),
			ProveedorDescripcion = UPPER(ISNULL(I.Descripcion,'')),
			P.Fecha,
			P.Cantidad
		FROM [Sukarne].[dbo].[CacPago] P (NOLOCK)
		INNER JOIN Organizacion O (NOLOCK)
			ON O.OrganizacionID = P.OrganizacionID
		/*LO SIGUIENTE ES NECESARIO PARA OBTENER EL PROVEEDOR DEL PAGO, ASI LO FUE DEFINIDO EN EL SISTEMA DE CENTROS*/
		LEFT OUTER JOIN [Sukarne].[dbo].[CacCompraIndividualEnc] B 
			ON P.Folio = B.CompraIndividualId AND P.OrganizacionId = B.OrganizacionId
		LEFT OUTER JOIN [Sukarne].[dbo].[CacCompra] C 
			ON P.Folio = C.Folio AND P.OrganizacionId = C.OrganizacionId
		LEFT OUTER JOIN [Sukarne].[dbo].[CacCompraProductoEnc] D 
			ON P.Folio = D.FolioCompraId AND P.OrganizacionId = D.OrganizacionId
		LEFT OUTER JOIN [Sukarne].[dbo].[CacMuestreo] E 
			ON P.Folio = E.MuestreoId AND P.OrganizacionId = E.OrganizacionId
		LEFT OUTER JOIN [Sukarne].[dbo].[CacCompraDirecta] G 
			ON P.Folio = G.Folio AND P.OrganizacionId = G.OrganizacionId
		LEFT OUTER JOIN [Sukarne].[dbo].[CacGastoEnc] H 
			ON H.FolioGastoID = P.Folio AND P.OrganizacionId = G.OrganizacionId
		LEFT OUTER JOIN [Sukarne].[dbo].[CatProveedor] I 
			ON P.OrganizacionId = I.OrganizacionId AND (I.ProveedorID = B.ProveedorId OR I.ProveedorID = D.ProveedorId OR I.ProveedorID = H.ProveedorId OR I.ProveedorID = E.ProveedorId OR I.ProveedorID = C.ProveedorCompraId OR I.ProveedorID = G.ProveedorCompraID)
		LEFT OUTER JOIN [Sukarne].[dbo].[Chequera] CQ (NOLOCK)
			ON CQ.OrganizacionId = O.OrganizacionID AND CQ.Activo IN(0,1)
		LEFT OUTER JOIN Banco BA (NOLOCK)
			ON BA.BancoID = CQ.BancoID		
		LEFT OUTER JOIN [Sukarne].[dbo].[PagosPorTransferencia] PPT 
			ON PPT.PagoID = P.PagoID AND PPT.OrganizacionId = P.OrganizacionId		
		WHERE PPT.PagoID IS NULL AND P.Transferencia = 1 AND P.Estatus >0
		SELECT  
			PagoID,  
			CentroAcopioID,
			CentroAcopioDescripcion,
			BancoID,  
			BancoDescripcion,
			FolioEntrada,
			Proveedor,
			ProveedorDescripcion,
			Fecha,
			Importe
		FROM #Pago	
		WHERE RowNum BETWEEN @Inicio AND @Limite
		SELECT  
		COUNT(*) AS [TotalReg]  
		FROM #Pago
		DROP TABLE #Pago  
	END
	ELSE
	BEGIN
		IF @CentroId > 0 AND @FolioId = 0
		BEGIN
			INSERT INTO #Pago(RowNum,PagoID,CentroAcopioID,CentroAcopioDescripcion,BancoID,BancoDescripcion,FolioEntrada,Proveedor,ProveedorDescripcion,Fecha,Importe)
			SELECT  
				ROW_NUMBER() OVER (ORDER BY P.Folio ASC) AS [RowNum],
				P.PagoId,  
				CentroAcopioID = P.OrganizacionID,
				CentroAcopioDescripcion = UPPER(O.Descripcion),
				BA.BancoID,  
				BancoDescripcion = UPPER(ISNULL(BA.Descripcion,'')),
				P.Folio,
				ProveedorId = ISNULL(I.ProveedorId,0),
				ProveedorDescripcion = UPPER(ISNULL(I.Descripcion,'')),
				P.Fecha,
				P.Cantidad
			FROM [Sukarne].[dbo].[CacPago] P (NOLOCK)
			INNER JOIN Organizacion O (NOLOCK)
				ON O.OrganizacionID = P.OrganizacionID
			/*LO SIGUIENTE ES NECESARIO PARA OBTENER EL PROVEEDOR DEL PAGO, ASI LO FUE DEFINIDO EN EL SISTEMA DE CENTROS*/
			LEFT OUTER JOIN [Sukarne].[dbo].[CacCompraIndividualEnc] B 
				ON P.Folio = B.CompraIndividualId AND P.OrganizacionId = B.OrganizacionId
			LEFT OUTER JOIN [Sukarne].[dbo].[CacCompra] C 
				ON P.Folio = C.Folio AND P.OrganizacionId = C.OrganizacionId
			LEFT OUTER JOIN [Sukarne].[dbo].[CacCompraProductoEnc] D 
				ON P.Folio = D.FolioCompraId AND P.OrganizacionId = D.OrganizacionId
			LEFT OUTER JOIN [Sukarne].[dbo].[CacMuestreo] E 
				ON P.Folio = E.MuestreoId AND P.OrganizacionId = E.OrganizacionId
			LEFT OUTER JOIN [Sukarne].[dbo].[CacCompraDirecta] G 
				ON P.Folio = G.Folio AND P.OrganizacionId = G.OrganizacionId
			LEFT OUTER JOIN [Sukarne].[dbo].[CacGastoEnc] H 
				ON H.FolioGastoID = P.Folio AND P.OrganizacionId = G.OrganizacionId
			LEFT OUTER JOIN [Sukarne].[dbo].[CatProveedor] I 
				ON P.OrganizacionId = I.OrganizacionId AND (I.ProveedorID = B.ProveedorId OR I.ProveedorID = D.ProveedorId OR I.ProveedorID = H.ProveedorId OR I.ProveedorID = E.ProveedorId OR I.ProveedorID = C.ProveedorCompraId OR I.ProveedorID = G.ProveedorCompraID) 
			LEFT OUTER JOIN [Sukarne].[dbo].[Chequera] CQ (NOLOCK)
				ON CQ.OrganizacionId = O.OrganizacionID AND CQ.Activo IN(0,1)
			LEFT OUTER JOIN Banco BA (NOLOCK)
				ON BA.BancoID = CQ.BancoID			
			LEFT OUTER JOIN [Sukarne].[dbo].[PagosPorTransferencia] PPT 
				ON PPT.PagoID = P.PagoID AND PPT.OrganizacionId = P.OrganizacionId
			WHERE PPT.PagoID IS NULL
			AND P.OrganizacionID = @CentroId
			AND P.Transferencia = 1 AND P.Estatus >0
			SELECT  
				PagoID,  
				CentroAcopioID,
				CentroAcopioDescripcion,
				BancoID,  
				BancoDescripcion,
				FolioEntrada,
				Proveedor,
				ProveedorDescripcion,
				Fecha,
				Importe
			FROM #Pago	
			WHERE RowNum BETWEEN @Inicio AND @Limite
			SELECT  
			COUNT(*) AS [TotalReg]  
			FROM #Pago
			DROP TABLE #Pago  
		END
		ELSE
		BEGIN
			IF @FolioId > 0 AND @CentroId = 0
			BEGIN
				INSERT INTO #Pago(RowNum,PagoID,CentroAcopioID,CentroAcopioDescripcion,BancoID,BancoDescripcion,FolioEntrada,Proveedor,ProveedorDescripcion,Fecha,Importe)
				SELECT  
					ROW_NUMBER() OVER (ORDER BY P.Folio ASC) AS [RowNum],
					P.PagoId,  
					CentroAcopioID = P.OrganizacionID,
					CentroAcopioDescripcion = UPPER(O.Descripcion),
					BA.BancoID,  
					BancoDescripcion = UPPER(ISNULL(BA.Descripcion,'')),
					P.Folio,
					ProveedorId = ISNULL(I.ProveedorId,0),
					ProveedorDescripcion = UPPER(ISNULL(I.Descripcion,'')),
					P.Fecha,
					P.Cantidad
				FROM [Sukarne].[dbo].[CacPago] P (NOLOCK)
				INNER JOIN Organizacion O (NOLOCK)
					ON O.OrganizacionID = P.OrganizacionID
				/*LO SIGUIENTE ES NECESARIO PARA OBTENER EL PROVEEDOR DEL PAGO, ASI LO FUE DEFINIDO EN EL SISTEMA DE CENTROS*/
				LEFT OUTER JOIN [Sukarne].[dbo].[CacCompraIndividualEnc] B 
					ON P.Folio = B.CompraIndividualId AND P.OrganizacionId = B.OrganizacionId
				LEFT OUTER JOIN [Sukarne].[dbo].[CacCompra] C 
					ON P.Folio = C.Folio AND P.OrganizacionId = C.OrganizacionId
				LEFT OUTER JOIN [Sukarne].[dbo].[CacCompraProductoEnc] D 
					ON P.Folio = D.FolioCompraId AND P.OrganizacionId = D.OrganizacionId
				LEFT OUTER JOIN [Sukarne].[dbo].[CacMuestreo] E 
					ON P.Folio = E.MuestreoId AND P.OrganizacionId = E.OrganizacionId
				LEFT OUTER JOIN [Sukarne].[dbo].[CacCompraDirecta] G 
					ON P.Folio = G.Folio AND P.OrganizacionId = G.OrganizacionId
				LEFT OUTER JOIN [Sukarne].[dbo].[CacGastoEnc] H 
					ON H.FolioGastoID = P.Folio AND P.OrganizacionId = G.OrganizacionId
				LEFT OUTER JOIN [Sukarne].[dbo].[CatProveedor] I 
					ON P.OrganizacionId = I.OrganizacionId AND (I.ProveedorID = B.ProveedorId OR I.ProveedorID = D.ProveedorId OR I.ProveedorID = H.ProveedorId OR I.ProveedorID = E.ProveedorId OR I.ProveedorID = C.ProveedorCompraId OR I.ProveedorID = G.ProveedorCompraID) 
				LEFT OUTER JOIN [Sukarne].[dbo].[Chequera] CQ (NOLOCK)
					ON CQ.OrganizacionId = O.OrganizacionID AND CQ.Activo IN(0,1)
				LEFT OUTER JOIN Banco BA (NOLOCK)
					ON BA.BancoID = CQ.BancoID	
				LEFT OUTER JOIN [Sukarne].[dbo].[PagosPorTransferencia] PPT 
					ON PPT.PagoID = P.PagoID AND PPT.OrganizacionId = P.OrganizacionId
				WHERE PPT.PagoID IS NULL
				AND P.Folio = @FolioId
				AND P.Transferencia = 1 AND P.Estatus >0
				SELECT  
					PagoID,  
					CentroAcopioID,
					CentroAcopioDescripcion,
					BancoID,  
					BancoDescripcion,
					FolioEntrada,
					Proveedor,
					ProveedorDescripcion,
					Fecha,
					Importe
				FROM #Pago	
				WHERE RowNum BETWEEN @Inicio AND @Limite
				SELECT  
				COUNT(*) AS [TotalReg]  
				FROM #Pago
				DROP TABLE #Pago  
			END
			ELSE
			BEGIN
				INSERT INTO #Pago(RowNum,PagoID,CentroAcopioID,CentroAcopioDescripcion,BancoID,BancoDescripcion,FolioEntrada,Proveedor,ProveedorDescripcion,Fecha,Importe)
				SELECT  
					ROW_NUMBER() OVER (ORDER BY P.Folio ASC) AS [RowNum],
					P.PagoId,  
					CentroAcopioID = P.OrganizacionID,
					CentroAcopioDescripcion = UPPER(O.Descripcion),
					BA.BancoID,  
					BancoDescripcion = UPPER(ISNULL(BA.Descripcion,'')),
					P.Folio,
					ProveedorId = ISNULL(I.ProveedorId,0),
					ProveedorDescripcion = UPPER(ISNULL(I.Descripcion,'')),
					P.Fecha,
					P.Cantidad
				FROM [Sukarne].[dbo].[CacPago] P (NOLOCK)
				INNER JOIN Organizacion O (NOLOCK)
					ON O.OrganizacionID = P.OrganizacionID
				/*LO SIGUIENTE ES NECESARIO PARA OBTENER EL PROVEEDOR DEL PAGO, ASI LO FUE DEFINIDO EN EL SISTEMA DE CENTROS*/
				LEFT OUTER JOIN [Sukarne].[dbo].[CacCompraIndividualEnc] B 
					ON P.Folio = B.CompraIndividualId AND P.OrganizacionId = B.OrganizacionId
				LEFT OUTER JOIN [Sukarne].[dbo].[CacCompra] C 
					ON P.Folio = C.Folio AND P.OrganizacionId = C.OrganizacionId
				LEFT OUTER JOIN [Sukarne].[dbo].[CacCompraProductoEnc] D 
					ON P.Folio = D.FolioCompraId AND P.OrganizacionId = D.OrganizacionId
				LEFT OUTER JOIN [Sukarne].[dbo].[CacMuestreo] E 
					ON P.Folio = E.MuestreoId AND P.OrganizacionId = E.OrganizacionId
				LEFT OUTER JOIN [Sukarne].[dbo].[CacCompraDirecta] G 
					ON P.Folio = G.Folio AND P.OrganizacionId = G.OrganizacionId
				LEFT OUTER JOIN [Sukarne].[dbo].[CacGastoEnc] H 
					ON H.FolioGastoID = P.Folio AND P.OrganizacionId = G.OrganizacionId
				LEFT OUTER JOIN [Sukarne].[dbo].[CatProveedor] I 
					ON P.OrganizacionId = I.OrganizacionId AND (I.ProveedorID = B.ProveedorId OR I.ProveedorID = D.ProveedorId OR I.ProveedorID = H.ProveedorId OR I.ProveedorID = E.ProveedorId OR I.ProveedorID = C.ProveedorCompraId OR I.ProveedorID = G.ProveedorCompraID) 
				LEFT OUTER JOIN [Sukarne].[dbo].[Chequera] CQ (NOLOCK)
					ON CQ.OrganizacionId = O.OrganizacionID AND CQ.Activo IN(0,1)
				LEFT OUTER JOIN Banco BA (NOLOCK)
					ON BA.BancoID = CQ.BancoID	
				LEFT OUTER JOIN [Sukarne].[dbo].[PagosPorTransferencia] PPT 
					ON PPT.PagoID = P.PagoID AND PPT.OrganizacionId = P.OrganizacionId
				WHERE PPT.PagoID IS NULL
				AND P.OrganizacionID = @CentroId
				AND P.Folio = @FolioId
				AND P.Transferencia = 1 AND P.Estatus >0
				SELECT  
					PagoID,  
					CentroAcopioID,
					CentroAcopioDescripcion,
					BancoID,  
					BancoDescripcion,
					FolioEntrada,
					Proveedor,
					ProveedorDescripcion,
					Fecha,
					Importe
				FROM #Pago	
				WHERE RowNum BETWEEN @Inicio AND @Limite
				SELECT  
				COUNT(*) AS [TotalReg]  
				FROM #Pago
				DROP TABLE #Pago  
			END 
		END
	END
	SET NOCOUNT OFF;  
END

GO
