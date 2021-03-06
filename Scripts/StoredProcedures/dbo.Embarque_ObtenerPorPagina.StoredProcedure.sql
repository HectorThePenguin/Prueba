USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Embarque_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jos� Gilberto Quintero L�pez
-- Create date: 21-11-2013
-- Description:	Obtiene un listado de las programaciones pendientes 
-- Embarque_ObtenerPorPagina 0, 0, 0, 300, 0, 0, null, null, 1 , 1, 15
-- =============================================
CREATE PROCEDURE [dbo].[Embarque_ObtenerPorPagina] @EmbarqueID INT
	,@OrganizacionID INT
	,@FolioEmbarque INT
	,@OrganizacionOrigenID INT
	,@OrganizacionDestinoID INT
	,@TipoOrganizacionOrigenID INT
	,@FechaSalida DATE
	,@FechaLlegada DATE
	,@Estatus INT
	,@Inicio INT
	,@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Embarque AS TABLE (
		EmbarqueID INT PRIMARY KEY
		,OrganizacionID INT
		,OrigenID INT
		,DestinoID INT
		,FolioEmbarque INT
		,FechaSalida DATE
		,FechaLlegada DATE
		,TipoEmbarqueID INT
		,Estatus INT
		,Orden INT
		,RowNum INT IDENTITY
		)
	IF (
			@FechaSalida IS NOT NULL
			AND @FechaLlegada IS NOT NULL
			)
	BEGIN
		INSERT @Embarque (
			EmbarqueID
			,OrganizacionID
			,OrigenID
			,DestinoID
			,FolioEmbarque
			,FechaSalida
			,FechaLlegada
			,TipoEmbarqueID
			,Estatus
			,Orden
			)
		SELECT *
		FROM (
			SELECT MIN(e.EmbarqueID) AS [EmbarqueID]
				,MIN(e.OrganizacionID) AS [OrganizacionID]
				,MIN(ed.OrganizacionOrigenID) AS [OrganizacionOrigenID]
				,MAX(ed.OrganizacionDestinoID) AS [OrganizacionDestinoID]
				,e.FolioEmbarque
				,MIN(ed.FechaSalida) AS [FechaSalida]
				,MAX(ed.FechaLlegada) AS [FechaLlegada]
				,MIN(e.TipoEmbarqueID) AS [TipoEmbarqueID]
				,MIN(e.Estatus) AS [Estatus]
				, Orden.Orden
			FROM EmbarqueDetalle ed
			INNER JOIN Embarque e ON e.EmbarqueID = ed.EmbarqueID
			INNER JOIN Organizacion o ON o.OrganizacionID = ed.OrganizacionOrigenID
			INNER JOIN TipoOrganizacion ot ON ot.TipoOrganizacionID = o.TipoOrganizacionID
			INNER JOIN (
							SELECT ed1.EmbarqueID, MAX(ed1.Orden) as Orden
							FROM EmbarqueDetalle ed1
							GROUP BY ed1.EmbarqueID
					) AS [Orden] ON ed.EmbarqueID = Orden.EmbarqueID
			WHERE @EmbarqueID IN (
					e.EmbarqueID
					,0
					)
				AND @FolioEmbarque IN (
					e.FolioEmbarque
					,0
					)
				AND @OrganizacionID IN (
					e.OrganizacionID
					,0
					)
				AND @OrganizacionOrigenID IN (
					ed.OrganizacionOrigenID
					,0
					)
				AND @OrganizacionDestinoID IN (
					ed.OrganizacionDestinoID
					,0
					)
				AND @TipoOrganizacionOrigenID IN (
					ot.TipoOrganizacionID
					,0
					)
				AND @Estatus IN (
					e.Estatus
					,0
					)
				AND ((cast(ed.FechaSalida AS DATE) >= @FechaSalida))
				AND ((cast(ed.FechaLlegada AS DATE) <= @FechaLlegada))
				AND ed.Orden = 1
			GROUP BY e.EmbarqueID
				,e.OrganizacionID
				,e.FolioEmbarque
				, Orden.Orden
			) e
	END
	ELSE
		INSERT @Embarque (
			EmbarqueID
			,OrganizacionID
			,OrigenID
			,DestinoID
			,FolioEmbarque
			,FechaSalida
			,FechaLlegada
			,TipoEmbarqueID
			,Estatus
			,Orden
			)
		SELECT *
		FROM (
			SELECT MIN(e.EmbarqueID) AS [EmbarqueID]
				,MIN(e.OrganizacionID) AS [OrganizacionID]
				,MIN(ed.OrganizacionOrigenID) AS [OrganizacionOrigenID]
				,MAX(ed.OrganizacionDestinoID) AS [OrganizacionDestinoID]
				,e.FolioEmbarque
				,MIN(ed.FechaSalida) AS [FechaSalida]
				,MAX(ed.FechaLlegada) AS [FechaLlegada]
				,MIN(e.TipoEmbarqueID) AS [TipoEmbarqueID]
				,MIN(e.Estatus) AS [Estatus]
				, Orden.Orden
			FROM EmbarqueDetalle ed
			INNER JOIN Embarque e ON e.EmbarqueID = ed.EmbarqueID
			INNER JOIN Organizacion o ON o.OrganizacionID = ed.OrganizacionOrigenID
			INNER JOIN TipoOrganizacion ot ON ot.TipoOrganizacionID = o.TipoOrganizacionID
			INNER JOIN (
							SELECT ed1.EmbarqueID, MAX(ed1.Orden) as Orden
							FROM EmbarqueDetalle ed1
							GROUP BY ed1.EmbarqueID
					) AS [Orden] ON ed.EmbarqueID = Orden.EmbarqueID
			WHERE @EmbarqueID IN (
					e.EmbarqueID
					,0
					)
				AND @FolioEmbarque IN (
					e.FolioEmbarque
					,0
					)
				AND @OrganizacionID IN (
					e.OrganizacionID
					,0
					)
				AND @OrganizacionOrigenID IN (
					ed.OrganizacionOrigenID
					,0
					)
				AND @OrganizacionDestinoID IN (
					ed.OrganizacionDestinoID
					,0
					)
				AND @TipoOrganizacionOrigenID IN (
					ot.TipoOrganizacionID
					,0
					)
				AND @Estatus IN (
					e.Estatus
					,0
					)
				AND (
					(cast(ed.FechaSalida AS DATE) = @FechaSalida)
					OR @FechaSalida IS NULL
					)
				AND (
					(cast(ed.FechaLlegada AS DATE) = @FechaLlegada)
					OR @FechaLlegada IS NULL
					)
				AND ed.Activo = 1
				AND ed.Orden = 1
			GROUP BY e.EmbarqueID
				,e.OrganizacionID
				,e.FolioEmbarque
				, Orden.Orden
			) e
	--SELECT *
	--FROM @Embarque
	SELECT e.EmbarqueID,
		oe.OrganizacionID AS [OrganizacionID],
		oe.Descripcion AS [Organizacion],
		oe.TipoOrganizacionID AS [TipoOrganizacionOrigenID],
		toe.Descripcion as [TipoOrganizacion],
		e.FolioEmbarque,
		oo.OrganizacionID AS [OrganizacionOrigenID],
		oo.Descripcion AS [Origen],
		oo.TipoOrganizacionID AS [TipoOrganizacionOrigenID],
		too.Descripcion as [TipoOrganizacionOrigen],
		od.OrganizacionID AS [OrganizacionDestinoID],		
		od.Descripcion AS [Destino],		
		od.TipoOrganizacionID AS [TipoOrganizacionDestinoID],
		tod.Descripcion as [TipoOrganizacionDestino],
		e.FechaSalida,
		e.FechaLlegada,
		te.TipoEmbarqueID,
		te.Descripcion AS [TipoEmbarque],
		ch.ChoferID,
		ch.Nombre,
		ch.ApellidoPaterno,
		ch.ApellidoMaterno,
		cm.CamionID,
		cm.PlacaCamion,
		e.Estatus				
	FROM @Embarque e
	INNER JOIN EmbarqueDetalle edo ON edo.EmbarqueID = e.EmbarqueID AND  edo.Orden = 1
	INNER JOIN EmbarqueDetalle edd ON edd.EmbarqueID = e.EmbarqueID AND  edd.Orden = e.Orden
	INNER JOIN Organizacion oe ON oe.OrganizacionID = e.OrganizacionID
	INNER JOIN Organizacion oo ON oo.OrganizacionID = edo.OrganizacionOrigenID
	INNER JOIN Organizacion od ON od.OrganizacionID = edd.OrganizacionDestinoID
	INNER JOIN TipoOrganizacion toe ON toe.TipoOrganizacionID = od.TipoOrganizacionID
	INNER JOIN TipoOrganizacion too ON too.TipoOrganizacionID = oo.TipoOrganizacionID
	INNER JOIN TipoOrganizacion tod ON tod.TipoOrganizacionID = od.TipoOrganizacionID
	INNER JOIN TipoEmbarque te ON te.TipoEmbarqueID = e.TipoEmbarqueID
	INNER JOIN Chofer ch ON ch.ChoferID = edo.ChoferID
	INNER JOIN Camion cm ON cm.CamionID = edo.CamionID		
	WHERE RowNum BETWEEN @Inicio AND @Limite
	ORDER BY e.EmbarqueID
	SELECT COUNT(EmbarqueID) AS TotalReg
	FROM @Embarque
END

GO
