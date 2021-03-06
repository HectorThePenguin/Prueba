USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FolioAlmacen_ActualizarFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FolioAlmacen_ActualizarFolio]
GO
/****** Object:  StoredProcedure [dbo].[FolioAlmacen_ActualizarFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Vel�zquez Araujo
-- Create date: 03/07/2014
-- Description:  Actualiza el consecutivo del folio al guardar
-- FolioAlmacen_ActualizarFolio 2, 14
-- =============================================
CREATE PROCEDURE [dbo].[FolioAlmacen_ActualizarFolio] @AlmacenID INT
	,@TipoMovimientoID INT
AS
BEGIN
	UPDATE FolioAlmacen
	SET Valor = Valor + 1
	WHERE AlmacenID = @AlmacenID
		AND TipoMovimientoID = @TipoMovimientoID
END

GO
