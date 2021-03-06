USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDetalle_ObtenerPorPremezclaID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PremezclaDetalle_ObtenerPorPremezclaID]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDetalle_ObtenerPorPremezclaID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 15/07/2014
-- Description: Obtiene premezcla detalle por premezclaid
-- SpName     : PremezclaDetalle_ObtenerPorPremezclaID 8
--======================================================
CREATE PROCEDURE [dbo].[PremezclaDetalle_ObtenerPorPremezclaID]
@PremezclaID INT,
@Activo INT
AS
BEGIN
	SELECT 
		PD.PremezclaDetalleID,
		PD.PremezclaID,
		PD.ProductoID,
		PD.Porcentaje,
		PD.Activo,
		PD.FechaCreacion,
		PD.UsuarioCreacionID
	FROM PremezclaDetalle PD (NOLOCK)
	WHERE PremezclaID = @PremezclaID
	AND PD.Activo = @Activo
END

GO
