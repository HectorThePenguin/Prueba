USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ObtenerAlmacenPorId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventario_ObtenerAlmacenPorId]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ObtenerAlmacenPorId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 20/05/2014
-- Description: Obtiene el almacen por id
-- SpName     : exec AlmacenInventario_ObtenerAlmacenPorId 1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventario_ObtenerAlmacenPorId]
@AlmacenInventarioId INT
AS 
BEGIN
	SELECT [AlmacenInventarioID]
      ,[AlmacenID]
      ,[ProductoID]
      ,[Minimo]
      ,[Maximo]
      ,[PrecioPromedio]
      ,[Cantidad]
      ,[Importe]
      ,[FechaCreacion]
      ,[UsuarioCreacionID]
      ,[FechaModificacion]
      ,[UsuarioModificacionID]
  FROM [dbo].[AlmacenInventario](NOLOCK) 
  WHERE [AlmacenInventarioID] = @AlmacenInventarioId
END

GO
