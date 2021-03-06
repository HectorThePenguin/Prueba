USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ObtenerPendientesRecibir]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Embarque_ObtenerPendientesRecibir]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ObtenerPendientesRecibir]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 07-11-2013
-- Description:	Actualiza el estatus de los embarques a recibido
-- Embarque_ObtenerPendientesRecibir 2296
-- 001 -- Gilberto Carranza -- Se agrega Validacion de estatus
-- =============================================
CREATE PROCEDURE [dbo].[Embarque_ObtenerPendientesRecibir]
	@EmbarqueID INT
AS
BEGIN	
	SET NOCOUNT ON;
	
	Declare @TipoOrganizacionDescanso INT
	DECLARE @TotalRecibidos INT
	DECLARE @TotalEscalas INT
	
	set @TipoOrganizacionDescanso = 6

    SELECT @TotalRecibidos = COUNT(EmbarqueID) 
	FROM EmbarqueDetalle 
	Where EmbarqueID = @EmbarqueID
		AND Recibido = 1
		AND Activo = 1

	SELECT @TotalEscalas = COUNT(EmbarqueID) 
	FROM EmbarqueDetalle ed	
	inner join Organizacion o on ed.OrganizacionOrigenID = o.OrganizacionID
	Where EmbarqueID = @EmbarqueID
		AND o.TipoOrganizacionID <> @TipoOrganizacionDescanso
		AND ED.Activo = 1
	
	Select @TotalEscalas - @TotalRecibidos Pendientes
	 
END

GO
