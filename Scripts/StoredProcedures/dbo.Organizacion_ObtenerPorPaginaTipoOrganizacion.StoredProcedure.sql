USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorPaginaTipoOrganizacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorPaginaTipoOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorPaginaTipoOrganizacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Edgar Villarreal
-- Create date: 23-05-2014
-- Description:	Otiene un listado de organizaciones paginado filtrado tipo organaizcion
-- Organizacion_ObtenerPorPaginaTipoOrganizacion 0, '', 1, 1,10 ,2
-- =============================================
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorPaginaTipoOrganizacion]  
	@OrganizacionID INT,      
	@Descripcion NVARCHAR(50),       
	@Activo BIT,      
	@Inicio INT,       
	@Limite INT,  
	@TipoOrganizacion INT,
	@Division VARCHAR(5) = NULL
AS      
BEGIN      
	SET NOCOUNT ON;
	CREATE TABLE #Organizacion(RowNum INT,OrganizacionID INT,TipoOrganizacionID INT,Descripcion VARCHAR(50),Direccion VARCHAR(255),Rfc VARCHAR(13),IvaID INT, Activo BIT)
	IF @Division IS NULL
	BEGIN
		INSERT INTO #Organizacion(RowNum,OrganizacionID,TipoOrganizacionID,Descripcion,Direccion,Rfc,IvaID,Activo)
		SELECT       
			ROW_NUMBER() OVER ( ORDER BY O.descripcion ASC) AS RowNum,      
			O.OrganizacionID,      
			O.TipoOrganizacionID,       
			O.Descripcion,      
			O.Direccion,      
			O.Rfc,    
			O.IvaID,      
			O.Activo        
		FROM Organizacion O      
		WHERE (O.descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')      
		AND @OrganizacionID IN (O.OrganizacionID, 0)      
		AND O.Activo = @Activo      
		AND O.TipoOrganizacionID = @TipoOrganizacion		
	END
	ELSE
	BEGIN
		INSERT INTO #Organizacion(RowNum,OrganizacionID,TipoOrganizacionID,Descripcion,Direccion,Rfc,IvaID,Activo)
		SELECT       
			ROW_NUMBER() OVER ( ORDER BY O.descripcion ASC) AS RowNum,      
			O.OrganizacionID,      
			O.TipoOrganizacionID,       
			O.Descripcion,      
			O.Direccion,      
			O.Rfc,    
			O.IvaID,      
			O.Activo        
		FROM Organizacion O      
		WHERE (O.descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')      
		AND @OrganizacionID IN (O.OrganizacionID, 0)      
		AND O.Activo = @Activo      
		AND O.TipoOrganizacionID = @TipoOrganizacion
		AND O.Division LIKE '%'+LTRIM(RTRIM(@Division))+'%'
	END
	SELECT       
		o.OrganizacionID,       
		o.TipoOrganizacionID,      
		ot.Descripcion as [TipoOrganizacion],        
		o.Descripcion,   
		o.Direccion,        
		o.Rfc,    
		o.IvaID,      
		i.Descripcion as [Iva],      
		o.Activo      
	FROM    #Organizacion o        
	INNER JOIN TipoOrganizacion ot on ot.TipoOrganizacionID = o.TipoOrganizacionID      
	INNER JOIN Iva i on i.IvaID = o.IvaID      
	WHERE   RowNum BETWEEN @Inicio AND @Limite
	SELECT       
		COUNT(OrganizacionID)AS TotalReg       
	FROM #Organizacion
END

GO
