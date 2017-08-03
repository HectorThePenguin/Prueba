IF object_id('dbo.TipoGanadoCentros_ObtenerPorDescripcion', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.TipoGanadoCentros_ObtenerPorDescripcion
END
GO
--======================================================  
-- Author     : Sergio Alberto Gámez Gómez 
-- Create date: 05/11/2015
-- Description:   
-- SpName     : TipoGanadoCentros_ObtenerPorDescripcion  ''
--======================================================  
CREATE PROCEDURE [dbo].[TipoGanadoCentros_ObtenerPorDescripcion]  
@Descripcion varchar(50)  
AS  
BEGIN  
SET NOCOUNT ON;  
	SELECT  
		TipoGanadoID,  
		Descripcion,  
		Sexo,  
		PesoMinimo,  
		PesoMaximo,  
		Activo  
	FROM Sukarne.dbo.CatTipoGanado  
	WHERE Descripcion = @Descripcion  
SET NOCOUNT OFF;  
END