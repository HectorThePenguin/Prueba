USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LectorRegistro_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LectorRegistro_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[LectorRegistro_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 10/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LectorRegistro_Guardar 
--======================================================
CREATE PROCEDURE [dbo].[LectorRegistro_Guardar] @XmlCabecero XML
	,@XmlDetalle XML
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #LectorRegistro  (
		[LectorRegistroID] INT
		,[OrganizacionID] INT
		,[Seccion] INT
		,[Lote] VARCHAR(10)
		,[LoteID] INT
		,[Fecha] SMALLDATETIME
		,[CambioFormula] BIT
		,[Cabezas] INT
		,[EstadoComederoID] INT
		,[CantidadOriginal] DECIMAL(10,4)
		,[CantidadPedido] DECIMAL(10,4)
		,[Activo] INT
		,[FechaCreacion] SMALLDATETIME
		,[UsuarioCreacion] VARCHAR(50)
		,[UsuarioCreacionID] INT
		,[LectorRegistroAuxId] INT
		)
	CREATE TABLE #LectorRegistroDetalle (
		[LectorRegistroDetalleID] INT
		,[LectorRegistroID] INT
		,[TipoServicioID] INT
		,[FormulaIDAnterior] VARCHAR(10)
		,[FormulaIDProgramada] VARCHAR(10)
		,[FechaCreacion] SMALLDATETIME
		,[UsuarioCreacionID] INT
		,[LectorRegistroAuxId] INT
		)
	INSERT #LectorRegistro (
		[LectorRegistroID]
		,[OrganizacionID]
		,[Seccion]
		,[Lote]
		,[LoteId]
		,[Fecha]
		,[CambioFormula]
		,[Cabezas]
		,[EstadoComederoID]
		,[CantidadOriginal]
		,[CantidadPedido]
		,[Activo]
		,[FechaCreacion]
		,[UsuarioCreacion]
		,[UsuarioCreacionId]
		,[LectorRegistroAuxId]
		)
	SELECT [LectorRegistroID] = 0--t.item.value('./LectorRegistroID[1]', 'INT')
		,[OrganizacionID] = t.item.value('./OrganizacionID[1]', 'INT')
		,[Seccion] = t.item.value('./Seccion[1]', 'INT')
		,[Lote] = t.item.value('./Lote[1]', 'varchar(10)')
		,[LoteID] = l.LoteID
		,[Fecha] = t.item.value('./Fecha[1]', 'SMALLDATETIME')
		,[CambioFormula] = t.item.value('./CambioFormula[1]', 'BIT')
		,[Cabezas] = t.item.value('./Cabezas[1]', 'INT')
		,[EstadoComederoID] = t.item.value('./EstadoComederoID[1]', 'INT')
		,[CantidadOriginal] = t.item.value('./CantidadOriginal[1]', 'DECIMAL(10,4)')
		,[CantidadPedido] = t.item.value('./CantidadPedido[1]', 'DECIMAL(10,4)')
		,[Activo] = t.item.value('./Activo[1]', 'INT')
		,[FechaCreacion] = GETDATE()--t.item.value('./FechaCreacion[1]', 'SMALLDATETIME')
		,[UsuarioCreacion] = t.item.value('./UsuarioCreacion[1]', 'varchar(50)')
		,[UsuarioCreacionID] = u.UsuarioID
		,[LectorRegistroAuxId] = t.item.value('./LectorRegistroID[1]', 'INT')
	FROM @XmlCabecero.nodes('root/LectorRegistro') AS T(item)
	INNER JOIN Usuario u ON u.UsuarioActiveDirectory = t.item.value('./UsuarioCreacion[1]', 'varchar(50)')
	INNER JOIN Corral c ON c.Codigo = t.item.value('./Corral[1]', 'varchar(10)')
     INNER JOIN Lote l ON l.OrganizacionID = t.item.value('./OrganizacionID[1]', 'INT') AND c.CorralID = l.CorralID AND l.Lote =  t.item.value('./Lote[1]', 'varchar(10)') 
	INSERT LectorRegistro (
		[OrganizacionID]
		,[Seccion]
		,[LoteID]
		,[Fecha]
		,[CambioFormula]
		,[Cabezas]
		,[EstadoComederoID]
		,[CantidadOriginal]
		,[CantidadPedido]
		,[Activo]
		,[FechaCreacion]
		,[UsuarioCreacionID]
		)
	SELECT lr.[OrganizacionID]
		,lr.[Seccion]
		,lr.[LoteID]
		,lr.[Fecha]
		,lr.[CambioFormula]
		,lr.[Cabezas]
		,lr.[EstadoComederoID]
		,lr.[CantidadOriginal]
		,lr.[CantidadPedido]
		,lr.[Activo]
		,lr.[FechaCreacion]
		,lr.[UsuarioCreacionID]
	FROM #LectorRegistro lr
	LEFT JOIN dbo.LectorRegistro lr2 ON lr2.LoteID = lr.LoteID AND lr2.Fecha = lr.Fecha
	WHERE lr2.LectorRegistroID IS NULL
	--[Refrescamos el campo LectorRegistroId para posteriormente hacer el match con la tabla detalle
	UPDATE lr
	SET LectorRegistroID = l.LectorRegistroID
	FROM #LectorRegistro lr
	INNER JOIN LectorRegistro l ON l.OrganizacionID = lr.OrganizacionID
		AND l.Seccion = lr.Seccion
		AND l.LoteID = lr.LoteID
		AND l.Fecha = lr.Fecha
	INSERT #LectorRegistroDetalle (
		[LectorRegistroDetalleID]
		,[LectorRegistroID]
		,[TipoServicioID]
		,[FormulaIDAnterior]
		,[FormulaIDProgramada]
		,[LectorRegistroAuxId]
		)
	SELECT [LectorRegistroDetalleID] = t.item.value('./LectorRegistroDetalleID[1]', 'INT')
		,[LectorRegistroID] = 0--t.item.value('./LectorRegistroID[1]', 'INT')
		,[TipoServicioID] = t.item.value('./Orden[1]', 'INT')
		,[FormulaIDAnterior] = t.item.value('./FormulaIDAnterior[1]', 'varchar(10)')
		,[FormulaIDProgramada] = t.item.value('./FormulaIDProgramada[1]', 'varchar(10)')
		,[LectorRegistroAuxId] = t.item.value('./LectorRegistroID[1]', 'INT')
	FROM @XmlDetalle.nodes('root/LectorRegistroDetalle') AS T(item)
	--[Refrescamos el campo LectorRegistroId de la tabla detalle]
	UPDATE det
	SET LectorRegistroID = cab.LectorRegistroID
		,FechaCreacion = cab.FechaCreacion
		,UsuarioCreacionID = cab.UsuarioCreacionID
	FROM #LectorRegistroDetalle det
	INNER JOIN #LectorRegistro cab ON det.LectorRegistroAuxId = cab.LectorRegistroAuxId
	INSERT LectorRegistroDetalle (
		LectorRegistroID
		,TipoServicioID
		,FormulaIDAnterior
		,FormulaIDProgramada
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		)
	SELECT det.LectorRegistroID
		,det.TipoServicioID
		,det.FormulaIDAnterior
		,det.FormulaIDProgramada
		,1
		,GETDATE()
		,det.UsuarioCreacionID
	FROM #LectorRegistroDetalle det
	LEFT JOIN LectorRegistroDetalle lrd ON lrd.LectorRegistroID = det.LectorRegistroID AND lrd.TipoServicioID = det.TipoServicioID
	WHERE lrd.LectorRegistroDetalleID IS NULL
	   AND det.LectorRegistroID != 0
	--Se actualizan los registros con conflictos
	UPDATE LectorRegistro SET EstadoComederoID = l.EstadoComederoID, CantidadPedido = l.CantidadPedido
	FROM #LectorRegistro l
     INNER JOIN LectorRegistro lr 
	   ON l.LectorRegistroID = lr.LectorRegistroID
     WHERE l.EstadoComederoID != lr.EstadoComederoID 
	   OR l.CantidadPedido != lr.CantidadPedido
     UPDATE LectorRegistroDetalle SET FormulaIDProgramada = lr.FormulaIDProgramada
     FROM #LectorRegistroDetalle lr
     INNER JOIN LectorRegistroDetalle lrd 
	   ON lr.LectorRegistroID = lrd.LectorRegistroID AND lr.TipoServicioID = lrd.TipoServicioID
     WHERE lr.FormulaIDProgramada != lrd.FormulaIDProgramada
	SET NOCOUNT OFF;
END

GO
