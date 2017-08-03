USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerProgramacionTransportePorOrganizacionID]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerProgramacionTransportePorOrganizacionID]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerProgramacionTransportePorOrganizacionID]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sandoval Toledo Jesús Alejandro 
-- Create date: 16-06-2017
-- Description: Obtiene los embarques de acuerdo a una organización para mostrar en la pestaña de transporte.
-- SpName     : ProgramacionEmbarque_ObtenerProgramacionTransportePorOrganizacionID 1, 15105
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerProgramacionTransportePorOrganizacionID]
@OrganizacionID 	INT,
@EmbarqueID 		INT = 0
AS
BEGIN
	CREATE TABLE #Transporte (
				EmbarqueID 			INT,
				FolioEmbarque 		INT,
				Origen				INT,
				Destino				INT,
				ProveedorID			INT,
				CodigoSAP			VARCHAR(10),
				Proveedor 			VARCHAR(100),
				Correo				VARCHAR(50),
				RutaID 				INT,
				DescripcionRuta 	VARCHAR(100),
				Kilometros 			DECIMAL(10, 2),
				CitaDescarga 		DATETIME,
				CitaCarga			DATETIME,
				Flete 				DECIMAL(10, 2),
				GastoFijo 			DECIMAL(10, 2),
				GastoVariable 		DECIMAL(10, 2),
				Demora 				DECIMAL(10, 2),
				Observaciones		TEXT,
				DobleTransportista	BIT
			)
	
	INSERT INTO #Transporte (EmbarqueID, FolioEmbarque, Origen, Destino, ProveedorID, CodigoSAP, Proveedor, Correo, RutaID,
							DescripcionRuta, Kilometros, CitaDescarga, CitaCarga, DobleTransportista)
	SELECT DISTINCT	emb.EmbarqueID, emb.FolioEmbarque, emb.OrganizacionOrigenID, emb.OrganizacionDestinoID, emb.ProveedorID, p.CodigoSAP,
					p.Descripcion, p.CorreoElectronico, emb.RutaID, ced.Descripcion, ced.Kilometros, emb.CitaDescarga, emb.CitaCarga, emb.DobleTransportista
	FROM Embarque emb
	INNER JOIN Organizacion org (NOLOCK) ON (emb.OrganizacionID = org.OrganizacionID)
	INNER JOIN Organizacion oo (NOLOCK) ON (emb.OrganizacionOrigenID = oo.OrganizacionID)
	LEFT JOIN Proveedor p (NOLOCK) ON (emb.ProveedorID = p.ProveedorID)
	LEFT JOIN ConfiguracionEmbarqueDetalle ced (NOLOCK) ON (emb.RutaID = ced.ConfiguracionEmbarqueDetalleID)
	WHERE emb.OrganizacionID = @OrganizacionID  
	AND (emb.EmbarqueID = @EmbarqueID OR @EmbarqueID = 0)
	AND Estatus = 1 
	AND emb.Activo = 1
	AND org.Activo = 1
	
	UPDATE #Transporte 
	SET Observaciones = (SELECT DISTINCT 
							 SUBSTRING(
										(
												SELECT 	Nombre + ' ' + CONVERT(VARCHAR, FechaCreacion, 120) + '\n' 
														+ embObs.Observacion + '\n' AS [text()]
												FROM EmbarqueObservaciones embObs
												INNER JOIN #Transporte d ON (embObs.EmbarqueID = d.EmbarqueID)
												INNER JOIN Usuario usr ON (usr.UsuarioID = embObs.UsuarioCreacionID)
												WHERE embObs.EmbarqueID = emb.EmbarqueID
												AND embObs.Activo = 1
												ORDER BY embObs.EmbarqueID
												For XML PATH ('')
										), 0, 10000) [Observaciones]
							FROM Embarque emb WHERE emb.EmbarqueID = #Transporte.EmbarqueID)
	
	UPDATE 	t
	SET 	GastoFijo = listaGastosFijos.Importe
	FROM #Transporte t
	INNER JOIN (SELECT t.EmbarqueID, SUM(egf.Importe) AS Importe
							FROM #Transporte t
							INNER JOIN Embarque emb ON (emb.EmbarqueID = t.EmbarqueID)
							INNER JOIN EmbarqueGastoFijo egf ON (egf.EmbarqueID = emb.EmbarqueID)
							WHERE egf.Activo = 1 AND emb.Activo = 1
							GROUP BY t.EmbarqueID
							) AS listaGastosFijos ON (listaGastosFijos.EmbarqueID = t.EmbarqueID)
	
	UPDATE 	#Transporte
	SET 		Flete = ec.Importe
	FROM 		EmbarqueCosto ec INNER JOIN #Transporte t ON (ec.EmbarqueID = t.EmbarqueID)
	WHERE 	ec.CostoID = 4 AND ec. Activo = 1

	UPDATE 	#Transporte
	SET 		GastoVariable = ec.Importe
	FROM 		EmbarqueCosto ec INNER JOIN #Transporte t ON (ec.EmbarqueID = t.EmbarqueID)
	WHERE 	ec.CostoID = 8 AND ec. Activo = 1

	UPDATE 	#Transporte
	SET 		Demora = ec.Importe
	FROM 		EmbarqueCosto ec INNER JOIN #Transporte t ON (ec.EmbarqueID = t.EmbarqueID)
	WHERE 	ec.CostoID = 32 AND ec. Activo = 1
	
	SELECT 	EmbarqueID,	FolioEmbarque, Origen, Destino, ProveedorID, CodigoSAP, Proveedor, Correo, RutaID, DescripcionRuta, Kilometros,
			CitaDescarga, CitaCarga, Flete, GastoFijo, GastoVariable, Demora, Observaciones,
					CASE WHEN Proveedor IS NULL AND (RutaID = 0 OR RutaID IS NULL) THEN CAST (1 AS BIT)
						ELSE CAST (0 AS BIT)
					END Pendiente, DobleTransportista, 
					CASE WHEN EXISTS (SELECT EmbarqueID FROM EmbarqueOperador embOP WHERE EmbarqueID = #Transporte.EmbarqueID AND embOP.Activo = 1)
						THEN CAST(1 AS BIT)
						ELSE CAST(0 AS BIT) END DatosCapturados 
	FROM #Transporte;
	
	DROP TABLE #Transporte;
END