USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCambio_ObtenerPorFechaActual]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCambio_ObtenerPorFechaActual]
GO
/****** Object:  StoredProcedure [dbo].[TipoCambio_ObtenerPorFechaActual]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Jesus Alvarez
-- Create date: 26/05/2014
-- Description:	Obtiene un tipo de contrato por id
-- TipoCambio_ObtenerPorFechaActual 1 
--======================================================
CREATE PROCEDURE [dbo].[TipoCambio_ObtenerPorFechaActual]
@Activo INT
AS
BEGIN
	SELECT  
		TipoCambioID,
		Descripcion,
		Cambio,
		Fecha,
		Activo
	FROM TipoCambio (NOLOCK)
	WHERE CAST(Fecha AS DATE) = CAST(GETDATE() AS DATE)
	AND Activo = @Activo
END

GO
