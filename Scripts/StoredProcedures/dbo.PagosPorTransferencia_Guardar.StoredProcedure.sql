USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PagosPorTransferencia_Guardar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PagosPorTransferencia_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[PagosPorTransferencia_Guardar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Sergio A. G�mez G�mez
-- Fecha: 05-06-2015
-- Descripci�n:	Guardar pago por transferencia a proveedores
-- PagosPorTransferencia_Guardar ''
-- =============================================
CREATE PROCEDURE [dbo].[PagosPorTransferencia_Guardar] @XmlPago XML
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		SELECT 
			PagoId = t.item.value('./PagoId[1]', 'INT'),
			OrganizacionId = t.item.value('./CentroAcopioId[1]', 'INT'),
			ProveedorId = t.item.value('./ProveedorId[1]', 'INT'),
			BancoId =  t.item.value('./BancoId[1]', 'INT'),
			Fecha = t.item.value('./Fecha[1]', 'SMALLDATETIME'),
			FechaPago = t.item.value('./FechaPago[1]', 'SMALLDATETIME'),
			CodigoAutorizacion = t.item.value('./CodigoAutorizacion[1]', 'VARCHAR(20)'),
			Importe = t.item.value('./Importe[1]', 'NUMERIC(18,4)'),
			FolioEntrada = t.item.value('./FolioEntrada[1]', 'INT'),
			UsuarioId = t.item.value('./UsuarioId[1]', 'INT')	
		INTO #Pago		
		FROM @XmlPago.nodes('ROOT/Pago') AS T(item)
		IF NOT EXISTS(SELECT '' 
					  FROM [SuKarne].[dbo].[PagosPorTransferencia] PPT (NOLOCK) 
					  INNER JOIN #Pago P (NOLOCK)
						ON P.PagoId = PPT.PagoId AND P.ProveedorId = PPT.OrganizacionId)
		BEGIN
			INSERT INTO [SuKarne].[dbo].[PagosPorTransferencia](PagoId,OrganizacionId,ProveedorId,BancoId,Fecha,FechaPago,CodigoAutorizacion,Importe,FolioEntrada,Estatus,FechaCreacion,UsuarioCreacionId)
			SELECT PagoId,OrganizacionId,ProveedorId,BancoId,Fecha,FechaPago,CodigoAutorizacion,Importe,FolioEntrada,0,GETDATE(),UsuarioId FROM #Pago
			UPDATE [SuKarne].[dbo].[CacPago]
			SET Estatus = 2, FechaModificacion = GETDATE(), UsuarioModificacionId = UsuarioCreacionId
			FROM #Pago P
			INNER JOIN SuKarne.dbo.CacPago CP
				ON CP.OrganizacionId = P.OrganizacionId AND CP.PagoId = P.PagoId		
			COMMIT TRAN
			SELECT PagoId FROM #Pago
		END
		ELSE
		BEGIN
			SELECT -1 PagoId
		END	
	END TRY  
	BEGIN CATCH  
		ROLLBACK TRAN
		SELECT 0 AS PagoId
	END CATCH
END

GO
