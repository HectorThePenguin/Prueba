USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RelacionClienteProveedor_ObtenerInformacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RelacionClienteProveedor_ObtenerInformacion]
GO
/****** Object:  StoredProcedure [dbo].[RelacionClienteProveedor_ObtenerInformacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================      
-- Autor: Sergio A. Gamez Gomez      
-- Fecha: 25-05-2015      
-- Descripcion: Obtener informacion general de la SOFOM de un cliente      
-- RelacionClienteProveedor_ObtenerInformacion 37      
-- =============================================      
CREATE PROCEDURE [dbo].[RelacionClienteProveedor_ObtenerInformacion] @Cliente INT      
AS      
BEGIN      
	SELECT       
		Credito = P.CreditoID,       
		Ganadera = RTRIM(LTRIM(CAST(P.GanaderaID AS VARCHAR(20)))) + ' - ' + RTRIM(LTRIM(O.Descripcion)),      
		Centro = RTRIM(LTRIM(CAST(P.OrganizacionID AS VARCHAR(20)))) + ' - ' + RTRIM(LTRIM(C.Descripcion)),      
		Proveedor = RTRIM(LTRIM(CAST(P.ProveedorID AS VARCHAR(20)))) + ' - ' + RTRIM(LTRIM(PR.Descripcion)),    
		Porcentaje = P.Porcentaje,    
		Retencion  = TipoRetencionID      
	FROM [Sukarne].[dbo].[RelacionClienteProveedor] P (NOLOCK)      
	INNER JOIN Organizacion O (NOLOCK)      
		ON O.OrganizacionID = P.GanaderaID AND O.TipoOrganizacionID = 2      
	INNER JOIN Organizacion C (NOLOCK)      
		ON C.OrganizacionID = P.OrganizacionID AND C.TipoOrganizacionID = 4      
	INNER JOIN [Sukarne].[dbo].[CatProveedor] PR (NOLOCK)      
		ON PR.ProveedorID = P.ProveedorID AND PR.OrganizacionId = C.OrganizacionID  
	WHERE P.ClienteID = @Cliente      
END

GO
