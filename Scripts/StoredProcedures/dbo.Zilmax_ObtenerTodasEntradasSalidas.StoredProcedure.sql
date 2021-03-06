USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Zilmax_ObtenerTodasEntradasSalidas]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Zilmax_ObtenerTodasEntradasSalidas]
GO
/****** Object:  StoredProcedure [dbo].[Zilmax_ObtenerTodasEntradasSalidas]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Francisco Alfonso Mendez Padilla
-- Create date: 15/10/2013
-- Description:  E/S Zilmax
-- Zilmax_ObtenerTodasEntradasSalidas 1, 4, 2, 6, 8
-- =============================================
CREATE PROCEDURE [dbo].[Zilmax_ObtenerTodasEntradasSalidas]
@OrganizacionID INT,
@TipoFormulaID_Fin INT,
@GrupoCorralID INT,
@CalculoID INT,
@TipoFormulaID_Ret INT
AS
  BEGIN
      SET NOCOUNT ON;
	  DECLARE @FormulaEZ AS VARCHAR(MAX)
	  DECLARE @FormulaSZ AS VARCHAR(50)
	  DECLARE @Fecha AS VARCHAR(12)
	  SET @FormulaEZ=''
	  SET @FormulaEZ= (SELECT FO.Descripcion+', ' 
					  FROM TipoFormula TF (NOLOCK) 
					  INNER JOIN Formula FO (NOLOCK) ON TF.TipoFormulaID=FO.TipoFormulaID AND FO.Activo=1 
					  Where TF.TipoFormulaID=@TipoFormulaID_Fin AND TF.Activo=1 
					  FOR XML PATH(''))
	  SET @FormulaEZ = LEFT(@FormulaEZ,LEN(@FormulaEZ) - 1)
	  SET @FormulaSZ=(Select FO.Descripcion + ', ' FROM TipoFormula TF (NOLOCK) 
	                  INNER JOIN Formula FO (NOLOCK) ON TF.TipoFormulaID=FO.TipoFormulaID AND FO.Activo=1 
					  Where TF.TipoFormulaID=@TipoFormulaID_Ret AND TF.Activo=1
					  FOR XML PATH(''))
	  SET @FormulaSZ = LEFT(@FormulaSZ,LEN(@FormulaSZ) - 1)
	  SET @Fecha=CONVERT(varchar(12),GETDATE(),112)
	  SELECT  CO.Codigo AS Corral,
			  'ya cumplen con la fecha entrada a zilmax' AS Estatus,
			  'entrada' AS Tipo,
			  @FormulaEZ AS Formula,
			  LO.Cabezas As Cbz	 
			  , LO.LoteID
	  FROM LoteProyeccion LP(NOLOCK)
	  INNER JOIN Lote LO (NOLOCK) ON LP.LoteID=LO.LoteID AND LO.Activo=1 AND
	                                 LO.FechaDisponibilidad IS NOT NULL AND 
									 CAST(LO.FechaDisponibilidad AS DATE) <= CAST(@Fecha AS DATE) AND
									 LO.DisponibilidadManual=1
									 AND LO.FechaEntradaZilmax IS NULL
	  INNER JOIN TipoCorral  TC (NOLOCK) ON LO.TipoCorralID=TC.TipoCorralID   AND TC.Activo=1
	  INNER JOIN GrupoCorral GC (NOLOCK) ON TC.GrupoCorralID=GC.GrupoCorralID AND GC.Activo=1 
	                                 AND GC.GrupoCorralID=@GrupoCorralID
	  INNER JOIN Corral CO (NOLOCK) ON LO.CorralID=CO.CorralID  AND CO.Activo=1
	  WHERE LP.OrganizacionID=@OrganizacionID AND 
	  CAST(DATEADD(day,(CAST((SELECT Valor FROM Calculo CA (NOLOCK) 
	                      INNER JOIN Constantes CO (NOLOCK) ON CA.CalculoID=CO.CalculoID AND CO.Codigo=1 
						  WHERE CA.CalculoID=@CalculoID AND CO.Sexo=DBO.ObtenerSexoMayoritarioLote(LO.LoteID) 
						  AND CO.OrganizacionID=@OrganizacionID) AS INT))*-1,LP.FechaEntradaZilmax) AS DATE)= CAST(DATEADD(day,1,@Fecha) AS DATE)
	  UNION ALL
	  SELECT  CO.Codigo AS Corral,
			  'ya debe entrar a zilmax' AS Estatus,
			  'entrada' AS Tipo,
			  @FormulaEZ AS Formula,
			  LO.Cabezas As Cbz	 	 
			  , LO.LoteID
	  FROM LoteProyeccion LP(NOLOCK)
	  INNER JOIN Lote LO (NOLOCK) ON LP.LoteID=LO.LoteID AND LO.Activo=1 AND 
	                                 LO.FechaDisponibilidad IS NOT NULL AND 
									 CAST(LO.FechaDisponibilidad AS DATE) <= CAST(@Fecha AS DATE) AND 
									 LO.DisponibilidadManual=1
									 AND LO.FechaEntradaZilmax IS NULL
	  INNER JOIN TipoCorral  TC (NOLOCK) ON LO.TipoCorralID=TC.TipoCorralID   AND TC.Activo=1
	  INNER JOIN GrupoCorral GC (NOLOCK) ON TC.GrupoCorralID=GC.GrupoCorralID AND GC.Activo=1 
	                                 AND GC.GrupoCorralID=@GrupoCorralID
	  INNER JOIN Corral CO (NOLOCK) ON LO.CorralID=CO.CorralID  AND CO.Activo=1
	  LEFT OUTER JOIN
	  (
	     Select RE.OrganizacionID,RE.LoteID From  Reparto RE
		 INNER JOIN RepartoDetalle RED (NOLOCK) ON RE.RepartoID=RED.RepartoID  
		 AND RED.FormulaIDServida IN (Select FO.FormulaID FROM TipoFormula TF (NOLOCK) 
		 INNER JOIN Formula FO (NOLOCK) ON TF.TipoFormulaID=FO.TipoFormulaID AND FO.Activo=1
		 Where TF.TipoFormulaID=@TipoFormulaID_Fin AND TF.Activo=1)
		 GROUP BY RE.OrganizacionID,RE.LoteID
	   ) AS TT ON LP.LoteID=TT.LoteID AND LP.OrganizacionID=TT.OrganizacionID
       WHERE LP.OrganizacionID=@OrganizacionID AND 
	   CAST(DATEADD(day,(CAST((SELECT Valor FROM Calculo CA (NOLOCK) 
	                       INNER JOIN Constantes CO (NOLOCK) ON CA.CalculoID=CO.CalculoID AND CO.Codigo=1 
						   WHERE CA.CalculoID=@CalculoID AND CO.Sexo=DBO.ObtenerSexoMayoritarioLote(LO.LoteID) 
						   AND CO.OrganizacionID=@OrganizacionID) AS INT))*-1,LP.FechaEntradaZilmax) AS DATE) <= CAST(@Fecha AS DATE)   
	   AND TT.OrganizacionID IS NULL
	   UNION ALL
	   SELECT CO.Codigo AS Corral,
       'por salida a zilmax' AS Estatus,
	   'salida'              As Tipo,
	   @FormulaSZ  AS Formula,	   
	   (Select Cabezas FROM Lote Where Lote.LoteID=RE.LoteID) AS Cbz 
	   , RE.LoteID
       FROM Reparto RE (NOLOCK)
       INNER JOIN Lote LO (NOLOCK) ON RE.LoteID=LO.LoteID 
										AND LO.Activo=1 
										AND LO.OrganizacionID=@OrganizacionID
										AND LO.FechaSalidaZilmax IS NULL
	   INNER JOIN TipoCorral TC (NOLOCK)  ON LO.TipoCorralID=TC.TipoCorralID AND TC.Activo=1
	   INNER JOIN GrupoCorral GC (NOLOCK) ON TC.GrupoCorralID=GC.GrupoCorralID  AND GC.Activo=1 
	                                      AND GC.GrupoCorralID=@GrupoCorralID
	   INNER JOIN Corral CO (NOLOCK) ON  LO.CorralID=CO.CorralID
       INNER JOIN
			 (
			    Select RD.RepartoID FROM RepartoDetalle RD (NOLOCK) WHERE RD.FormulaIDServida 
			    IN (Select FormulaID From Formula Where TipoFormulaID=@TipoFormulaID_Fin)
			    GROUP BY RD.RepartoID
			  ) AS SQRD ON RE.RepartoID=SQRD.RepartoID
		WHere CAST(RE.Fecha AS DATE) <= CAST(DATEADD(day,1,@Fecha) AS DATE) AND RE.OrganizacionID=@OrganizacionID
		GROUP BY CO.Codigo,RE.LoteID HAVING COUNT(RE.Fecha)=
		     (SELECT  Valor FROM Calculo CA (NOLOCK) INNER JOIN Constantes CO (NOLOCK) ON CA.CalculoID=CO.CalculoID AND 
			  CO.Codigo=2 WHERE CA.CalculoID=@CalculoID AND CO.Sexo=DBO.ObtenerSexoMayoritarioLote(RE.LoteID)  
			  AND CO.OrganizacionID=@OrganizacionID)			  
		UNION ALL
		SELECT CO.Codigo AS Corral,
        'por salida a zilmax pero no se les a servido formula de retiro' AS Estatus,
	    'salida' As Tipo,
		@FormulaSZ  AS Formula,
		(Select Cabezas From Lote Where Lote.LoteID=RE.LoteID) AS Cbz 
		, RE.LoteID
         FROM Reparto RE (NOLOCK)
         INNER JOIN Lote LO (NOLOCK) ON RE.LoteID=LO.LoteID 
										AND LO.Activo=1 
										AND LO.OrganizacionID=@OrganizacionID
										AND LO.FechaSalidaZilmax IS NULL
		 INNER JOIN TipoCorral TC (NOLOCK)  ON LO.TipoCorralID=TC.TipoCorralID AND TC.Activo=1
		 INNER JOIN GrupoCorral GC (NOLOCK) ON TC.GrupoCorralID=GC.GrupoCorralID  AND GC.Activo=1 
		                                    AND GC.GrupoCorralID=@GrupoCorralID
		 INNER JOIN Corral CO (NOLOCK) ON  LO.CorralID=CO.CorralID
         INNER JOIN
			 (
			   Select RD.RepartoID FROM RepartoDetalle RD (NOLOCK)
				INNER JOIN Formula F ON (RD.FormulaIDServida = F.FormulaID)
				WHERE F.TipoFormulaID = @TipoFormulaID_Fin
			   GROUP BY RD.RepartoID
			  ) AS SQRD ON RE.RepartoID=SQRD.RepartoID
		WHere CAST(RE.Fecha AS DATE) <= CAST(DATEADD(day,1,@Fecha) AS DATE) AND RE.OrganizacionID=@OrganizacionID
		GROUP BY CO.Codigo,RE.LoteID HAVING COUNT(RE.Fecha)>
		     (SELECT Valor FROM Calculo CA (NOLOCK) INNER JOIN Constantes CO (NOLOCK) ON CA.CalculoID=CO.CalculoID AND
			  CO.Codigo=2 WHERE CA.CalculoID=@CalculoID AND CO.Sexo=DBO.ObtenerSexoMayoritarioLote(RE.LoteID)
			  AND CO.OrganizacionID=@OrganizacionID)
      SET NOCOUNT OFF;
  END

GO
