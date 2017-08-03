USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[NivelAlerta_Crear]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramón Abel Atondo Echavarría
-- Create date: 14/03/2016 03:00:00 p.m.
-- Description: Crear los niveles de alertas
-- SpName     : NivelAlerta_Crear
--======================================================
CREATE PROCEDURE [dbo].[NivelAlerta_Crear]
@Descripcion varchar(150),
@Activo BIT,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT NivelAlerta (
		Descripcion,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	)
	VALUES(
		@Descripcion,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
	)
	SET NOCOUNT OFF;
END
