USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RevisionReimplante_GuardarRevision]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RevisionReimplante_GuardarRevision]
GO
/****** Object:  StoredProcedure [dbo].[RevisionReimplante_GuardarRevision]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Roque Solis
-- Create date: 09/07/2014
-- Description:  Guardar la lista de Almacen MovimientoDetalle
-- EXEC RevisionReimplante_GuardarRevision 
--'<ROOT>
--  <RevisionReimplante>
--    <LoteID>2722</LoteID>
--    <Fecha>09/12/2014</Fecha>
--    <AreaRevisionID>3</AreaRevisionID>
--    <EstadoImplanteID>2</EstadoImplanteID>
--    <UsuarioCreacionID>5</UsuarioCreacionID>
--  </RevisionReimplante>
--</ROOT>'
-- ===============================================================
CREATE PROCEDURE [dbo].[RevisionReimplante_GuardarRevision] @XmlRevision XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @RevisionReimplanteTmp AS TABLE (
		LoteID int,
		AnimalID int,
		Fecha DATETIME,
		AreaRevisionID int,
		EstadoImplanteID int,
		UsuarioCreacionID int	
		)
	INSERT @RevisionReimplanteTmp (
		LoteID,
		AnimalID,
		Fecha,
		AreaRevisionID,
		EstadoImplanteID,
		UsuarioCreacionID	
		)
	SELECT LoteID = t.item.value('./LoteID[1]', 'INT')
		,AnimalID = t.item.value('./AnimalID[1]', 'INT')
		,Fecha = t.item.value('./Fecha[1]', 'DATETIME')
		,AreaRevisionID = t.item.value('./AreaRevisionID[1]', 'INT')
		,EstadoImplanteID = t.item.value('./EstadoImplanteID[1]', 'INT')
		,UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM @XmlRevision.nodes('ROOT/RevisionReimplante') AS T(item)	
	INSERT RevisionImplante (
		LoteID,
		AnimalID,
		Fecha,
		AreaRevisionID,
		EstadoImplanteID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
		)
	SELECT LoteID,
		AnimalID,
		Fecha,
		AreaRevisionID,
		EstadoImplanteID,
		1,
		GETDATE(),
		UsuarioCreacionID	 
	FROM @RevisionReimplanteTmp
	SET NOCOUNT OFF;
END

GO
