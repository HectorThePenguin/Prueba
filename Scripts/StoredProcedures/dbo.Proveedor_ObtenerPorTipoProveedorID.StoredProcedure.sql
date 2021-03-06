USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorTipoProveedorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerPorTipoProveedorID]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorTipoProveedorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jesus Alvarez
-- Modified: Luis Alfonso Sandoval Huerta
-- Create date: 14/05/2014
-- Modification date: 22/06/2017
-- Description:  Obtener listado de Proveedores por Tipo Proveedor
-- Se añade campo correo para envio de correo a transportista
-- en programcion de embarque
-- Proveedor_ObtenerPorTipoProveedorID 1, 5
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerPorTipoProveedorID]
@Activo INT,
@TipoProveedorID INT
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT ProveedorID,
			CodigoSAP,
			Descripcion,
			TipoProveedorID,
      CorreoElectronico,
			Activo
      FROM Proveedor
       WHERE Activo = @Activo
	   AND TipoProveedorID = @TipoProveedorID
      SET NOCOUNT OFF;
  END

GO
