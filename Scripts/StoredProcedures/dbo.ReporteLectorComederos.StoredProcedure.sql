IF EXISTS(SELECT ''
FROM sys.objects
WHERE [object_id] = Object_id(N'[dbo].[ReporteLectorComederos]'))
	DROP PROCEDURE [dbo].[ReporteLectorComederos]; 
GO

--======================================================
-- Author     : Luis David Mendoza Romero
-- Create date: 14/10/2015 09:50:00 a.m.
-- Description: 
-- SpName     : ReporteLectorComederos
--======================================================
CREATE PROCEDURE [dbo].[ReporteLectorComederos]
@OrganizacionID int,
@horario int,
@FechaHoy Date
AS
BEGIN
	SET NOCOUNT ON;
	IF (@horario = 0)
  BEGIN 
SELECT OrganizacionID, Fecha ,   
          ISNULL(CAST(CAST( ROUND(CAST( Cantidad_Mañana + Cantidad_Tarde - (Cantidad_Mañana_Ayer + Cantidad_Tarde_Ayer)AS decimal ) / NULLIF((Cantidad_Mañana_Ayer + Cantidad_Tarde_Ayer),0) * 100,0) AS int) AS varchar), '*')AS Diferencia ,   
          Codigo ,   
          EstadoComedero ,   
          ISNULL( fmh.Descripcion , '' )AS Formula_Mañana ,   
          Cantidad_Mañana ,   
          ISNULL( fth.Descripcion , '' )AS Formula_Tarde ,   
          Cantidad_Tarde ,   
          Cantidad_Mañana + Cantidad_Tarde AS Total_Hoy ,   
          EstadoComedero_Ayer ,   
          ISNULL( fma.Descripcion , '' )AS Formula_Mañana_Ayer ,   
          Cantidad_Mañana_Ayer ,   
          ISNULL( fta.Descripcion , '' )AS Formula_Tarde_Ayer ,   
          Cantidad_Tarde_Ayer ,   
          Cantidad_Mañana_Ayer + Cantidad_Tarde_Ayer AS Total_Ayer  
   FROM(   
         SELECT r.OrganizacionID, r.Fecha ,   
                '[' + RIGHT( '000' + RTRIM( LTRIM( c.Codigo )) + ']' , 4 )AS Codigo ,   
                ISNULL( COALESCE( rd2.EstadoComederoID , rd1.EstadoComederoID ) , 0 )EstadoComedero ,   
                ISNULL( CASE  
                        WHEN rd1.servido = 1 THEN rd1.FormulaIDServida  
                            ELSE rd1.FormulaIDProgramada  
                        END , '' )AS Formula_Mañana ,   
                ISNULL( CASE  
                        WHEN rd1.servido = 1 THEN rd1.CantidadServida  
                            ELSE rd1.CantidadProgramada  
                        END , 0 )AS Cantidad_Mañana ,   
                ISNULL( CASE  
                        WHEN rd2.servido = 1 THEN rd2.FormulaIDServida  
                            ELSE rd2.FormulaIDProgramada  
                        END , '' )AS Formula_Tarde ,   
                ISNULL( CASE  
                        WHEN rd2.servido = 1 THEN rd2.CantidadServida  
                            ELSE rd2.CantidadProgramada  
                        END , 0 )AS Cantidad_Tarde ,   
                ISNULL( COALESCE( ray.EstadoComederoAyerTarde , ray.EstadoComederoAyerManana ) , 0 )EstadoComedero_Ayer ,   
                ISNULL( CASE  
                        WHEN ray.ServidoMañana = 1 THEN ray.FormulaServidoAyerManana  
                            ELSE ray.FormulaProgramadaAyerManana  
                        END , '' )AS Formula_Mañana_Ayer ,   
                ISNULL( CASE  
                        WHEN ray.ServidoMañana = 1 THEN ray.CantidadServidoAyerManana  
                            ELSE ray.CantidadProgramadaAyerManana  
                        END , 0 )AS Cantidad_Mañana_Ayer ,   
                ISNULL( CASE  
                        WHEN ray.ServidoTarde = 1 THEN ray.FormulaServidoAyerTarde  
                            ELSE ray.FormulaProgramadaAyerTarde  
                        END , '' )AS Formula_Tarde_Ayer ,   
                ISNULL( CASE  
                        WHEN ray.ServidoTarde = 1 THEN ray.CantidadServidoAyerTarde  
                            ELSE ray.CantidadProgramadaAyerTarde  
                        END , 0 )AS Cantidad_Tarde_Ayer  
         FROM reparto r  
               INNER JOIN corral c  
                   ON c.corralid = r.corralid  
               LEFT JOIN RepartoDetalle rd1  
                   ON r.RepartoID = rd1.RepartoID AND rd1.TipoServicioID = 1  
               LEFT JOIN RepartoDetalle rd2  
                   ON r.RepartoID = rd2.RepartoID AND rd2.TipoServicioID = 2  
            LEFT JOIN (  
            SELECT ra.CorralID, ra.Fecha, rad2.EstadoComederoID AS EstadoComederoAyerTarde, rad1.EstadoComederoID AS EstadoComederoAyerManana,   
                  rad2.FormulaIDProgramada AS FormulaProgramadaAyerTarde, rad1.FormulaIDProgramada AS FormulaProgramadaAyerManana,   
                  rad2.FormulaIDServida AS FormulaServidoAyerTarde, rad1.FormulaIDServida AS FormulaServidoAyerManana,   
                  rad2.CantidadProgramada AS CantidadProgramadaAyerTarde, rad1.CantidadProgramada AS CantidadProgramadaAyerManana,  
                  rad2.CantidadServida AS CantidadServidoAyerTarde, rad1.CantidadServida AS CantidadServidoAyerManana,  
                  rad2.Servido AS ServidoTarde, rad1.Servido AS ServidoMañana  
            FROM reparto ra  
               INNER JOIN RepartoDetalle rad1  
                   ON ra.RepartoID = rad1.RepartoID AND rad1.TipoServicioID = 1   
               INNER JOIN RepartoDetalle rad2  
                   ON ra.RepartoID = rad2.RepartoID AND rad2.TipoServicioID = 2  
						WHERE CONVERT( varchar , ra.fecha , 112 ) = DATEADD( day , -1 , CONVERT( varchar , @FechaHoy , 112 ))
                ) ray ON ray.corralid = r.corralid  
        WHERE CONVERT( varchar , r.fecha , 112 ) = CONVERT( varchar , @FechaHoy, 112) 
    AND c.Activo = 1  AND r.OrganizacionID = @OrganizacionID
    )Rep  
        LEFT JOIN Formula fmh  
            ON rep.Formula_Mañana = fmh.FormulaID  
        LEFT JOIN Formula fth  
            ON rep.Formula_Tarde = fth.FormulaID  
        LEFT JOIN Formula fma  
            ON rep.Formula_Mañana_Ayer= fma.FormulaID  
        LEFT JOIN Formula fta  
            ON rep.Formula_Tarde_Ayer = fta.FormulaID
		ORDER BY Codigo
  END 
ELSE IF (@horario = 1)
  BEGIN 
		SELECT OrganizacionID, Fecha , 
          ISNULL(CAST(CAST( ROUND(CAST( Cantidad_Mañana + Cantidad_Tarde - (Cantidad_Mañana_Ayer + Cantidad_Tarde_Ayer)AS decimal ) / NULLIF((Cantidad_Mañana_Ayer + Cantidad_Tarde_Ayer),0) * 100,0) AS int) AS varchar), '*')AS Diferencia , 
          Codigo , 
          EstadoComedero , 
          ISNULL( fmh.Descripcion , '' )AS Formula_Mañana , 
          Cantidad_Mañana , 
          ISNULL( fth.Descripcion , '' )AS Formula_Tarde , 
          Cantidad_Tarde , 
          Cantidad_Mañana + Cantidad_Tarde AS Total_Hoy , 
          EstadoComedero_Ayer , 
          ISNULL( fma.Descripcion , '' )AS Formula_Mañana_Ayer , 
          Cantidad_Mañana_Ayer , 
          ISNULL( fta.Descripcion , '' )AS Formula_Tarde_Ayer , 
          Cantidad_Tarde_Ayer , 
          Cantidad_Mañana_Ayer + Cantidad_Tarde_Ayer AS Total_Ayer
   FROM( 
         SELECT r.OrganizacionID, r.Fecha , 
                '[' + RIGHT( '000' + RTRIM( LTRIM( c.Codigo )) + ']' , 4 )AS Codigo , 
                ISNULL( COALESCE( rd2.EstadoComederoID , rd1.EstadoComederoID ) , 0 )EstadoComedero , 
                ISNULL( CASE
                        WHEN rd1.servido = 1 THEN rd1.FormulaIDServida
                            ELSE rd1.FormulaIDProgramada
                        END , '' )AS Formula_Mañana , 
                ISNULL( CASE
                        WHEN rd1.servido = 1 THEN rd1.CantidadServida
                            ELSE rd1.CantidadProgramada
                        END , 0 )AS Cantidad_Mañana , 
                ISNULL( CASE
                        WHEN rd2.servido = 1 THEN rd2.FormulaIDServida
                            ELSE rd2.FormulaIDProgramada
                        END , '' )AS Formula_Tarde , 
                ISNULL( CASE
                        WHEN rd2.servido = 1 THEN rd2.CantidadServida
                            ELSE rd2.CantidadProgramada
                        END , 0 )AS Cantidad_Tarde , 
                ISNULL( COALESCE( ray.EstadoComederoAyerTarde , ray.EstadoComederoAyerManana ) , 0 )EstadoComedero_Ayer , 
                ISNULL( CASE
                        WHEN ray.ServidoMañana = 1 THEN ray.FormulaServidoAyerManana
                            ELSE ray.FormulaProgramadaAyerManana
                        END , '' )AS Formula_Mañana_Ayer , 
                ISNULL( CASE
                        WHEN ray.ServidoMañana = 1 THEN ray.CantidadServidoAyerManana
                            ELSE ray.CantidadProgramadaAyerManana
                        END , 0 )AS Cantidad_Mañana_Ayer , 
                ISNULL( CASE
                        WHEN ray.ServidoTarde = 1 THEN ray.FormulaServidoAyerTarde
                            ELSE ray.FormulaProgramadaAyerTarde
                        END , '' )AS Formula_Tarde_Ayer , 
                ISNULL( CASE
                        WHEN ray.ServidoTarde = 1 THEN ray.CantidadServidoAyerTarde
                            ELSE ray.CantidadProgramadaAyerTarde
                        END , 0 )AS Cantidad_Tarde_Ayer
         FROM reparto r
               INNER JOIN corral c
                   ON c.corralid = r.corralid
               LEFT JOIN RepartoDetalle rd1
                   ON r.RepartoID = rd1.RepartoID AND rd1.TipoServicioID = 1
               LEFT JOIN RepartoDetalle rd2
                   ON r.RepartoID = rd2.RepartoID AND rd2.TipoServicioID = 2
			LEFT JOIN (
			SELECT ra.CorralID, ra.Fecha, rad2.EstadoComederoID AS EstadoComederoAyerTarde, rad1.EstadoComederoID AS EstadoComederoAyerManana, 
				  rad2.FormulaIDProgramada AS FormulaProgramadaAyerTarde, rad1.FormulaIDProgramada AS FormulaProgramadaAyerManana, 
				  rad2.FormulaIDServida AS FormulaServidoAyerTarde, rad1.FormulaIDServida AS FormulaServidoAyerManana, 
				  rad2.CantidadProgramada AS CantidadProgramadaAyerTarde, rad1.CantidadProgramada AS CantidadProgramadaAyerManana,
				  rad2.CantidadServida AS CantidadServidoAyerTarde, rad1.CantidadServida AS CantidadServidoAyerManana,
				  rad2.Servido AS ServidoTarde, rad1.Servido AS ServidoMañana
			FROM reparto ra
               INNER JOIN RepartoDetalle rad1
                   ON ra.RepartoID = rad1.RepartoID AND rad1.TipoServicioID = 1 AND (rad1.CantidadProgramada > 0 OR rad1.CantidadServida > 0)
               INNER JOIN RepartoDetalle rad2
                   ON ra.RepartoID = rad2.RepartoID AND rad2.TipoServicioID = 2 AND (rad2.CantidadProgramada > 0 OR rad2.CantidadServida > 0)
			WHERE CONVERT( varchar , ra.fecha , 112 ) = DATEADD( day , -1 , CONVERT( varchar , @FechaHoy , 112 ))
			    ) ray ON ray.corralid = r.corralid
        WHERE CONVERT( varchar , r.fecha , 112 ) = CONVERT( varchar , DATEADD( day , +1 , CONVERT( varchar , @FechaHoy , 112 )), 112 ) AND rd1.CantidadProgramada > 0 AND r.OrganizacionID = @OrganizacionID
	   AND c.Activo = 1
	   )Rep
        LEFT JOIN Formula fmh
            ON rep.Formula_Mañana = fmh.FormulaID
        LEFT JOIN Formula fth
            ON rep.Formula_Tarde = fth.FormulaID
        LEFT JOIN Formula fma
            ON rep.Formula_Mañana_Ayer= fma.FormulaID
        LEFT JOIN Formula fta
            ON rep.Formula_Tarde_Ayer = fta.FormulaID
		ORDER BY Codigo
	END 
		SET NOCOUNT OFF;
END

GO

