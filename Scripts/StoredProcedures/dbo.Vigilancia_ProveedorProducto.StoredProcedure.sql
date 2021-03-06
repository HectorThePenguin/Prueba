USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_ProveedorProducto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Vigilancia_ProveedorProducto]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_ProveedorProducto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eduardo Cota
-- Create date: 16-05-2014
-- Description:	Obtiene listado de productos para el modulo -Materias Prima/Vigilancia-
-- EXEC Vigilancia_ProveedorProducto  '','', 1, 1, 1500,0,0 
-- =============================================
CREATE PROCEDURE [dbo].[Vigilancia_ProveedorProducto]
 @Descripcion NVARCHAR(50),
 @CodigoSap NVARCHAR(10),
 @Activo BIT,    
 @Inicio INT,
 @Limite INT,
 @TipoProveedorID INT,
 @Producto INT
AS    
BEGIN    
 SET NOCOUNT ON; 
 SELECT
  ROW_NUMBER() OVER ( ORDER BY P.Descripcion ASC) AS RowNum,
  P.ProveedorID,
  P.Descripcion,
  P.CodigoSAP,
  P.Activo     
  INTO #Proveedor
  FROM Proveedor P
  WHERE (P.Descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')    
    AND (@TipoProveedorID = P.TipoProveedorID)
    AND (P.Activo = @Activo) 
	AND (p.CodigoSap LIKE '%'+@CodigoSap+'%' OR @CodigoSap ='')
	GROUP BY P.ProveedorID,P.Descripcion,P.CodigoSAP,P.Activo  
SELECT     
  P.ProveedorID,
  P.Descripcion,
  P.CodigoSAP,
  P.Activo     
 FROM    
  #Proveedor P
 WHERE RowNum BETWEEN @Inicio AND @Limite 
 SELECT     
  COUNT(ProveedorID)AS TotalReg     
 FROM #Proveedor
END

GO
