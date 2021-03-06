USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Tratamiento_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Tratamiento_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Tratamiento_Actualizar]
@TratamientoID int,
@OrganizacionID int,
@CodigoTratamiento int,
@TipoTratamientoID int,
@Sexo char(1),
@RangoInicial int,
@RangoFinal int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Tratamiento SET
		OrganizacionID = @OrganizacionID,
		CodigoTratamiento = @CodigoTratamiento,
		TipoTratamientoID = @TipoTratamientoID,
		Sexo = @Sexo,
		RangoInicial = @RangoInicial,
		RangoFinal = @RangoFinal,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TratamientoID = @TratamientoID
	SET NOCOUNT OFF;
END

GO
