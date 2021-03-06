USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteInterno_ObtenerPorEstado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteInterno_ObtenerPorEstado]
GO
/****** Object:  StoredProcedure [dbo].[FleteInterno_ObtenerPorEstado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 15/05/2014
-- Description: 
-- SpName     : FleteInterno_ObtenerPorEstado 1
--======================================================
CREATE PROCEDURE [dbo].[FleteInterno_ObtenerPorEstado]
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		 FT.FleteInternoID,
		 FT.OrganizacionID,
		 FT.TipoMovimientoID,
		 FT.AlmacenIDOrigen,
		 FT.AlmacenIDDestino,
		 FT.ProductoID,
		 FT.Activo,
		 FT.FechaCreacion,
		 FT.UsuarioCreacionID
	FROM FleteInterno FT
	WHERE FT.Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
