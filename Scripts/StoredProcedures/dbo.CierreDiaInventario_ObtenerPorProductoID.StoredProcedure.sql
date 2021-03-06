USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CierreDiaInventario_ObtenerPorProductoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CierreDiaInventario_ObtenerPorProductoID]
GO
/****** Object:  StoredProcedure [dbo].[CierreDiaInventario_ObtenerPorProductoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Edgar villarreal
-- Create date: 04/03/2014
-- Description:  Obtener el Producto en base a su ID
-- CierreDiaInventario_ObtenerPorProductoID 1
-- =============================================
CREATE PROCEDURE [dbo].[CierreDiaInventario_ObtenerPorProductoID] 
@ProductoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ProductoID
					,Cantidad
					,PrecioPromedio
					,Importe
	FROM AlmacenInventario
	WHERE ProductoID=@ProductoID
	SET NOCOUNT OFF;
END

GO
