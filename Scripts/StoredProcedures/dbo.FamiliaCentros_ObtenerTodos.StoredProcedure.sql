IF object_id('dbo.FamiliaCentros_ObtenerTodos', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.FamiliaCentros_ObtenerTodos
END
GO
-- ===============================================================  
-- Author:      Sergio Alberto Gamez Gomez
-- Create date: 13/11/2015  
-- Description: Obtenener Familias
-- FamiliaCentros_ObtenerTodos  
-- ===============================================================  
CREATE PROCEDURE [dbo].[FamiliaCentros_ObtenerTodos]  
@Activo BIT = NULL  
AS  
BEGIN  
SET NOCOUNT ON;  
	SELECT  
		FamiliaID,  
		Descripcion,  
		Activo  
	FROM Familia  
	WHERE Activo = @Activo OR @Activo IS NULL  
SET NOCOUNT OFF;  
END