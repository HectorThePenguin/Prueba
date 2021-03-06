USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerNombreProveedor]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_ObtenerNombreProveedor]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerNombreProveedor]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		ricardo.lopez
-- Fecha: 17-12-2013
-- Descripci�n:	Obtiene nombre del proveedor
-- CorteGanado_ObtenerNombreProveedor 1
-- =============================================
CREATE PROCEDURE [dbo].[CorteGanado_ObtenerNombreProveedor]
	@NoEmbarqueID INT
AS
BEGIN
	SELECT p.Descripcion 
	  FROM Proveedor p 
	 INNER JOIN EmbarqueDetalle ed ON p.ProveedorID = ed.ProveedorID 
	 WHERE ed.EmbarqueID = @NoEmbarqueID
	 GROUP BY  p.Descripcion
END

GO
