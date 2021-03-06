IF EXISTS(SELECT * FROM   sys.objects WHERE  [object_id] = Object_id(N'[dbo].[CacImagen_ObtenerImagenes]'))
BEGIN
	DROP PROCEDURE [dbo].[CacImagen_ObtenerImagenes]
END
GO
CREATE PROCEDURE [dbo].[CacImagen_ObtenerImagenes]     
(    
	@OrganizacionId INT,     
	@ProveedorId INT,
	@UltimoIne INT,
	@UltimoCurp INT  
)    
AS    
BEGIN     
SET NOCOUNT ON
    
	DECLARE @Ine VARBINARY(MAX)    
	DECLARE @IneId INT
	DECLARE @IneCount INT
	DECLARE @IneMax INT
	DECLARE @Curp VARBINARY(MAX)    
	DECLARE @CurpId INT
	DECLARE @CurpCount INT
	DECLARE @CurpMax INT

	-- CREDENCIAL DE ELECTOR
	SELECT     
		TOP 1 @Ine = I.Imagen, @IneId = I.ImagenID    
	FROM sukarne.dbo.CacProveedorImagen P (NOLOCK)    
	INNER JOIN sukarne.dbo.CatImagen I (NOLOCK)    
		ON P.ImagenID = I.ImagenID AND P.OrganizacionId = I.OrganizacionID    
	WHERE P.OrganizacionId = @OrganizacionId      
	AND P.ProveedorID = @ProveedorId      
	AND CASE WHEN ISNULL(P.INE,0) = 0 THEN 0 ELSE P.INE END IN(1,2) AND P.Activo = 1
	AND I.ImagenID > @UltimoIne

	SELECT     
		@IneMax = MAX(I.ImagenID)
	FROM sukarne.dbo.CacProveedorImagen P (NOLOCK)    
	INNER JOIN sukarne.dbo.CatImagen I (NOLOCK)    
		ON P.ImagenID = I.ImagenID AND P.OrganizacionId = I.OrganizacionID    
	WHERE P.OrganizacionId = @OrganizacionId      
	AND P.ProveedorID = @ProveedorId      
	AND CASE WHEN ISNULL(P.INE,0) = 0 THEN 0 ELSE P.INE END IN(1,2) AND P.Activo = 1

	SELECT     
		@IneCount = COUNT(I.ImagenID)
	FROM sukarne.dbo.CacProveedorImagen P (NOLOCK)    
	INNER JOIN sukarne.dbo.CatImagen I (NOLOCK)    
		ON P.ImagenID = I.ImagenID AND P.OrganizacionId = I.OrganizacionID    
	WHERE P.OrganizacionId = @OrganizacionId      
	AND P.ProveedorID = @ProveedorId      
	AND CASE WHEN ISNULL(P.INE,0) = 0 THEN 0 ELSE P.INE END In( 1,2) AND P.Activo = 1       

	-- CLAVE ÚNICA DE REGISTRO DE POBLACIÓN
	SELECT     
		TOP 1 @Curp = I.Imagen, @CurpId = I.ImagenID      
	FROM sukarne.dbo.CacProveedorImagen P (NOLOCK)    
	INNER JOIN sukarne.dbo.CatImagen I (NOLOCK)    
		ON P.ImagenID = I.ImagenID AND P.OrganizacionId = I.OrganizacionID    
	WHERE P.OrganizacionId = @OrganizacionId      
	AND P.ProveedorID = @ProveedorId     
	AND CASE WHEN RTRIM(LTRIM(ISNULL(P.CURP,''))) = '' THEN '' ELSE P.CURP END  <> '' AND P.Activo = 1
	AND I.ImagenID > @UltimoCurp

	SELECT     
		@CurpCount = COUNT(I.ImagenID)
	FROM sukarne.dbo.CacProveedorImagen P (NOLOCK)    
	INNER JOIN sukarne.dbo.CatImagen I (NOLOCK)    
		ON P.ImagenID = I.ImagenID AND P.OrganizacionId = I.OrganizacionID    
	WHERE P.OrganizacionId = @OrganizacionId      
	AND P.ProveedorID = @ProveedorId     
	AND CASE WHEN RTRIM(LTRIM(ISNULL(P.CURP,''))) = '' THEN '' ELSE P.CURP END  <> '' AND P.Activo = 1    

	SELECT     
		@CurpMax = MAX(I.ImagenID)
	FROM sukarne.dbo.CacProveedorImagen P (NOLOCK)    
	INNER JOIN sukarne.dbo.CatImagen I (NOLOCK)    
		ON P.ImagenID = I.ImagenID AND P.OrganizacionId = I.OrganizacionID    
	WHERE P.OrganizacionId = @OrganizacionId      
	AND P.ProveedorID = @ProveedorId     
	AND CASE WHEN RTRIM(LTRIM(ISNULL(P.CURP,''))) = '' THEN '' ELSE P.CURP END  <> '' AND P.Activo = 1    

	SELECT @Ine AS [Ine], 
			ISNULL(@IneId,0) AS [IneId], 
			ISNULL(@IneCount,0) AS [IneCount],
			ISNULL(@IneMax,0) AS [IneMax],
 
			@Curp AS [Curp], 
			ISNULL(@CurpId,0) AS [CurpId], 
			ISNULL(@CurpCount,0) AS [CurpCount],
			ISNULL(@CurpMax,0) AS [CurpMax]
     
SET NOCOUNT OFF    
END
GO