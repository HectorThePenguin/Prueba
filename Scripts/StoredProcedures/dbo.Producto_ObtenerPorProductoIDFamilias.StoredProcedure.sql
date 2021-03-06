USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoIDFamilias]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorProductoIDFamilias]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoIDFamilias]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Pedro Delgado
-- Create date: 2014/06/25
-- Description: Procedimiento para obtener los productos por la familia
/* Producto_ObtenerPorProductoIDFamilias '<ROOT>
											<Familias>
												<FamiliaID>4</FamiliaID>
											</Familias>
											<Familias>
												<FamiliaID>3</FamiliaID>
											</Familias>
										</ROOT>', '', 1, 1, 100*/
--=============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorProductoIDFamilias]
@XMLFamilias XML,
@ProductoID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		FamiliaID = T.item.value('./FamiliaID[1]', 'INT')
	INTO #Familias
	FROM  @XMLFamilias.nodes('ROOT/Familias') AS T(item)
	SELECT 
		   Pro.ProductoID
		,  Pro.Descripcion	AS Producto
		,  sb.Descripcion	AS SubFamilia	
	FROM Producto Pro
	INNER JOIN SubFamilia SB
		ON (Pro.SubFamiliaID = sb.SubFamiliaID)
	INNER JOIN Familia F
		ON (SB.FamiliaID = F.FamiliaID)
	INNER JOIN #Familias TF
		ON (F.FamiliaID = TF.FamiliaID)
	WHERE pro.Activo = @Activo				
				AND pro.ProductoID = @ProductoID
	SET NOCOUNT OFF;
END

GO
