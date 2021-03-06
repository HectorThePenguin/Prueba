USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormulaAutomaticas_Resumen]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionFormulaAutomaticas_Resumen]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormulaAutomaticas_Resumen]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Alejandro Quiroz
-- Create date: 15/12/2014
-- Description  Obtiene una lista de formulas ingresadas y para la organizacion del usuario, cuanto hay en inventario y 
--              cuanto esta programada para repartir
/* 
ProduccionFormulaAutomaticas_Resumen 1, 6, 
'<ROOT>
  <Formula>
    <FormulaID>3</FormulaID>
  </Formula>
  <Formula>
    <FormulaID>6</FormulaID>
  </Formula>
</ROOT>' 
*/
--======================================================
CREATE PROCEDURE [dbo].[ProduccionFormulaAutomaticas_Resumen] 
@OrganizacionID int,
@AlmacenID int,
@Formulas XML
AS
BEGIN
	SELECT FormulaID = t.item.value('./FormulaID[1]', 'INT')
	INTO #Formulas
	FROM @Formulas.nodes('ROOT/Formula') AS T(item)
	SELECT f.FormulaID, f.Descripcion, AI.Cantidad, ISNULL(SUM(T.CantidadServida),0) AS CantidadServida
	FROM AlmacenInventario AI
	INNER JOIN Producto P on AI.ProductoID = P.ProductoID 
	INNER JOIN Formula F ON F.ProductoID = P.ProductoID
	LEFT JOIN ( 
		SELECT RD.FormulaIDServida, RD.CantidadServida, R.Fecha
		FROM Reparto R 
		INNER JOIN RepartoDetalle RD  ON R.RepartoID = RD.RepartoID 
		WHERE R.Fecha = GETDATE() 
		AND R.OrganizacionID = @OrganizacionID
		AND RD.Servido = 1 
		AND RD.CostoPromedio = 0
	) AS T ON T.FormulaIDServida = F.FormulaID
	WHERE AI.AlmacenID = @AlmacenID
	AND AI.ProductoID IN (SELECT FormulaID FROM #Formulas)
	GROUP BY f.FormulaID,f.Descripcion, AI.Cantidad
	DROP TABLE #Formulas
END

GO
