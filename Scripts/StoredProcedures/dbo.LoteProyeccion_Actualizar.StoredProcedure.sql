USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteProyeccion_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LoteProyeccion_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[LoteProyeccion_Actualizar]
@LoteProyeccionID int,
@LoteID int,
@OrganizacionID int,
@Frame decimal(10,2),
@GananciaDiaria decimal(10,2),
@ConsumoBaseHumeda decimal(10,2),
@Conversion decimal(10,2),
@PesoMaduro int,
@PesoSacrificio int,
@DiasEngorda int,
@FechaEntradaZilmax smalldatetime,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE LoteProyeccion SET
		LoteID = @LoteID,
		OrganizacionID = @OrganizacionID,
		Frame = @Frame,
		GananciaDiaria = @GananciaDiaria,
		ConsumoBaseHumeda = @ConsumoBaseHumeda,
		Conversion = @Conversion,
		PesoMaduro = @PesoMaduro,
		PesoSacrificio = @PesoSacrificio,
		DiasEngorda = @DiasEngorda,
		FechaEntradaZilmax = @FechaEntradaZilmax,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE LoteProyeccionID = @LoteProyeccionID
	SET NOCOUNT OFF;
END

GO
