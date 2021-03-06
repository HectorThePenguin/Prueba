USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerCorralesConEnfermosPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Enfermeria_ObtenerCorralesConEnfermosPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerCorralesConEnfermosPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Roque Solis
-- Create date: 11/01/2014
-- Origen: APInterfaces
-- Description:	Obtiene los corrales en los que fueron detectados cabezas enfermas.
-- execute Enfermeria_ObtenerCorralesConEnfermosPorPagina 4,1,15,1;
-- =============================================
CREATE PROCEDURE [dbo].[Enfermeria_ObtenerCorralesConEnfermosPorPagina]
	@OrganizacionID INT,
	@Inicio INT,
	@Limite INT,
    @TipoRecepcion INT	
AS
BEGIN	
SET NOCOUNT ON;	
	SELECT 
		ROW_NUMBER() OVER (ORDER BY Codigo ASC) AS [RowNum],
		COUNT(1) [Cabezas],
		cr.CorralID, 
		cr.Codigo, 
		cr.TipoCorralID,
		tp.GrupoCorralID,
		/* Se comenta este case  por que en el where ya se contempla esta condicion */
		/*(CASE WHEN dt.Arete = '' OR dt.Arete IS NULL
			THEN (*/
		CASE WHEN tp.GrupoCorralID = 1
		THEN (SELECT TOP 1 CAST( COALESCE(eg.FolioEntrada,0) AS INT) 
				FROM EntradaGanado eg 
			   WHERE eg.CorralID = cr.CorralID 
				 AND eg.LoteID = l.LoteID 
				 AND eg.Activo= 1 )		
		ELSE 0 END AS FolioEntrada
		   /*)ELSE
				(SELECT TOP 1 CAST( COALESCE(AN.FolioEntrada, 0) AS INT)
				   FROM Animal AN
				  WHERE AN.Arete= dt.Arete 
					AND AN.Activo= 1)
			END)*/
	  INTO #CorralesEnfermos
	  FROM Deteccion dt
	 INNER JOIN TipoDeteccion td ON dt.TipoDeteccionID = td.TipoDeteccionID AND td.Activo = 1
	 INNER JOIN Operador op ON dt.OperadorID = op.OperadorID AND op.Activo = 1
	 INNER JOIN Grado gr ON dt.GradoID = gr.GradoID AND gr.Activo = 1
	 INNER JOIN Lote l ON l.LoteID = dt.LoteID AND l.Activo= 1
	 INNER JOIN Corral cr ON l.CorralID = cr.CorralID AND cr.Activo= 1 
	 INNER JOIN TipoCorral tp ON tp.TipoCorralID = l.TipoCorralID
	 WHERE op.OrganizacionID = @OrganizacionID
	   AND dt.Activo = 1
	   AND (dt.Arete IS NULL OR dt.Arete ='')
	 GROUP BY cr.CorralID, cr.Codigo, cr.TipoCorralID, dt.Arete,l.TipoCorralID,l.LoteID,tp.GrupoCorralID;
	SELECT
		Cabezas,
		CorralID,
		Codigo,
		TipoCorralID,
		GrupoCorralID,
		FolioEntrada
	FROM #CorralesEnfermos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(1) AS [TotalReg]
	FROM #CorralesEnfermos
	DROP TABLE #CorralesEnfermos
SET NOCOUNT OFF;	
END

GO
