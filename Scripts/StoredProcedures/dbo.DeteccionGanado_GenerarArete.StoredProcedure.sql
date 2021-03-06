USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_GenerarArete]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_GenerarArete]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_GenerarArete]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 20/02/2014
-- Description:	Obtiene los grados a mostrar
	/*DECLARE @Variable varchar(15)
	EXEC DeteccionGanado_GenerarArete '000100001593',@AreteCalculado = @Variable OUTPUT
	SELECT @Variable*/
--======================================================
CREATE PROCEDURE [dbo].[DeteccionGanado_GenerarArete]
@Inicio VARCHAR(12),
@AreteCalculado VARCHAR(15) OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @AretesCalculados TABLE(Arete INT)
	INSERT INTO @AretesCalculados
	SELECT ISNULL((SELECT TOP 1 CAST(RIGHT(Arete,3) AS INT)
									FROM Muertes
									WHERE SUBSTRING(Arete,1,12) LIKE '%'+@Inicio+'%' ORDER BY 1 DESC ),0 )
	SET @AreteCalculado = (SELECT TOP 1 (@Inicio+RIGHT('000'+CAST(MAX(Arete)+1 AS VARCHAR(3)),3)) FROM @AretesCalculados)
END

GO
