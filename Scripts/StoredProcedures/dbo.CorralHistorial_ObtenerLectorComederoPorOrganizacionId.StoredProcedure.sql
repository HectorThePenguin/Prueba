USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorralHistorial_ObtenerLectorComederoPorOrganizacionId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorralHistorial_ObtenerLectorComederoPorOrganizacionId]
GO
/****** Object:  StoredProcedure [dbo].[CorralHistorial_ObtenerLectorComederoPorOrganizacionId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:    José Gilberto Quintero López 
-- Create date: 02-04-2014  
-- Description:  Obtiene el historial de reparto por organizacionID
-- CorralHistorial_ObtenerLectorComederoPorOrganizacionId 4
-- =============================================  
CREATE PROCEDURE [dbo].[CorralHistorial_ObtenerLectorComederoPorOrganizacionId] @OrganizacionID INT
AS
BEGIN
    DECLARE @TipoServicioMatutino INT
    DECLARE @TipoServicioVespertino INT
    SET @TipoServicioMatutino = 1
    SET @TipoServicioVespertino = 2
    CREATE TABLE #tblAliLectorHistorial (
        IdGanadera INTEGER NOT NULL
        ,IdCorralEngorda VARCHAR(8) NOT NULL
        ,IdLoteEngorda VARCHAR(10) NOT NULL
        ,Fecha DATETIME NOT NULL
        ,Servido BIT NOT NULL
        ,IdFormula VARCHAR(8) NOT NULL
        ,IdFormula2 VARCHAR(50) NOT NULL
        ,PesoInicio NUMERIC NOT NULL
        ,PesoProyectado NUMERIC NOT NULL
        ,DiasEngorda INTEGER NOT NULL
        ,Cabezas INTEGER NOT NULL
        ,CantidadServicio NUMERIC(14,4) NOT NULL
        ,CantidadConsumo NUMERIC(14,4) NOT NULL
        ,CantidadPedido NUMERIC(14,4) NOT NULL
        ,CantidadPlaneada NUMERIC(14,4) NOT NULL
        ,CantidadProyectada NUMERIC(14,4) NOT NULL
        ,IdEstadoComedero INTEGER NOT NULL
        ,PesoRepeso NUMERIC NOT NULL
        ,HoraReparto1 VARCHAR(6) NOT NULL
        ,HoraReparto2 VARCHAR(5) NOT NULL
        ,FechaReg DATETIME
        ,UsuarioReg INT
        ,Activo BIT
        ,RepartoID INT
        ,PRIMARY KEY (
            IdGanadera
            ,IdCorralEngorda
            ,IdLoteEngorda
            ,Fecha
            )
        )
    CREATE TABLE #Reparto (
        RepartoId BIGINT
        ,OrganizacionID int
        ,LoteID INT
        ,Fecha DATETIME
        ,PesoInicio INT
        ,PesoProyectado INT
        ,DiasEngorda INT
        ,PesoRepeso INT
        ,UsuarioCreacionID int
        )
    CREATE NONCLUSTERED INDEX [IDX_Reparto_OrganizacionID_LoteID]
    ON [dbo].[#Reparto] ([OrganizacionID],[LoteID])
    INCLUDE ([RepartoId],[Fecha],[PesoInicio],[PesoProyectado],[DiasEngorda],[PesoRepeso],[UsuarioCreacionID])

    CREATE TABLE #RepartoPromedio (
        RepartoId BIGINT
        ,Cabezas int
        ,Servido Int
        ,CantidadProgramada numeric(14,4)
        ,CantidadServida numeric(14,4)
        ,EstadoComederoID INT
        ,FormulaIDServida INT
        ,FormulaIDProgramada INT
        ,HoraReparto1 CHAR(5)
        ,HoraReparto2 CHAR(5)
        )
    CREATE NONCLUSTERED INDEX [IDX_RepartoPromedio_RepartoID_Servido]
    ON [dbo].[#RepartoPromedio] ([RepartoID],[Servido])
    INCLUDE ([Cabezas],[CantidadProgramada],[CantidadServida],[EstadoComederoID],[FormulaIDServida],[FormulaIDProgramada],[HoraReparto1],[HoraReparto2])

    INSERT #Reparto
    SELECT r.RepartoID
        ,r.OrganizacionID
        ,r.LoteID
        ,r.Fecha
        ,r.PesoInicio
        ,r.PesoProyectado
        ,r.DiasEngorda
        ,r.PesoRepeso
        ,r.UsuarioCreacionID
    FROM Reparto r (NOLOCK)
	INNER JOIN Lote l (NOLOCK) ON l.LoteID = r.LoteID AND l.Activo = 1
    WHERE r.OrganizacionID = @OrganizacionID
        AND r.Activo = 1

	CREATE TABLE #FechaEntrada (
	LoteID INT
	, FechaEntrada smalldatetime)

	INSERT INTO #FechaEntrada
    SELECT l.LoteID, CAST(AVG(CAST(eg.FechaEntrada AS float)) AS smalldatetime) AS FechaEntrada
    FROM Lote l (NOLOCK)
    INNER JOIN AnimalMovimiento am (NOLOCK) ON am.LoteID = l.LoteID AND am.Activo = 1
    INNER JOIN Animal a (NOLOCK) ON a.AnimalID = am.AnimalID AND a.Activo = 1
    INNER JOIN EntradaGanado eg (NOLOCK) ON eg.OrganizacionID = a.OrganizacionIDEntrada AND eg.FolioEntrada = a.FolioEntrada
	WHERE l.OrganizacionID = @OrganizacionID AND l.Activo = 1
    GROUP BY l.LoteID

    INSERT #RepartoPromedio
    SELECT r.RepartoID RepartoId
        ,AVG(rd.Cabezas) AS Cabezas
        ,AVG(case When rd.Servido  =1 then 1 else 0 End) as [Servido]
        ,SUM(rd.CantidadProgramada) AS [CantidadProgramada]
        ,SUM(rd.CantidadServida) AS CantidadServida
	   ,MAX(CASE rd.TipoServicioID WHEN 2 THEN rd.EstadoComederoID ELSE 0 END) AS [EstadoComederoID]
        --,AVG(rd.EstadoComederoID) AS [EstadoComederoID]
        --,MIN(rd.FormulaIDServida) AS [IdFormula]
        --,MAX(rd.FormulaIDProgramada) AS [IdFormula2]
        ,MAX(CASE 
                WHEN rd.TipoServicioID = @TipoServicioMatutino
                    THEN rd.FormulaIDServida 
                ELSE 0
                END) AS [IdFormula]
        ,MAX(CASE 
                WHEN rd.TipoServicioID = @TipoServicioVespertino
                    THEN rd.FormulaIDServida
                ELSE 0
                END) AS [IdFormula2]        
        ,MAX(CASE 
                WHEN rd.TipoServicioID = @TipoServicioMatutino
                    THEN rd.HoraReparto
                ELSE ''
                END) AS [HoraReparto1]
        ,MAX(CASE 
                WHEN rd.TipoServicioID = @TipoServicioVespertino
                    THEN rd.HoraReparto
                ELSE ''
                END) AS [HoraReparto2]
    FROM #Reparto r
    INNER JOIN RepartoDetalle rd (NOLOCK) ON rd.RepartoID = r.RepartoId
    WHERE rd.TipoServicioID IN (@TipoServicioMatutino, @TipoServicioVespertino)
    GROUP BY r.RepartoID

    INSERT INTO #tblAliLectorHistorial (
        IdGanadera
        ,IdCorralEngorda
        ,IdLoteEngorda
        ,Fecha
        ,Servido
        ,IdFormula
        ,IdFormula2
        ,PesoInicio
        ,PesoProyectado
        ,DiasEngorda
        ,Cabezas
        ,CantidadServicio
        ,CantidadConsumo
        ,CantidadPedido
        ,CantidadPlaneada
        ,CantidadProyectada
        ,IdEstadoComedero
        ,PesoRepeso
        ,HoraReparto1
        ,HoraReparto2
        ,FechaReg
        ,UsuarioReg
        ,Activo
        ,RepartoID
        )
    SELECT IdGanadera
        ,IdCorralEngorda
        ,IdLoteEngorda
        ,Fecha
        ,Servido
        ,IdFormula
        ,IdFormula2
        ,PesoInicio
        ,PesoProyectado
        ,DiasEngorda
        ,Cabezas
        ,CantidadServicio
        ,CantidadConsumo
        ,CantidadPedido
        ,CantidadPlaneada
        ,CantidadProyectada
        ,IdEstadoComedero
        ,PesoRepeso
        ,HoraReparto1
        ,HoraReparto2
        ,FechaReg
        ,UsuarioReg
        ,Activo
        ,RepartoID
    FROM (
        SELECT r.OrganizacionID AS [IdGanadera] --OrganizacionID  
            ,c.Codigo AS [IdCorralEngorda] --Se liga el lote donde CorralID corresponda con la tabla Corral  
            ,l.Lote AS [IdLoteEngorda] --LoteID  
            ,r.Fecha AS [Fecha] --Fecha              
            ,rd.Servido --Servido Calculado 
            ,isnull(rd.FormulaIDServida,0) AS [IdFormula] --FormulaIdServida (Tabla RepartoDetalle)  
            ,isnull(rd.FormulaIDProgramada,0) AS [IdFormula2] --FormulaIDProgramada (Tabla RepartoDetalle)  
            ,r.PesoInicio AS [PesoInicio] --PesoInicio  
            ,r.PesoProyectado AS [PesoProyectado] --PesoProyectado  
            ,CASE WHEN ISNULL(DATEDIFF(DD, fe.FechaEntrada, r.Fecha), r.DiasEngorda) < 0 THEN 0 ELSE ISNULL(DATEDIFF(DD, fe.FechaEntrada, r.Fecha), r.DiasEngorda) END AS [DiasEngorda] --DiasEngorda              
            ,rd.Cabezas
            ,case when  rd.Cabezas = 0 then 0 else (rd.CantidadServida / rd.Cabezas) End as [CantidadServicio]
            ,case when  rd.Cabezas = 0 then 0 else (rd.CantidadServida / rd.Cabezas) End as [CantidadConsumo]
            ,case when  rd.Cabezas = 0 then 0 else (rd.CantidadProgramada / rd.Cabezas) End as [CantidadPedido]
            ,0 AS [CantidadPlaneada] --0 - Cero  
            ,0 AS [CantidadProyectada] --0 - Cero  
            ,rd.EstadoComederoID AS [IdEstadoComedero] --EstadoComedero  
            ,r.PesoRepeso AS [PesoRepeso] --PesoRepeso  
            ,rd.HoraReparto1 --Se obtiene de la tabla RepartoDetalle Campo HoraReparto  
            ,rd.HoraReparto2 --Se obtiene de la tabla RepartoDetalle  Campo HoraReparto  
            ,r.Fecha AS [FechaReg] --FechaCreacion  
            ,r.UsuarioCreacionID AS [UsuarioReg] --UsuarioCreacionID  
            ,l.Activo AS [Activo]
            ,ROW_NUMBER() OVER (
                PARTITION BY l.OrganizacionID
                ,c.Codigo
                ,l.Lote
                ,r.Fecha ORDER BY l.OrganizacionID
                    ,c.Codigo
                    ,l.Lote
                    ,r.Fecha
                ) AS [Orden]
            ,c.TipoCorralID
            ,r.RepartoID
        FROM Corral c (NOLOCK)
        INNER JOIN Lote l (NOLOCK) ON l.CorralID = c.CorralID
        INNER JOIN #Reparto r ON r.LoteID = l.LoteId
        INNER JOIN #RepartoPromedio rd on rd.RepartoId = r.RepartoId 
	   LEFT JOIN #FechaEntrada fe ON fe.LoteID = l.LoteID
        WHERE r.OrganizacionID = @OrganizacionID
            AND l.Activo = 1
            AND c.TipoCorralID NOT IN (1) --Tipo corral recepci?n  
            AND rd.Servido = 1
        ) a
    WHERE Orden = 1

    SELECT *
    FROM #tblAliLectorHistorial 
END


GO
