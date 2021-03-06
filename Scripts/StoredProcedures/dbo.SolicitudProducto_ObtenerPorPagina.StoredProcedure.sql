USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProducto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudProducto_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProducto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SolicitudProducto_ObtenerPorPagina 4,26,0,'','VI',0,1,1,15
--======================================================
CREATE PROCEDURE [dbo].[SolicitudProducto_ObtenerPorPagina]
@OrganizacionId int,
@UsuarioIDSolicita int,
@UsuarioIDAutoriza int,
@Solicita VARCHAR(50),
@Autoriza VARCHAR(50),
@EstatusID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	set @Solicita =  '%' + rtrim(@Solicita) + '%' 
	set @Autoriza =  '%' + rtrim(@Autoriza) + '%' 
	SELECT 
		ROW_NUMBER() OVER (ORDER BY s.FolioSolicitud ASC) AS [RowNum]
		,s.OrganizacionID
		,s.SolicitudProductoID
		,s.SolicitudProductoID as [FolioID]
		,s.FolioSolicitud 
		,s.UsuarioIDSolicita
		,us.Nombre as [Solicita]
		,coalesce(s.UsuarioIDAutoriza,ccu.UsuarioID) as [UsuarioIDAutoriza]
		,isnull(ua.Nombre,'') as [Descripcion]
		,isnull(ua.Nombre,'') as [Autoriza]
		,s.CentroCostoID
		,s.EstatusID
		,s.Activo
		,s.FechaEntrega
		INTO #SolicitudProducto
		FROM SolicitudProducto s 
		INNER JOIN CentroCostoUsuario ccu on ccu.CentroCostoID = s.CentroCostoID --And ccu.Autoriza = 1 
		INNER JOIN Usuario us on us.UsuarioID = s.UsuarioIDSolicita
		LEFT JOIN Usuario ua on ua.UsuarioID = coalesce(s.UsuarioIDAutoriza,ccu.UsuarioID)
		WHERE @UsuarioIDSolicita in (s.UsuarioIDSolicita, 0)
			AND @UsuarioIDAutoriza in (coalesce(s.UsuarioIDAutoriza,ccu.UsuarioID,0) , 0)
			AND @OrganizacionId in (s.OrganizacionID, 0)
			AND us.Nombre like @Solicita
			AND isnull(ua.Nombre,'') like @Autoriza
			AND s.Activo = @Activo
			AND exists (Select '' From SolicitudProductoDetalle where SolicitudProductoID = s.SolicitudProductoID AND @EstatusID in (EstatusID, 0) AND Activo = 1) 
	SELECT *		
	FROM #SolicitudProducto s
	DROP TABLE #SolicitudProducto	
	SET NOCOUNT OFF;
END

GO
