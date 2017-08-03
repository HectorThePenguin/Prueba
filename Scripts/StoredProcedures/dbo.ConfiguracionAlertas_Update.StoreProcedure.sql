USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAlertasConsultaAccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionAlertasConsultaAccion]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAlertasConsultaAccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Torres Lugo Manuel
-- Create date: 19/03/2016
-- Description: Edita un registro en la tabla AlertaConfiguracion y la relacion que tiene
-- 							en la tabla AlertaAccion
--=============================================
CREATE PROCEDURE [dbo].[ConfiguracionAlertas_Update]
	@AlertaConfiguracionID INT,
	@AlertaID INT,
	@Datos VARCHAR(MAX),
	@Fuentes VARCHAR(MAX),
	@Condiciones VARCHAR(MAX),
	@Agrupador VARCHAR(MAX),
	@Activo bit,
	@UsuarioModificacionID INT,
	@NivelAlertaID INT,
	@XmlAcciones XML
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE AlertaConfiguracion
	SET
		AlertaID							= @AlertaID,
		Datos									= @Datos, 
		Fuentes								=	@Fuentes,
		Condiciones 					= @Condiciones,
		Agrupador							= @Agrupador,
		Activo								= @Activo,
		FechaModificacion 		= GETDATE(),
		UsuarioModificacionID = @UsuarioModificacionID,
		NivelAlertaID					= @NivelAlertaID
	WHERE AlertaConfiguracionID = @AlertaConfiguracionID

	SELECT 
		@AlertaID AS AlertaID,
		t.item.value('./AccionID[1]', 'INT') AS AccionID,
		t.item.value('./AlertaAccionID[1]', 'INT') AS AlertaAccionID,
		t.item.value('./Activo[1]', 'BIT') AS Activo,
		@UsuarioModificacionID AS UsuarioModificacionID
	INTO #TempAlertaAccion
	FROM  @XmlAcciones.nodes('ROOT/Acciones') AS T(item)
--SELECT * FROM #TempAlertaAccion
	INSERT AlertaAccion
	(
		AlertaID,
		AccionID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	)
	SELECT 
		AlertaID,
		AccionID,
		Activo,
		GETDATE(),
		UsuarioModificacionID
	FROM #TempAlertaAccion
	WHERE AlertaAccionID = 0
	
	UPDATE AA
	SET
		AA.Activo = 0,
		AA.FechaModificacion = GETDATE(),
		AA.UsuarioModificacionID = @UsuarioModificacionID
	FROM AlertaAccion AS AA
	INNER JOIN #TempAlertaAccion AS TEM ON TEM.AlertaAccionID = AA.AlertaAccionID
	WHERE TEM.AlertaAccionID > 0

	SET NOCOUNT OFF;
END