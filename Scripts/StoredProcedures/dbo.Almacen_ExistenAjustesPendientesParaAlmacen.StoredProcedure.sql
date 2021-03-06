USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ExistenAjustesPendientesParaAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ExistenAjustesPendientesParaAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ExistenAjustesPendientesParaAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 20/02/2014
-- Description:  Cuenta los ajustes que faltan por aplicar en almacen(Diferencias de inventario)
-- Origen: APInterfaces
-- Almacen_ExistenAjustesPendientesParaAlmacen 
-- =============================================
CREATE PROCEDURE [dbo].[Almacen_ExistenAjustesPendientesParaAlmacen]
	@AlmacenID INT,
	@TipoMovimientoID INT,
	@Status INT
AS
BEGIN
	SELECT COUNT(1) AS AjustesPendientes
 	  FROM AlmacenMovimiento
	 WHERE AlmacenID = @AlmacenID
	   AND TipoMovimientoID = @TipoMovimientoID
	   AND Status = @Status 
END

GO
