USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Reparto_Crear
--======================================================
CREATE PROCEDURE [dbo].[Reparto_Crear]
@OrganizacionID int,
@CorralID int,
@LoteID int,
@Fecha smalldatetime,
@PesoInicio int,
@PesoProyectado int,
@DiasEngorda int,
@PesoRepeso int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	IF(@LoteID = 0)
		BEGIN 
			SET @LoteID = NULL;
		END
	INSERT Reparto (		
		OrganizacionID,
		CorralID,
		LoteID,
		Fecha,
		PesoInicio,
		PesoProyectado,
		DiasEngorda,
		PesoRepeso,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(		
		@OrganizacionID,
		@CorralID,
		@LoteID,
		@Fecha,
		@PesoInicio,
		@PesoProyectado,
		@DiasEngorda,
		@PesoRepeso,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
