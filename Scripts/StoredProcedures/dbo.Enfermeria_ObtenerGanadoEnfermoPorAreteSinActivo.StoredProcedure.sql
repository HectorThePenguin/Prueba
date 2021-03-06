USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerGanadoEnfermoPorAreteSinActivo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Enfermeria_ObtenerGanadoEnfermoPorAreteSinActivo]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerGanadoEnfermoPorAreteSinActivo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Roque Solis
-- Create date: 13/01/2014
-- Origen: APInterfaces
-- Description:	Obtiene las detecciones de animales enfermos.
-- execute Enfermeria_ObtenerGanadoEnfermoPorAreteSinActivo '1816457',null, 4, 1;
-- =============================================
CREATE PROCEDURE [dbo].[Enfermeria_ObtenerGanadoEnfermoPorAreteSinActivo]
	@Arete VARCHAR(15),
	@AreteTestigo VARCHAR(15),
	@OrganizacionID INT,
	@TipoRecepcion INT
AS

BEGIN	

IF @AreteTestigo= ''
	BEGIN
		SET @AreteTestigo = NULL;
	END

SET NOCOUNT ON;	
SELECT 
        dt.DeteccionID,
		cr.CorralID, 
		cr.Codigo, 
		(CASE WHEN l.TipoCorralID = @TipoRecepcion 
		THEN 
			(SELECT TOP 1 CAST( COALESCE(eg.FolioEntrada,0) AS INT) 
			FROM EntradaGanado eg 
			WHERE eg.CorralID= cr.CorralID 
			AND eg.LoteID= l.LoteID 
			AND eg.Activo= 1 )
		ELSE
			(SELECT TOP 1 CAST( COALESCE(AN.FolioEntrada, 0) AS INT)
			FROM Animal AN(NOLOCK)
			WHERE AN.Arete= dt.Arete 
			AND AN.Activo= 1)
		END) AS FolioEntrada,
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
		l.TipoCorralID, 
		CAST(dt.FechaDeteccion AS DATE) [FechaDeteccion]
		FROM Deteccion dt
		INNER JOIN TipoDeteccion td ON dt.TipoDeteccionID = td.TipoDeteccionID AND td.Activo = 1
		INNER JOIN Operador op ON dt.OperadorID = op.OperadorID AND op.Activo = 1
		INNER JOIN Grado gr ON dt.GradoID = gr.GradoID AND gr.Activo = 1
		INNER JOIN Lote l ON l.LoteID = dt.LoteID
		INNER JOIN Corral cr ON l.CorralID = cr.CorralID
		INNER JOIN DescripcionGanado dg ON dg.DescripcionGanadoID = dt.DescripcionGanadoID
		WHERE op.OrganizacionID = @OrganizacionID
		AND   ( dt.Arete = COALESCE(@Arete, '') OR dt.AreteMetalico = COALESCE(@AreteTestigo, 'x'))

SET NOCOUNT OFF;	

END
GO
