USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAlertas_CrearNueva]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionAlertas_CrearNueva]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAlertas_CrearNueva]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Diego Rivera
-- Create date: 17/03/2016
-- Description: Inserta un nuevo registro para AlertaConfiguracion
--=============================================
CREATE PROCEDURE [dbo].[ConfiguracionAlertas_CrearNueva]
	@AlertaID INT,
	@Datos VARCHAR(MAX),
	@Fuentes VARCHAR(MAX),
	@Condiciones VARCHAR(MAX),
	@Agrupador VARCHAR(MAX),
	@Activo bit,
	@UsuarioCreacionID INT,
	@NivelAlertaID INT,
	@XmlAcciones XML
AS
BEGIN
	SET NOCOUNT ON;
	INSERT AlertaConfiguracion(
		AlertaID, 
		Datos, 
		Fuentes,
		Condiciones,
		Agrupador,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		NivelAlertaID
	)
	VALUES(
		@AlertaID,
		@Datos,
		@Fuentes,
		@Condiciones,
		@Agrupador,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID,
		@NivelAlertaID
	)

	INSERT AlertaAccion
		(
				AlertaID
			,	AccionID
			,	Activo
			,	FechaCreacion
			,	UsuarioCreacionID
		)
		SELECT 
				  @AlertaID AS AlertaID
				, t.item.value('./AccionID[1]', 'INT') AS AccionID
				, t.item.value('./Activo[1]', 'BIT') AS Activo
				, GETDATE() AS FechaCreacion
				, @UsuarioCreacionID AS UsuarioCreacionID
		FROM  @XmlAcciones.nodes('ROOT/Acciones') AS T(item)

	SET NOCOUNT OFF;
END