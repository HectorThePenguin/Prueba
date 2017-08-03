USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerProgramacionPorOrganizacionID]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerProgramacionPorOrganizacionID]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerProgramacionPorOrganizacionID]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sandoval Toledo Jesús Alejandro 
-- Create date: 31-05-2017
-- Description: Procedimiento almacenado que obtiene los datos para la pestaña "Programacion" en la pantalla programación embarque
-- SpName     : ProgramacionEmbarque_ObtenerProgramacionPorOrganizacionID 1
--======================================================  
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerProgramacionPorOrganizacionID]
@OrganizacionID BIGINT
AS
BEGIN
	CREATE TABLE #Programacion (
	OrganizacionID 			INT,
	Estatus 				VARCHAR(100),
	FolioEmbarque			INT,
	OrganizacionOrigenID	INT,
	OrganizacionOrigen		VARCHAR(50),
	OrganizacionDestinoID	INT,
	OrganizacionDestino		VARCHAR(50),
	ResponsableEmbarque		VARCHAR(255),
	TipoEmbarque			VARCHAR(50),
	CitaCarga				SMALLDATETIME,
	EmbarqueID				INT,
	CitaDescarga			SMALLDATETIME,
	HorasTransito			INT,
	TipoEmbarqueID			INT,
	Observaciones			TEXT
	)
	INSERT INTO #Programacion (OrganizacionID, Estatus, FolioEmbarque, OrganizacionOrigenID, OrganizacionOrigen, OrganizacionDestinoID,
														OrganizacionDestino, ResponsableEmbarque, TipoEmbarque, CitaCarga, EmbarqueID, CitaDescarga, HorasTransito,
														TipoEmbarqueID)
	SELECT org.OrganizacionID, est.Descripcion AS Estatus, emb.FolioEmbarque, emb.OrganizacionOrigenID, oo.Descripcion AS OrganizacionOrigen,
				emb.OrganizacionDestinoID, od.Descripcion AS OrganizacionDestino, ResponsableEmbarque, tiemb.Descripcion AS TipoEmbarque, 
				CitaCarga, emb.EmbarqueID, emb.CitaDescarga, emb.HorasTransito, emb.TipoEmbarqueID
	FROM Embarque emb (NOLOCK)
	INNER JOIN Organizacion org (NOLOCK) ON (emb.OrganizacionID = org.OrganizacionID)
	INNER JOIN Organizacion oo (NOLOCK) ON (emb.OrganizacionOrigenID = oo.OrganizacionID)
	INNER JOIN Organizacion od (NOLOCK) ON (emb.OrganizacionDestinoID = od.OrganizacionID)
	INNER JOIN Estatus est (NOLOCK) ON (emb.Estatus = est.EstatusID)
	INNER JOIN TipoEmbarque tiemb (NOLOCK) ON (emb.TipoEmbarqueID = tiemb.TipoEmbarqueID)
	WHERE emb.OrganizacionID = @OrganizacionID AND emb.Estatus = 1 AND emb.Activo = 1;
	
	UPDATE #Programacion 
	SET Observaciones = (SELECT DISTINCT 
							 SUBSTRING(
										(
												SELECT 	Nombre + ' ' + CONVERT(VARCHAR, FechaCreacion, 120) + '\n' 
														+ embObs.Observacion + '\n' AS [text()]
												FROM EmbarqueObservaciones embObs
												INNER JOIN #Programacion p ON (embObs.EmbarqueID = p.EmbarqueID)
												INNER JOIN Usuario usr ON (usr.UsuarioID = embObs.UsuarioCreacionID)
												WHERE embObs.EmbarqueID = emb.EmbarqueID
												AND embObs.Activo = 1
												ORDER BY embObs.EmbarqueID
												For XML PATH ('')
										), 0, 10000) [Observaciones]
							FROM Embarque emb WHERE emb.EmbarqueID = #Programacion.EmbarqueID)

	SELECT 	OrganizacionID, Estatus, FolioEmbarque, OrganizacionOrigenID, OrganizacionOrigen,
					OrganizacionDestinoID, OrganizacionDestino, ResponsableEmbarque, TipoEmbarque,
					CitaCarga, EmbarqueID, CitaDescarga, HorasTransito, TipoEmbarqueID, Observaciones,
					CASE WHEN EXISTS (SELECT EmbarqueID FROM EmbarqueOperador embOP WHERE EmbarqueID = #Programacion.EmbarqueID AND embOP.Activo = 1)
						THEN CAST(1 AS BIT)
						ELSE CAST(0 AS BIT) END DatosCapturados
	FROM #Programacion;
	
	DROP TABLE #Programacion
END