USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiaria_GuardarProduccionDiariaDetalle]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionDiaria_GuardarProduccionDiariaDetalle]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiaria_GuardarProduccionDiariaDetalle]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 24/06/2014
-- Description:  Guardar el detalle de Produccion Diaria
-- ProduccionDiaria_GuardarProduccionDiariaDetalle
-- ===============================================================
CREATE PROCEDURE [dbo].[ProduccionDiaria_GuardarProduccionDiariaDetalle] @XmlProduccionDiariaDetalle XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @ProduccionDiariaDetalle AS TABLE (
		ProduccionDiariaDetalleID INT
		,ProduccionDiariaID INT
		,ProductoID INT
		,PesajeMateriaPrimaID INT
		,EspecificacionForraje INT
		,HoraInicial VARCHAR(5)
		,HoraFinal VARCHAR(5)
		,Activo BIT
		,UsuarioCreacionID INT
		,UsuarioModificacionID INT
		)
	INSERT @ProduccionDiariaDetalle (
		ProduccionDiariaDetalleID
		,ProduccionDiariaID
		,ProductoID
		,PesajeMateriaPrimaID
		,EspecificacionForraje
		,HoraInicial
		,HoraFinal
		,Activo
		,UsuarioCreacionID
		,UsuarioModificacionID
		)
	SELECT ProduccionDiariaDetalleID = t.item.value('./ProduccionDiariaDetalleID[1]', 'INT')
		,ProduccionDiariaID = t.item.value('./ProduccionDiariaID[1]', 'INT')
		,ProductoID = t.item.value('./ProductoID[1]', 'INT')
		,PesajeMateriaPrimaID = t.item.value('./PesajeMateriaPrimaID[1]', 'INT')
		,EspecificacionForraje = t.item.value('./EspecificacionForraje[1]', 'INT')
		,HoraInicial = t.item.value('./HoraInicial[1]', 'VARCHAR(5)')
		,HoraFinal = t.item.value('./HoraFinal[1]', 'VARCHAR(5)')
		,Activo = t.item.value('./Activo[1]', 'BIT')
		,UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
		,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM @XmlProduccionDiariaDetalle.nodes('ROOT/ProduccionDiariaDetalle') AS T(item)
	UPDATE pdd
	SET ProduccionDiariaID = dt.ProduccionDiariaID
		,ProductoID = dt.ProductoID
		,PesajeMateriaPrimaID = dt.PesajeMateriaPrimaID
		,EspecificacionForraje = dt.EspecificacionForraje
		,HoraInicial = dt.HoraInicial
		,HoraFinal = dt.HoraFinal
		,Activo = dt.Activo
		,[FechaModificacion] = GETDATE()
		,UsuarioModificacionID = dt.UsuarioModificacionID
	FROM ProduccionDiariaDetalle pdd
	INNER JOIN @ProduccionDiariaDetalle dt ON dt.ProduccionDiariaDetalleID = pdd.ProduccionDiariaDetalleID
	INSERT ProduccionDiariaDetalle (
		ProduccionDiariaID
		,ProductoID
		,PesajeMateriaPrimaID
		,EspecificacionForraje
		,HoraInicial
		,HoraFinal
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		)
	SELECT ProduccionDiariaID
		,ProductoID
		,PesajeMateriaPrimaID
		,EspecificacionForraje
		,HoraInicial
		,HoraFinal
		,Activo
		,GETDATE()
		,UsuarioCreacionID
	FROM @ProduccionDiariaDetalle
	WHERE ProduccionDiariaDetalleID = 0
	SET NOCOUNT OFF;
END

GO
