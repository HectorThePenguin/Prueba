USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_DesactivarLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_DesactivarLote]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_DesactivarLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 02/07/2014
-- Description: Actualiza el almacen inventario lote
-- SpName     : exec AlmacenInventarioLote_DesactivarLote 
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_DesactivarLote]
@AlmacenInventarioLoteID INT,
@Activo INT,
@UsuarioModificacionID INT
AS 
BEGIN
	UPDATE AlmacenInventarioLote 
	SET FechaFin = GETDATE(),
	Activo = @Activo,
	FechaModificacion = GETDATE(),
	UsuarioModificacionID = @UsuarioModificacionID
	WHERE AlmacenInventarioLoteID = @AlmacenInventarioLoteId
END

GO
