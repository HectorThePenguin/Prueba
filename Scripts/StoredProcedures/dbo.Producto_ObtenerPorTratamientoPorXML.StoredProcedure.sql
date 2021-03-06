USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorTratamientoPorXML]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorTratamientoPorXML]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorTratamientoPorXML]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 11/02/2015
-- Description:  Obtener listado de Corrales Cerrados para programacion de reimplante de ganado
-- Producto_ObtenerPorTratamientoPorXML
-- =============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorTratamientoPorXML] @xmlTratamientos XML
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #tTratamientos (
		OrganizacionID INT
		,CodigoTratamiento INT
		,Sexo CHAR
		,TipoTratamiento INT
		)
	INSERT INTO #tTratamientos
	SELECT T.N.value('./OrganizacionID[1]', 'INT') AS OrganizacionID
		,T.N.value('./CodigoTratamiento[1]', 'INT') AS CodigoTratamiento
		,T.N.value('./Sexo[1]', 'CHAR(1)') AS Sexo
		,T.N.value('./TipoTratamiento[1]', 'CHAR(1)') AS TipoTratamiento
	FROM @xmlTratamientos.nodes('/ROOT/Tratamientos') AS T(N)
	SELECT P.ProductoID
		,P.Descripcion
		,P.SubFamiliaID
		,P.UnidadID
		,P.Activo
		,TP.Dosis
		, TP.TratamientoID
	FROM Tratamiento T
	INNER JOIN TratamientoProducto TP ON T.TratamientoID = TP.TratamientoID
	INNER JOIN Producto P ON TP.ProductoID = P.ProductoID
	INNER JOIN #tTratamientos xT ON (
			T.CodigoTratamiento = xT.CodigoTratamiento
			AND T.TipoTratamientoID = xT.TipoTratamiento
			AND T.OrganizacionID = xT.OrganizacionID
			AND T.Sexo = xT.Sexo
			)
	WHERE P.Activo = 1
		AND T.Activo = 1
		AND TP.Activo = 1
	SET NOCOUNT OFF;
END

GO
