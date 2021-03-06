USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ActualizaFechaReal]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ActualizaFechaReal]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ActualizaFechaReal]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Ricardo L�pez
-- Create date: 2013/12/28
-- Description: Actualiza los campos FechaReal y PesoReal de la tabla LoteReimplante
-- 
--=============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ActualizaFechaReal]
  @NoLote AS int,
  @TipoMovimiento int,
  @FolioEntrada int,
  @NumCabezas int,
  @LoteReimplanteID int
AS
BEGIN
  DECLARE @PesoReal INT;
 SET @PesoReal = (SELECT (SUM(am.peso)) 
				  FROM Animal a
				 INNER JOIN AnimalMovimiento am  ON a.AnimalID = am.AnimalID 
				 WHERE am.TipoMovimientoID = @TipoMovimiento
				   AND a.FolioEntrada = @FolioEntrada
				   AND a.Activo = 1 AND am.Activo = 1
				)
  IF @NumCabezas > 0
    BEGIN
     SET @PesoReal = @PesoReal/@NumCabezas;
	END
  ELSE 
    BEGIN
     SET @PesoReal = 0;
    END  
  UPDATE LoteReimplante 
       SET FechaReal = GETDATE(), PesoReal = @PesoReal 
  WHERE LoteReimplanteID = @LoteReimplanteID;
END

GO
