USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_GenerarRepartoServicioCorral]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_GenerarRepartoServicioCorral]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_GenerarRepartoServicioCorral]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 26/08/2015
-- Description:	Genera el reparto para los corrales de Servicio de Corrales
--======================================================
CREATE PROCEDURE [dbo].[Reparto_GenerarRepartoServicioCorral]
@XmlReparto XML
AS
BEGIN
	IF OBJECT_ID('tempdb..#Reparto') IS NOT NULL 
			DROP TABLE #Reparto
	CREATE TABLE #Reparto (
		OrganizacionID INT,
		RepartoID BIGINT,
		RepartoDetalleIdManiana BIGINT,
		RepartoDetalleIdTarde BIGINT,
		CorralCodigo VARCHAR(20),
		Lote VARCHAR(20),
		TipoServicioID INT,
		FormulaIDProgramada INT,
		CantidadProgramada INT,
		EstadoComederoID INT,
		Observaciones VARCHAR(250),
		FechaReparto DATE,
		UsuarioCreacionID INT,
		Servido INT,		
	)
	INSERT INTO #Reparto 
	            (OrganizacionID, RepartoID, RepartoDetalleIdManiana, RepartoDetalleIdTarde, CorralCodigo, Lote, TipoServicioID, FormulaIDProgramada, CantidadProgramada,
	                                  EstadoComederoID, Observaciones, FechaReparto, UsuarioCreacionID, Servido)
	SELECT 
		OrganizacionID  = T.item.value('./OrganizacionID[1]', 'INT'),
		RepartoID  = T.item.value('./RepartoID[1]', 'BIGINT'),
		RepartoDetalleIdManiana  = T.item.value('./RepartoDetalleIdManiana[1]', 'BIGINT'),
		RepartoDetalleIdTarde  = T.item.value('./RepartoDetalleIdTarde[1]', 'BIGINT'),
		CorralCodigo  = T.item.value('./CorralCodigo[1]', 'VARCHAR(20)'),
		Lote  = CASE WHEN T.item.value('./Lote[1]', 'VARCHAR(20)') = '0' THEN NULL ELSE T.item.value('./Lote[1]', 'VARCHAR(20)') END,
		TipoServicioID    = T.item.value('./TipoServicioID[1]', 'INT'),
		FormulaIDProgramada    = T.item.value('./FormulaIDProgramada[1]', 'INT'),
		CantidadProgramada  = T.item.value('./CantidadProgramada[1]', 'INT'),
		EstadoComederoID   = T.item.value('./EstadoComederoID[1]', 'INT'),
		Observaciones = T.item.value('./Observaciones[1]','VARCHAR(250)'),
		FechaReparto = CONVERT(CHAR(8),T.item.value('./FechaReparto[1]','DATETIME'),112),
		UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT'),
		Servido = T.item.value('./Servido[1]', 'INT')		
	FROM  @XmlReparto.nodes('ROOT/RepartoGrabar') AS T(item)	
	INSERT INTO Reparto 
			(OrganizacionID,CorralID,LoteID,Fecha,PesoInicio,PesoProyectado,DiasEngorda,PesoRepeso,Activo,FechaCreacion,UsuarioCreacionID)
	SELECT DISTINCT
		OrganizacionID=R.OrganizacionID,
		CorralID=C.CorralID,
		LoteID=CASE WHEN L.LoteID = 0 THEN NULL ELSE L.LoteID END,
		-- CAST(DATEADD(DAY, 1, GETDATE()) AS DATE),
		Fecha = FechaReparto,
		CASE WHEN COUNT(Peso) > 0 THEN CAST(SUM(Peso) / COUNT(Peso) AS INT) ELSE 0 END AS PesoInicio,
		CASE WHEN COUNT(Peso) > 0 THEN CAST(SUM(Peso) / COUNT(Peso) AS INT) ELSE 0 END AS PesoProyectado,
		DiasEngorda=1,
		PesoRepeso=0,
		Activo=1,
		FechaCreacion=GETDATE(),
		R.UsuarioCreacionID
	FROM #Reparto R
	INNER JOIN Corral C ON (C.Codigo = R.CorralCodigo AND C.OrganizacionID = R.OrganizacionID)
	LEFT JOIN Lote L ON (L.Lote = R.Lote AND L.OrganizacionID = R.OrganizacionID AND C.CorralID = L.CorralID AND L.Activo = 1)
	LEFT JOIN AnimalMovimiento AM(NOLOCK) ON ( AM.LoteID = L.LoteID AND AM.Activo = 1 ) 
	WHERE R.RepartoID = 0
	GROUP BY R.OrganizacionID,C.CorralID,R.UsuarioCreacionID,R.FechaReparto,L.LoteID
	CREATE TABLE #RepartosTodos
	(
		RepartoID int,
		CorralID int,
		LoteID int
	)
	CREATE TABLE #RepartosUnicos
	(
		RepartoID int		
	)
	insert into #RepartosTodos
	select 
	r.RepartoID,
	r.CorralID,
	r.LoteID	
	 from Reparto R
	 INNER JOIN Corral C ON (C.CorralID = R.CorralID AND C.OrganizacionID = R.OrganizacionID)
	LEFT JOIN Lote L ON (R.LoteID = L.LoteID AND L.CorralID = R.CorralID AND L.Activo = 1 )
	INNER JOIN #Reparto RT ON (C.Codigo = RT.CorralCodigo AND C.OrganizacionID = RT.OrganizacionID )
	WHERE (RT.RepartoDetalleIdManiana = 0 AND RT.RepartoDetalleIdTarde = 0)
	AND CONVERT(CHAR(8),R.Fecha,112) = CONVERT(CHAR(8),RT.FechaReparto,112)
		AND (rt.Lote = L.Lote OR L.LoteID is null)
	insert into #RepartosUnicos
	select MAX(RepartoID) from #RepartosTodos
	group by CorralID
	INSERT INTO RepartoDetalle 
		(RepartoID,TipoServicioID,FormulaIDProgramada,CantidadProgramada,Servido,
					Cabezas,EstadoComederoID,Observaciones,Activo,FechaCreacion,UsuarioCreacionID, Ajuste)
	SELECT DISTINCT 
		R.RepartoID,
		RT.TipoServicioID,
		RT.FormulaIDProgramada,
		CantidadProgramada = rt.CantidadProgramada,
		0,
		COALESCE(L.Cabezas,1),
		RT.EstadoComederoID,
		RT.Observaciones,
		1,
		GETDATE(),
		RT.UsuarioCreacionID,
		Ajuste = 0
	FROM Reparto R
	INNER JOIN Corral C ON (C.CorralID = R.CorralID AND C.OrganizacionID = R.OrganizacionID)
	LEFT JOIN Lote L ON (R.LoteID = L.LoteID AND L.CorralID = R.CorralID AND L.Activo = 1 )
	INNER JOIN #Reparto RT ON (C.Codigo = RT.CorralCodigo AND C.OrganizacionID = RT.OrganizacionID )
	inner join #RepartosUnicos ru on r.RepartoID = ru.RepartoID
	WHERE (RT.RepartoDetalleIdManiana = 0 AND RT.RepartoDetalleIdTarde = 0)
	AND CONVERT(CHAR(8),R.Fecha,112) = CONVERT(CHAR(8),RT.FechaReparto,112)
		AND (rt.Lote = L.Lote OR L.LoteID is null)
	/*  Se actuaizan los detalles de los repartos de la ma�ana */
		UPDATE RD SET RD.FormulaIDProgramada = Tmp.FormulaIDProgramada,
			RD.CantidadProgramada = tmp.CantidadProgramada,			
			RD.Observaciones = Tmp.Observaciones,
			RD.FechaModificacion = GETDATE(), 
			RD.UsuarioModificacionID = Tmp.UsuarioCreacionID			
	FROM RepartoDetalle RD
	INNER JOIN #Reparto Tmp ON (Tmp.RepartoID = RD.RepartoID AND RD.RepartoDetalleID = Tmp.RepartoDetalleIdManiana)
	WHERE Tmp.RepartoID > 0
	AND Tmp.Servido = 0
	/*  Se actuaizan los detalles de los repartos de la tarde */
		UPDATE RD SET RD.FormulaIDProgramada = Tmp.FormulaIDProgramada,
			RD.CantidadProgramada = tmp.CantidadProgramada,			
			RD.Observaciones = Tmp.Observaciones,
			RD.FechaModificacion = GETDATE(), 
			RD.UsuarioModificacionID = Tmp.UsuarioCreacionID			
	FROM RepartoDetalle RD
	INNER JOIN #Reparto Tmp ON (Tmp.RepartoID = RD.RepartoID AND RD.RepartoDetalleID = Tmp.RepartoDetalleIdTarde)
	WHERE Tmp.RepartoID > 0
	AND Tmp.Servido = 0
END

GO
