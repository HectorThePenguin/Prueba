USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ConsultaCorralRecepcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ConsultaCorralRecepcion]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ConsultaCorralRecepcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 08/03/2014
-- Description:	Obtiene el arete y arete metalico de Muertes.
-- [DeteccionGanado_ConsultaCorralRecepcion] '001'
--======================================================
CREATE PROCEDURE [dbo].[DeteccionGanado_ConsultaCorralRecepcion]
@OrganizacionID INT,
@CodigoCorral VARCHAR(10),
@Arete VARCHAR(15)
AS 
BEGIN
	DECLARE @LoteID INT
	DECLARE @CorralID INT
	SELECT TOP 1 @LoteID = L.LoteID,@CorralID = C.CorralID
	  FROM Lote (NOLOCK) L
	 INNER JOIN Corral (NOLOCK) C ON (L.CorralID = C.CorralID)
	 WHERE C.Codigo = @CodigoCorral AND L.Activo = 1 AND C.Activo = 1 AND C.OrganizacionID = @OrganizacionID
	SELECT I.SalidaID 
	  FROM InterfaceSalida (NOLOCK) I
	 INNER JOIN InterfaceSalidaAnimal ISA ON I.SalidaID = ISA.SalidaID AND I.OrganizacionID = ISA.OrganizacionID
	 INNER JOIN EntradaGanado (NOLOCK) EG ON (EG.FolioOrigen = I.SalidaID
									     AND EG.OrganizacionOrigenID = I.OrganizacionID)
										 --AND EG.OrganizacionID = I.OrganizacionDestinoID)
	 WHERE EG.LoteID = @LoteID 
	   AND EG.CorralID = @CorralID
	   AND ISA.Arete = @Arete
	   AND (ISA.AnimalID IS NULL or ISA.AnimalID = 0)
END

GO
