USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarInventario]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Migracion_GuardarInventario]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarInventario]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    César Valdez Figueroa
-- Create date: 03/02/2015
-- Description: Guarda informacion el inventario de la carga inicial en SIAP
-- Origen: APInterfaces
-- Migracion_GuardarInventario 1
-- =============================================
CREATE PROCEDURE [dbo].[Migracion_GuardarInventario]
	@OrganizacionID INT
AS
  BEGIN
    SET NOCOUNT ON

	/* Creacion de Lotes */
	INSERT INTO Lote(OrganizacionID, CorralID, Lote, TipoCorralID, TipoProcesoID
					, FechaInicio, CabezasInicio, FechaCierre, Cabezas, FechaDisponibilidad
					, DisponibilidadManual, Activo, FechaSalida, FechaCreacion, UsuarioCreacionID
					, FechaModificacion, UsuarioModificacionID)
	SELECT	OrganizacionID, CorralID,	Lote,	TipoCorralID,	TipoProcesoID, FechaInicio, CabezasInicio,
		CASE WHEN TipoCorralID = 2 THEN FechaInicio+7 ELSE FechaCierre END 'FechaCierre',
		Cabezas, FechaDisponibilidad, DisponibilidadManual,	Activo,	FechaSalida, FechaCreacion, UsuarioCreacionID,
	    FechaModificacion=null,UsuarioModificacionID=null
	FROM LoteCargaInicial;

	-- UPDATE Lote SET Lote = LoteID WHERE Lote BETWEEN 1 AND (SELECT COUNT(*) FROM LoteCargaInicial) AND Activo = 1 AND FechaModificacion != null;
	/* Actualziar el Foliador de Lotes */
	IF ((SELECT COUNT(*) FROM Folio WHERE OrganizacionID = @OrganizacionID AND TipoFolioID = 3 ) > 0 )
		BEGIN
			UPDATE Folio 
			   SET Valor = (SELECT COUNT(*) FROM Lote WHERE OrganizacionID = @OrganizacionID) 
			 WHERE TipoFolioID = 3 
			   AND OrganizacionID = @OrganizacionID;
		END
	ELSE
		BEGIN
			INSERT INTO Folio (OrganizacionID, TipoFolioID, Valor) 
			VALUES (@OrganizacionID, 3,(SELECT COUNT(*) FROM Lote WHERE OrganizacionID = @OrganizacionID));
		END

	/* Se crea el Embarque */
	DECLARE @EmbarqueID INT;
	EXEC @EmbarqueID = Embarque_Crear @OrganizacionID, 2, 2, 1, 1;	
	SET @EmbarqueID = (SELECT MAX(EmbarqueID) FROM Embarque) --(SELECT SCOPE_IDENTITY());

	/* Se crean las EntradaGanado  */
	INSERT INTO EntradaGanado(FolioEntrada, OrganizacionID, OrganizacionOrigenID, FechaEntrada
							, EmbarqueID, FolioOrigen, FechaSalida, CamionID, ChoferID, JaulaID
							, CabezasOrigen, CabezasRecibidas, OperadorID, PesoBruto, PesoTara
							, EsRuteo, Fleje, CheckList, CorralID, LoteID, Observacion, ImpresionTicket
							, Costeado, Manejado, Guia, Factura, Poliza, HojaEmbarque, ManejoSinEstres
							, CabezasMuertas, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion
							, UsuarioModificacionID, CertificadoZoosanitario, PruebaTB, PruebaTR, CondicionJaulaID)
	SELECT FolioEntrada, OrganizacionID, OrganizacionOrigenID = OrganizacionID, FechaEntrada, EmbarqueID = @EmbarqueID,
			FolioOrigen, FechaSalida, CamionID = 1, ChoferID, JaulaID, CabezasOrigen, CabezasRecibidas, OperadorID,
			PesoBruto, PesoTara, EsRuteo, Fleje, CheckList, CorralID, LoteID, Observaciones,
			ImpresionTicket, Costeado, Manejado, Guia, Factura, Poliza, HojaEmbarque, ManejoSinEstres,
			CabezasMuertas, Activo, FechaCreacion, UsuarioCreacionID = 1, FechaModificacion = null ,
			UsuarioModificacionID = null, CertificadoZoosanitario = NULL, PruebaTB = NULL, PruebaTR = NULL,
			CondicionJaulaID = NULL
	FROM EntradaGanadoCargaInicial;

	-- UPDATE EntradaGanado SET FolioEntrada = EntradaGanadoID WHERE Observacion = 'Carga Inicial';
	/* Se Actualizan el Corral/Lote de la entrada de ganado */
	UPDATE EG
	   SET EG.CorralID = C.CorralID, EG.LoteID = L.LoteID
	  FROM EntradaGanado EG
	 INNER JOIN Corral C ON  RIGHT(LTRIM(RTRIM('000' + EG.CheckList)),3) = RIGHT(LTRIM(RTRIM('000' + c.Codigo)),3) AND C.OrganizacionID = @OrganizacionID
	 INNER JOIN Lote L ON L.CorralID = C.CorralID AND L.Activo = 1
	 WHERE EG.Observacion = 'Carga Inicial'
	   AND EG.Activo = 1
	   AND EG.OrganizacionID = @OrganizacionID;

	/* Se crea la EntradaGanadoCosteo para cada EntradaGanado*/  
	INSERT INTO EntradaGanadoCosteo
	SELECT OrganizacionID = @OrganizacionID,
		EntradaGanadoID = EG.EntradaGanadoID,
		Observacion = 'Carga Inicial',
		Prorrateado = 1,
		Activo = 1,
		FechaCreacion = GETDATE(),
		UsuarioCreacion = 1,
		FechaModificacion = NULL,
		UsuarioModificacion = NULL  
	 FROM EntradaGanado EG
	WHERE EG.Observacion = 'Carga Inicial'
	  AND EG.Activo = 1
	  AND EG.OrganizacionID = @OrganizacionID;
	
	/* Se crea la EntradaDetalle para cada EntradaGanado*/  
	INSERT INTO EntradaDetalle(EntradaGanadoCosteoID, TipoGanadoID, Cabezas, PesoOrigen, PesoLlegada
								, PrecioKilo, Importe, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion
								, UsuarioModificacionID)
	SELECT  EntradaGanadoCosteoID =  EGC.EntradaGanadoCosteoID,
			TipoGanadoID = COALESCE((SELECT TipoGanadoID	
							  FROM TipoGanado 
							 WHERE sexo = (CASE WHEN LEFT(RTRIM(R.[TIPO DE GANADO]),1) > 4 THEN 'H' ELSE 'M' END ) 
							   AND ((EG.PesoBruto-EG.PesoTara)/EG.CabezasRecibidas) BETWEEN PesoMinimo AND PesoMaximo) , 1),
			Cabezas = EG.CabezasRecibidas,
			PesoOrigen = (EG.PesoBruto-EG.PesoTara),
			PesoLlegada = 0,
			PrecioKilo = 0,
			Importe = 0,
			Activo = 1,
			FechaCreacion = GETDATE(),
			UsuarioCreacionID = 1,
			FechaModificacion = NULL,
			UsuarioModificacionID = NULL
	 FROM EntradaGanado EG
	INNER JOIN EntradaGanadoCosteo EGC ON EGC.EntradaGanadoID = EG.EntradaGanadoID
	INNER JOIN RESUMEN R ON R.Organizacion = @OrganizacionID AND RIGHT(LTRIM(RTRIM('000' + R.CORRAL)),3) = RIGHT(LTRIM(RTRIM('000' + EG.CheckList)),3)
	WHERE EG.Observacion = 'Carga Inicial'
	  AND EG.Activo = 1
	  AND EG.OrganizacionID = @OrganizacionID;
	
	/* Se crean las Calidades de Ganado! */
	INSERT INTO EntradaGanadoCalidad
	SELECT EntradaGanadoID = EG.EntradaGanadoID,
			CalidadGanadoID = 3,
			Valor = EG.CabezasRecibidas,
			Activo = 1,
			FechaCreacion = GETDATE(),
			UsuarioCreacionID = 1,
			FechaModificacion = NULL,
			UsuarioModificacionID = NULL
     FROM EntradaGanado EG
	WHERE EG.Observacion = 'Carga Inicial'
	  AND EG.Activo = 1
	  AND EG.OrganizacionID = @OrganizacionID;

	/* Actualziar el Foliador de EntradaGanado */
	IF ((SELECT COUNT(*) FROM Folio WHERE OrganizacionID = @OrganizacionID AND TipoFolioID = 2 ) > 0 )
		BEGIN
			UPDATE Folio 
			   SET Valor = (SELECT COUNT(*) FROM EntradaGanado WHERE OrganizacionID = @OrganizacionID AND Observacion = 'Carga Inicial') 
			 WHERE TipoFolioID = 2 
			   AND OrganizacionID = @OrganizacionID;
		END
	ELSE
		BEGIN
			INSERT INTO Folio (OrganizacionID, TipoFolioID, Valor) 
			VALUES (@OrganizacionID, 2,(SELECT COUNT(*) FROM EntradaGanado WHERE OrganizacionID = @OrganizacionID AND Observacion = 'Carga Inicial'));
		END
	
	/* Se crean las proyecciones de los lotes en LoteProyeccion*/
	INSERT INTO LoteProyeccion
	SELECT LoteID 				   = L.LoteID,
		OrganizacionID 			   = L.OrganizacionID,
		Frame                      = 0,
		GananciaDiaria             = R.[GANANCIA DIARIA],
		ConsumoBaseHumeda          = 0,
		Conversion                 = 0,
		PesoMaduro                 = 0,
		PesoSacrificio             = 0,
		DiasEngorda                = DATEDIFF(DAY, CONVERT(CHAR(8),R.[FECHA INICIO],112), R.[FECHA DISPONIBILIDAD]),
		FechaEntradaZilmax         = (CAST(R.[FECHA DISPONIBILIDAD] AS DATETIME)-30),
		FechaCreacion              = GETDATE(),
		UsuarioCreacionID 		   = 1,
		FechaModificacion 		   = NULL,
		UsuarioModificacionID 	   = NULL,
		Revision 				   = 0
	FROM EntradaGanado EG
	INNER JOIN Corral C ON EG.CorralID = C.CorralID AND C.OrganizacionID = @OrganizacionID
	INNER JOIN Lote L ON L.CorralID = C.CorralID AND L.Activo = 1
	INNER JOIN RESUMEN R ON R.Organizacion = @OrganizacionID AND RIGHT('000' + RTRIM(R.CORRAL),3)  = RIGHT('000' + RTRIM(C.Codigo),3) 
	WHERE EG.Observacion = 'Carga Inicial'
	AND EG.Activo = 1
	AND EG.OrganizacionID = @OrganizacionID;
	
	/* Se inserta el Inventario de Animales */
	INSERT INTO Animal
	SELECT Arete, AreteMetalico, FechaCompra, Tipo_Gan, 
		CASE CAST( CalEng AS INT) WHEN 0 THEN 3 ELSE CAST( CalEng AS INT) END AS Cal_eng, 
		ClasificacionGanadoID, PesoCompra, OrganizacionIDEntrada, FolioEntradaID = 1, PesoLlegada, 
		Paletas, CausaRechazoID, Venta, Cronico = 0, CambioSexo = 0, Activo, FechaCreacion, UsuarioCreacionID,
		FechaModificacion, UsuarioModificacionID
	FROM AnimalCargaInicial;

	/* Actualizar el FolioEntrada de la tabla animal*/
	UPDATE A
		SET A.FolioEntrada = EG.FolioEntrada
	-- select a.AnimalID , A.FolioEntrada, EG.FolioEntrada
	FROM Animal A
	INNER JOIN AnimalMovimiento AM ON A.AnimalID = AM.AnimalID
	INNER JOIN EntradaGanado EG ON AM.CorralID = EG.CorralID AND EG.OrganizacionID = @OrganizacionID
	WHERE A.OrganizacionIDEntrada = @OrganizacionID
	AND AM.Activo = 1;
	
	/* Se insertan los movimientos de Animales */
	INSERT INTO AnimalMovimiento
	SELECT A.AnimalID,  AMC.OrganizacionID, AMC.CorralID, L.LoteID, AMC.FechaMovimiento, 
			AMC.Peso, AMC.Temperatura, AMC.TipoMovimientoID, AMC.TrampaID, AMC.OperadorID, 
			AMC.Observaciones, LoteIDOrigen = null,AnimalMovimientoIDAnterior = null, Activo = 0, 
			AMC.FechaCreacion, AMC.UsuarioCreacionID, AMC.FechaModificacion, AMC.UsuarioModificacionID
	FROM AnimalMovimientoCargaInicial AMC
	INNER JOIN Animal A ON (A.Arete = AMC.Arete AND A.OrganizacionIDEntrada  = @OrganizacionID)
	INNER JOIN Lote L ON (L.CorralID = AMC.CorralID AND L.OrganizacionID  = @OrganizacionID);

	/* Se inserta la info de las enfermerias */
	INSERT INTO AnimalMovimiento
	SELECT AMC.AnimalID,  AMC.OrganizacionID, AMC.CorralID, AMC.LoteID, AMC.FechaMovimiento, 
			AMC.Peso, AMC.Temperatura, TipoMovimientoID = 7, AMC.TrampaID, AMC.OperadorID, 
			AMC.Observaciones, LoteIDOrigen = AMC.LoteID, AnimalMovimientoIDAnterior = AMC.AnimalMovimientoID, Activo = 0, 
			AMC.FechaCreacion, AMC.UsuarioCreacionID, AMC.FechaModificacion, AMC.UsuarioModificacionID
	FROM AnimalMovimiento AMC
	WHERE AMC.AnimalMovimientoID IN (
					SELECT MAX(AnimalMovimientoID) 
					  FROM AnimalMovimiento AM
				     INNER JOIN Corral C ON C.CorralID = AM.CorralID AND C.OrganizacionID = @OrganizacionID
				     INNER JOIN TipoCorral TC ON C.TipoCorralID = TC.TipoCorralID 
				     WHERE AM.OrganizacionID = @OrganizacionID
 					   AND TC.GrupoCorralID = 3
					 GROUP BY AnimalID);
	
	/* Se habilita el ultimo Movimiento*/
	UPDATE AnimalMovimiento
	   SET Activo = 1
	 WHERE AnimalMovimientoID IN (
					SELECT MAX(AnimalMovimientoID) 
					  FROM AnimalMovimiento 
					 WHERE OrganizacionID = @OrganizacionID
					  GROUP BY AnimalID);
	
	/* Se crean las detecciones y las deteccion sintoma  para los animales de enfermeria */
	INSERT INTO DeteccionAnimal
	SELECT AnimalMovimientoID = AM.AnimalMovimientoID,
		Arete = A.Arete,
		AreteMetalico = A.AreteMetalico,
		FotoDeteccion = '',
		LoteID = L.LoteID,
		OperadorID = 6,
		TipoDeteccionID = 2,
		GradoID = 2,
		Observaciones = 'Carga Inicial',
		DescripcionGanadoID = 1,
		NoFierro = '',
		FechaDeteccion = GETDATE(),
		DeteccionAnalista = 0,
		Activo = 1,
		FechaCreacion = GETDATE(),
		UsuarioCreacionID = 1,
		FechaModificacion = NULL,
		UsuarioModificacionID = NULL
	 FROM Animal A
	INNER JOIN AnimalMovimiento AM ON A.AnimalID = AM.AnimalID
	INNER JOIN Lote L ON L.LoteID = AM.LoteID
	WHERE AM.Activo = 1
	  AND AM.TipoMovimientoID = 7
	  AND A.OrganizacionIDEntrada = @OrganizacionID
	  AND AM.OrganizacionID = @OrganizacionID;

	/* Se insertan los sintomas de las detecciones */
	INSERT INTO DeteccionSintomaAnimal
	SELECT DeteccionAnimalID = DA.DeteccionAnimalID,
		SintomaID = S.SintomaID,
		Activo = 1,
		FechaCreacion = GETDATE(),
		UsuarioCreacionID = 1,
		FechaModificacion = NULL,
		UsuarioModificacionID = NULL
	 FROM DeteccionAnimal DA
	INNER JOIN AnimalMovimiento AM ON DA.AnimalMovimientoID = AM.AnimalMovimientoID
	INNER JOIN Problema P ON P.ProblemaID = 1
	INNER JOIN ProblemaSintoma PS ON P.ProblemaID = PS.ProblemaID
	INNER JOIN Sintoma S ON S.SintomaID = PS.SintomaID
	WHERE DA.Observaciones = 'Carga Inicial'
	  AND AM.Activo = 1
	  AND AM.OrganizacionID = @OrganizacionID;
					  
	
	/* Se inserta el Costo del Inventario de Animal */
	INSERT INTO AnimalCosto
	SELECT A.AnimalID, AMCI.FechaCosto, AMCI.CostoID, TipoReferencia = 0, AMCI.FolioReferencia, AMCI.Importe, 
		   AMCI.FechaCreacion, AMCI.UsuarioCreacionID,AMCI.FechaModificacion,AMCI.UsuarioModificacionID
	FROM AnimalCostoCargaInicial AMCI
	INNER JOIN Animal A ON (A.Arete = AMCI.Arete AND A.OrganizacionIDEntrada  = @OrganizacionID);
	
	SELECT CAST(SUM(1) AS BIGINT) AS TotalCabezas, CAST(SUM(Importe) AS DOUBLE PRECISION) AS TotalCostos
	  FROM (
		SELECT A.AnimalID, SUM(Importe) Importe
		  FROM Animal A
		 INNER JOIN AnimalCosto AC ON A.AnimalID = AC.AnimalID
		WHERE A.OrganizacionIDEntrada = @OrganizacionID
		GROUP BY A.AnimalID
      ) AS Animales;
	
	SET NOCOUNT OFF
  END


GO
