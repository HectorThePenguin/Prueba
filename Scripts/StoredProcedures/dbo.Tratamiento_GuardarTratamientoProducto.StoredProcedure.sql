USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_GuardarTratamientoProducto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Tratamiento_GuardarTratamientoProducto]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_GuardarTratamientoProducto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 17/01/2014
-- Description:  Guardar el detalle de Tratamiento Producto
-- Tratamiento_GuardarTratamientoProducto
-- ===============================================================
CREATE PROCEDURE [dbo].[Tratamiento_GuardarTratamientoProducto] @XmlTratamientoProducto XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TratamientoProducto AS TABLE (
		TratamientoProductoID INT
		,TratamientoID INT
		,ProductoID INT
		,Dosis INT
		,Activo BIT
		,UsuarioCreacionID INT
		,UsuarioModificacionID INT
		)
	INSERT @TratamientoProducto (
		TratamientoProductoID
		,TratamientoID
		,ProductoID
		,Dosis
		,Activo
		,UsuarioCreacionID
		,UsuarioModificacionID
		)
	SELECT TratamientoProductoID = t.item.value('./TratamientoProductoID[1]', 'INT')
		,TratamientoID = t.item.value('./TratamientoID[1]', 'INT')
		,ProductoID = t.item.value('./ProductoID[1]', 'INT')
		,Dosis = t.item.value('./Dosis[1]', 'INT')
		,Activo = t.item.value('./Activo[1]', 'BIT')
		,UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
		,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM @XmlTratamientoProducto.nodes('ROOT/TratamientoProducto') AS T(item)
	UPDATE tp
	SET TratamientoID = dt.TratamientoID
		,ProductoID = dt.ProductoID
		,Dosis = dt.Dosis
		,Activo = dt.Activo
		,[FechaModificacion] = GETDATE()
		,UsuarioModificacionID = dt.UsuarioModificacionID
	FROM TratamientoProducto tp
	INNER JOIN @TratamientoProducto dt ON dt.TratamientoProductoID = tp.TratamientoProductoID
	INSERT TratamientoProducto (
		TratamientoID
		,ProductoID
		,Dosis
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		)
	SELECT TratamientoID
		,ProductoID
		,Dosis
		,Activo
		,GETDATE()
		,UsuarioCreacionID
	FROM @TratamientoProducto
	WHERE TratamientoProductoID = 0
	SET NOCOUNT OFF;
END

GO
