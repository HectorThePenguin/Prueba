USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Organizacion_ObtenerPorDescripcion 'Corporativo'
--======================================================
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		o.OrganizacionID,   
		o.TipoOrganizacionID,  
		ot.Descripcion as [TipoOrganizacion],    
		o.Descripcion,    
		o.Direccion,    
		o.IvaID,  
		i.Descripcion as [Iva],  
		o.Activo, 
		o.RFC  
	FROM Organizacion o    
	INNER JOIN TipoOrganizacion ot on ot.TipoOrganizacionID = o.TipoOrganizacionID  
	INNER JOIN Iva i on i.IvaID = o.IvaID  
	WHERE o.Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
