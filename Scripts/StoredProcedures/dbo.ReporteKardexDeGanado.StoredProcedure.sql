USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteKardexDeGanado]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteKardexDeGanado]
GO
/****** Object:  StoredProcedure [dbo].[ReporteKardexDeGanado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Martin
-- Create date: 30/07/2014
-- Description: Obtiene los datos para el reporte de kardex
-- SpName     : ReporteKardexDeGanado 2, 1, '20150601', '20150602'
--======================================================
CREATE PROC [dbo].[ReporteKardexDeGanado] @OrganizacionID INT, @TipoProceso INT, @FechaInicio DATE, @FechaFin DATE
AS
BEGIN
SET NOCOUNT ON
	declare @FechasInicioValor DATE,
	@FechaFinValor DATE

	set @FechasInicioValor = @FechaInicio
	set @FechaFinValor = @FechaFin


    --Obtiene los corrales activos
    SELECT L.LoteID, C.CorralId, C.Codigo, L.Lote, L.Cabezas, C.TipoCorralID
    into #tmp_Corral
    FROM Lote (NOLOCK) L
    INNER JOIN Corral(NOLOCK) c 
    ON c.CorralID = L.CorralID
    WHERE L.OrganizacionID = @OrganizacionID
    AND L.Activo = 1 
		AND C.Codigo <> 'ZZZ'
    AND L.TipoProcesoID = @TipoProceso

    --Obtiene los animales de cada corral
    CREATE TABLE #tmp_animal
    (
        LoteID int not null default 0,
        PesoFinal int not null default 0,
        Descripcion    varchar(100) not null default 0
    )
    INSERT INTO #tmp_animal (LoteID,PesoFinal,Descripcion)
    SELECT am.LoteID, am.Peso AS PesoFinal, tp.Descripcion
    FROM AnimalMovimiento(NOLOCK) am
    INNER JOIN Animal(NOLOCK) a 
    ON A.AnimalID = am.AnimalID
    INNER JOIN TipoGanado(NOLOCK) tp 
    ON tp.TipoGanadoID = a.TipoGanadoID
    WHERE am.OrganizacionID = @OrganizacionID and am.Activo = 1
    AND am.Activo = 1

    --Obtiene los movimientos de la fecha consultada
    SELECT am.LoteID AS LoteEntrada, am.Peso, am.LoteIdOrigen AS LoteSalida
    INTO #tmp_lote
    FROM AnimalMovimiento(NOLOCK) am
    INNER JOIN Animal(NOLOCK) a ON A.AnimalID = am.AnimalID
    WHERE am.OrganizacionID = @OrganizacionID AND am.Activo = 1 
    AND CAST(am.FechaMovimiento as DATE) >= CAST(@FechasInicioValor AS DATE) AND CAST(am.FechaMovimiento as DATE) <= CAST(@FechaFinValor AS DATE)
    
--    --Obtiene los costos por corral
    CREATE TABLE #tmp_importe
    (
        LoteID    INT NOT NULL DEFAULT 0,
        Importe    DECIMAL(14,4) NOT NULL DEFAULT 0
    )
    insert into #tmp_importe(LoteID,Importe)
    SELECT am.LoteID, SUM(ac.Importe) AS Importe
    FROM AnimalMovimiento(NOLOCK) am
    --INNER JOIN Animal(NOLOCK) a ON A.AnimalID = am.AnimalID
    --INNER JOIN TipoGanado(NOLOCK) tp ON tp.TipoGanadoID = a.TipoGanadoID
    INNER JOIN AnimalCosto(NOLOCK) ac ON ac.AnimalID = am.AnimalID
    WHERE am.OrganizacionID = @OrganizacionID AND am.Activo = 1
    GROUP BY am.LoteID

    create table #tmp_Info
    (
        LoteID    int not null default 0,
        PesoInicio    decimal not null default 0,
        Entradas    int not null default 0,
        Salidas    int not null default 0,
        Importe    decimal(14,4) not null default 0
    )

    Create table #tmpLotesEntrada
    (
        LoteID    int not null default 0,
        PesoInicio    decimal not null default 0,
        Entradas    int not null default 0,
        Salidas    int not null default 0,
        Importe    decimal(14,4) not null default 0
    )
    
    --Llenamos la informacion de las entradas
    INSERT INTO #tmp_Info(LoteID,PesoInicio,Entradas,Salidas,Importe)
    SELECT l.LoteID, SUM(PesoBruto) - SUM(PesoTara) AS PesoInicio,
    CASE WHEN CAST(eg.FechaEntrada as DATE) >= CAST(@FechasInicioValor AS DATE) AND CAST(eg.FechaEntrada as DATE) <= CAST(@FechaFinValor AS DATE) THEN 
            SUM(eg.CabezasRecibidas)
        ELSE 0 
    END AS Entradas,
    CASE WHEN CAST(l.FechaSalida as DATE) >= CAST(@FechasInicioValor AS DATE) AND CAST(l.FechaSalida as DATE) <= CAST(@FechaFinValor AS DATE) THEN 
            SUM(l.CabezasInicio) - SUM(l.Cabezas)
        ELSE 0 
        END AS Salidas, 0 as importe
    FROM Lote(NOLOCK) l
    INNER JOIN EntradaGanado(NOLOCK) eg ON eg.LoteID = l.LoteID
    WHERE l.Activo = 1 AND EG.OrganizacionID = @OrganizacionID AND l.TipoProcesoID = @TipoProceso
    GROUP BY l.LoteID, eg.FechaEntrada, l.FechaSalida

    --Llenamos los importes de los lotes de entrada
    insert into #tmpLotesEntrada
    select E.LoteId, E.PesoInicio, E.Entradas, E.Salidas, sum(ISNULL(EC.Importe,0)) as Importe
    FROM #tmp_Info E
    INNER JOIN EntradaGanado(NOLOCK) EG on e.LoteId = EG.LoteID
    LEFT JOIN EntradaGanadoCosteo(NOLOCK) EGC ON EG.EntradaGanadoID = EGC.EntradaGanadoID AND EGC.Activo = 1
    LEFT JOIN EntradaGanadoCosto(NOLOCK) EC ON EC.EntradaGanadoCosteoID = EGC.EntradaGanadoCosteoID
    group by  E.LoteId,  E.PesoInicio, E.Entradas, E.Salidas

    create table #tmp_animalxlote
    (
        LoteID    int not null default 0,
        Descripcion    varchar(100) not null default '',
        cant    int not null default 0
    )
    insert into #tmp_animalxlote(LoteID,Descripcion,cant)
    SELECT LoteID,Descripcion,COUNT(Descripcion) AS cant
    FROM #tmp_animal
    GROUP BY LoteID,Descripcion
    ORDER BY LoteID,cant

    create table #tmp_animalxlote_Cant
    (
        LoteID    int not null default 0,
        Descripcion    varchar(100) not null default '',
        cant int not null default 0,
        registros    bigint not null default 0
    )
    INSERT INTO #tmp_animalxlote_Cant(LoteID,Descripcion,cant,registros)
    SELECT *,ROW_NUMBER() OVER (PARTITION BY LoteID ORDER BY LoteID,cant DESC)AS registros
    FROM #tmp_animalxlote
    ORDER BY LoteID,cant DESC

    create table #tmp_TipoGanado
    (
        LoteID    int not null default 0,
        Descripcion    varchar(100) not null default ''
    )
    INSERT INTO  #tmp_TipoGanado(LoteID,Descripcion)
    SELECT LoteID,Descripcion 
    FROM #tmp_animalxlote_Cant WHERE registros=1

    CREATE TABLE #Kardex
    (
        CorralId INT,
        Codigo VARCHAR(100) NOT NULL DEFAULT '',
        LoteID    INT NOT NULL DEFAULT 0,
        TipoCorralId INT NOT NULL DEFAULT 0,
        Lote varchar(20) DEFAULT '',
        Tipo VARCHAR(100) NOT NULL DEFAULT '',
        CabezasInicial INT NOT NULL DEFAULT 0,
        KgsInicial int NOT NULL DEFAULT 0,
        CabezasEntradas INT NOT NULL DEFAULT 0,
        KgsEntradas int NOT NULL DEFAULT 0,
        CabezasSalidas INT NOT NULL DEFAULT 0,
        KgsSalidas int NOT NULL DEFAULT 0,
        CabezasFinal INT NOT NULL DEFAULT 0,
        KgsFinal int NOT NULL DEFAULT 0,
        CostoInv decimal(14,4) NOT NULL DEFAULT 0,
        Promedio decimal(14,4) NOT NULL DEFAULT 0
    )
IF(@TipoProceso = 1)
BEGIN
        INSERT INTO #Kardex (CorralID, Codigo, TipoCorralId, LoteID, Lote, Tipo, CabezasFinal, KgsFinal, CostoInv)
        SELECT a.CorralID, a.Codigo, a.TipoCorralID, a.LoteID, a.Lote, COALESCE(b.Descripcion, ' ') as Tipo ,a.Cabezas, 
        COALESCE(SUM(c.PesoFinal),0) AS PesoFinal, COALESCE(d.Importe,0) as Importe
        FROM #tmp_Corral a 
        LEFT JOIN #tmp_TipoGanado b ON a.LoteID=b.LoteID
        LEFT JOIN #tmp_animal c ON c.LoteID=b.LoteID
        LEFT JOIN #tmp_importe d ON a.LoteID=d.LoteID
        GROUP BY a.CorralID, a.Codigo, a.TipoCorralID ,a.LoteID, a.Lote,b.Descripcion,a.Cabezas,d.Importe

        --OBTENER MOVIMIENTO ENTRADAS
        create table #tmp_cabezasEntrada
        (
            LoteEntrada    int not null default 0,
            Entradas    int not null default 0,
            Peso    int not null default 0
        )
        INSERT INTO #tmp_cabezasEntrada(LoteEntrada,Entradas,Peso)
        SELECT LoteEntrada, COUNT(LoteEntrada) AS Entradas,0 AS Peso
        FROM #tmp_lote 
        GROUP BY LoteEntrada
        ORDER BY LoteEntrada

        --OBTENER EL PESO DE MOVIMIENTO ENTRADA
        SELECT LoteEntrada, SUM(peso) AS peso
        INTO #tmp_pesoEntrada
        FROM #tmp_lote 
        GROUP BY LoteEntrada
        ORDER BY LoteEntrada

        UPDATE #tmp_cabezasEntrada SET Peso=b.peso
        FROM #tmp_cabezasEntrada a
        JOIN #tmp_pesoEntrada b
        ON a.LoteEntrada=b.LoteEntrada

        --ACTUALIZAR MOVIMIENTO CABEZAS ENTRADA Y PESO ENTRADA

        UPDATE #Kardex 
        SET CabezasEntradas=Entradas,KgsEntradas=b.Peso
        FROM #Kardex a JOIN #tmp_cabezasEntrada b 
        ON a.LoteID=b.LoteEntrada

        --OBTENER MOVIMIENTO SALIDAS
        SELECT LoteSalida, COUNT(LoteSalida) AS Salidas,0 AS Peso
        INTO #tmp_cabezasSalidas
        FROM #tmp_lote
        where LoteSalida > 0
        GROUP BY LoteSalida
        ORDER BY LoteSalida

        ---OBTENER EL PESO DE MOVIMIENTO SALIDA
        SELECT LoteSalida,SUM(peso) AS peso
        INTO #tmp_pesoSalida
        FROM #tmp_lote 
        GROUP BY LoteSalida
        ORDER BY LoteSalida

        UPDATE #tmp_cabezasSalidas SET Peso=b.peso
        FROM #tmp_cabezasSalidas a
        JOIN #tmp_pesoSalida b
        ON a.LoteSalida = b.LoteSalida

        --ACTUALIZAR MOVIMIENTO CABEZAS SALIDA Y PESO SALIDA
        UPDATE #Kardex 
        SET CabezasSalidas=Salidas,KgsSalidas=b.Peso
        FROM #Kardex a JOIN #tmp_cabezasSalidas b 
        ON a.LoteID=b.LoteSalida
        --Actualizamos los corrales de recepcion

        UPDATE #Kardex SET 
        CabezasEntradas = COALESCE(b.Entradas, 0),
        KgsFinal = COALESCE(b.PesoInicio, 0),
        CabezasSalidas = COALESCE(b.Salidas, 0),
        CostoInv = COALESCE(b.Importe,0)
        FROM #Kardex a 
        JOIN #tmpLotesEntrada b ON a.LoteID = b.LoteID 
        WHERE a.TipoCorralId = 1
                
        --ACTUALIZAR PROMEDIO
        UPDATE #Kardex SET Promedio= CASE CostoInv WHEN 0 THEN 0 ELSE CostoInv/KgsFinal END
        --ACTUALIZAR INVENTARIO INICIAL CABEZAS
        UPDATE #Kardex SET CabezasInicial=(CabezasFinal-CabezasEntradas) + CabezasSalidas
        --ACTUALIZAR INVENTARIO INICIAL KGS
        UPDATE #Kardex SET KgsInicial= (KgsFinal-KgsEntradas) + KgsSalidas
        DROP TABLE #tmp_pesoSalida
        DROP TABLE #tmp_cabezasSalidas
        DROP TABLE #tmp_cabezasEntrada
        DROP TABLE #tmp_pesoEntrada
END
ELSE
BEGIN
        INSERT INTO #Kardex (CorralID, Codigo, TipoCorralId, LoteID, Lote, Tipo, CabezasFinal,KgsFinal,CostoInv)
        SELECT a.CorralID, a.Codigo, a.TipoCorralID, a.LoteID, a.Lote, COALESCE(b.Descripcion, ' ') as Tipo ,a.Cabezas, 
        COALESCE(SUM(c.PesoFinal),0) AS PesoFinal, COALESCE(d.Importe,0) as Importe
        FROM #tmp_Corral a 
        LEFT JOIN #tmp_TipoGanado b ON a.LoteID=b.LoteID
        LEFT JOIN #tmp_animal c ON c.LoteID=b.LoteID
        LEFT JOIN #tmp_importe d ON a.LoteID=d.LoteID
        WHERE TipoCorralID = 1
        GROUP BY a.CorralID, a.Codigo, a.TipoCorralID ,a.LoteID, a.Lote,b.Descripcion,a.Cabezas,d.Importe

        UPDATE #Kardex 
        SET CabezasEntradas=Entradas, KgsEntradas=b.PesoInicio
        FROM #Kardex a 
        JOIN #tmpLotesEntrada b 
        ON a.LoteID=b.LoteID

        --ACTUALIZAR MOVIMIENTO CABEZAS SALIDA Y PESO SALIDA
        UPDATE #Kardex 
        SET CabezasSalidas=Salidas,KgsSalidas = b.PesoInicio
        FROM #Kardex a 
        JOIN #tmpLotesEntrada b 
        ON a.LoteID=b.LoteID

        --ACTUALIZAR PROMEDIO
        UPDATE #Kardex SET Promedio=CASE CostoInv WHEN 0 THEN 0 ELSE CostoInv/KgsFinal END

        --ACTUALIZAR INVENTARIO INICIAL CABEZAS
        UPDATE #Kardex SET CabezasInicial= (CabezasFinal-CabezasEntradas) + CabezasSalidas

        --ACTUALIZAR INVENTARIO INICIAL KGS
        UPDATE #Kardex SET KgsInicial=b.PesoInicio
        FROM #Kardex a 
        JOIN #tmp_Info b 
        ON a.LoteID=b.LoteID
END
        SELECT CorralId, Codigo, LoteId, TipoCorralId, Lote, Tipo, CabezasInicial, KgsInicial, CabezasEntradas, KgsEntradas, CabezasSalidas, KgsSalidas, CabezasFinal,
        KgsFinal, CostoInv, Promedio FROM #Kardex 
        ORDER BY CorralID

    DROP TABLE #Kardex
    DROP TABLE #tmp_TipoGanado
    DROP TABLE #tmp_animalxlote_Cant
    DROP TABLE #tmp_animalxlote
    drop table #tmpLotesEntrada
    drop table #tmp_Info 
    DROP TABLE #tmp_importe
    DROP TABLE #tmp_lote
    DROP TABLE #tmp_animal
    DROP TABLE #tmp_Corral
SET NOCOUNT OFF
END

GO
