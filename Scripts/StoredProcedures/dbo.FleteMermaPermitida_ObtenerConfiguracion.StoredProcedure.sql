USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteMermaPermitida_ObtenerConfiguracion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteMermaPermitida_ObtenerConfiguracion]
GO
/****** Object:  StoredProcedure [dbo].[FleteMermaPermitida_ObtenerConfiguracion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jesus Alvarez
-- Create date: 23/07/2014
-- Description: Consulta un registro por organizacion, producto y proveedor
-- FleteMermaPermitida_ObtenerConfiguracion 1, 1, 4381, 32, 1
-- =============================================
CREATE PROCEDURE [dbo].[FleteMermaPermitida_ObtenerConfiguracion]
@OrganizacionIDDestino INT,
@SubFamilia INT,
@Activo INT
AS
BEGIN
	SELECT 
		FM.FleteMermaPermitidaID,
		FM.OrganizacionID,
		O.Descripcion AS DescripcionOrganizacion,
		FM.SubFamiliaID,
		FM.MermaPermitida,
		FM.Activo,
		FM.FechaCreacion,
		FM.UsuarioCreacionID
	FROM FleteMermaPermitida (NOLOCK) FM
	INNER JOIN Organizacion O ON FM.OrganizacionID = O.OrganizacionID
	WHERE FM.OrganizacionID = @OrganizacionIDDestino
	AND FM.SubFamiliaID = @SubFamilia
	AND FM.Activo = @Activo
	AND O.Activo = @Activo
END

GO
