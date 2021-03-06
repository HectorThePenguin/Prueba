USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimentoDetalle_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RepartoAlimentoDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimentoDetalle_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : RepartoAlimentoDetalle_Crear
--======================================================
CREATE PROCEDURE [dbo].[RepartoAlimentoDetalle_Crear]
@RepartoAlimentoDetalleID int,
@RepartoAlimentoID int,
@FolioReparto int,
@FormulaIDRacion int,
@Tolva varchar(10),
@KilosEmbarcados int,
@KilosRepartidos int,
@Sobrante int,
@PesoFinal int,
@CorralIDInicio int,
@CorralIDFinal int,
@HoraRepartoInicio varchar(5),
@HoraRepartoFinal varchar(5),
@Observaciones varchar(255),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT RepartoAlimentoDetalle (
		RepartoAlimentoDetalleID,
		RepartoAlimentoID,
		FolioReparto,
		FormulaIDRacion,
		Tolva,
		KilosEmbarcados,
		KilosRepartidos,
		Sobrante,
		PesoFinal,
		CorralIDInicio,
		CorralIDFinal,
		HoraRepartoInicio,
		HoraRepartoFinal,
		Observaciones,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@RepartoAlimentoDetalleID,
		@RepartoAlimentoID,
		@FolioReparto,
		@FormulaIDRacion,
		@Tolva,
		@KilosEmbarcados,
		@KilosRepartidos,
		@Sobrante,
		@PesoFinal,
		@CorralIDInicio,
		@CorralIDFinal,
		@HoraRepartoInicio,
		@HoraRepartoFinal,
		@Observaciones,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
