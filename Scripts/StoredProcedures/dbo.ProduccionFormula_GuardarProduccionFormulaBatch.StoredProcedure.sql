USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_GuardarProduccionFormulaBatch]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionFormula_GuardarProduccionFormulaBatch]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_GuardarProduccionFormulaBatch]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Eduardo Cota
-- Create date: 25/11/2014
-- Description: Gurda los datos capturados en el formulario "Produccion Formulas" en la tabla ProduccionFormulaBatch
-- SpName     : [ProduccionFormula_GuardarProduccionFormulaBatch]
--======================================================
CREATE PROCEDURE [dbo].[ProduccionFormula_GuardarProduccionFormulaBatch] 
@XmlListaProduccionFormula XML,
@OrganizacionID int,
@FormulaID int,
@RotomixID int,
@Batch int,
@CantidadProgramada decimal,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @ListaProduccionFormula AS TABLE 
		(
			ProduccionFormulaID int,
			OrganizacionID int,
			ProductoId INT, --
			FormulaID int,
			RotomixID int,
			Batch int,
			CantidadProgramada int,
			CantidadReal int, --
			Activo bit,
			UsuarioCreacionID int		
		)
	INSERT @ListaProduccionFormula
		(
			ProduccionFormulaID,
			OrganizacionID,
			ProductoId, --
			FormulaID,
			RotomixID,
			Batch,
			CantidadProgramada,
			CantidadReal, --
			Activo,
			UsuarioCreacionID
		)
	SELECT
			ProduccionFormulaID = t.item.value('./ProduccionFormulaID[1]', 'INT'),--
			OrganizacionID = @OrganizacionID,
			ProductoID = t.item.value('./ProductoId[1]', 'INT'),--
			FormulaID = @FormulaID,
			RotomixID = @RotomixID,
			Batch = @Batch,
			CantidadProgramada = @CantidadProgramada,
			CantidadReal = t.item.value('./CantidadProducto[1]', 'DECIMAL'),--
			Activo = @Activo,
			UsuarioCreacionID = @UsuarioCreacionID
		FROM @XmlListaProduccionFormula.nodes('ROOT/ListaProduccionFormula') AS T(item)
INSERT ProduccionFormulaBatch
		(
			ProduccionFormulaID,
			OrganizacionID,
			ProductoId, --
			FormulaID,
			RotomixID,
			Batch,
			CantidadProgramada,
			CantidadReal, --
			Activo,
			FechaCreacion,
			UsuarioCreacionID
		)
	SELECT 
			ProduccionFormulaID,
			OrganizacionID,
			ProductoId, --
			FormulaID,
			RotomixID,
			Batch,
			CantidadProgramada,
			CantidadReal, --
			Activo,
			GETDATE(),
			UsuarioCreacionID
	FROM @ListaProduccionFormula
	WHERE 1=1
	SELECT @@IDENTITY AS INSERTO
	SET NOCOUNT OFF;
END

GO
