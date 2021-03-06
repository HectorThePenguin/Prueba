USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionEmbarque_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionEmbarque_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionEmbarque_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/11/13
-- Description: 
-- ConfiguracionEmbarque_ObtenerTodos
--=============================================
CREATE PROCEDURE [dbo].[ConfiguracionEmbarque_ObtenerTodos]
@Activo BIT = NULL	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		ce.ConfiguracionEmbarqueID,
		ce.OrganizacionOrigenID,
		oo.Descripcion as [Origen],
		ce.OrganizacionDestinoID,
		od.Descripcion as [Destino],
		ce.Kilometros,
		ce.Horas,
		ce.Activo
	FROM ConfiguracionEmbarque ce
	INNER JOIN Organizacion oo on oo.OrganizacionID = ce.OrganizacionOrigenID
	INNER JOIN Organizacion od on od.OrganizacionID = ce.OrganizacionDestinoID
		WHERE (
			ce.Activo = @Activo
			OR @Activo IS NULL
			)	
	SET NOCOUNT OFF;
END

GO
