USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCambio_ObtenerPorEstado]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCambio_ObtenerPorEstado]
GO
/****** Object:  StoredProcedure [dbo].[TipoCambio_ObtenerPorEstado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jesus Alvarez
-- Create date: 19/05/2014
-- Description: Obtiene el total de tipos de cambio
-- TipoCambio_ObtenerPorEstado
-- =============================================
CREATE PROCEDURE [dbo].[TipoCambio_ObtenerPorEstado]
@Activo BIT = null
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT TipoCambioID,
			 Descripcion,
			 Cambio,
			 Fecha,
			 Activo
      FROM TipoCambio (NOLOCK)
      WHERE (Activo = @Activo OR @Activo is null)
	  ORDER BY TipoCambioID
      SET NOCOUNT OFF;
  END

GO
