USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConexionOrganisacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConexionOrganisacion]
GO
/****** Object:  StoredProcedure [dbo].[ConexionOrganisacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConexionOrganisacion]
(  
@Organisacion INT  
)  
AS   
BEGIN   
 SELECT PO.Valor FROM Parametro AS P (NOLOCK)  
 INNER JOIN ParametroOrganizacion AS PO(NOLOCK)  
  ON P.ParametroID = PO.ParametroID  
 WHERE PO.OrganizacionID = @Organisacion  
    AND  P.Clave in ('ServidorSPI','BaseDatosSPI', 'UsuarioSPI', 'PasswordSPI')  
END
GO
