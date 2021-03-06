USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TratamientoProducto_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TratamientoProducto_Crear
--======================================================
CREATE PROCEDURE [dbo].[TratamientoProducto_Crear]
@TratamientoID int,
@ProductoID int,
@Dosis int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT TratamientoProducto (
		TratamientoID,
		ProductoID,
		Dosis,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@TratamientoID,
		@ProductoID,
		@Dosis,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
