USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerCantidadNotificacionesAutorizadas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerCantidadNotificacionesAutorizadas]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerCantidadNotificacionesAutorizadas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 05-12-2014
-- Description:	Actualiza las cabezas en lote
-- EntradaProducto_ObtenerCantidadNotificacionesAutorizadas 1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerCantidadNotificacionesAutorizadas]
@OrganizacionID INT
AS 
BEGIN	

	SET NOCOUNT ON

		DECLARE @EstatusAutorizadas INT, @TipoEstatusEntradaProducto INT

		SET @EstatusAutorizadas = 24
		SET @TipoEstatusEntradaProducto = 7

		SELECT COUNT(EP.EntradaProductoID) AS CantidadNotificacionesAutorizadas
		FROM EntradaProducto EP
		INNER JOIN Estatus E
			ON (EP.EstatusID = E.EstatusID
				AND E.EstatusID = @EstatusAutorizadas
				AND E.TipoEstatus = @TipoEstatusEntradaProducto
				AND EP.OperadorIDAutoriza > 0)
		WHERE EP.OrganizacionID = @OrganizacionID
			AND Revisado = 0

	SET NOCOUNT OFF
END

GO
