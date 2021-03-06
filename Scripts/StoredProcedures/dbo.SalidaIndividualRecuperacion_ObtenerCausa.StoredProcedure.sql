USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualRecuperacion_ObtenerCausa]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividualRecuperacion_ObtenerCausa]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualRecuperacion_ObtenerCausa]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 26/02/2014
-- Description:	Obtiene las causas del tipo seleccionado
-- [SalidaIndividualRecuperacion_ObtenerCausa] 11
--======================================================
CREATE PROCEDURE [dbo].[SalidaIndividualRecuperacion_ObtenerCausa]
@TipoMovimientoID INT
AS 
BEGIN
	SELECT CausaSalidaID,Descripcion,TipoMovimientoID,Activo,FechaCreacion,UsuarioCreacionID 
	FROM CausaSalida (NOLOCK)
	WHERE TipoMovimientoID = @TipoMovimientoID
		AND Activo = 1
END

GO
