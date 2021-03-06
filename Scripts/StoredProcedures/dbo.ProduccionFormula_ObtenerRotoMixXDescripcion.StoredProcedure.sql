USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ObtenerRotoMixXDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionFormula_ObtenerRotoMixXDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ObtenerRotoMixXDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================    
-- Author     : Jose Angel Rodriguez  
-- Create date: 04/12/2014    
-- Description:     
-- SpName     : ProduccionFormula_ObtenerRotoMixXDescripcion 1    
--======================================================    
CREATE PROCEDURE [dbo].[ProduccionFormula_ObtenerRotoMixXDescripcion]     
@organizacionID int,  
@Descripcion varchar (50)  
AS    
BEGIN    
 SET NOCOUNT ON;    
 SELECT    
  Descripcion,    
  RotoMixID    
 FROM    
  Rotomix    
 WHERE    
  OrganizacionID = @organizacionID    
  and Descripcion = @Descripcion  
 SET NOCOUNT OFF;    
END 

GO
