USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerReemplazoArete]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerReemplazoArete]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerReemplazoArete]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 17/06/2015
-- Description:	Obtiene los Aretes a Reemplazar
-- EntradaGanado_ObtenerReemplazoArete 4464, 1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerReemplazoArete]
	@FolioEntrada INT, 
	@OrganizacionID INT
AS 
BEGIN	
	SELECT 
		 eg.FolioEntrada AS FolioEntradaCentro
		 ,eg.CabezasOrigen
         ,eg.CabezasRecibidas         
         ,isa.Arete AS AreteCentro         
		 ,isa.AreteMetalico AS AreteMetalicoCentro
		 ,a.FolioEntrada As FolioEntradaCorte
         ,a.Arete AS AreteCorte
		 ,a.AreteMetalico AS AreteMetalicoCorte
		 ,a.AnimalID
         ,isa.PesoCompra         
FROM EntradaGanado eg (nolock)
INNER JOIN Organizacion o (nolock) ON o.OrganizacionID = eg.OrganizacionOrigenID
INNER JOIN InterfaceSalidaAnimal isa (nolock) ON eg.OrganizacionOrigenID = isa.OrganizacionID AND eg.FolioOrigen = isa.SalidaID
LEFT JOIN Animal a (nolock) ON a.AnimalID = isa.AnimalID
LEFT JOIN AnimalMovimiento am (nolock) ON am.AnimalID = a.AnimalID AND am.Activo = 1
WHERE eg.OrganizacionID = @OrganizacionID
    AND eg.FolioEntrada = @FolioEntrada

END

GO
