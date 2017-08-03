USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Accion_ValidarAsignacionesAsignadasByID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Accion_ValidarAsignacionesAsignadasByID]
GO
/****** Object:  StoredProcedure [dbo].[Accion_ValidarAsignacionesAsignadasByID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Manuel Enrique Torres Lugo
-- Create date: 29/03/2016
-- Description: SP para validar si una accion ya fue asignada.
-- SpName     : dbo.Accion_ValidarAsignacionesAsignadasByID
--======================================================
CREATE PROCEDURE [dbo].[Accion_ValidarAsignacionesAsignadasByID]
@ID INT
AS
BEGIN
	DECLARE @result BIT;

	SET NOCOUNT ON;

	SELECT @result = COUNT(*) FROM AlertaAccion WHERE AccionID = @ID

	SELECT @result
	SET NOCOUNT OFF;
END