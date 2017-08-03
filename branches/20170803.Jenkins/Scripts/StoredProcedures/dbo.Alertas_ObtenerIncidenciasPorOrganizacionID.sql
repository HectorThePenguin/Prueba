USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Alertas_ObtenerIncidenciasPorOrganizacionID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Alertas_ObtenerIncidenciasPorOrganizacionID]
GO
/****** Object:  StoredProcedure [dbo].[Alertas_ObtenerIncidenciasPorOrganizacionID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================  
-- Author     : Eric García  
-- Create date: 14/03/2016  
-- Description: Obtiene las incidencias por organizacion  
-- Alertas_ObtenerIncidenciasPorOrganizacionID   1,1
--=============================================  
CREATE PROCEDURE [dbo].[Alertas_ObtenerIncidenciasPorOrganizacionID]  
@OrganizacionID INT,  
@Corporativo BIT  
AS  
BEGIN  
 SET NOCOUNT ON;  
 SELECT
 Inc.Fecha,  
 Inc.FechaVencimiento,  
 A.AlertaID,   
 A.Descripcion AS AlertaDescripcion,   
 A.ModuloID,  
 Mo.Descripcion AS ModuloDescripcion,   
 Inc.IncidenciaID,  
 Inc.Folio,  
 Inc.Comentarios,  
 Inc.XmlConsulta,  
 Inc.NivelAlertaID,  
 Inc.OrganizacionID,  
 ISNULL(Inc.UsuarioResponsableID,0) AS UsuarioResponsableID,  
 Org.Descripcion AS OrganizacionDescripcion,  
 Inc.EstatusID,  
 Es.Descripcion AS EstatusDescripcion,  
 AC.NivelAlertaID AS NivelInicial,  
 ISNULL(Acc.AccionID, 0) AS AccionID,  
 Acc.Descripcion AS AccionDescripcion  
  FROM Alerta A  
  INNER JOIN Incidencia AS Inc ON A.AlertaID = Inc.AlertaID
  INNER JOIN Modulo AS Mo ON A.ModuloID = Mo.ModuloID  
  INNER JOIN Estatus AS Es ON Inc.EstatusID = Es.EstatusID  
  LEFT JOIN Accion AS Acc ON Acc.AccionID = Inc.AccionID  
  INNER JOIN Organizacion AS Org ON Org.OrganizacionID = Inc.OrganizacionID  
  INNER JOIN AlertaConfiguracion AS AC ON A.AlertaID = AC.AlertaID AND AC.Activo = 1  
  WHERE (  
      (@Corporativo = 0   
         AND Inc.OrganizacionID = @OrganizacionID  
         AND A.Activo = 1  
      )   
      OR   
      (@Corporativo = 1   
         AND A.Activo = 1)  
     ) AND Inc.EstatusID != 59
 SET NOCOUNT OFF;  
END  