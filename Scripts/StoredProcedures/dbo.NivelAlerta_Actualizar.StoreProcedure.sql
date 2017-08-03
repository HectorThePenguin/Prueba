
USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[NivelAlerta_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramón Abel Atondo Echavarria
-- Create date: 15/03/2016
-- Description: SP para actualizar NivelAlerta.
-- SpName     : dbo.NivelAlerta_Actualizar
-- --======================================================
create PROCEDURE [dbo].[NivelAlerta_Actualizar]
@NivelAlertaID int,
@Descripcion varchar(150),
@Activo varchar(50),
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE NivelAlerta SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = @UsuarioModificacionID
	WHERE NivelAlertaID = @NivelAlertaID
	SET NOCOUNT OFF;
END