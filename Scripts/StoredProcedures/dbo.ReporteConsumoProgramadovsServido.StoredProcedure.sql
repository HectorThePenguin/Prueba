USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteConsumoProgramadovsServido]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteConsumoProgramadovsServido]
GO
/****** Object:  StoredProcedure [dbo].[ReporteConsumoProgramadovsServido]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gumaro Alberto Lugo D�az
-- Create date: 26/07/2014 10:37:00:00 p.m.
-- Description: Procedimiento almacenado para generar el reporte Consumo Programado VS Servido
-- SpName     : ReporteConsumoProgramadovsServido 1 ,'2014-09-11'
--======================================================
CREATE PROCEDURE [dbo].[ReporteConsumoProgramadovsServido]
   @OrganizacionID AS INT ,
   @Fecha DATETIME
AS
   SELECT   C.Codigo AS Corral, L.Cabezas, R.PesoProyectado, R.DiasEngorda,
            RD.FormulaIDServida, F.Descripcion,
            CantidadProgramada = SUM(RD.CantidadProgramada),
            CantidadServida = SUM(RD.CantidadServida),
            Diferencia = SUM(RD.CantidadProgramada) - SUM(RD.CantidadServida),
            ConsumoPromedio = CASE L.Cabezas
                                WHEN 0 THEN 0
                                ELSE SUM(RD.CantidadServida) / l.Cabezas
                              END,
            CPV = CASE CASE L.Cabezas
                         WHEN 0.0 THEN 0.0
                         ELSE SUM(RD.CantidadServida) / l.Cabezas
                       END
                    WHEN 0.0 THEN 0.0
                    ELSE 
						CASE R.PesoProyectado WHEN 0.0 THEN 0.0
						ELSE CAST(SUM(RD.CantidadServida) / l.Cabezas AS DECIMAL) / R.PesoProyectado * 100
						END
                  END, Fecha
   FROM     RepartoDetalle RD
   INNER JOIN Reparto R ON R.RepartoID = RD.RepartoID
   INNER JOIN Organizacion O ON O.OrganizacionID = R.OrganizacionID
   INNER JOIN Lote L ON L.LoteID = R.LoteID
   INNER JOIN Corral C ON C.CorralID = L.CorralID
   INNER JOIN Formula F ON F.FormulaID = RD.FormulaIDServida
   WHERE    O.OrganizacionID = @OrganizacionID AND CAST(R.Fecha AS DATE) =  CAST(@Fecha as DATE)
            AND r.Activo = 1 AND RD.Activo = 1 AND l.Activo = 1 AND c.Activo = 1 AND c.Activo = 1 AND f.Activo = 1
						AND c.Codigo <> 'ZZZ'
   GROUP BY Codigo, L.Cabezas, PesoProyectado, DiasEngorda,
            RD.FormulaIDServida, F.Descripcion, Fecha
   ORDER BY C.Codigo

GO
