USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PrecioGanado_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PrecioGanado_Crear]
GO
/****** Object:  StoredProcedure [dbo].[PrecioGanado_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 17/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : PrecioGanado_Crear
--======================================================
CREATE PROCEDURE [dbo].[PrecioGanado_Crear]
@OrganizacionID int,
@TipoGanadoID int,
@Precio decimal(10,2),
@FechaVigencia smalldatetime,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT PrecioGanado (
		OrganizacionID,
		TipoGanadoID,
		Precio,
		FechaVigencia,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@OrganizacionID,
		@TipoGanadoID,
		@Precio,
		@FechaVigencia,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SET NOCOUNT OFF;
END

GO
