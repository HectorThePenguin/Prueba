USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoGanado_ObtenerPorTipo]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TratamientoGanado_ObtenerPorTipo]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoGanado_ObtenerPorTipo]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Noel.Gerardo
-- Fecha: 13-12-2013
-- DescripciÛn:	Obtiene el tratamiento del ganado de acuerdo al peso y sexo.
-- TratamientoGanado_ObtenerPorTipo 1, 'H' , 150 , 3
-- =============================================
CREATE PROCEDURE [dbo].[TratamientoGanado_ObtenerPorTipo]
	@OrganizacionID INT,
	@Sexo VARCHAR(1), 
	@Peso DECIMAL,
    @TipoTratamiento INT
AS
BEGIN	
	SELECT DISTINCT t.TratamientoID, t.CodigoTratamiento, t.OrganizacionID, t.RangoInicial, t.RangoFinal, t.Sexo, t.TipoTratamientoID
			FROM Tratamiento t(NOLOCK)
			INNER JOIN TratamientoProducto tp(NOLOCK) ON tp.TratamientoID = t.TratamientoID
			INNER JOIN Producto p(NOLOCK) ON p.ProductoID = tp.ProductoID
			WHERE  t.OrganizacionID = @OrganizacionID 
			AND t.sexo = @Sexo 
			AND @Peso BETWEEN t.RangoInicial AND t.RangoFinal
			AND T.TipoTratamientoID = @TipoTratamiento
			AND T.Activo = 1
			AND p.Activo = 1
			AND tp.Activo = 1;
END

GO
