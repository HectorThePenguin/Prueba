USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ConsultaCorralCorraleta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ConsultaCorralCorraleta]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ConsultaCorralCorraleta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/************************************
Autor	  : Pedro Delgado
Fecha	  : 20/03/2014
Proposito : Obtiene corral por lote
************************************/
CREATE PROCEDURE [dbo].[DeteccionGanado_ConsultaCorralCorraleta]
@LoteId INT,
@OrganizacionID INT
AS 
BEGIN
		SELECT C.CorralID,C.OrganizacionID,C.Codigo,C.TipoCorralID,TC.GrupoCorralID, C.Seccion, C.Orden
		FROM Corral (NOLOCK) C
		INNER JOIN Lote (NOLOCK) L
		ON (C.CorralID = L.CorralID)
		INNER JOIN TipoCorral (NOLOCK) TC
		ON (L.TipoCorralID = TC.TipoCorralID)
		WHERE LoteID = @LoteId AND C.OrganizacionID = @OrganizacionID
END

GO
