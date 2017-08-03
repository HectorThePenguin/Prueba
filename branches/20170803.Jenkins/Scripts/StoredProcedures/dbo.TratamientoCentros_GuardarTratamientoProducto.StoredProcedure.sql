IF object_id('dbo.TratamientoCentros_GuardarTratamientoProducto', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.TratamientoCentros_GuardarTratamientoProducto
END
GO
-- ===============================================================================
-- Author:		Sergio Alberto Gamez Gomez
-- Create date: 17/11/2015
-- Description: Guardar el detalle de Tratamiento Producto en Sistema de Centros
-- TratamientoCentros_GuardarTratamientoProducto
-- ===============================================================================
CREATE PROCEDURE [dbo].[TratamientoCentros_GuardarTratamientoProducto] 
	@XmlTratamientoProducto XML,
	@OrganizacionID INT
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
		,OrganizacionID INT
		,FactorMacho NUMERIC(8,4)
		,FactorHembra NUMERIC(8,4)
		,Factor BIT
		)

	INSERT @TratamientoProducto (
		TratamientoProductoID
		,TratamientoID
		,ProductoID
		,Dosis
		,Activo
		,UsuarioCreacionID
		,UsuarioModificacionID
		,OrganizacionID
		,FactorMacho
		,FactorHembra
		,Factor
		)
	SELECT TratamientoProductoID = t.item.value('./TratamientoProductoID[1]', 'INT')
		,TratamientoID = t.item.value('./TratamientoID[1]', 'INT')
		,ProductoID = t.item.value('./ProductoID[1]', 'INT')
		,Dosis = t.item.value('./Dosis[1]', 'INT')
		,Activo = t.item.value('./Activo[1]', 'BIT')
		,UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
		,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')
		,OrganizacionID = t.item.value('./OrganizacionID[1]', 'INT')
		,FactorMacho = t.item.value('./FactorMacho[1]', 'NUMERIC(8,4)')
		,FactorHembra = t.item.value('./FactorHembra[1]', 'NUMERIC(8,4)')
		,Factor = t.item.value('./Factor[1]', 'BIT')
	FROM @XmlTratamientoProducto.nodes('ROOT/TratamientoProducto') AS T(item)

	-- Registros Existentes
	UPDATE tp
	SET TratamientoID = dt.TratamientoID
		,ProductoID = dt.ProductoID
		,Dosis = dt.Dosis
		,Activo = dt.Activo
		,[FechaModificacion] = GETDATE()
		,UsuarioModificacionID = dt.UsuarioModificacionID
		,FactorMacho = dt.FactorMacho
		,FactorHembra = dt.FactorHembra
		,Factor = dt.Factor
	FROM Sukarne.dbo.CatTratamientoProducto tp
	INNER JOIN @TratamientoProducto dt ON dt.TratamientoProductoID = tp.TratamientoProductoID AND dt.OrganizacionID = tp.OrganizacionID
	
	-- Nuevos Registros
	DECLARE @TratamientoProductoIDMax INT
	SELECT @TratamientoProductoIDMax = ISNULL(MAX(TratamientoProductoID),0)
	FROM Sukarne.dbo.CatTratamientoProducto 
	WHERE OrganizacionID = @OrganizacionID

	INSERT Sukarne.dbo.CatTratamientoProducto (
		TratamientoProductoID
		,TratamientoID
		,ProductoID
		,Dosis
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		,OrganizacionID
		,FactorMacho
		,FactorHembra
		,Factor
		)
	SELECT ROW_NUMBER() OVER(ORDER BY TratamientoID) + @TratamientoProductoIDMax
		,TratamientoID
		,ProductoID
		,Dosis
		,Activo
		,GETDATE()
		,UsuarioCreacionID
		,OrganizacionID
		,FactorMacho
		,FactorHembra
		,Factor
	FROM @TratamientoProducto 
	WHERE TratamientoProductoID = 0

	SET NOCOUNT OFF;
END
GO