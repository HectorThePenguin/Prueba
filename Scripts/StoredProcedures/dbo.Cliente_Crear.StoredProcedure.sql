USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cliente_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cliente_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Cliente_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================IF EXISTS(SELECT ''
FROM sys.objects
WHERE [object_id] = Object_id(N'[dbo].[Cliente_Crear]'))
	DROP PROCEDURE [dbo].[Cliente_Crear]; 
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/09/2015 12:00:00 a.m.
-- Description: 
-- SpName     : Cliente_Crear
--======================================================
CREATE PROCEDURE [dbo].[Cliente_Crear]
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
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Cliente (
		CodigoSAP,
		Descripcion,
		Poblacion,
		Estado,
		Pais,
		Calle,
		CodigoPostal,
		RFC,
		MetodoPagoID,
		CondicionPago,
		DiasPago,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@CodigoSAP,
		@Descripcion,
		@Poblacion,
		@Estado,
		@Pais,
		@Calle,
		@CodigoPostal,
		@RFC,
		@MetodoPagoID,
		@CondicionPago,
		@DiasPago,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END
GO


-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Cliente_Crear
--======================================================
CREATE PROCEDURE [dbo].[Cliente_Crear]
@CodigoSAP varchar(10),
@Descripcion varchar(150),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Cliente (
		CodigoSAP,
		Descripcion,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@CodigoSAP,
		@Descripcion,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
