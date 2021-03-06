USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteDistribucionAlimento_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteDistribucionAlimento_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[LoteDistribucionAlimento_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 18/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LoteDistribucionAlimento_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[LoteDistribucionAlimento_Actualizar]
@LoteDistribucionAlimentoID int,
@LoteID int,
@TipoServicioID int,
@EstatusDistribucionID int,
@Fecha smalldatetime,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE LoteDistribucionAlimento SET
		LoteID = @LoteID,
		TipoServicioID = @TipoServicioID,
		EstatusDistribucionID = @EstatusDistribucionID,
		Fecha = @Fecha,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioCreacionID,
		FechaModificacion = GETDATE()
	WHERE LoteDistribucionAlimentoID = @LoteDistribucionAlimentoID
	SET NOCOUNT OFF;
END

GO
