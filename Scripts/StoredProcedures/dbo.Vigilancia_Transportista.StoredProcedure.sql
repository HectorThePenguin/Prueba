USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_Transportista]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Vigilancia_Transportista]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_Transportista]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eduardo Cota
-- Create date: 19-05-2014
-- Description:	Otiene listado de productos para el modulo -Materias Prima/Vigilancia-
-- Vigilancia_Transportista 2, '','', 1, 1, 15
-- =============================================
CREATE PROCEDURE [dbo].[Vigilancia_Transportista]
 @TipoProveedorID INT,
 @ContratoID INT,
 @Descripcion NVARCHAR(50),
 @CodigoSap NVARCHAR(10),
 @Activo BIT,    
 @Inicio INT,
 @Limite INT
AS    
BEGIN
	SET NOCOUNT ON; 
	CREATE TABLE #Proveedor(
		RowNum INT,
		ProveedorID INT,
		Descripcion VARCHAR(100),
		CodigoSAP VARCHAR(10),
		Activo BIT
	)
	IF (@ContratoID > 0)
	BEGIN
		INSERT INTO #Proveedor
		SELECT
			ROW_NUMBER() OVER ( ORDER BY p.ProveedorID ASC) AS RowNum,
			p.ProveedorID,
			p.Descripcion,
			p.CodigoSAP,
			p.Activo    
		FROM Proveedor p
		INNER JOIN Flete F ON (p.ProveedorID = F.ProveedorID)
		WHERE (p.Descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')
			AND (p.CodigoSap LIKE '%'+@CodigoSap+'%' OR @CodigoSap ='')
			--AND (p.TipoProveedorID = @TipoProveedorID )   
			AND (F.ContratoID = @ContratoID)
			AND (p.Activo = @Activo)
			AND (f.Activo = @Activo)
		GROUP BY p.ProveedorID, p.Descripcion,
			p.CodigoSAP,
			p.Activo
	END
	ELSE
	BEGIN
		INSERT INTO #Proveedor
		SELECT
			ROW_NUMBER() OVER ( ORDER BY p.ProveedorID ASC) AS RowNum,
			p.ProveedorID,
			p.Descripcion,
			p.CodigoSAP,
			p.Activo     
		FROM Proveedor p
		WHERE (p.Descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')
			AND (p.CodigoSap LIKE '%'+@CodigoSap+'%' OR @CodigoSap ='')
			--AND (p.TipoProveedorID = @TipoProveedorID )
			AND (p.Activo = @Activo)
	END
	SELECT     
		P.ProveedorID,
		P.Descripcion,
		p.CodigoSAP,
		P.Activo     
	FROM #Proveedor P
	WHERE RowNum BETWEEN @Inicio AND @Limite 
	SELECT     
		COUNT(ProveedorID)AS TotalReg     
	FROM #Proveedor
END

GO
