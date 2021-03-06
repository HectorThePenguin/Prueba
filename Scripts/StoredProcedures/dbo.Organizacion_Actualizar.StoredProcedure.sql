USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================    
-- Author     : Jos� Gilberto Quintero L�pez    
-- Create date: 14/01/2014 12:00:00 a.m.    
-- Description:     
-- SpName     : Organizacion_Actualizar    
--001 Jorge Luis Velazquez Araujo 31/07/2015 ** Se agregan las columnas Division y Sociedad    
--======================================================    
CREATE PROCEDURE [dbo].[Organizacion_Actualizar]    
@OrganizacionID int,    
@TipoOrganizacionID int,    
@Descripcion varchar(50),    
@Direccion varchar(255),    
@RFC varchar(13),    
@IvaID int,    
@Activo bit,    
@Division varchar(4),--001    
@Sociedad varchar(3),--001    
@UsuarioModificacionID int,    
@ZonaID int   
AS    
BEGIN    
 SET NOCOUNT ON;    
 UPDATE Organizacion SET    
  TipoOrganizacionID = @TipoOrganizacionID,    
  Descripcion = @Descripcion,    
  Direccion = @Direccion,    
  RFC = @RFC,    
  IvaID = @IvaID,    
  Activo = @Activo,    
  Division = @Division,--001    
  Sociedad = @Sociedad,--001    
  UsuarioModificacionID = @UsuarioModificacionID,    
  FechaModificacion = GETDATE(),  
  ZonaID =@ZonaID    
 WHERE OrganizacionID = @OrganizacionID    
 SET NOCOUNT OFF;    
END

GO
