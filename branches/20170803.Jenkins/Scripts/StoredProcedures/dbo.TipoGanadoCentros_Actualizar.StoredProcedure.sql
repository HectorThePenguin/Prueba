IF object_id('dbo.TipoGanadoCentros_Actualizar', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.TipoGanadoCentros_Actualizar
END
GO
--======================================================  
-- Author     : Sergio Alberto Gamez Gomez
-- Create date: 05/11/2015
-- Description:   
-- SpName     : TipoGanadoCentros_Actualizar  
--======================================================  
CREATE PROCEDURE [dbo].[TipoGanadoCentros_Actualizar]  
@TipoGanadoID int,  
@Descripcion varchar(50),  
@Sexo char(1),  
@PesoMinimo int,  
@PesoMaximo int,  
@PesoSalida int,  
@Activo bit,  
@UsuarioModificacionID int  
AS  
BEGIN  
SET NOCOUNT ON;  
	UPDATE Sukarne.dbo.CatTipoGanado SET  
		Descripcion = @Descripcion,  
		Sexo = @Sexo,  
		PesoMinimo = @PesoMinimo,  
		PesoMaximo = @PesoMaximo,  
		PesoSalida = @PesoSalida,  
		Activo = @Activo,  
		UsuarioModificacionID = @UsuarioModificacionID,  
		FechaModificacion = GETDATE()  
	WHERE TipoGanadoID = @TipoGanadoID  
SET NOCOUNT OFF;  
END