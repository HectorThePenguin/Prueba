USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoBoleta_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProductoBoleta_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoBoleta_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 24/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProductoBoleta_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProductoBoleta_Actualizar]
@IndicadorProductoBoletaID int,
@IndicadorProductoID int,
@OrganizacionID int,
@RangoMinimo decimal(10,3),
@RangoMaximo decimal(10,3),
@Activo bit,
@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
		UPDATE IndicadorProductoBoleta SET
			IndicadorProductoID = @IndicadorProductoID,
			OrganizacionID = @OrganizacionID,
			RangoMinimo = @RangoMinimo,
			RangoMaximo = @RangoMaximo,
			Activo = @Activo,
			UsuarioModificacionID = @UsuarioModificacionID,
			FechaModificacion = GETDATE()
		WHERE IndicadorProductoBoletaID = @IndicadorProductoBoletaID
	SET NOCOUNT OFF;
END

GO
