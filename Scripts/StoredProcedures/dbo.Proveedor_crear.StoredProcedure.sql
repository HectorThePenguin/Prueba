USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_crear]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 31/10/2013
-- Description:  Guardar un Proveedor.
-- Proveedor_Crear 'Proveedor001', 1
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_crear]
 @Descripcion       VARCHAR(100),
 @CodigoSAP			VARCHAR(10),
 @TipoProveedorID   INT,
 @ImporteComision	DECIMAL(10,2),
 @Activo            BIT,
 @UsuarioCreacionID INT,
 @CorreoElectronico VARCHAR(50) = NULL
AS
  BEGIN
      SET NOCOUNT ON;
      INSERT INTO Proveedor(
		 Descripcion,
		 CodigoSAP,
		 TipoProveedorID,
		 ImporteComision,
		 Activo,
		 FechaCreacion,
		 UsuarioCreacionID,
		 CorreoElectronico
       )
      VALUES      
      ( 
		 @Descripcion  
		 ,@CodigoSAP      
		,@TipoProveedorID    
		,@ImporteComision
		,@Activo             
		,GETDATE()      
		,@UsuarioCreacionID,
		@CorreoElectronico
		)
      SELECT SCOPE_IDENTITY()
      SET NOCOUNT OFF;
  END  

GO
