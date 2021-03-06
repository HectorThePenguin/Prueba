USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerGanadoEnfermoPorCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Enfermeria_ObtenerGanadoEnfermoPorCorral]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerGanadoEnfermoPorCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Roque Solis
-- Create date: 11/01/2014
-- Origen: APInterfaces
-- Description:	Obtiene las detecciones de animales enfermos.
-- execute Enfermeria_ObtenerGanadoEnfermoPorCorral 4, 1, 1;
-- =============================================
CREATE PROCEDURE [dbo].[Enfermeria_ObtenerGanadoEnfermoPorCorral]
	@OrganizacionID INT,
	@CorralID INT,
	@TipoRecepcion INT
AS
BEGIN	
SET NOCOUNT ON;	
	SELECT 
		dt.DeteccionID,
		cr.CorralID, 
		cr.Codigo, 
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
		ELSE 0 END AS FolioEntrada,
		   /*)ELSE
				(SELECT TOP 1 CAST( COALESCE(AN.FolioEntrada, 0) AS INT)
				   FROM Animal AN
				  WHERE AN.Arete= dt.Arete 
					AND AN.Activo= 1)
			END) AS FolioEntrada,*/
		dt.Arete,
		dt.AreteMetalico,
		dt.FechaDeteccion,
		dt.FotoDeteccion,  
		op.OperadorID,    
		dt.GradoID,         
		gr.Descripcion [DescripcionGrado], 
		(op.Nombre +' ' + op.ApellidoPaterno + ' ' + op.ApellidoMaterno) [NombreDetector], 
		gr.NivelGravedad,
		dt.DescripcionGanadoID,
		dg.Descripcion [DescripcionGanado],
		CAST(dt.FechaDeteccion AS DATE) [FechaDeteccion]
	  FROM Deteccion dt
	 INNER JOIN TipoDeteccion td ON dt.TipoDeteccionID = td.TipoDeteccionID AND td.Activo = 1
	 INNER JOIN Operador op ON dt.OperadorID = op.OperadorID AND op.Activo = 1
	 INNER JOIN Grado gr ON dt.GradoID = gr.GradoID AND gr.Activo = 1
	 INNER JOIN Lote l ON l.LoteID = dt.LoteID AND l.Activo= 1
	 INNER JOIN Corral cr ON l.CorralID = cr.CorralID AND cr.Activo= 1 
	 INNER JOIN TipoCorral tp ON tp.TipoCorralID = l.TipoCorralID
	 INNER JOIN DescripcionGanado dg ON dg.DescripcionGanadoID = dt.DescripcionGanadoID
	 WHERE op.OrganizacionID = @OrganizacionID
	   AND (dt.Arete IS NULL OR dt.Arete ='')
	   AND cr.CorralID = @CorralID
	   AND dt.Activo = 1
SET NOCOUNT OFF;	
END

GO
