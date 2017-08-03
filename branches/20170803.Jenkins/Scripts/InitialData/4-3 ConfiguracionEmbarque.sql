INSERT ConfiguracionEmbarque (OrganizacionOrigenID,OrganizacionDestinoID,Kilometros,Horas,Activo,FechaCreacion,UsuarioCreacionID)
SELECT O1.OrganizacionID, O2.OrganizacionID, 1000, 10.0, 1, GETDATE(), 1
FROM Organizacion O1 (NOLOCK)
JOIN Organizacion O2 (NOLOCK) ON O2.TipoOrganizacionID != 1
WHERE O1.TipoOrganizacionID != 1
