USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaAnimal_ObtenerAretesPorSalida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaAnimal_ObtenerAretesPorSalida]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaAnimal_ObtenerAretesPorSalida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Ramses Santos
-- Create date: 12/03/2014
-- Origen: APInterfaces
-- Descripcion: Obtener todos los aretes de la salida.
-- EXEC InterfaceSalidaAnimal_ObtenerAretesPorSalida 'F11',1 
-- =============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaAnimal_ObtenerAretesPorSalida]
	@CodigoCorral VARCHAR(10),
	@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @LoteID INT
	DECLARE @CorralID INT
	SELECT @LoteID = L.LoteID,@CorralID = C.CorralID
	  FROM Lote (NOLOCK) L
	 INNER JOIN Corral (NOLOCK) C ON (L.CorralID = C.CorralID)
	 WHERE C.Codigo = @CodigoCorral AND L.Activo = 1 AND C.Activo = 1 AND C.OrganizacionID = @OrganizacionID
	SELECT ISA.OrganizacionID,
		   ISA.SalidaID,
		   ISA.Arete,
		   ISA.FechaCompra,
		   ISA.PesoCompra,
		   ISA.TipoGanadoID,
		   ISA.PesoOrigen,
		   ISA.FechaRegistro,
		   ISA.UsuarioRegistro,
		   EG.FolioEntrada Partida,
		   EG.CorralID
	  FROM InterfaceSalida (NOLOCK) I
	 INNER JOIN InterfaceSalidaAnimal ISA ON I.SalidaID = ISA.SalidaID AND I.OrganizacionID = ISA.OrganizacionID
	 INNER JOIN EntradaGanado (NOLOCK) EG ON (EG.FolioOrigen = I.SalidaID
									     AND EG.OrganizacionOrigenID = I.OrganizacionID)
										 --AND EG.OrganizacionID = I.OrganizacionDestinoID)
	 WHERE EG.LoteID = @LoteID  
	   AND EG.CorralID = @CorralID
	SET NOCOUNT OFF;
END 

GO
