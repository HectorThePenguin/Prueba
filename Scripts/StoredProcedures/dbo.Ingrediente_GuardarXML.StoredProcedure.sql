USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_GuardarXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Ingrediente_GuardarXML]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_GuardarXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 09/09/2014
-- Description: Guardar los ingredientes de la Formula
-- 
-- =============================================
CREATE PROCEDURE [dbo].[Ingrediente_GuardarXML] @IngredientesXML XML
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #INGREDIENTE (
		IngredienteID INT
		,OrganizacionID INT
		,FormulaID INT
		,ProductoID INT
		,PorcentajeProgramado DECIMAL(10, 3)
		,Activo BIT
		,UsuarioCreacionID INT
		,UsuarioModificacionID INT
		)
	INSERT #INGREDIENTE (
		IngredienteID
		,OrganizacionID
		,FormulaID
		,ProductoID
		,PorcentajeProgramado
		,Activo
		,UsuarioCreacionID
		,UsuarioModificacionID
		)
	SELECT IngredienteID = t.item.value('./IngredienteID[1]', 'INT')
		,OrganizacionID = t.item.value('./OrganizacionID[1]', 'INT')
		,FormulaID = t.item.value('./FormulaID[1]', 'INT')
		,ProductoID = t.item.value('./ProductoID[1]', 'INT')
		,PorcentajeProgramado = t.item.value('./PorcentajeProgramado[1]', 'DECIMAL(10,3)')
		,Activo = t.item.value('./Activo[1]', 'BIT')
		,UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
		,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM @IngredientesXML.nodes('ROOT/Ingrediente') AS T(item)
	UPDATE i
	SET OrganizacionID = tmp.OrganizacionID
		,FormulaID = tmp.FormulaID
		,ProductoID = tmp.ProductoID
		,PorcentajeProgramado = tmp.PorcentajeProgramado
		,Activo = tmp.Activo
		,UsuarioModificacionID = tmp.UsuarioModificacionID
		,FechaModificacion = GETDATE()
	FROM Ingrediente i
	INNER JOIN #INGREDIENTE tmp ON i.IngredienteID = tmp.IngredienteID
	INSERT Ingrediente (
		OrganizacionID
		,FormulaID
		,ProductoID
		,PorcentajeProgramado
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		)
	SELECT OrganizacionID
		,FormulaID
		,ProductoID
		,PorcentajeProgramado
		,Activo		
		,GETDATE()
		,UsuarioCreacionID		
	FROM #INGREDIENTE
	WHERE IngredienteID = 0
	SET NOCOUNT OFF;
END

GO
