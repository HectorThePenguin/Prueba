USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[sp_CorteFicticio]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[sp_CorteFicticio]
GO
/****** Object:  StoredProcedure [dbo].[sp_CorteFicticio]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CorteFicticio] 
(@Org int, @Folio int, @Cabezas int, @Codigo varchar(10))
AS
BEGIN

IF (SELECT COUNT(1)
FROM Corral c
INNER JOIN Lote l ON l.CorralID = c.CorralID AND l.Activo = 1
WHERE c.OrganizacionID = @Org AND c.Codigo = @Codigo) = 1

BEGIN

    SELECT TOP (@Cabezas)
		  eg.OrganizacionID
		 ,eg.FolioEntrada
		 ,o.Descripcion
		 ,eg.OrganizacionOrigenID
		 ,eg.FolioOrigen
		 ,eg.CabezasRecibidas
		 ,tg.Sexo
		 ,isa.Arete AS AreteCentro
		 ,a.FolioEntrada AS FolioEntradaAnimal
		 ,a.Arete AS AreteCorte
		 ,isa.PesoCompra
		 ,a.PesoCompra AS PesoOrigen
		 ,CASE a.UsuarioCreacionID WHEN 1 THEN 1 ELSE 0 END AS CargaInicial
		 ,c.Codigo
		 ,ac.Importe AS CostoGanado
    INTO #Arete
    FROM EntradaGanado eg
    INNER JOIN Organizacion o ON o.OrganizacionID = eg.OrganizacionOrigenID
    INNER JOIN InterfaceSalidaAnimal isa ON eg.OrganizacionOrigenID = isa.OrganizacionID AND eg.FolioOrigen = isa.SalidaID
    LEFT JOIN Animal a ON a.Arete = isa.Arete
    LEFT JOIN AnimalMovimiento am ON am.AnimalID = a.AnimalID AND am.Activo = 1
    LEFT JOIN Corral c ON c.CorralID = am.CorralID
    LEFT JOIN AnimalCosto ac ON ac.AnimalID = a.AnimalID AND ac.CostoID = 1
    LEFT JOIN TipoGanado tg ON tg.TipoGanadoID = isa.TipoGanadoID
    WHERE eg.OrganizacionID = @Org
	   AND eg.FolioEntrada = @Folio
	   AND a.FolioEntrada IS NULL
    ORDER BY isa.PesoCompra

    SELECT * FROM #Arete

    --Insertamos el Animal
    INSERT INTO Animal
    SELECT a.AreteCentro, '', isa.FechaCompra, isa.TipoGanadoID, cg.CalidadGanadoID, 4, a.PesoCompra, a.OrganizacionID, a.FolioEntrada, a.PesoCompra * 0.92, 0, NULL, 0, 0, 0, 1, GETDATE(), 1, NULL, NULL
    FROM #Arete a
    INNER JOIN InterfaceSalidaAnimal isa ON isa.Arete = a.AreteCentro AND isa.SalidaID = a.FolioOrigen AND isa.OrganizacionID = a.OrganizacionOrigenID
    INNER JOIN CalidadGanado cg ON cg.Sexo = a.Sexo AND cg.Descripcion = 'CEBU'

    --Insertamos el movimiento
    INSERT INTO AnimalMovimiento
    SELECT an.AnimalID, a.OrganizacionID, l.CorralID, l.LoteID, GETDATE(), an.PesoCompra * 0.96, 0, 5, 4, 8, 'Corte Manual', NULL, NULL, 1, GETDATE(), 1, NULL, NULL
    FROM #Arete a
    INNER JOIN Animal an ON an.Arete = a.AreteCentro AND an.OrganizacionIDEntrada = a.OrganizacionID AND an.FolioEntrada = a.FolioEntrada
    INNER JOIN Corral c ON c.OrganizacionID = a.OrganizacionID AND c.Codigo = @Codigo
    INNER JOIN Lote l ON l.CorralID = c.CorralID AND l.Activo = 1

    --Actualizamos Lote Destino
    --SELECT *
    UPDATE Lote SET Cabezas = Cabezas + (SELECT COUNT(*) FROM #Arete)
    FROM Corral c
    INNER JOIN Lote l ON l.CorralID = c.CorralID AND l.Activo = 1
    WHERE c.OrganizacionID = @Org AND c.Codigo = @Codigo

    --SELECT *
    --UPDATE Lote SET Cabezas = l.Cabezas - (SELECT COUNT(*) FROM #Arete), Activo = CASE WHEN l.Cabezas - (SELECT COUNT(*) FROM #Arete) = 0 THEN 0 ELSE 1 END
    --FROM #Arete a
    --INNER JOIN EntradaGanado eg ON eg.OrganizacionID = a.OrganizacionID AND eg.FolioEntrada = a.FolioEntrada
    --INNER JOIN Lote l ON l.LoteID = eg.LoteID

    DROP TABLE #Arete

END

ELSE 
    
    SELECT 'EL CORRAL ' + @Codigo + ' NO TIENE 1 LOTE ACTIVO'

SELECT *
FROM vw_RevisionPartidaAgrupada
WHERE OrganizacionID = @Org AND FolioEntrada = @Folio

END
GO
