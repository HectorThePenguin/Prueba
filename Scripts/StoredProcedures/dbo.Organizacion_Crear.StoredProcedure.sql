USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================    
-- Author     : Jos� Gilberto Quintero L�pez    
-- Create date: 14/01/2014 12:00:00 a.m.    
-- Description:     
-- SpName     : Organizacion_Crear    
--001 Jorge Luis Velazquez Araujo 31/07/2015 **Se agrega para que se guarden las columnas Division y Sociedad    
--======================================================    
CREATE PROCEDURE [dbo].[Organizacion_Crear]    
@TipoOrganizacionID int,    
@Descripcion varchar(50),    
@Direccion varchar(255),    
@RFC varchar(13),    
@IvaID int,    
@Activo bit,    
@Division varchar(4),--001    
@Sociedad varchar(3),--001    
@UsuarioCreacionID int,  
@ZonaID int   
AS    
BEGIN    
 SET NOCOUNT ON;    
 INSERT Organizacion (    
  TipoOrganizacionID,    
  Descripcion,    
  Direccion,    
  RFC,    
  IvaID,    
  Activo,    
  Division,--001    
  Sociedad,--001    
  UsuarioCreacionID,    
  FechaCreacion,  
  ZonaID    
 )    
 VALUES(    
  @TipoOrganizacionID,    
  @Descripcion,    
  @Direccion,    
  @RFC,    
  @IvaID,    
  @Activo,    
  @Division, --001    
  @Sociedad, --001    
  @UsuarioCreacionID,    
  GETDATE(),  
  @ZonaID    
 )    
 SET NOCOUNT OFF;    
END

GO
