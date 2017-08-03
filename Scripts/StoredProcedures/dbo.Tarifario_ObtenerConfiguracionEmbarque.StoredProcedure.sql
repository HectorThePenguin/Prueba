USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Tarifario_ObtenerConfiguracionEmbarque]    Script Date: 22/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Tarifario_ObtenerConfiguracionEmbarque]
GO
/****** Object:  StoredProcedure [dbo].[Tarifario_ObtenerConfiguracionEmbarque]    Script Date: 22/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Lorenzo Antonio Villaseñor Martínez
-- Create date: 22-05-2017
-- Description: sp para validar si existen configuraciones de embarques activos
-- SpName     : Tarifario_ObtenerConfiguracionEmbarque 1
--======================================================  
CREATE PROCEDURE [dbo].[Tarifario_ObtenerConfiguracionEmbarque]
@Activo BIT = NULL,
@organizacionOrigenId INT,
@organizacionDestinoId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1
		ConfiguracionEmbarqueID,
		OrganizacionOrigenID,
		OrganizacionDestinoID,
		Kilometros,
		Horas,
		Activo
	FROM ConfiguracionEmbarque
  WHERE 
		(@organizacionOrigenId = 0 or OrganizacionOrigenID = @organizacionOrigenId) AND
		(@organizacionDestinoId = 0 or OrganizacionDestinoID = @organizacionDestinoId) AND
		Activo = @Activo OR @Activo IS NULL
  SET NOCOUNT OFF;
END
