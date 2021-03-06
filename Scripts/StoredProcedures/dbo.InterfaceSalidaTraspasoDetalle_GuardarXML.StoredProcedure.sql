USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspasoDetalle_GuardarXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaTraspasoDetalle_GuardarXML]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspasoDetalle_GuardarXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Jorge Luis Velazquez Araujo
-- Create date: 26/02/2015
-- Description: Procedimiento para almacenar los Detalles de la Interface Salida Traspaso
-- EXEC [dbo].[InterfaceSalidaTraspasoDetalle_GuardarXML] 
-- =============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaTraspasoDetalle_GuardarXML]
	@InterfaceSalidaDetalleXML XML
AS
BEGIN

	create table #InterfaceSalidaTraspasoDetalle  (
		InterfaceSalidaTraspasoID	int
		,LoteID	int
		,TipoGanadoID	int
		,PesoProyectado	int
		,GananciaDiaria	decimal(5,3)
		,DiasEngorda	int
		,FormulaID	int
		,DiasFormula	int
		,Cabezas	int
		,Activo	bit
		,UsuarioCreacionID	int		
	)

	INSERT #InterfaceSalidaTraspasoDetalle (
	InterfaceSalidaTraspasoID
	,LoteID
	,TipoGanadoID
	,PesoProyectado
	,GananciaDiaria
	,DiasEngorda
	,FormulaID
	,DiasFormula
	,Cabezas
	,Activo
	,UsuarioCreacionID)
	SELECT InterfaceSalidaTraspasoID = t.item.value('./InterfaceSalidaTraspasoID[1]', 'INT'),
		LoteID = t.item.value('./LoteID[1]', 'INT'),
		TipoGanadoID = t.item.value('./TipoGanadoID[1]', 'INT'),
		PesoProyectado = t.item.value('./PesoProyectado[1]', 'INT'),		
		GananciaDiaria = t.item.value('./GananciaDiaria[1]', 'DECIMAL(5,3)'),
		DiasEngorda = t.item.value('./DiasEngorda[1]', 'INT'),
		FormulaID = t.item.value('./FormulaID[1]', 'INT'),
		DiasFormula = t.item.value('./DiasFormula[1]', 'INT'),
		Cabezas = t.item.value('./Cabezas[1]', 'INT'),				
		Activo = t.item.value('./Activo[1]', 'BIT'),
		UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM @InterfaceSalidaDetalleXML.nodes('ROOT/InterfaceSalidaTraspasoDetalle') AS T(item)
	
		
	/* Se crea registro en la tabla de Animal Movimiento*/
	INSERT INTO InterfaceSalidaTraspasoDetalle(
		InterfaceSalidaTraspasoID
		,LoteID
		,TipoGanadoID
		,PesoProyectado
		,GananciaDiaria
		,DiasEngorda
		,FormulaID
		,DiasFormula
		,Cabezas
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		,CabezasPorSacrificar)
	SELECT
		InterfaceSalidaTraspasoID
		,LoteID
		,TipoGanadoID
		,PesoProyectado
		,GananciaDiaria
		,DiasEngorda
		,FormulaID
		,DiasFormula
		,Cabezas
		,Activo
		,GETDATE()
		,UsuarioCreacionID
		,Cabezas
		FROM #InterfaceSalidaTraspasoDetalle

END


GO
