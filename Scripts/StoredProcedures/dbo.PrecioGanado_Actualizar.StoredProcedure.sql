USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PrecioGanado_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PrecioGanado_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[PrecioGanado_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 17/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : PrecioGanado_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[PrecioGanado_Actualizar]
@PrecioGanadoID int,
@OrganizacionID int,
@TipoGanadoID int,
@Precio decimal(10,2),
@FechaVigencia smalldatetime,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE PrecioGanado SET
		OrganizacionID = @OrganizacionID,
		TipoGanadoID = @TipoGanadoID,
		Precio = @Precio,
		FechaVigencia = @FechaVigencia,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE PrecioGanadoID = @PrecioGanadoID
	SET NOCOUNT OFF;
END

GO
