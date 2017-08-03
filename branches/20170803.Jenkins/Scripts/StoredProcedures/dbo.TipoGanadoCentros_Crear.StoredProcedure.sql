IF object_id('dbo.TipoGanadoCentros_Crear', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.TipoGanadoCentros_Crear
END
GO
CREATE PROCEDURE [dbo].[TipoGanadoCentros_Crear]  
@Descripcion varchar(50),  
@Sexo char(1),  
@PesoMinimo int,  
@PesoMaximo int,  
@PesoSalida int,  
@Activo bit,  
@UsuarioCreacionID int  
AS  
BEGIN  
SET NOCOUNT ON;
  
	DECLARE @ID INT
	
	SELECT @ID = ISNULL(MAX(TipoGanadoID),0) + 1 FROM Sukarne.dbo.CatTipoGanado

	INSERT Sukarne.dbo.CatTipoGanado (TipoGanadoID, Descripcion, Sexo, PesoMinimo, PesoMaximo, PesoSalida, Activo, UsuarioCreacionID, FechaCreacion)  
	VALUES(@ID, @Descripcion, @Sexo, @PesoMinimo, @PesoMaximo, @PesoSalida, @Activo, @UsuarioCreacionID, GETDATE())
	  
	SELECT TipoGanadoID, Descripcion, Sexo, PesoMinimo, PesoMaximo, PesoSalida, Activo, UsuarioCreacionID, FechaCreacion FROM Sukarne.dbo.CatTipoGanado WHERE TipoGanadoID = @ID
SET NOCOUNT OFF;  
END