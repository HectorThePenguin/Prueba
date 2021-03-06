USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_ObtenerPorTratamientoID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TratamientoProducto_ObtenerPorTratamientoID]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_ObtenerPorTratamientoID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Pedro Delgado
-- Create date: 08/09/2014
-- Origen: APInterfaces
-- Description:	Obtiene los productos del tratamiento
-- execute TratamientoProducto_ObtenerPorTratamientoID 1
-- =============================================
CREATE PROCEDURE [dbo].[TratamientoProducto_ObtenerPorTratamientoID]
@TratamientoID INT
AS 
BEGIN
	SELECT 
		TratamientoProductoID,
		TratamientoID,
		P.ProductoID,
		P.Descripcion,
		Dosis,
		TP.Activo,
		TP.FechaCreacion,
		TP.UsuarioCreacionID,
		TP.FechaModificacion,
		TP.UsuarioModificacionID
	FROM TratamientoProducto TP
	INNER JOIN Producto P ON (TP.ProductoID = P.ProductoID)
	WHERE TP.TratamientoID = @TratamientoID AND TP.Activo = 1 AND P.Activo = 1
END

GO
