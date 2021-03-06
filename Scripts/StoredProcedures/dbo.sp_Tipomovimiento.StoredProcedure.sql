USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[sp_Tipomovimiento]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[sp_Tipomovimiento]
GO
/****** Object:  StoredProcedure [dbo].[sp_Tipomovimiento]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Tipomovimiento]
AS
BEGIN
	SELECT  TipoMovimientoID, Descripcion FROM TipoMovimiento
	WHERE Descripcion IN ('Salida por Venta', 'Muerte', 'Salida por Sacrificio')
	AND EsGanado = 1 AND EsSalida =1 AND Activo = 1 
END
GO
