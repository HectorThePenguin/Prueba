USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ReemplazoArete]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ReemplazoArete]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ReemplazoArete]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Jorge Luis Velazquez
-- Create date: 18/06/2015
-- Description:	Reemplaza los aretes
-- EntradaGanado_ReemplazoArete 0, 5, 1, 15,'' 
--=============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ReemplazoArete] 
@XmlAretes XML,
@FolioOrigen int,
@OrganizacionOrigenID int    
AS    
BEGIN  
create table #Aretes
(
	AreteViejo varchar(15),
	AreteNuevo varchar(15),
	ExisteInterfaz bit
)
insert into #Aretes
	SELECT AreteViejo = T.item.value('./AreteCentro[1]', 'VARCHAR(15)')
			,  AreteNuevo = T.item.value('./AreteCorte[1]', 'VARCHAR(15)')
			,ExisteInterfaz = 0
		FROM  @XmlAretes.nodes('ROOT/Aretes') AS T(item)    
update a set 
a.ExisteInterfaz = case when isa.Arete is null then 0
					else 1 
					end 
from #Aretes a
left join InterfaceSalidaAnimal isa on isa.Arete = a.AreteNuevo
	--Seccion para cuando el Arete no Existe en la InterfaceSalidaAnimal
  INSERT INTO dbo.InterfaceSalidaAnimal     
    SELECT isa.OrganizacionID    
    , isa.SalidaID    
    , a.AreteNuevo AS Arete    
    , isa.FechaCompra    
    , isa.PesoCompra    
    , isa.TipoGanadoID    
    , isa.PesoOrigen    
    , isa.FechaRegistro    
    , isa.UsuarioRegistro    
	,NULL
	,null
    FROM dbo.InterfaceSalidaAnimal isa   
	inner join #Aretes a on isa.Arete = a.AreteViejo and a.ExisteInterfaz = 0
    WHERE isa.OrganizacionID = @OrganizacionOrigenID
	and isa.SalidaID = @FolioOrigen
    --UPDATE dbo.InterfaceSalidaCosto SET Arete = @AreteBUENO WHERE Arete = @AreteMALO   
	update isc set isc.Arete = a.AreteNuevo
	from InterfaceSalidaCosto isc 
	inner join #Aretes a on isc.Arete = a.AreteViejo and a.ExisteInterfaz = 0
	where isc.OrganizacionID = @OrganizacionOrigenID
	and isc.SalidaID = @FolioOrigen
    DELETE isa
	FROM InterfaceSalidaAnimal isa
	inner join #Aretes a on isa.Arete = a.AreteViejo and a.ExisteInterfaz = 0
	where isa.OrganizacionID = @OrganizacionOrigenID
	and isa.SalidaID = @FolioOrigen
    --Seccion para cuando el Arete si Existe en la InterfaceSalidaAnimal
	  INSERT INTO dbo.InterfaceSalidaAnimal     
 SELECT isa.OrganizacionID    
    , isa.SalidaID    
    , a.AreteViejo AS Arete    
    , isa.FechaCompra    
    , isa.PesoCompra    
    , isa.TipoGanadoID    
    , isa.PesoOrigen    
    , isa.FechaRegistro    
    , isa.UsuarioRegistro    
	,null
	,null
    FROM dbo.InterfaceSalidaAnimal(nolock) isa    
	inner join #Aretes a on isa.Arete = a.AreteNuevo and a.ExisteInterfaz = 1
   WHERE 
	isa.SalidaID <> @FolioOrigen
	--UPDATE dbo.InterfaceSalidaCosto SET Arete = @AreteMALO WHERE Arete = @AreteBUENO
	update isc set isc.Arete = a.AreteViejo
	from InterfaceSalidaCosto isc 
	inner join #Aretes a on isc.Arete = a.AreteNuevo and a.ExisteInterfaz = 1
	--where isc.SalidaID = @FolioOrigen
	--DELETE FROM dbo.InterfaceSalidaAnimal WHERE Arete = @AreteBUENO  
	   DELETE isa
	FROM dbo.InterfaceSalidaAnimal isa
	inner join #Aretes a on isa.Arete = a.AreteNuevo and a.ExisteInterfaz = 1
	--where 	isa.OrganizacionID = @OrganizacionOrigenID
	--and isa.SalidaID = @FolioOrigen
	--isa.Arete = @AreteBUENO --AND isa.OrganizacionID = 71 AND isa.SalidaID = 483  
    INSERT INTO dbo.InterfaceSalidaAnimal     
    SELECT isa.OrganizacionID    
        , isa.SalidaID    
        , a.AreteNuevo AS Arete    
        , isa.FechaCompra    
        , isa.PesoCompra    
        , isa.TipoGanadoID    
        , isa.PesoOrigen    
        , isa.FechaRegistro    
        , isa.UsuarioRegistro    
		,null
		,null
    FROM dbo.InterfaceSalidaAnimal(nolock) isa   
	inner join #Aretes a on isa.Arete = a.AreteViejo and a.ExisteInterfaz = 1 
    WHERE isa.OrganizacionID = @OrganizacionOrigenID AND isa.SalidaID = @FolioOrigen  
	--UPDATE dbo.InterfaceSalidaCosto SET Arete = @AreteBUENO WHERE Arete = @AreteMALO AND OrganizacionID != @Origen AND SalidaID != @Salida  
	update isc set isc.Arete = a.AreteNuevo
	from InterfaceSalidaCosto isc 
	inner join #Aretes a on isc.Arete = a.AreteViejo and a.ExisteInterfaz = 1
	WHERE isc.OrganizacionID = @OrganizacionOrigenID AND isc.SalidaID = @FolioOrigen    
    --DELETE FROM dbo.InterfaceSalidaAnimal WHERE Arete = @AreteMALO AND OrganizacionID != @Origen AND SalidaID != @Salida  
	  DELETE isa
	FROM dbo.InterfaceSalidaAnimal isa
	inner join #Aretes a on isa.Arete = a.AreteViejo and a.ExisteInterfaz = 1
	WHERE isa.OrganizacionID = @OrganizacionOrigenID AND isa.SalidaID = @FolioOrigen    
END   

GO
