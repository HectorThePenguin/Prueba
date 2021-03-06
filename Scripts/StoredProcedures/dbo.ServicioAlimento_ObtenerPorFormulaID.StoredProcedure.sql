USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ServicioAlimento_ObtenerPorFormulaID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ServicioAlimento_ObtenerPorFormulaID]
GO
/****** Object:  StoredProcedure [dbo].[ServicioAlimento_ObtenerPorFormulaID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leonel Ayala Flores
-- Create date: 22-11-2013
-- Description:	Consulta Obtiene Tipo de Formula 
-- Empresa: Apinterfaces
-- [dbo].[ServicioAlimento_ObtenerPorFormulaID] 1
-- =============================================
CREATE PROCEDURE [dbo].[ServicioAlimento_ObtenerPorFormulaID]
@TipoFormulaID INT 
AS
BEGIN 
SET NOCOUNT ON;
	SELECT 
		FormulaID,
		Descripcion,
		TipoFormulaID
	FROM Formula
	WHERE TipoFormulaID=@TipoFormulaID
SET NOCOUNT OFF;	
END

GO
