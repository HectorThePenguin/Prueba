USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cliente_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cliente_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Cliente_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: IF EXISTS(SELECT ''
FROM sys.objects
WHERE [object_id] = Object_id(N'[dbo].[Cliente_Actualizar]'))
	DROP PROCEDURE [dbo].[Cliente_Actualizar]; 
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/09/2015 12:00:00 a.m.
-- Description: 
-- SpName     : Cliente_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Cliente_Actualizar]
@ClienteID int,
@CodigoSAP varchar(10),
@Descripcion varchar(150),
@Poblacion varchar(50),
@Estado varchar(50),
@Pais varchar(50),
@Calle varchar(255),
@CodigoPostal varchar(10),
@RFC varchar(13),
@MetodoPagoID int,
@CondicionPago int,
@DiasPago int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Cliente SET
		CodigoSAP = @CodigoSAP,
		Descripcion = @Descripcion,
		Poblacion = @Poblacion,
		Estado = @Estado,
		Pais = @Pais,
		Calle = @Calle,
		CodigoPostal = @CodigoPostal,
		RFC = @RFC,
		MetodoPagoID = @MetodoPagoID,
		CondicionPago = @CondicionPago,
		DiasPago = @DiasPago,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ClienteID = @ClienteID
	SET NOCOUNT OFF;
END
GO


-- SpName     : Cliente_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Cliente_Actualizar]
@ClienteID int,
@CodigoSAP varchar(10),
@Descripcion varchar(150),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Cliente SET
		CodigoSAP = @CodigoSAP,
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ClienteID = @ClienteID
	SET NOCOUNT OFF;
END

GO
