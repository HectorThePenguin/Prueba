USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Acciones_ObtenerTodasLasActivas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Acciones_ObtenerTodasLasActivas]
GO
/****** Object:  StoredProcedure [dbo].[Acciones_ObtenerTodasLasActivas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Valenzuela Rivera Juan Diego
-- Create date: 17/03/2016
-- Description: Obtiene todas las Acciones activas
-- Alertas_ObtenerTodas  1
--=============================================
CREATE PROCEDURE [dbo].[Acciones_ObtenerTodasLasActivas]
@Activo bit 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 	A.AccionID, 
			A.Descripcion
		FROM Accion (NOLOCK)  A 
		WHERE A.Activo = @Activo
	SET NOCOUNT OFF;
END