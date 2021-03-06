USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[sp_ReemplazaAreteInterface]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[sp_ReemplazaAreteInterface]
GO
/****** Object:  StoredProcedure [dbo].[sp_ReemplazaAreteInterface]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ReemplazaAreteInterface] (@AreteBUENO varchar(15), @AreteMALO varchar(15))    
AS    
BEGIN  
    
--DECLARE @AreteBUENO varchar(15)    
--DECLARE @AreteMALO varchar(15)    
    
--SET @AreteBUENO = '2278158'    
--SET @AreteMALO = '2278278'    
    
IF (SELECT a.Arete FROM Animal (nolock) a WHERE a.Arete = @AreteBUENO) IS NOT NULL    
BEGIN    
    PRINT 'El arete ' + @AreteBUENO + ' ya fue cortado'    
    RETURN    
END    
  
IF (SELECT Arete FROM InterfaceSalidaAnimal (nolock) WHERE Arete = @AreteBUENO) IS NOT NULL    
BEGIN    
     DECLARE @Origen int, @Salida int  
     SELECT @Origen = OrganizacionID, @Salida = SalidaID FROM InterfaceSalidaAnimal (nolock) WHERE Arete = @AreteBUENO  
  
     INSERT INTO dbo.InterfaceSalidaAnimal     
	SELECT isa.OrganizacionID    
    , isa.SalidaID    
    , @AreteMALO AS Arete    
    , isa.FechaCompra    
    , isa.PesoCompra    
    , isa.TipoGanadoID    
    , isa.PesoOrigen    
    , isa.FechaRegistro    
    , isa.UsuarioRegistro   
	, isa.AreteMetalico 
	, isa.AnimalID
    FROM dbo.InterfaceSalidaAnimal(nolock) isa    
    WHERE isa.Arete = @AreteBUENO --AND isa.OrganizacionID = 71 AND isa.SalidaID = 483  
  
    UPDATE dbo.InterfaceSalidaCosto SET Arete = @AreteMALO WHERE Arete = @AreteBUENO  
  
    DELETE FROM dbo.InterfaceSalidaAnimal WHERE Arete = @AreteBUENO  
  
    INSERT INTO dbo.InterfaceSalidaAnimal     
    SELECT isa.OrganizacionID    
        , isa.SalidaID    
        , @AreteBUENO AS Arete    
        , isa.FechaCompra    
        , isa.PesoCompra    
        , isa.TipoGanadoID    
        , isa.PesoOrigen    
        , isa.FechaRegistro    
        , isa.UsuarioRegistro    
		, isa.AreteMetalico
		, isa.AnimalID
    FROM dbo.InterfaceSalidaAnimal(nolock) isa    
    WHERE isa.Arete = @AreteMALO AND isa.OrganizacionID != @Origen AND isa.SalidaID != @Salida  
  
    UPDATE dbo.InterfaceSalidaCosto SET Arete = @AreteBUENO WHERE Arete = @AreteMALO AND OrganizacionID != @Origen AND SalidaID != @Salida  
    
    DELETE FROM dbo.InterfaceSalidaAnimal WHERE Arete = @AreteMALO AND OrganizacionID != @Origen AND SalidaID != @Salida  
  
END   
  
ELSE  
    BEGIN    
    
    INSERT INTO dbo.InterfaceSalidaAnimal     
    SELECT isa.OrganizacionID    
    , isa.SalidaID    
    , @AreteBUENO AS Arete    
    , isa.FechaCompra    
    , isa.PesoCompra    
    , isa.TipoGanadoID    
    , isa.PesoOrigen    
    , isa.FechaRegistro    
    , isa.UsuarioRegistro
	, isa.AreteMetalico    
	, isa.AnimalID
    FROM dbo.InterfaceSalidaAnimal isa    
    WHERE isa.Arete = @AreteMALO   
  
    UPDATE dbo.InterfaceSalidaCosto SET Arete = @AreteBUENO WHERE Arete = @AreteMALO   
    
    DELETE FROM dbo.InterfaceSalidaAnimal WHERE Arete = @AreteMALO   
    
    END  
  
END
GO
