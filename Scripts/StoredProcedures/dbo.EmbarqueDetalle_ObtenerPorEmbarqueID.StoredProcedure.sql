USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EmbarqueDetalle_ObtenerPorEmbarqueID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EmbarqueDetalle_ObtenerPorEmbarqueID]
GO
/****** Object:  StoredProcedure [dbo].[EmbarqueDetalle_ObtenerPorEmbarqueID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/11/13
-- Description: 
-- EmbarqueDetalle_ObtenerPorEmbarqueID 2
--=============================================
CREATE PROCEDURE [dbo].[EmbarqueDetalle_ObtenerPorEmbarqueID]
@EmbarqueID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		EmbarqueDetalleID,
		EmbarqueID,
		ProveedorID,
		ChoferID,
		JaulaID,
		CamionID,
		OrganizacionOrigenID,
		OrganizacionDestinoID,
		FechaSalida,
		FechaLlegada,
		Orden,
		Horas,
		Recibido,
		Activo,
		Comentarios,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID	
	FROM EmbarqueDetalle (nolock)
	WHERE EmbarqueID = @EmbarqueID
	SET NOCOUNT OFF;
END

GO
