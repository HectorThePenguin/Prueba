USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_Placas]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Vigilancia_Placas]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_Placas]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eduardo Cota
-- Create date: 19/Mayo/2014
-- Description:	Otiene un listado de las placas de camion limitado por @Limite y por tipo de proveedor
-- Vigilancia_Placas 375, '', 1, 1,15
-- =============================================
CREATE PROCEDURE [dbo].[Vigilancia_Placas]
 @ProveedorID INT,    
 @Descripcion NVARCHAR(50),     
 @Activo BIT,    
 @Inicio INT,     
 @Limite INT    
AS    
BEGIN    
 SET NOCOUNT ON;    
 SELECT     
     ROW_NUMBER() OVER ( ORDER BY ProveedorID ASC) AS RowNum,    
  CamionID, 
  PlacaCamion,
  Activo
  INTO #Camion
  FROM Camion    
  WHERE (PlacaCamion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')    
    AND (@ProveedorID = ProveedorID)
    AND (Activo = @Activo)        
 SELECT     
  CamionID,     
  PlacaCamion,
  Activo    
 FROM    #Camion
  WHERE   RowNum BETWEEN @Inicio AND @Limite    
 SELECT     
  COUNT(CamionID)AS TotalReg     
 FROM #Camion     
END

GO
