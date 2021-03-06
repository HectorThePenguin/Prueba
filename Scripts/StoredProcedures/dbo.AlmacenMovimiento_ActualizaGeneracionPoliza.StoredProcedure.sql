USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ActualizaGeneracionPoliza]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimiento_ActualizaGeneracionPoliza]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ActualizaGeneracionPoliza]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 07/07/2014
-- Description:  Obtiene los movimientos para generar
--				 la poliza
-- AlmacenMovimiento_ActualizaGeneracionPoliza
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenMovimiento_ActualizaGeneracionPoliza]
@XmlAlmacenMovimiento XML
AS
BEGIN
	SET NOCOUNT ON
		UPDATE AM
		SET PolizaGenerada = 1
			, FechaModificacion = GETDATE()
			, UsuarioModificacionID = B.UsuarioModificacionID
		FROM AlmacenMovimiento AM
		INNER JOIN
		(
			SELECT 
					AlmacenMovimientoID  = T.item.value('./AlmacenMovimientoID[1]', 'BIGINT')
					, UsuarioModificacionID  = T.item.value('./UsuarioModificacionID[1]', 'INT')
			FROM  @XmlAlmacenMovimiento.nodes('ROOT/AlmacenMovimiento') AS T(item)
		) B	ON (AM.AlmacenMovimientoID = B.AlmacenMovimientoID)
	SET NOCOUNT OFF
END

GO
