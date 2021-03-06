USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MateriaPrima_ObtenerReporteRecepcionMateriaPrima]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MateriaPrima_ObtenerReporteRecepcionMateriaPrima]
GO
/****** Object:  StoredProcedure [dbo].[MateriaPrima_ObtenerReporteRecepcionMateriaPrima]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 12/12/2014
-- Description: Actualiza la entrada de producto cuando se llega con el producto antes de descargarlo.
-- SpName     : MateriaPrima_ObtenerReporteRecepcionMateriaPrima 1, '20141122','20141212'
--======================================================
CREATE PROCEDURE [dbo].[MateriaPrima_ObtenerReporteRecepcionMateriaPrima]
@OrganizacionID INT
, @FechaInicio DATE
, @FechaFin DATE
AS
BEGIN
	SET NOCOUNT ON
			SELECT P.Descripcion	AS Empresa
				,  RV.Chofer
				,  RV.Marca
				,  RV.Camion		AS Placa
				,  RV.Color
				,  RV.ProductoID
				,  Prod.Descripcion	AS Producto
				,  ISNULL(CONVERT(CHAR(10), RV.FechaLlegada, 103), '') AS FechaLlegada
				,  ISNULL(CONVERT(CHAR(5), RV.FechaLlegada, 108), '') AS HoraLlegada
				,  ISNULL(CONVERT(CHAR(10), EP.FechaDestara, 103), '') AS FechaDestara
				,  ISNULL(CONVERT(CHAR(5), EP.FechaDestara, 108), '') AS HoraDestara
				,  CONCAT(O.Nombre, ' ', O.ApellidoPaterno, ' ', O.ApellidoMaterno) AS OperadorBasculista
				,  ISNULL(CONVERT(CHAR(10), EP.FechaPesaje, 103), '') AS FechaPesaje
				,  ISNULL(CONVERT(CHAR(5), EP.FechaPesaje, 108), '') AS HoraPesaje
				,  ISNULL(ED.Tiempo, '') AS Tiempo
			FROM RegistroVigilancia RV
			INNER JOIN Proveedor P
				ON (RV.ProveedorIDMateriasPrimas = P.ProveedorID)
			INNER JOIN Producto Prod
				ON (RV.ProductoID = Prod.ProductoID)
			INNER JOIN EntradaProducto EP
				ON (RV.RegistroVigilanciaID = EP.RegistroVigilanciaID
					AND RV.OrganizacionID = EP.OrganizacionID
					AND Prod.ProductoID = EP.ProductoID)
			INNER JOIN Operador O
				ON (EP.OperadorIDBascula = O.OperadorID)
			LEFT OUTER JOIN EstandarDescarga ED
				ON (RV.ProductoID = ED.ProductoID
					AND RV.OrganizacionID = ED.OrganizacionID)
			WHERE CAST(RV.FechaLlegada AS DATE) BETWEEN @FechaInicio AND @FechaFin
				AND RV.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF
END

GO
