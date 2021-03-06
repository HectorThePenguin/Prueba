USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 31/10/2013
-- Description:  Guardar un Proveedor.
-- Proveedor_Actualizar 'Proveedor001', 1
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_Actualizar]		
 @ProveedorID			INT,
 @CodigoSAP				VARCHAR(10),
 @Descripcion           VARCHAR(100),
 @TipoProveedorID       INT, 
 @ImporteComision		DECIMAL(10,2),
 @Activo                BIT,
 @UsuarioModificacionID INT,
 @CorreoElectronico VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Proveedor 
		SET Descripcion = @Descripcion,
		CodigoSAP = @CodigoSAP,
		TipoProveedorID = @TipoProveedorID,
		ImporteComision = @ImporteComision,
		Activo = @Activo,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = @UsuarioModificacionID,
		CorreoElectronico = @CorreoElectronico
	WHERE ProveedorID = @ProveedorID
	SET NOCOUNT OFF;
END

GO
