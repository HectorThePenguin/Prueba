USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CacCompraGanadoFactura_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CacCompraGanadoFactura_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[CacCompraGanadoFactura_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sergio Alberto Gamez Gomez
-- Create date: 17/08/2016
-- Description: Guarda movimientos de auto facturaci�n
-- SpName     : CacCompraGanadoFactura_Guardar
--======================================================  
CREATE PROCEDURE [dbo].[CacCompraGanadoFactura_Guardar]
	@OrganizacionID INT,
	@Folio INT,
	@TipoCompra VARCHAR(50),
	@Factura VARCHAR(50),
	@UsuarioId INT	 
AS  
BEGIN  
	SET NOCOUNT ON; 
	INSERT INTO Sukarne.dbo.CacCompraGanadoFactura(OrganizacionID,Folio,TipoCompra,Factura,Activo,UsuarioCreacionID)
	VALUES(@OrganizacionID,@Folio,@TipoCompra,@Factura,1,@UsuarioId);
	SELECT @Folio
	SET NOCOUNT OFF;  
END

GO
