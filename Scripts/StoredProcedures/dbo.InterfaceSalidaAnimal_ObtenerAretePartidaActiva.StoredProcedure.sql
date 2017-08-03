USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaAnimal_ObtenerAretePartidaActiva]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaAnimal_ObtenerAretePartidaActiva]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaAnimal_ObtenerAretePartidaActiva]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Jorge Luis Velazquez Araujo
-- Create date: 24/02/2016
-- Descripcion: Obtener arete de la salida con su partida activa.
-- EXEC InterfaceSalidaAnimal_ObtenerAretePartidaActiva '48400708928012', 5
-- =============================================
    CREATE PROCEDURE [dbo].[InterfaceSalidaAnimal_ObtenerAretePartidaActiva]
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
		left join Lote lo on eg.LoteID = lo.LoteID and lo.Activo = 1
		WHERE ISA.Arete = @Arete 
		AND ISNULL(ISA.AnimalID,0) = 0
		order by eg.FolioEntrada desc
		SET NOCOUNT OFF;
	END 

GO
