USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteInternoDetalle_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteInternoDetalle_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[FleteInternoDetalle_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 21/07/2014
-- Description: 
-- SpName     : FleteInternoDetalle_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[FleteInternoDetalle_Actualizar]  
@FleteInternoDetalleID INT,  
@MermaPermitida DECIMAL(12,2),  
@Observaciones VARCHAR(255),  
@Activo INT,  
@UsuarioModificacionID INT 
,@TipoTarifaID INT 
AS  
BEGIN  
 SET NOCOUNT ON;  
 UPDATE FleteInternoDetalle SET  
  MermaPermitida = @MermaPermitida,  
  Observaciones = @Observaciones,  
  Activo = @Activo,  
  FechaModificacion = GETDATE(),  
  UsuarioModificacionID = @UsuarioModificacionID
  ,TipoTarifaID = @TipoTarifaID 
 WHERE FleteInternoDetalleID = @FleteInternoDetalleID  
 SET NOCOUNT OFF;  
END 

GO
