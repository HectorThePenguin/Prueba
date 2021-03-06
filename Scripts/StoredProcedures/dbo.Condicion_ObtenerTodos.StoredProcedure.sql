USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Condicion_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Condicion_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Condicion_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 17-10-2013
-- Description:	Obtiene una lista de condiciones 
-- Condicion_ObtenerTodos
-- =============================================
CREATE PROCEDURE [dbo].[Condicion_ObtenerTodos]
@Activo BIT = NULL		
AS
BEGIN
	SET NOCOUNT ON;
    SELECT 
	CondicionID,
	Descripcion,
	Activo
	FROM Condicion
	WHERE (
			Activo = @Activo
			OR @Activo IS NULL
			)		
END

GO
