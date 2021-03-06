USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteProyeccion_Crear]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author      : Jorge Luis Velazquez Araujo
-- Create date : 15/01/2014 12:00:00 a.m.
-- Description : 
-- Modifico    : Roque Solis
-- Fecha Mod   : 27/11/2014
-- Modificacion: Se agrega el campo Revision par indicar si se mostrara en la opción de disponibilidad
-- SpName      : sp_helptext LoteProyeccion_Crear
--======================================================
CREATE PROCEDURE [dbo].[LoteProyeccion_Crear]
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
@UsuarioCreacionID int,
@Revision bit
AS
BEGIN
	SET NOCOUNT ON;
	INSERT LoteProyeccion (
		LoteID,
		OrganizacionID,
		Frame,
		GananciaDiaria,
		ConsumoBaseHumeda,
		Conversion,
		PesoMaduro,
		PesoSacrificio,
		DiasEngorda,
		FechaEntradaZilmax,
		UsuarioCreacionID,
		FechaCreacion,
		Revision
	)
	VALUES(
		@LoteID,
		@OrganizacionID,
		@Frame,
		@GananciaDiaria,
		@ConsumoBaseHumeda,
		@Conversion,
		@PesoMaduro,
		@PesoSacrificio,
		@DiasEngorda,
		@FechaEntradaZilmax,
		@UsuarioCreacionID,
		GETDATE(),
		@Revision
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
