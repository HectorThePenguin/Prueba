USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraHorometro_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladoraHorometro_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraHorometro_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jorge Luis Velazquez Araujo
-- Create date: 08/07/2014
-- Description:  Guardar el detalle del CheckListRoladoraHorometro
-- CheckListRoladoraHorometro_Guardar
-- =============================================
CREATE PROCEDURE [dbo].[CheckListRoladoraHorometro_Guardar] @XmlCheckListRoladoraHorometro XML
AS
BEGIN
	CREATE TABLE #tHorometro (
		CheckListRoladoraHorometroID INT
		,CheckListRoladoraGeneralID INT
		,RoladoraID INT
		,HorometroInicial VARCHAR(5)
		,HorometroFinal VARCHAR(5)
		,Activo BIT
		,UsuarioCreacionID INT
		,UsuarioModificacionID INT
		)
	INSERT INTO #tHorometro
	SELECT t.item.value('./CheckListRoladoraHorometroID[1]', 'INT')
		,t.item.value('./CheckListRoladoraGeneralID[1]', 'INT')
		,t.item.value('./RoladoraID[1]', 'INT')
		,t.item.value('./HorometroInicial[1]', 'varchar(5)')
		,t.item.value('./HorometroFinal[1]', 'varchar(5)')
		,t.item.value('./Activo[1]', 'BIT')
		,t.item.value('./UsuarioCreacionID[1]', 'INT')		
		,t.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM @XmlCheckListRoladoraHorometro.nodes('ROOT/CheckListRoladoraHorometro') AS T(item)
	UPDATE ckh
	SET ckh.CheckListRoladoraGeneralID = ho.CheckListRoladoraGeneralID
		,ckh.RoladoraID = ho.RoladoraID
		,ckh.HorometroInicial = ho.HorometroInicial
		,ckh.HorometroFinal = ho.HorometroFinal
		,ckh.Activo = ho.Activo
		,ckh.UsuarioModificacionID = ho.UsuarioModificacionID
		,ckh.FechaModificacion = GetDATE()
	FROM CheckListRoladoraHorometro ckh
	INNER JOIN #tHorometro ho ON ckh.CheckListRoladoraHorometroID = ho.CheckListRoladoraHorometroID
	INSERT INTO CheckListRoladoraHorometro (
		CheckListRoladoraGeneralID
		,RoladoraID
		,HorometroInicial
		,HorometroFinal
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		)
	SELECT CheckListRoladoraGeneralID
		,RoladoraID
		,HorometroInicial
		,HorometroFinal
		,Activo
		,GETDATE()
		,UsuarioCreacionID
	FROM #tHorometro
	WHERE CheckListRoladoraHorometroID = 0
END

GO
