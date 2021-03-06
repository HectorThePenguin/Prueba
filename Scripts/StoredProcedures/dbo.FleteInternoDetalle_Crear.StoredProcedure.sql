USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteInternoDetalle_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteInternoDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[FleteInternoDetalle_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 21/07/2014
-- Description: Crea un nuevo flete interno detalle
-- FleteInternoDetalle_Crear 
--=============================================
CREATE PROCEDURE [dbo].[FleteInternoDetalle_Crear]  
 @FleteInternoID INT,  
 @ProveedorID INT,  
 @MermaPermitida DECIMAL(12,2),  
 @Observaciones VARCHAR(255),  
 @Activo INT,  
 @UsuarioCreacionID INT
 ,@TipoTarifaID INT
AS  
BEGIN  
 SET NOCOUNT ON;  
 INSERT FleteInternoDetalle(  
  FleteInternoID,  
  ProveedorID,  
  MermaPermitida,  
  Observaciones,  
  Activo,  
  FechaCreacion,  
  UsuarioCreacionID
  ,TipoTarifaID  
 )  
 VALUES(  
  @FleteInternoID,  
  @ProveedorID,  
  @MermaPermitida,  
  @Observaciones,  
  @Activo,  
  GETDATE(),  
  @UsuarioCreacionID
  ,@TipoTarifaID  
 )  
 SELECT SCOPE_IDENTITY()  
 SET NOCOUNT OFF;  
END 

GO
