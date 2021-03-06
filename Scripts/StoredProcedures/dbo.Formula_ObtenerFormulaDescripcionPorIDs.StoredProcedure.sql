USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerFormulaDescripcionPorIDs]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Formula_ObtenerFormulaDescripcionPorIDs]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerFormulaDescripcionPorIDs]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author: Cesar Fernando Vega Vazquez  
-- Create date: 26/04/2013  
-- Description: Consulta para obtener la descripcion de formulas por IDs  
-- Empresa: SuKarne  
-- =============================================  
create procedure [dbo].[Formula_ObtenerFormulaDescripcionPorIDs]  
 @ids varchar(max)  
as  
begin  
 set NOCOUNT ON  
 DECLARE @tabla TABLE  
 (  
  id int  
 );  
 INSERT INTO @tabla  
 SELECT Registros from dbo.FuncionSplit(@ids, '|')  
 SELECT   
  f.FormulaID  
  , f.Descripcion   
 FROM   
  Formula f   
  INNER JOIN @tabla t on f.FormulaID = t.id  
 set NOCOUNT OFF  
end

GO
