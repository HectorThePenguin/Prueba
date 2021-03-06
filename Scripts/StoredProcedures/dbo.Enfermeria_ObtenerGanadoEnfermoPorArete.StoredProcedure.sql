USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerGanadoEnfermoPorArete]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Enfermeria_ObtenerGanadoEnfermoPorArete]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerGanadoEnfermoPorArete]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Roque Solis
-- Create date: 13/01/2014
-- Origen: APInterfaces
-- Description:	Obtiene las detecciones de animales enfermos.
-- execute Enfermeria_ObtenerGanadoEnfermoPorArete '1816457',null, 4, 1;
-- =============================================
CREATE PROCEDURE [dbo].[Enfermeria_ObtenerGanadoEnfermoPorArete]
	@Arete VARCHAR(15),
	@AreteTestigo VARCHAR(15),
	@OrganizacionID INT,
	@TipoRecepcion INT
AS
BEGIN	
IF @Arete= ''
	BEGIN
		SET @Arete = NULL;
	END
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
			(CASE WHEN COALESCE((SELECT TOP 1 CAST( COALESCE(eg.FolioEntrada,0) AS INT) 
								   FROM InterfaceSalidaAnimal ISA
								  INNER JOIN InterfaceSalida I ON I.SalidaID = ISA.SalidaID AND I.Activo = 1
								   LEFT JOIN EntradaGanado eg ON (eg.FolioOrigen = ISA.SalidaID AND eg.OrganizacionOrigenID = ISA.OrganizacionID AND eg.OrganizacionID = @OrganizacionID)
								  WHERE (ISA.Arete = COALESCE(@Arete, 'x') OR ISA.AreteMetalico = COALESCE(@AreteTestigo, 'x'))
								  ORDER BY eg.FolioEntrada DESC),0) > 0
			THEN 
				(SELECT TOP 1 CAST( COALESCE(eg.FolioEntrada,0) AS INT) 
				   FROM InterfaceSalidaAnimal ISA
				  INNER JOIN InterfaceSalida I ON I.SalidaID = ISA.SalidaID AND I.Activo = 1
				   LEFT JOIN EntradaGanado eg ON (eg.FolioOrigen = ISA.SalidaID AND eg.OrganizacionOrigenID = ISA.OrganizacionID AND eg.OrganizacionID = @OrganizacionID)
				  WHERE (ISA.Arete = COALESCE(@Arete, 'x') OR ISA.AreteMetalico = COALESCE(@AreteTestigo, 'x'))
				  ORDER BY eg.FolioEntrada DESC)
			ELSE
				(SELECT TOP 1 CAST( COALESCE(eg.FolioEntrada,0) AS INT) 
				FROM EntradaGanado eg 
				WHERE eg.CorralID= cr.CorralID 
				AND eg.LoteID= l.LoteID 
				AND eg.Activo= 1 )
			END)
		ELSE
			(SELECT TOP 1 CAST( COALESCE(AN.FolioEntrada, 0) AS INT)
			FROM Animal AN
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
		INNER JOIN Lote l ON l.LoteID = dt.LoteID AND l.Activo= 1
		INNER JOIN Corral cr ON l.CorralID = cr.CorralID AND cr.Activo= 1 
		INNER JOIN DescripcionGanado dg ON dg.DescripcionGanadoID = dt.DescripcionGanadoID
		WHERE op.OrganizacionID = @OrganizacionID
		AND ( dt.Arete = COALESCE(@Arete, 'x') OR dt.AreteMetalico = COALESCE(@AreteTestigo, 'x'))
		AND dt.Activo = 1
SET NOCOUNT OFF;	
END

GO
