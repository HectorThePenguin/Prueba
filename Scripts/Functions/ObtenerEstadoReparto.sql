IF EXISTS(SELECT *
          FROM   sys.objects
          WHERE  [object_id] = Object_id(N'[dbo].[obtenerEstadoReparto]'))
  DROP FUNCTION [dbo].[obtenerEstadoReparto]
GO
--=============================================  
-- Author:  Roberto Aguilar Pozos  
-- Create date: 2014-09-26  
-- Origen: APInterfaces  
-- Description: Obtiene el estado de comedero y el reparto a un dia dado  
-- select dbo.obtenerEstadoReparto 1 , '20140919'  
--=============================================  
create function obtenerEstadoReparto(  
 @Organizacion int,  
 @Fecha date  
)  
RETURNS TABLE  
RETURN  
SELECT Re.CorralID,   
RDV.EstadoComederoID as EstadoComederoID,  
RDM.CantidadServida as CantidadServidaMatutino ,  
FM.Descripcion as FormulaServidaMatutino,   
RDV.CantidadServida as CantidadServidaVespertino ,  
FV.Descripcion as FormulaServidaVespertino,  
RDM.CantidadServida + RDV.CantidadServida as TotalServido  
FROM Corral(nolock) C  
INNER JOIN Reparto(nolock) Re ON C.CorralID = Re.CorralID  
left JOIN RepartoDetalle(nolock) RDM on Re.RepartoID = RDM.RepartoID AND RDM.TipoServicioID = 1   
left JOIN RepartoDetalle(nolock) RDV on Re.RepartoID = RDV.RepartoID AND RDV.TipoServicioID = 2   
left join Formula(nolock) FM on FM.FormulaID = RDM.FormulaIDServida  
left join Formula(nolock) FV on FV.FormulaID = RDV.FormulaIDServida  
where C.OrganizacionID = @Organizacion   
and Re.Fecha = @Fecha