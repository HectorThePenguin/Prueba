USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CierreDiaInventarioPA_ValidarCierreDia]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CierreDiaInventarioPA_ValidarCierreDia]
GO
/****** Object:  StoredProcedure [dbo].[CierreDiaInventarioPA_ValidarCierreDia]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Vel�zquez Araujo
-- Create date: 03/07/2014
-- Description:  Obtiene la informacion para el Cierre de D�a de Inventario PA
-- CierreDiaInventarioPA_ValidarCierreDia 23,'20140712'
-- =============================================
CREATE PROCEDURE [dbo].[CierreDiaInventarioPA_ValidarCierreDia] 
	@AlmacenID INT
	,@FechaMovimiento smalldatetime
AS
BEGIN
	select AlmacenMovimientoID from
	AlmacenMovimiento
	where convert(varchar(20),@FechaMovimiento,112) = convert(varchar(20),FechaMovimiento,112)
	AND AlmacenID = @AlmacenID
	AND TipoMovimientoID = 18 --InventarioFisicio
END

GO
