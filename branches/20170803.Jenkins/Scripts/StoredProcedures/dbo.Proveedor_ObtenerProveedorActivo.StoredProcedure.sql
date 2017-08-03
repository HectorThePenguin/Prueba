USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerProveedorActivo]    Script Date: 22/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerProveedorActivo]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerProveedorActivo]    Script Date: 22/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Lorenzo Antonio Villaseñor Martínez
-- Create date: 22-05-2017
-- Description: sp para validar si existen proveedores activos
-- SpName     : Proveedor_ObtenerProveedorActivo 1
--======================================================  
CREATE PROCEDURE [dbo].[Proveedor_ObtenerProveedorActivo]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 
			ProveedorID,
			CodigoSAP,
			Descripcion,
			TipoProveedorID,
			Activo  
	FROM Proveedor
  WHERE 
		Activo = @Activo OR @Activo IS NULL
    SET NOCOUNT OFF;
END
