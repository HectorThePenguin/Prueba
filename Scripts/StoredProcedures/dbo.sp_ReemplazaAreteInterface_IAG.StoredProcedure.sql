USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[sp_ReemplazaAreteInterface_IAG]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[sp_ReemplazaAreteInterface_IAG]
GO
/****** Object:  StoredProcedure [dbo].[sp_ReemplazaAreteInterface_IAG]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ReemplazaAreteInterface_IAG] (@AreteBUENO varchar(15), @AreteMALO varchar(15),@SALIDAID INT)    
AS    
BEGIN    
--DECLARE @AreteBUENO varchar(15)    
--DECLARE @AreteMALO varchar(15)    
    
--SET @AreteBUENO = '1982588'    
--SET @AreteMALO = '1982568'    
    
IF (SELECT a.Arete FROM Animal a WHERE a.Arete = @AreteBUENO) IS NOT NULL    
BEGIN    
    PRINT 'El arete ' + @AreteBUENO + ' ya fue cortado'    
    RETURN    
END    
    
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
FROM dbo.InterfaceSalidaAnimal isa    
WHERE isa.Arete = @AreteMALO AND isa.SalidaID=@SALIDAID
UPDATE dbo.InterfaceSalidaCosto SET Arete = @AreteBUENO WHERE Arete = @AreteMALO  and SalidaID=@SALIDAID  
    
DELETE FROM dbo.InterfaceSalidaAnimal WHERE Arete = @AreteMALO and  SalidaID=@SALIDAID
    
END
GO
