USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormulaBatch_ValidarBatch]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionFormulaBatch_ValidarBatch]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormulaBatch_ValidarBatch]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 21/05/2015
-- Description: Valida que no este repetida la Produccion de Formulas
-- ProduccionFormulaBatch_ValidarBatch 101,'20150522',1,6,1,1
-- --=============================================
CREATE PROCEDURE [dbo].[ProduccionFormulaBatch_ValidarBatch] 
	@ProductoID INT
	,@Fecha DATE
	,@OrganizacionID INT
	,@FormulaID INT
	,@RotoMixID INT
	,@Batch INT
AS

SELECT 
	pfb.ProduccionFormulaBatchID
	,pfb.ProduccionFormulaID
	,pfb.OrganizacionID
	,pfb.ProductoID
	,pfb.FormulaID
	,pfb.RotomixID
	,pfb.Batch
	,pfb.CantidadProgramada
	,pfb.CantidadReal
FROM ProduccionFormulaBatch pfb
inner join ProduccionFormula pf on pfb.ProduccionFormulaID = pf.ProduccionFormulaID
where CAST(pf.FechaProduccion AS DATE) = @Fecha
and pfb.ProductoID = @ProductoID
and pf.OrganizacionID = @OrganizacionID
and pfb.FormulaID = @FormulaID
and pfb.RotomixID = @RotoMixID
and pfb.Batch = @Batch

GO
