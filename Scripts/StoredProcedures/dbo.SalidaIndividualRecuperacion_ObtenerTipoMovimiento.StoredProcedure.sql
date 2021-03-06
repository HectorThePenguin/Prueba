USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualRecuperacion_ObtenerTipoMovimiento]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividualRecuperacion_ObtenerTipoMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualRecuperacion_ObtenerTipoMovimiento]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 26/02/2014
-- Description:	Obtiene los tipos de movimiento
-- [SalidaIndividualRecuperacion_ObtenerTipoMovimiento] 1
--======================================================
CREATE PROCEDURE [dbo].[SalidaIndividualRecuperacion_ObtenerTipoMovimiento]
@TipoMovimientoID INT
AS
BEGIN
	SELECT TipoMovimientoID,Descripcion,Activo 
	FROM TipoMovimiento (NOLOCK)
	WHERE (TipoMovimientoID = @TipoMovimientoID OR @TipoMovimientoID = 0) AND Activo = 1
END

GO
