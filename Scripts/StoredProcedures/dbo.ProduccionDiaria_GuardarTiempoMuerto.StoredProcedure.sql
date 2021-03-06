USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiaria_GuardarTiempoMuerto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionDiaria_GuardarTiempoMuerto]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiaria_GuardarTiempoMuerto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 24/06/2014
-- Description:  Guardar el detalle de los tiempos muertos en la Produccion Diaria
-- ProduccionDiaria_GuardarTiempoMuerto
-- ===============================================================
CREATE PROCEDURE [dbo].[ProduccionDiaria_GuardarTiempoMuerto] @XmlTiempoMuerto XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TiempoMuerto AS TABLE (
		TiempoMuertoID INT
		,ProduccionDiariaID INT
		,RepartoAlimentoID INT
		,HoraInicio VARCHAR(5)
		,HoraFin VARCHAR(5)
		,CausaTiempoMuertoID INT
		,Activo BIT
		,UsuarioCreacionID INT
		,UsuarioModificacionID INT
		)
	INSERT @TiempoMuerto (
		TiempoMuertoID
		,ProduccionDiariaID
		,RepartoAlimentoID
		,HoraInicio
		,HoraFin
		,CausaTiempoMuertoID
		,Activo
		,UsuarioCreacionID
		,UsuarioModificacionID
		)
	SELECT TiempoMuertoID = t.item.value('./TiempoMuertoID[1]', 'INT')
		,ProduccionDiariaID = t.item.value('./ProduccionDiariaID[1]', 'INT')
		,RepartoAlimentoID = t.item.value('./RepartoAlimentoID[1]', 'INT')
		,HoraInicio = t.item.value('./HoraInicio[1]', 'VARCHAR(5)')
		,HoraFin = t.item.value('./HoraFin[1]', 'VARCHAR(5)')
		,CausaTiempoMuertoID = t.item.value('./CausaTiempoMuertoID[1]', 'INT')
		,Activo = t.item.value('./Activo[1]', 'BIT')
		,UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
		,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM @XmlTiempoMuerto.nodes('ROOT/TiempoMuerto') AS T(item)
	UPDATE tm
	SET ProduccionDiariaID = dt.ProduccionDiariaID
		,RepartoAlimentoID = dt.RepartoAlimentoID
		,HoraInicio = dt.HoraInicio
		,HoraFin = dt.HoraFin
		,CausaTiempoMuertoID = dt.CausaTiempoMuertoID
		,Activo = dt.Activo
		,[FechaModificacion] = GETDATE()
		,UsuarioModificacionID = dt.UsuarioModificacionID
	FROM TiempoMuerto tm
	INNER JOIN @TiempoMuerto dt ON dt.TiempoMuertoID = tm.TiempoMuertoID
	INSERT TiempoMuerto (
		ProduccionDiariaID
		,RepartoAlimentoID
		,HoraInicio
		,HoraFin
		,CausaTiempoMuertoID
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		)
	SELECT ProduccionDiariaID
		,RepartoAlimentoID
		,HoraInicio
		,HoraFin
		,CausaTiempoMuertoID
		,Activo
		,GETDATE()
		,UsuarioCreacionID
	FROM @TiempoMuerto
	WHERE TiempoMuertoID = 0
	SET NOCOUNT OFF;
END

GO
