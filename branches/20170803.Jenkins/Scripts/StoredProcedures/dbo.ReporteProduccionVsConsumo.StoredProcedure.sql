IF EXISTS(SELECT * FROM   sys.objects WHERE  [object_id] = Object_id(N'[dbo].[ReporteProduccionVsConsumo]'))
BEGIN
	DROP PROCEDURE [dbo].[ReporteProduccionVsConsumo]
END
GO
-- =============================================
-- Author:    Juan Diego Valenzuela Rivera
-- Create date: 14/10/2015
-- Description:  Genera los datos para el reporte de produccion vs consumo
-- Origen: APInterfaces
-- EXEC ReporteProduccionVsConsumo 1,20100801,20151014  idorganizacion,fechadesde,fechahasta
-- =============================================

CREATE PROCEDURE [dbo].[ReporteProduccionVsConsumo]
		@organizacionid INT,
		@fechainicio DATE,
		@fechafin DATE,
		@ParametroDescripcion VARCHAR(100)
AS
BEGIN

	DECLARE @SubProductos as TABLE
	(
		ProductoID INT primary key
	)
	DECLARE @Valor AS VARCHAR(100)

SELECT @Valor = Valor 
	FROM ParametroOrganizacion PO
	INNER JOIN Parametro P
		ON P.ParametroID = PO.ParametroID
	WHERE P.Clave = @ParametroDescripcion
	and PO.OrganizacionID = @organizacionid

  INSERT INTO @SubProductos(ProductoID)
	SELECT * 
	FROM dbo.FuncionSplit(@Valor, '|')

  SELECT

	Descripcion,

	Cantidad AS CantidadEnPlanta,

	ISNULL(CantidadReparto, 0) AS CantidadDeReparto,

	Organizacion,

	inv.OrganizacionID

FROM (

--Inventario

SELECT

	pr.Descripcion,

	ai.Cantidad,

	f.FormulaID,

	a.OrganizacionID,

	a.Descripcion AS Organizacion

FROM AlmacenInventario ai (NOLOCK)

INNER JOIN Producto pr (NOLOCK)	ON ai.ProductoID = pr.ProductoID

INNER JOIN Almacen a (NOLOCK)	ON ai.AlmacenID = a.AlmacenID

INNER JOIN Formula f (NOLOCK)	ON f.ProductoID = pr.ProductoID

WHERE a.OrganizacionID= @organizacionid

AND ai.ProductoID IN (SELECT ProductoID FROM @SubProductos)

) inv

LEFT JOIN (

--Reparto

SELECT

	fo.FormulaID,

	SUM(rd.CantidadServida) AS CantidadReparto,

	re.OrganizacionID

FROM Reparto re (NOLOCK)

INNER JOIN RepartoDetalle rd (NOLOCK)	ON re.RepartoID = rd.RepartoID

INNER JOIN Formula fo (NOLOCK)	ON rd.FormulaIDServida = fo.FormulaID

WHERE 
(CONVERT(VARCHAR(10), re.Fecha, 112) >=CONVERT(VARCHAR(10), @fechainicio, 112) AND CONVERT(VARCHAR(10), re.Fecha, 112) <=CONVERT(VARCHAR(10), @fechafin, 112))

AND rd.Servido = 1

AND rd.CostoPromedio = 0

AND rd.AlmacenMovimientoID IS NULL

GROUP BY	re.OrganizacionID,

			fo.FormulaID) rep

	ON rep.FormulaID = inv.FormulaID

	AND rep.OrganizacionID = inv.OrganizacionID
END