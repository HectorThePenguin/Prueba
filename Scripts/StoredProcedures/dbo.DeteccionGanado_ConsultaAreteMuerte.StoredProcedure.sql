USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ConsultaAreteMuerte]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ConsultaAreteMuerte]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ConsultaAreteMuerte]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 08/03/2014
-- Description:	Obtiene el arete y arete metalico de Muertes.
-- [DeteccionGanado_ConsultaAreteMuerte] '4','1'
--======================================================
CREATE PROCEDURE [dbo].[DeteccionGanado_ConsultaAreteMuerte]
@Arete VARCHAR(15),
@AreteTestigo VARCHAR(15)
AS
BEGIN
	SELECT Arete AS Arete
	FROM Muertes (NOLOCK)
	WHERE ((Arete = @Arete AND @Arete != '') OR 
			(AreteMetalico = @AreteTestigo AND @AreteTestigo != ''))  
			AND Activo = 1
END

GO
