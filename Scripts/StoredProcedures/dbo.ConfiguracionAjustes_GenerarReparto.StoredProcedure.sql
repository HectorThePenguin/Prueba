USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAjustes_GenerarReparto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionAjustes_GenerarReparto]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAjustes_GenerarReparto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 04/09/2014
-- Description:	Genera el reparto para los corrales que no tienen
/*
	ConfiguracionAjustes_GenerarReparto
	'<ROOT>
	  <RepartoGrabar>
		<OrganizacionID>1</OrganizacionID>
		<CorralCodigo>33</CorralCodigo>
		<Lote>0</Lote>
		<TipoServicioID>1</TipoServicioID>
		<FormulaIDProgramada>1</FormulaIDProgramada>
		<CantidadProgramada>1000</CantidadProgramada>
		<EstadoComederoID>4</EstadoComederoID>
		<Observaciones>QWERTYQ</Observaciones>
		<FechaReparto>2014-09-09T00:00:00</FechaReparto>
		<UsuarioCreacionID>5</UsuarioCreacionID>
	  </RepartoGrabar>
	  <RepartoGrabar>
		<OrganizacionID>1</OrganizacionID>
		<CorralCodigo>33</CorralCodigo>
		<Lote>0</Lote>
		<TipoServicioID>2</TipoServicioID>
		<FormulaIDProgramada>1</FormulaIDProgramada>
		<CantidadProgramada>1000</CantidadProgramada>
		<EstadoComederoID>4</EstadoComederoID>
		<Observaciones>QWERTYQ</Observaciones>
		<FechaReparto>2014-09-09T00:00:00</FechaReparto>
		<UsuarioCreacionID>5</UsuarioCreacionID>
	  </RepartoGrabar>
	</ROOT>'
*/
--001 Jorge Luis Velazquez Araujo 21/08/2015 **Se agrega para que a los detalles les actualice el Ajuste en 1
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionAjustes_GenerarReparto]
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
		ValidaPorcentaje INT,
		InactivarDetalle bit,
		CambiarLote bit,
	)
	INSERT INTO #Reparto 
	            (OrganizacionID, RepartoID, RepartoDetalleIdManiana, RepartoDetalleIdTarde, CorralCodigo, Lote, TipoServicioID, FormulaIDProgramada, CantidadProgramada,
	                                  EstadoComederoID, Observaciones, FechaReparto, UsuarioCreacionID, Servido, ValidaPorcentaje,InactivarDetalle,CambiarLote)
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
		Servido = T.item.value('./Servido[1]', 'INT'),
		ValidaPorcentaje = T.item.value('./ValidaPorcentaje[1]', 'INT'),
		InactivarDetalle = T.item.value('./InactivarDetalle[1]', 'bit'),
		CambiarLote = T.item.value('./CambiarLote[1]', 'bit')
	FROM  @XmlReparto.nodes('ROOT/RepartoGrabar') AS T(item)
	delete rd 
	from RepartoDetalle rd
	inner join #Reparto rt on rd.RepartoDetalleID = rt.RepartoDetalleIdTarde
	where rt.InactivarDetalle = 1
	delete #Reparto
	where InactivarDetalle = 1
	update re set re.LoteID = l.LoteID,re.UsuarioModificacionID = rt.UsuarioCreacionID, re.FechaModificacion = GETDATE()
	from Reparto re
	inner join #Reparto rt on re.RepartoID = rt.RepartoID
	INNER JOIN Corral C ON (C.Codigo = rt.CorralCodigo AND C.OrganizacionID = rt.OrganizacionID)
	INNER JOIN Lote L ON (L.Lote = rt.Lote AND L.OrganizacionID = rt.OrganizacionID AND C.CorralID = L.CorralID AND L.Activo = 1)	
	where rt.CambiarLote = 1
	--delete #Reparto
	--where CambiarLote = 1
	DECLARE @PorcentajeVespertino VARCHAR(50)
	DECLARE @PorcentajeMatutino VARCHAR(50)
	SELECT TOP 1 @PorcentajeMatutino = COALESCE(Valor,40) 
	FROM Parametro P
	INNER JOIN ParametroOrganizacion PO ON (P.ParametroID = PO.ParametroID)
	INNER JOIN #Reparto Tmp ON (PO.OrganizacionID = Tmp.OrganizacionID)
	WHERE Clave = 'porcentajeMatutino'
	SELECT @PorcentajeVespertino = COALESCE(Valor,60) 
	FROM Parametro P
	INNER JOIN ParametroOrganizacion PO ON (P.ParametroID = PO.ParametroID)
	INNER JOIN #Reparto Tmp ON (PO.OrganizacionID = Tmp.OrganizacionID)
	WHERE Clave = 'porcentajeVespertino'
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
		CantidadProgramada = CASE WHEN RT.CantidadProgramada = 0 THEN RT.CantidadProgramada
			WHEN RT.TipoServicioID = 1 THEN CAST(dbo.RoundMult((RT.CantidadProgramada * CAST(@PorcentajeMatutino AS INT)) / 100,5) AS DECIMAL)
		    ELSE CAST(dbo.RoundMult((RT.CantidadProgramada * CAST(@PorcentajeVespertino AS INT)) / 100,5) AS DECIMAL) END,
		0,
		COALESCE(L.Cabezas,1),
		RT.EstadoComederoID,
		RT.Observaciones,
		1,
		GETDATE(),
		RT.UsuarioCreacionID,
		Ajuste = 1
	FROM Reparto R
	INNER JOIN Corral C ON (C.CorralID = R.CorralID AND C.OrganizacionID = R.OrganizacionID)
	LEFT JOIN Lote L ON (R.LoteID = L.LoteID AND L.CorralID = R.CorralID AND L.Activo = 1 )
	INNER JOIN #Reparto RT ON (C.Codigo = RT.CorralCodigo AND C.OrganizacionID = RT.OrganizacionID )
	inner join #RepartosUnicos ru on r.RepartoID = ru.RepartoID
	WHERE (RT.RepartoDetalleIdManiana = 0 AND RT.RepartoDetalleIdTarde = 0)
	AND CONVERT(CHAR(8),R.Fecha,112) = CONVERT(CHAR(8),RT.FechaReparto,112)
		AND (rt.Lote = L.Lote OR L.LoteID is null)
	/*  Se actuaizan los detalles de los repartos */
		UPDATE RD SET RD.FormulaIDProgramada = Tmp.FormulaIDProgramada,
			RD.CantidadProgramada = CASE WHEN Tmp.ValidaPorcentaje = 1 THEN Tmp.CantidadProgramada
								WHEN Tmp.TipoServicioID = 1 THEN CAST(dbo.RoundMult((Tmp.CantidadProgramada * CAST(@PorcentajeMatutino AS INT)) / 100,5) AS DECIMAL)
								ELSE CAST(dbo.RoundMult((Tmp.CantidadProgramada * CAST(@PorcentajeVespertino AS INT)) / 100,5) AS DECIMAL) END,
			RD.EstadoComederoID = Tmp.EstadoComederoID,
			RD.Observaciones = Tmp.Observaciones,
			RD.FechaModificacion = GETDATE(), 
			RD.UsuarioModificacionID = Tmp.UsuarioCreacionID,
			RD.Ajuste = 1 --001
	FROM RepartoDetalle RD
	INNER JOIN #Reparto Tmp ON (Tmp.RepartoID = RD.RepartoID AND RD.TipoServicioID = Tmp.TipoServicioID)
	WHERE Tmp.RepartoID > 0
	AND Tmp.Servido = 0
END

GO
