USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_CalcularDiasEngorda70]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[OrdenSacrificio_CalcularDiasEngorda70]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_CalcularDiasEngorda70]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/02/27
-- Description: SP para consultar dias de engorda 70
-- Origen     : APInterfaces
-- EXEC OrdenSacrificio_CalcularDiasEngorda70 1,3,4,4
-- =============================================
CREATE PROCEDURE [dbo].[OrdenSacrificio_CalcularDiasEngorda70]
    @LoteId INT,
	@TipoProduccion INT,
	@TipoFinalizacion INT,
	@OrganizacionID INT
AS
BEGIN
	DECLARE @DiasEngorda INT
	DECLARE @DiasFormula INT
	DECLARE @Resultado DECIMAL(14,2)
	DECLARE @FormulasAplicadas AS TABLE
	(
		Fecha DATE,
		FormulaID INT,
		TipoFormulaID INT,
		Dias INT
	)
	INSERT @FormulasAplicadas (Fecha,FormulaID,TipoFormulaID,Dias)
	SELECT R.Fecha,F.FormulaID, F.TipoFormulaID, DATEDIFF(day, R.Fecha,GETDATE())
	FROM Reparto R
	INNER JOIN RepartoDetalle RD ON R.RepartoID=RD.RepartoID AND RD.Activo=1
	INNER JOIN Formula F ON RD.FormulaIDServida=F.FormulaID AND F.Activo=1
	WHERE R.LoteID=@LoteId
	AND F.TipoFormulaID IN(@TipoProduccion,@TipoFinalizacion)
	AND R.OrganizacionID = @OrganizacionID
	AND R.Activo = 1
	SELECT @DiasFormula=ISNULL(SUM(Dias),0)
	FROM @FormulasAplicadas
	SELECT @DiasEngorda = ISNULL(DATEDIFF(day,FechaInicio,GETDATE()),0) 
	  FROM Lote 
	 WHERE LoteID = @LoteId 
	SET @Resultado = 0
	IF @DiasEngorda > 0
	BEGIN
		SET @Resultado = (@DiasFormula / @DiasEngorda) * 100
	END 
	SELECT  @Resultado AS DiasEngorda70;
END

GO
