USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_ObtenerPorFamilia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SubFamilia_ObtenerPorFamilia]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_ObtenerPorFamilia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SubFamilia_ObtenerPorFamilia
--======================================================
CREATE PROCEDURE [dbo].[SubFamilia_ObtenerPorFamilia]
@XmlFamilias XML
, @SubFamiliaID INT
AS
SET NOCOUNT ON
	SELECT f.FamiliaID
		,f.Descripcion		AS DescripcionFamilia
		,sf.SubFamiliaID
		,sf.Descripcion	AS DescripcionSubFamilia
		,sf.Activo
	FROM SubFamilia sf
	INNER JOIN
	(
		SELECT FamiliaID = t.item.value('./FamiliaID[1]', 'INT')
		FROM @XmlFamilias.nodes('ROOT/Familia') AS T(item)
	) xF ON (SF.FamiliaID = xF.FamiliaID)
	INNER JOIN Familia F
		ON (xF.FamiliaID = F.FamiliaID)
	WHERE SF.SubFamiliaID = @SubFamiliaID
SET NOCOUNT OFF

GO
