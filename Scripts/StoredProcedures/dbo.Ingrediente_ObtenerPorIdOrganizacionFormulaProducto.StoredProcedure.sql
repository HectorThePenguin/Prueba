USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_ObtenerPorIdOrganizacionFormulaProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Ingrediente_ObtenerPorIdOrganizacionFormulaProducto]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_ObtenerPorIdOrganizacionFormulaProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:    Jose Angel Rodriguez
-- Create date: 08/12/2014  
--   
-- =============================================  
CREATE PROCEDURE [dbo].[Ingrediente_ObtenerPorIdOrganizacionFormulaProducto]  
@FormulaID INT
,@ProductoID	int
,@OrganizacionID	int  
,@Activo INT = 1
AS  
BEGIN  
 SELECT   
  IngredienteID,  
  OrganizacionID,  
  FormulaID,  
  ProductoID,  
  PorcentajeProgramado,  
  Activo,  
  FechaCreacion,  
  UsuarioCreacionID,  
  FechaModificacion,  
  UsuarioModificacionID  
 FROM Ingrediente (NOLOCK) I  
 WHERE FormulaID = @FormulaID AND Activo = @Activo
 AND ProductoID = @ProductoID
 AND OrganizacionID = @OrganizacionID
END  

GO
