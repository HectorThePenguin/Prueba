USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerFolioAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerFolioAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerFolioAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Edgar Villarreal
-- Create date: 25/03/2014 12:00:00 a.m.
-- Description: Obtiene todos los almacenes por Organizacion
-- SpName     : EXEC Almacen_ObtenerFolioAlmacen 1,4,1
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerFolioAlmacen]
@Activo INT,
@OrganizacionID INT,
@AlmacenID INT,
@TipoMovimiento INT
AS
BEGIN
	SET NOCOUNT ON;
	Declare @Contador INT
	SET @Contador = ISNULL((
			SELECT FA.Valor
			FROM Almacen A
			INNER JOIN FolioAlmacen AS FA ON FA.AlmacenID=A.AlmacenID
			WHERE A.AlmacenID=@AlmacenID
			AND A.OrganizacionID=@OrganizacionID
			AND A.Activo=@Activo
			AND FA.TipoMovimientoID=@TipoMovimiento)+ 1,1)
	SELECT @Contador AS Valor
	SET NOCOUNT OFF;
END

GO
