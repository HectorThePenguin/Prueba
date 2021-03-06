USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraDetalle_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladoraDetalle_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraDetalle_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 23/06/2014
-- Description:  Obtener CheckList
-- CheckListRoladoraDetalle_Guardar
-- =============================================
CREATE PROCEDURE [dbo].[CheckListRoladoraDetalle_Guardar]
@XmlCheckListRoladoraDetalle XML
AS
BEGIN
	CREATE TABLE #tDetalle
	(
		CheckListRoladoraID			INT
		, CheckListRoladoraRangoID	INT
		, CheckListRoladoraAccionID	INT
		, Activo					BIT
		, UsuarioCreacionID			INT
	)
	INSERT INTO #tDetalle
		SELECT t.item.value('./CheckListRoladoraID[1]', 'INT')
		,  t.item.value('./CheckListRoladoraRangoID[1]', 'INT')
		,  t.item.value('./CheckListRoladoraAccionID[1]', 'INT')
		,  t.item.value('./Activo[1]', 'BIT')
		,  t.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM @XmlCheckListRoladoraDetalle.nodes('ROOT/CheckListRoladoraDetalle') AS T(item)
	UPDATE #tDetalle
	SET CheckListRoladoraAccionID = NULL
	WHERE CheckListRoladoraAccionID = 0
	INSERT INTO CheckListRoladoraDetalle
	(
		CheckListRoladoraID
		, CheckListRoladoraRangoID
		, CheckListRoladoraAccionID
		, Activo
		, FechaCreacion
		, UsuarioCreacionID
	)
	SELECT CheckListRoladoraID
		,  CheckListRoladoraRangoID
		,  CheckListRoladoraAccionID
		,  Activo
		,  GETDATE()
		,  UsuarioCreacionID
	FROM #tDetalle
END

GO
