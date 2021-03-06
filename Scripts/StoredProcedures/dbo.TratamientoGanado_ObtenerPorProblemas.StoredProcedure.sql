USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoGanado_ObtenerPorProblemas]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TratamientoGanado_ObtenerPorProblemas]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoGanado_ObtenerPorProblemas]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Fecha: 17/02/2014
-- Descripci�n:	Obtiene el tratamiento del ganado de acuerdo al peso y sexo.
-- TratamientoGanado_ObtenerPorProblemas 1, 'H', 423
-- =============================================
CREATE PROCEDURE [dbo].[TratamientoGanado_ObtenerPorProblemas]
	@OrganizacionID INT, 
	@Sexo VARCHAR(1),
	@Peso DECIMAL,
	@XmlProblemas XML,
	@XmlTipos XML
AS
BEGIN	
	DECLARE @Problemas AS TABLE ([ProblemaID] INT)
	DECLARE @TiposTratamientos AS TABLE ([TiposTratamientosID] INT)
	INSERT @Problemas ([ProblemaID])
	SELECT [ProblemaID] = t.item.value('./ProblemaID[1]', 'INT')
	FROM @XmlProblemas.nodes('ROOT/Problemas') AS t(item)
	INSERT @TiposTratamientos ([TiposTratamientosID])
	SELECT [TiposTratamientosID] = t.item.value('./TiposTratamientosID[1]', 'INT')
	FROM @XmlTipos.nodes('ROOT/TiposTratamientos') AS t(item)
	SELECT DISTINCT t.TratamientoID, 
			pt.ProblemaID,
			pt.Dias,
			t.CodigoTratamiento, 
			t.OrganizacionID, 
			t.RangoInicial, 
			t.RangoFinal, 
			t.Sexo, 
			t.TipoTratamientoID
			FROM Tratamiento t(NOLOCK)
			INNER JOIN TratamientoProducto tp(NOLOCK) ON tp.TratamientoID = t.TratamientoID
			INNER JOIN ProblemaTratamiento pt (NOLOCK) ON pt.TratamientoID= t.TratamientoID AND pt.Activo=1
			WHERE  t.OrganizacionID = @OrganizacionID
			AND t.sexo = @Sexo 
			AND @Peso BETWEEN t.RangoInicial AND t.RangoFinal
			AND pt.ProblemaID IN (SELECT ProblemaID FROM @Problemas)
			AND t.TipoTratamientoID IN (SELECT TiposTratamientosID FROM @TiposTratamientos)
			AND t.Activo = 1
			AND tp.Activo = 1
			AND pt.Activo = 1;
END

GO
