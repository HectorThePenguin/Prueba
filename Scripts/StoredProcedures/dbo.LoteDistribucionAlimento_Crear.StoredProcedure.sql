USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteDistribucionAlimento_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteDistribucionAlimento_Crear]
GO
/****** Object:  StoredProcedure [dbo].[LoteDistribucionAlimento_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 18/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LoteDistribucionAlimento_Crear
--======================================================
CREATE PROCEDURE [dbo].[LoteDistribucionAlimento_Crear]
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
	INSERT LoteDistribucionAlimento (
		LoteDistribucionAlimentoID,
		LoteID,
		TipoServicioID,
		EstatusDistribucionID,
		Fecha,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@LoteDistribucionAlimentoID,
		@LoteID,
		@TipoServicioID,
		@EstatusDistribucionID,
		@Fecha,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
