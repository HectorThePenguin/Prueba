USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_DescontarProduccionDiaria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventario_DescontarProduccionDiaria]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_DescontarProduccionDiaria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 13/08/2014
-- Description: Actualizar inventario para descontar la produccion diaria
-- SpName     : AlmacenInventario_DescontarProduccionDiaria '<ROOT>
  /*<AlmacenInventario>
    <PesajeMateriaPrimaID>1</PesajeMateriaPrimaID>
    <KilosNeto>20</KilosNeto>
    <UsuarioModificacionID>5</UsuarioModificacionID>
  </AlmacenInventario>
</ROOT>'
*/
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventario_DescontarProduccionDiaria] 
@AlmacenInventarioXML XML
AS
SET NOCOUNT ON;
	DECLARE @AlmacenInventario AS TABLE (
		PesajeMateriaPrimaID INT
		,KilosNeto INT		
		,UsuarioModificacionID INT
		)
		INSERT @AlmacenInventario (
		PesajeMateriaPrimaID
		,KilosNeto
		,UsuarioModificacionID		
		)
	SELECT PesajeMateriaPrimaID = t.item.value('./PesajeMateriaPrimaID[1]', 'INT')
		,KilosNeto = t.item.value('./KilosNeto[1]', 'INT')
		,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')		
	FROM @AlmacenInventarioXML.nodes('ROOT/AlmacenInventario') AS T(item)
UPDATE ai
set ai.Cantidad = (ai.Cantidad - ailtemp.KilosNeto )
,ai.Importe = (ai.Cantidad - ailtemp.KilosNeto ) * ai.PrecioPromedio
,ai.UsuarioModificacionID = ailtemp.UsuarioModificacionID
,ai.FechaModificacion = GETDATE()
FROM AlmacenInventario ai
INNER JOIN AlmacenInventarioLote ail on ail.AlmacenInventarioID = ai.AlmacenInventarioID
INNER JOIN ProgramacionMateriaPrima pmp on ail.AlmacenInventarioLoteID = pmp.InventarioLoteIDOrigen
INNER JOIN PesajeMateriaPrima pm on pmp.ProgramacionMateriaPrimaID = pm.ProgramacionMateriaPrimaID
INNER JOIN @AlmacenInventario ailtemp on pm.PesajeMateriaPrimaID = ailtemp.PesajeMateriaPrimaID
SET NOCOUNT OFF;

GO
