USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SP_RevisionPartida_MA]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SP_RevisionPartida_MA]
GO
/****** Object:  StoredProcedure [dbo].[SP_RevisionPartida_MA]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_RevisionPartida_MA] (@ENTRADA VARCHAR(6),@CORRAL VARCHAR(7))
AS  
BEGIN 


SELECT eg.FolioEntrada  
       ,o.Descripcion AS Origen  
   ,eg.FolioOrigen  
   ,eg.CabezasRecibidas  
   ,c2.Codigo AS CorralPartida  
   ,l.Cabezas AS CabezasRestantes  
   ,isa.FechaCompra AS FechaCompraCentro  
   ,a.FechaCompra  
   ,isa.Arete AS AreteCentro  
   ,a.FolioEntrada AS FolioEntradaAnimal  
   ,a.Arete AS AreteCorte  
   ,isa.PesoCompra  
   ,a.PesoCompra AS PesoOrigen  
   ,CASE a.UsuarioCreacionID WHEN 1 THEN 1 ELSE 0 END AS CargaInicial  
   ,c.Codigo  
   ,ac.Importe AS CostoGanado  
   ,egc.Prorrateado  
   ,o2.Descripcion  
FROM EntradaGanado eg (NOLOCK)  
INNER JOIN Organizacion o (NOLOCK) ON o.OrganizacionID = eg.OrganizacionOrigenID  
INNER JOIN InterfaceSalidaAnimal isa (NOLOCK) ON eg.OrganizacionOrigenID = isa.OrganizacionID AND eg.FolioOrigen = isa.SalidaID  
INNER JOIN Corral c2 (NOLOCK) ON c2.CorralID = eg.CorralID  
INNER JOIN Lote l (NOLOCK) ON l.LoteID = eg.LoteID  
INNER JOIN EntradaGanadoCosteo egc (NOLOCK) ON egc.EntradaGanadoID = eg.EntradaGanadoID AND egc.Activo = 1  
LEFT JOIN Animal a (NOLOCK) ON a.Arete = isa.Arete  
LEFT JOIN AnimalMovimiento am (NOLOCK) ON am.AnimalID = a.AnimalID AND am.Activo = 1  
LEFT JOIN Corral c (NOLOCK) ON c.CorralID = am.CorralID  
LEFT JOIN AnimalCosto ac (NOLOCK) ON ac.AnimalID = a.AnimalID AND ac.CostoID = 1  
LEFT JOIN EntradaGanado eg2 (NOLOCK) ON eg2.FolioEntrada = a.FolioEntrada  
LEFT JOIN Organizacion o2 (NOLOCK) ON o2.OrganizacionID = eg2.OrganizacionOrigenID  
LEFT JOIN AnimalHistorico ah (NOLOCK) ON ah.Arete = isa.Arete  
WHERE 1=1 AND a.Arete IS NULL AND EG.FOLIOENTRADA=@ENTRADA AND c2.Codigo=@CORRAL

END
GO
