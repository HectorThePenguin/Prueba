USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GrupoFormulario_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GrupoFormulario_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[GrupoFormulario_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 10/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : GrupoFormulario_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[GrupoFormulario_Actualizar]
@XmlGrupoFormulario XML
AS
BEGIN
	SET NOCOUNT ON;
	DELETE GF
	FROM GrupoFormulario GF
	INNER JOIN 
	(
		SELECT 
				GrupoID			= t.item.value('./GrupoID[1]', 'INT'),
				FormularioID	= t.item.value('./FormularioID[1]', 'INT'),
				AccesoID        = t.item.value('./AccesoID[1]', 'INT')
			FROM   @XmlGrupoFormulario.nodes('ROOT/GrupoFormulario') AS T(item)
	) Gx
		ON (GF.GrupoID = Gx.GrupoID
			AND GF.FormularioID = Gx.FormularioID
			AND Gx.AccesoID = 0)
	UPDATE GF
	SET    AccesoID = Gx.AccesoID
	FROM   GrupoFormulario GF
	INNER JOIN 
	(
		SELECT 
				GrupoID			= t.item.value('./GrupoID[1]', 'INT'),
				FormularioID	= t.item.value('./FormularioID[1]', 'INT'),
				AccesoID        = t.item.value('./AccesoID[1]', 'INT')
			FROM   @XmlGrupoFormulario.nodes('ROOT/GrupoFormulario') AS T(item)
	) Gx
		ON (GF.GrupoID = Gx.GrupoID
			AND GF.FormularioID = Gx.FormularioID
			AND Gx.AccesoID > 0)
	INSERT INTO GrupoFormulario(GrupoID, FormularioID, AccesoID)
	SELECT Gx.GrupoID
		,  Gx.FormularioID
		,  Gx.AccesoID
	FROM   GrupoFormulario GF
	RIGHT JOIN
	(
		SELECT 
				GrupoID			= t.item.value('./GrupoID[1]', 'INT'),
				FormularioID	= t.item.value('./FormularioID[1]', 'INT'),
				AccesoID        = t.item.value('./AccesoID[1]', 'INT')
			FROM   @XmlGrupoFormulario.nodes('ROOT/GrupoFormulario') AS T(item)
	) Gx
		ON (GF.GrupoID = Gx.GrupoID
			AND GF.FormularioID = Gx.FormularioID)
	WHERE GF.GrupoID IS NULL
		AND GF.FormularioID IS NULL
		AND Gx.AccesoID > 0
	SET NOCOUNT OFF;
END

GO
