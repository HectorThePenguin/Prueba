USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteReimplante_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LoteReimplante_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[LoteReimplante_Actualizar]
@LoteReimplanteID int,
@LoteProyeccionID int,
@NumeroReimplante int,
@FechaProyectada smalldatetime,
@PesoProyectado int,
@FechaReal smalldatetime,
@PesoReal int,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE LoteReimplante SET
		LoteProyeccionID = @LoteProyeccionID,		
		NumeroReimplante = @NumeroReimplante,
		FechaProyectada = @FechaProyectada,
		PesoProyectado = @PesoProyectado,
		FechaReal = @FechaReal,
		PesoReal = @PesoReal,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE LoteReimplanteID = @LoteReimplanteID
	SET NOCOUNT OFF;
END

GO
