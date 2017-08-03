IF EXISTS(SELECT *
          FROM   sys.objects
          WHERE  [object_id] = Object_id(N'[dbo].[ObtenerPesoProyectadoLote]'))
  DROP FUNCTION [dbo].[ObtenerPesoProyectadoLote]
GO
--=============================================
-- Author:		Roberto Aguilar Pozos
-- Create date: 2014-09-30
-- Origen: APInterfaces
-- Description:	Obtiene Los dias promedio de engorda de un Lote segun sus animales
-- select ObtenerPesoProyectadoLote(3)
--=============================================
CREATE FUNCTION dbo.ObtenerPesoProyectadoLote(
@LoteID int
)
RETURNS DECIMAL(10,4)
AS
BEGIN
 DECLARE @PesoPromedio DECIMAL(10,4)
 SELECT @PesoPromedio = AVG(CAST(A.PesoCompra AS DECIMAL(10,4))) + (CAST(LP.gananciaDiaria AS DECIMAL(10,4)) * CAST(LP.DiasEngorda AS DECIMAL(10,4)))
 FROM Animal A
 INNER JOIN AnimalMovimiento AM ON AM.AnimalID = A.AnimalID AND AM.activo = 1
 INNER JOIN LoteProyeccion LP on LP.LoteID = AM.LoteID
 WHERE AM.LoteID = @LoteID 
 group by LP.gananciaDiaria, LP.DiasEngorda
 RETURN @PesoPromedio 
END