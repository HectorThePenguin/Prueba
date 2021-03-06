USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilancia_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RegistroVigilancia_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilancia_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/04/2015
-- Description: Obtiene un registro de vigilancia por folioturno
-- SpName     : RegistroVigilancia_ObtenerPorPagina 0,1,1,1,15
--======================================================
CREATE PROCEDURE [dbo].[RegistroVigilancia_ObtenerPorPagina]
@FolioTurno INT,
@OrganizacionID INT,
@Activo BIT,
@Inicio INT, 
@Limite INT

AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
	    ROW_NUMBER() OVER ( ORDER BY rv.RegistroVigilanciaID ASC) AS RowNum,
		rv.RegistroVigilanciaID,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		ProveedorIDMateriasPrimas,
		pr.Descripcion AS Proveedor,
		ProveedorChoferID,
		Transportista,
		Chofer,
		pro.ProductoID,
		pro.Descripcion AS Producto,
		CamionID,
		Camion,
		Marca,
		Color,
		FolioTurno,
		FechaLlegada,
		FechaSalida,
		co.ContratoID,
		co.Folio,
		rv.Activo	
		INTO #RegistroVigilancia
		FROM RegistroVigilancia rv
		INNER JOIN Organizacion o on rv.OrganizacionID = o.OrganizacionID
		INNER JOIN Proveedor pr on rv.ProveedorIDMateriasPrimas = pr.ProveedorID
		INNER JOIN Producto pro on rv.ProductoID = pro.ProductoID
		inner join Contrato co on rv.ContratoID = co.ContratoID
		WHERE rv.OrganizacionID = @OrganizacionID
		and @FolioTurno in (rv.FolioTurno,0)
		and rv.Activo = @Activo
		and CAST(rv.FechaLlegada AS DATE) > GETDATE() - 3

		  SELECT 
			RegistroVigilanciaID,
			OrganizacionID,
			Organizacion,
			ProveedorIDMateriasPrimas,
			Proveedor,
			ProveedorChoferID,
			Transportista,
			Chofer,
			ProductoID,
			Producto,
			CamionID,
			Camion,
			Marca,
			Color,
			FolioTurno,
			FechaLlegada,
			FechaSalida,
			ContratoID,
			Folio,	
			Activo
	FROM #RegistroVigilancia rv 	
	WHERE RowNum BETWEEN @Inicio AND @Limite

	SELECT 
		COUNT(RegistroVigilanciaID)AS TotalReg 
	FROM #RegistroVigilancia	

	SET NOCOUNT OFF;

END
GO
