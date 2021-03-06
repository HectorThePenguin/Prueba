USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionReimplante_ValidarReimplanteCorralACorral]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionReimplante_ValidarReimplanteCorralACorral]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionReimplante_ValidarReimplanteCorralACorral]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		C�sar Valdez
-- Create date: 2014-12-08
-- Description:	Valdia si el corral a reimplantar es de corral a corral
-- ProgramacionReimplante_ValidarReimplanteCorralACorral 1,1
-- =============================================
CREATE PROCEDURE [dbo].[ProgramacionReimplante_ValidarReimplanteCorralACorral]
	@LoteIDOrigen INT,
	@CorralIDDestino INT
AS
BEGIN	
	DECLARE @CodigoCorralGenerico VARCHAR(10) = 'R99';
	DECLARE @TotalAsignacionesValidas INT = 1;
	DECLARE @TotalDiasPermitidos INT = 7;
	DECLARE @TotalDestino INT;
	DECLARE @TotalOrigen INT;
	/* Se obtienen a cuantos corrales esta configurado este destino */
	SELECT @TotalDestino = COUNT(1) -- AS TotalDestino
	  FROM (
			SELECT C.Codigo,PRD.CorralDestinoID, COUNT(1) AS Total
			  FROM ProgramacionReimplante PR
			 INNER JOIN ProgramacionReimplanteDetalle PRD ON PR.FolioProgramacionID = PRD.FolioProgramacionID
			 INNER JOIN Lote L ON L.LOteID = PR.LoteID
			 INNER JOIN Corral C ON C.CorralID = L.CorralID
			 INNER JOIN Corral Cd ON Cd.CorralID = PRD.CorralDestinoID
			 WHERE 1 = 1
			   AND PR.Activo = 1
			   AND PRD.CorralDestinoID = @CorralIDDestino
		       AND C.Codigo != @CodigoCorralGenerico
			   AND CONVERT(CHAR(8),PR.Fecha,112) BETWEEN CONVERT(CHAR(8),GETDATE() - @TotalDiasPermitidos,112) AND CONVERT(CHAR(8),GETDATE(),112) 
			 GROUP BY C.Codigo,PRD.CorralDestinoID
		) AS ProgDestino;
	/* Se obtiene a cuantos corrales esta programado el Origen */
	SELECT @TotalOrigen = COUNT(1) -- AS TotalOrigen
	  FROM (
			SELECT C.Codigo,PRD.CorralDestinoID, COUNT(1) AS Total
			  FROM ProgramacionReimplante PR
			 INNER JOIN ProgramacionReimplanteDetalle PRD ON PR.FolioProgramacionID = PRD.FolioProgramacionID
			 INNER JOIN Lote L ON L.LOteID = PR.LoteID
			 INNER JOIN Corral C ON C.CorralID = L.CorralID
			 INNER JOIN Corral Cd ON Cd.CorralID = PRD.CorralDestinoID
			 WHERE 1 = 1
			   AND PR.Activo = 1
			   AND PR.LoteID = @LoteIDOrigen
		       AND C.Codigo != @CodigoCorralGenerico
			   AND CONVERT(CHAR(8),PR.Fecha,112) BETWEEN CONVERT(CHAR(8),GETDATE() - @TotalDiasPermitidos,112) AND CONVERT(CHAR(8),GETDATE(),112) 
			 GROUP BY C.Codigo,PRD.CorralDestinoID
		) AS ProgOrigen
	/* Si los totales son igual a @TotalAsignacionesValidas se aplica el cambio del campo Lote */	
	IF (@TotalAsignacionesValidas = @TotalDestino) AND (@TotalAsignacionesValidas = @TotalOrigen)
		BEGIN
			SELECT @TotalAsignacionesValidas AS TotalAsignacionesValidas;
		END 
	ELSE
		BEGIN
			SELECT 0 AS TotalAsignacionesValidas;
		END 
END

GO
