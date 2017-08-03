IF object_id('dbo.SubFamiliaCentros_ObtenerTodos', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.SubFamiliaCentros_ObtenerTodos
END
GO
-- ===============================================================  
-- Author:      Sergio Alberto Gamez Gomez
-- Create date: 13/11/2015  
-- Description: Obtenener SubFamilias
-- SubFamiliaCentros_ObtenerTodos  
-- ===============================================================  
CREATE PROCEDURE [dbo].[SubFamiliaCentros_ObtenerTodos]  
@Activo BIT = NULL  
AS  
BEGIN  
SET NOCOUNT ON;  
	SELECT  
		SF.FamiliaID,  
		SF.SubFamiliaID,  
		SF.Descripcion,  
		SF.Activo,  
		F.Descripcion AS Familia  
	FROM Sukarne.dbo.CatSubFamilia SF  
	INNER JOIN Sukarne.dbo.CatFamilia F  
	ON (SF.FamiliaID = F.FamiliaID)   
	WHERE SF.Activo = @Activo OR @Activo IS NULL  
SET NOCOUNT OFF;  
END  