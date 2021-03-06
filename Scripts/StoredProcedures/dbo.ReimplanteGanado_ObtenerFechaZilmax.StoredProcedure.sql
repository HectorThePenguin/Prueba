USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerFechaZilmax]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ObtenerFechaZilmax]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerFechaZilmax]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/10/17
-- Description: SP para Obtener La fecha Zilmax de un Lote
-- Origen     : APInterfaces
-- EXEC ReimplanteGanado_ObtenerFechaZilmax 1,'M','20141017',90
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ObtenerFechaZilmax]
	@OrganizacionID INT,
	@Sexo CHAR(1),
	@FechaInicio DATETIME,
	@DiasEngorda INT
AS
BEGIN
	DECLARE @ZILMAX SMALLDATETIME;
	--Calculamos Entrada Zilmax
	SELECT @ZILMAX = 
		DATEADD(DAY, 
				-1 * CAST((SELECT dbo.ObtenerConstanteValor('ZILMAX', @OrganizacionID, 1, @Sexo)) AS INT),
				DATEADD(DAY,CAST(@DiasEngorda AS INT), CONVERT(CHAR(8),@FechaInicio,112))); 
	--Regresamos FechaEntradaZilmax
	SELECT @ZILMAX AS EntradaZilmax;
END

GO
