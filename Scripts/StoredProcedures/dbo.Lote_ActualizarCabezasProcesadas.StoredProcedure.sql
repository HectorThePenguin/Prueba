USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarCabezasProcesadas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ActualizarCabezasProcesadas]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarCabezasProcesadas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/************************************
Autor	  : Jorge Luis Velazquez Araujo
Fecha	  : 13/07/2015
Proposito : Actualizar las Cabezas del Lote
sp_helptext Lote_ActualizarCabezasProcesadas
--001 Jorge Luis Velazquez Araujo 23/12/2015 **Se agrega la validacion para que no se inactiven los Lotes de Enfermeria
*/
CREATE PROCEDURE [dbo].[Lote_ActualizarCabezasProcesadas]
@LoteIDOrigen INT
,@LoteIDDestino INT
, @CabezasProcesadas INT
, @UsuarioModificacionID INT
AS

BEGIN
UPDATE Lote
SET	Cabezas = Cabezas + @CabezasProcesadas,
	FechaModificacion = GETDATE(),
	UsuarioModificacionID = @UsuarioModificacionID,
	CabezasInicio = (CASE
		WHEN (Cabezas + @CabezasProcesadas) > CabezasInicio THEN (Cabezas + @CabezasProcesadas)
		ELSE CabezasInicio
	END)
	where LoteID = @LoteIDDestino

	if @LoteIDOrigen <> 0 and @LoteIDOrigen <> @LoteIDDestino
	UPDATE Lote
SET	Cabezas = Cabezas - @CabezasProcesadas,
	FechaModificacion = GETDATE(),
	UsuarioModificacionID = @UsuarioModificacionID
	where LoteID = @LoteIDORigen

	declare @CabezasOrigen int 
	set @CabezasOrigen = (select Cabezas from Lote where LoteID = @LoteIDORigen)

	declare @CabezasDestino int 
	set @CabezasDestino = (select Cabezas from Lote where LoteID = @LoteIDDestino)

	IF ( @CabezasOrigen <= 0 )
	BEGIN
		UPDATE EntradaGanado
			SET Manejado = 1, 
			UsuarioModificacionID = @UsuarioModificacionID,
			FechaModificacion = GETDATE()
		WHERE LoteID = @LoteIDOrigen
		
		/* Si se sacan todas las cabezas del lote se inactiva el lote */
		UPDATE lo
		SET lo.Activo = 0,
			lo.UsuarioModificacionID = @UsuarioModificacionID,
			lo.FechaModificacion = GETDATE()
			from Lote lo
		inner join TipoCorral tc on lo.TipoCorralID = tc.TipoCorralID --001
		WHERE LoteID = @LoteIDOrigen
		and tc.GrupoCorralID <> 3 --001
		
	
	END

	select @LoteIDOrigen AS LoteIDORigen
	,@CabezasOrigen AS CabezasORigen
	,@LoteIDDestino AS LoteIDDEstino
	,@CabezasDestino AS CabezasDestino
END	

GO
