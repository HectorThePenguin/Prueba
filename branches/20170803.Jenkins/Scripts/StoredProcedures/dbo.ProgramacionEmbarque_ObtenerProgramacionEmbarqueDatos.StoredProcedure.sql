USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerProgramacionEmbarqueDatos]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerProgramacionEmbarqueDatos]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerProgramacionEmbarqueDatos]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sandoval Toledo Jesús Alejandro 
-- Create date: 16-06-2017
-- Description: Obtiene los embarques de acuerdo a una organización para mostrar en la pestaña de transporte.
-- SpName     : ProgramacionEmbarque_ObtenerProgramacionEmbarqueDatos 1,15249
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerProgramacionEmbarqueDatos]
@OrganizacionID		INT,
@EmbarqueID			INT = 0
AS
BEGIN
	CREATE TABLE #Datos (
				EmbarqueID 				INT,
				FolioEmbarque 			INT,
				DobleTransportista		BIT,
				Operador1ID				INT,	
				Operador1 				VARCHAR(150),
				Operador2ID				INT,
				Operador2				VARCHAR(150),
				CamionID				INT,
				PlacaTracto				VARCHAR(10),
				EcoTracto				VARCHAR(10),
				JaulaID 				INT,
				PlacaJaula				VARCHAR(10),
				EcoJaula				VARCHAR(10),
				Observaciones			TEXT,
				ProveedorID				INT
			)
		
	INSERT INTO #Datos (EmbarqueID, FolioEmbarque, DobleTransportista, CamionID, PlacaTracto, EcoTracto, 
						JaulaID, PlacaJaula, EcoJaula, ProveedorID)
	SELECT DISTINCT	emb.EmbarqueID, emb.FolioEmbarque, emb.DobleTransportista, emb.CamionID, c.PlacaCamion,
			c.NumEconomico AS EcoTracto, emb.JaulaID, j.PlacaJaula, j.NumEconomico AS EcoJaula, emb.ProveedorID
	FROM Embarque emb (NOLOCK)
	INNER JOIN Organizacion org (NOLOCK) ON (emb.OrganizacionID = org.OrganizacionID)
	INNER JOIN Organizacion oo (NOLOCK) ON (emb.OrganizacionOrigenID = oo.OrganizacionID)
	LEFT JOIN Jaula j (NOLOCK) ON (emb.JaulaID = j.JaulaID)
	LEFT JOIN Camion c (NOLOCK) ON (emb.CamionID = c.CamionID)
	WHERE emb.OrganizacionID = @OrganizacionID
	AND emb.Estatus = 1
	AND emb.Activo = 1
	AND org.Activo = 1
	AND (emb.EmbarqueID = @EmbarqueID OR @EmbarqueID = 0);

	UPDATE #Datos 
	SET Observaciones = (SELECT DISTINCT 
							 SUBSTRING(
										(
												SELECT 	Nombre + ' ' + CONVERT(VARCHAR, FechaCreacion, 120) + '\n' 
														+ embObs.Observacion + '\n' AS [text()]
												FROM EmbarqueObservaciones embObs
												INNER JOIN #Datos d ON (embObs.EmbarqueID = d.EmbarqueID)
												INNER JOIN Usuario usr ON (usr.UsuarioID = embObs.UsuarioCreacionID)
												WHERE embObs.EmbarqueID = emb.EmbarqueID
												AND embObs.Activo = 1
												ORDER BY embObs.EmbarqueID
												For XML PATH ('')
										), 0, 10000) [Observaciones]
							FROM Embarque emb WHERE emb.EmbarqueID = #Datos.EmbarqueID)

	UPDATE #Datos
	SET Operador1ID = c.ChoferID,
		Operador1 = c.Nombre + ' ' + c.ApellidoPaterno + ' ' + c.ApellidoMaterno
	FROM EmbarqueOperador embOp
	INNER JOIN #Datos d ON (embOp.EmbarqueID = d.EmbarqueID)
	INNER JOIN ProveedorChofer provCho (NOLOCK) ON (embOp.ProveedorChoferID = provCho.ProveedorChoferID)
	INNER JOIN Chofer c (NOLOCK) ON (provCho.ChoferID = c.ChoferID)
	AND embOp.Activo = 1

	IF EXISTS(SELECT COUNT(1) FROM #Datos WHERE DobleTransportista = 1)
	BEGIN
		UPDATE #Datos
		SET Operador2ID = c.ChoferID,
			Operador2 = c.Nombre + ' ' + c.ApellidoPaterno + ' ' + c.ApellidoMaterno
		FROM EmbarqueOperador embOp
		INNER JOIN #Datos d ON (embOp.EmbarqueID = d.EmbarqueID)
		INNER JOIN ProveedorChofer provCho (NOLOCK) ON (embOp.ProveedorChoferID = provCho.ProveedorChoferID)
		INNER JOIN Chofer c (NOLOCK) ON (provCho.ChoferID = c.ChoferID AND d.Operador1ID <> c.ChoferID)
		AND embOp.Activo = 1
		AND d.DobleTransportista = 1
	END
	
	SELECT 	EmbarqueID, FolioEmbarque, DobleTransportista, Operador1ID, Operador1, Operador2ID, Operador2,
			CamionID, PlacaTracto, EcoTracto, JaulaID, PlacaJaula, EcoJaula, Observaciones, ProveedorID,
			CASE WHEN FolioEmbarque = 0 
				THEN CAST (1 AS BIT)
				ELSE CAST (0 AS BIT)
				END Pendiente
	FROM #Datos;
	
	DROP TABLE #Datos;
END