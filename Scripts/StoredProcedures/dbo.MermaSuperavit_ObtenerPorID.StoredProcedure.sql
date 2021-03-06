USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MermaSuperavit_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 13/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : MermaSuperavit_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[MermaSuperavit_ObtenerPorID]
@MermaSuperavitID int
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
	WHERE MermaSuperavitID = @MermaSuperavitID
	SET NOCOUNT OFF;
END

GO
