USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MermaSuperavit_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 13/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : MermaSuperavit_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[MermaSuperavit_ObtenerPorDescripcion]
@AlmacenID INT
, @ProductoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		MS.MermaSuperavitID,
		MS.AlmacenID,
		MS.ProductoID,
		MS.Merma,
		MS.Superavit,
		MS.Activo
		, A.Descripcion		AS Almacen
		, P.Descripcion		AS Producto
	FROM MermaSuperavit MS
	INNER JOIN Almacen A
		ON (MS.AlmacenID = A.AlmacenID)
	INNER JOIN Producto P
		ON (MS.ProductoID = P.ProductoID)
	WHERE MS.AlmacenID = @AlmacenID
		AND MS.ProductoID = @ProductoID
	SET NOCOUNT OFF;
END

GO
