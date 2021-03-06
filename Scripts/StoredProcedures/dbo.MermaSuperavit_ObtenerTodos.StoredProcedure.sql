USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MermaSuperavit_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 13/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : MermaSuperavit_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[MermaSuperavit_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		MermaSuperavitID,
		AlmacenID,
		ProductoID,
		Merma,
		Superavit,
		Activo
	FROM MermaSuperavit
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
