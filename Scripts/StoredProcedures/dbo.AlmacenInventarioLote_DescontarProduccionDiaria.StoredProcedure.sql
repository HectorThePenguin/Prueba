USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_DescontarProduccionDiaria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_DescontarProduccionDiaria]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_DescontarProduccionDiaria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Vel�zquez Araujo
-- Create date: 18/06/2014
-- Description: Obtiene los Productos filtrados por la Familia
-- SpName     : AlmacenInventarioLote_DescontarProduccionDiaria 80
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_DescontarProduccionDiaria] 
@AlmacenInventarioLotesXML XML
AS
SET NOCOUNT ON;
	DECLARE @AlmacenInventarioLote AS TABLE (
		PesajeMateriaPrimaID INT
		,KilosNeto INT		
		,UsuarioModificacionID INT
		)
		INSERT @AlmacenInventarioLote (
		PesajeMateriaPrimaID
		,KilosNeto
		,UsuarioModificacionID		
		)
	SELECT PesajeMateriaPrimaID = t.item.value('./PesajeMateriaPrimaID[1]', 'INT')
		,KilosNeto = t.item.value('./KilosNeto[1]', 'INT')
		,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')		
	FROM @AlmacenInventarioLotesXML.nodes('ROOT/AlmacenInventarioLote') AS T(item)
UPDATE ail
set ail.Cantidad = (ail.Cantidad - ailtemp.KilosNeto )
,ail.Importe = (ail.Cantidad - ailtemp.KilosNeto ) * ail.PrecioPromedio
,ail.UsuarioModificacionID = ailtemp.UsuarioModificacionID
,ail.FechaModificacion = GETDATE()
FROM AlmacenInventarioLote ail
INNER JOIN ProgramacionMateriaPrima pmp on ail.AlmacenInventarioLoteID = pmp.InventarioLoteIDOrigen
INNER JOIN PesajeMateriaPrima pm on pmp.ProgramacionMateriaPrimaID = pm.ProgramacionMateriaPrimaID
INNER JOIN @AlmacenInventarioLote ailtemp on pm.PesajeMateriaPrimaID = ailtemp.PesajeMateriaPrimaID
SET NOCOUNT OFF;

GO
