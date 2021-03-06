USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaAnimal_ObtenerArete]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaAnimal_ObtenerArete]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaAnimal_ObtenerArete]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Noel.Gerardo
-- Create date: 11/12/2013
-- Origen: APInterfaces
-- Descripcion: Obtener arete de la salida.
-- EXEC InterfaceSalidaAnimal_ObtenerArete '48402508718226', 1
-- =============================================
    CREATE PROCEDURE [dbo].[InterfaceSalidaAnimal_ObtenerArete]
		@Arete VARCHAR(15),
		@OrganizacionID INT
	AS
	BEGIN
		SET NOCOUNT ON;
		SELECT TOP 1 ISA.OrganizacionID,
		             ISA.SalidaID,
					 ISA.Arete,
					 ISNULL(ISA.AreteMetalico,'') AS AreteMetalico,
					 ISA.FechaCompra,
					 ISA.PesoCompra,
					 ISA.TipoGanadoID,
					 ISA.PesoOrigen,
					 ISA.FechaRegistro,
					 ISA.UsuarioRegistro,
					 ISNULL(eg.FolioEntrada,0) AS Partida,
					 ISNULL(eg.CorralID, 0) AS CorralID					
		FROM InterfaceSalidaAnimal ISA
		INNER JOIN InterfaceSalida I ON I.SalidaID = ISA.SalidaID AND I.Activo = 1
		LEFT JOIN EntradaGanado eg on (eg.FolioOrigen = ISA.SalidaID AND eg.OrganizacionOrigenID = ISA.OrganizacionID AND eg.OrganizacionID = @OrganizacionID)
		WHERE ISA.Arete = @Arete
		AND ISNULL(ISA.AnimalID,0) = 0
		order by eg.FolioEntrada desc
		SET NOCOUNT OFF;
	END 

GO
