USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_GuardarXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalMovimiento_GuardarXML]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_GuardarXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Jorge Luis Velazquez Araujo
-- Create date: 24/02/2015
-- Description: Procedimiento para almacenar los movimientos de animales
-- EXEC [dbo].[AnimalMovimiento_GuardarXML] 
-- =============================================
CREATE PROCEDURE [dbo].[AnimalMovimiento_GuardarXML]
	@AnimalMovimientoXML XML
AS
BEGIN
	create table #AnimalMovimiento  (
		AnimalID BIGINT,
	OrganizacionID INT,
	CorralID INT,
	LoteID INT,
	Peso INT,
	Temperatura DECIMAL(5,1),
	TipoMovimientoID INT,
	TrampaID INT,
	OperadorID INT,
	Observaciones VARCHAR(255),
	LoteIDOrigen INT,
	AnimalMovimientoIDAnterior INT,
	Activo INT,	
	UsuarioCreacionID INT			
	)
	INSERT #AnimalMovimiento (
	AnimalID ,
	OrganizacionID ,
	CorralID ,
	LoteID ,
	Peso ,
	Temperatura ,
	TipoMovimientoID ,
	TrampaID ,
	OperadorID,
	Observaciones,
	LoteIDOrigen,
	AnimalMovimientoIDAnterior,
	Activo,	
	UsuarioCreacionID)
	SELECT AnimalID = t.item.value('./AnimalID[1]', 'BIGINT'),
		OrganizacionID = t.item.value('./OrganizacionID[1]', 'INT'),
		CorralID = t.item.value('./CorralID[1]', 'BIGINT'),
		LoteID = t.item.value('./LoteID[1]', 'INT'),
		Peso = t.item.value('./Peso[1]', 'INT'),
		Temperatura = t.item.value('./Temperatura[1]', 'DECIMAL(5,1)'),
		TipoMovimientoID = t.item.value('./TipoMovimientoID[1]', 'INT'),
		TrampaID = t.item.value('./TrampaID[1]', 'INT'),
		OperadorID = t.item.value('./OperadorID[1]', 'INT'),
		Observaciones = t.item.value('./Observaciones[1]', 'VARCHAR(255)'),
		0,--LoteIDOrigen = t.item.value('./LoteIDOrigen[1]', 'INT'),
		0,--AnimalMovimientoIDAnterior = t.item.value('./AnimalMovimientoIDAnterior[1]', 'INT'),
		Activo = t.item.value('./Activo[1]', 'BIT'),
		UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM @AnimalMovimientoXML.nodes('ROOT/AnimalMovimiento') AS T(item)
	update tam set tam.LoteIDOrigen = am.LoteID, tam.AnimalMovimientoIDAnterior = am.AnimalMovimientoID	
	from AnimalMovimiento am
	inner join #AnimalMovimiento tam on am.AnimalID = tam.AnimalID 
	and am.Activo = 1
	update #AnimalMovimiento set LoteIDOrigen = null, AnimalMovimientoIDAnterior = NULL
	where LoteIDOrigen = 0 
	update #AnimalMovimiento set AnimalMovimientoIDAnterior = NULL
	where AnimalMovimientoIDAnterior = 0 
	update am set Activo = 0
	from #AnimalMovimiento tam 
	inner join AnimalMovimiento am on tam.AnimalMovimientoIDAnterior = am.AnimalMovimientoID
	/* Se crea registro en la tabla de Animal Movimiento*/
	INSERT INTO AnimalMovimiento(
		AnimalID,
		OrganizacionID,
		CorralID,
		LoteID,
		FechaMovimiento,
		Peso,
		Temperatura,
		TipoMovimientoID,
		TrampaID,
		OperadorID,
		Observaciones,
		LoteIDOrigen,
		AnimalMovimientoIDAnterior,
		Activo,
		FechaCreacion,
		UsuarioCreacionID)
	SELECT
		tam.AnimalID,
		tam.OrganizacionID,
		tam.CorralID,
		tam.LoteID,
		GETDATE(),
		tam.Peso,
		tam.Temperatura,
		tam.TipoMovimientoID,
		tam.TrampaID,
		tam.OperadorID,
		tam.Observaciones,
		tam.LoteIDOrigen,
		tam.AnimalMovimientoIDAnterior,
		tam.Activo,
		GETDATE(),
		tam.UsuarioCreacionID
		FROM #AnimalMovimiento tam
END

GO
