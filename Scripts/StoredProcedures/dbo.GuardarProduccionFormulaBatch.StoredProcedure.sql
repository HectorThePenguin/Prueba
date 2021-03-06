USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GuardarProduccionFormulaBatch]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GuardarProduccionFormulaBatch]
GO
/****** Object:  StoredProcedure [dbo].[GuardarProduccionFormulaBatch]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Jose Angel Rodriguez
-- Create date: 25/11/2014  
-- Description: Gurda los datos capturados del archivo en la tabla ProduccionFormulaBatch  
-- SpName     : GuardarProduccionFormulaBatch
--======================================================  
CREATE PROCEDURE [dbo].[GuardarProduccionFormulaBatch]   
@XmlProduccionFormulaBatch XML
AS  
BEGIN  
 SET NOCOUNT ON;  
 DECLARE @ListaProduccionFormulaBatch AS TABLE   
  (  
   ProduccionFormulaID int,  
   OrganizacionID int,  
   ProductoId INT, --  
   FormulaID int,  
   RotomixID int,  
   Batch int,  
   CantidadProgramada int,  
   CantidadReal int, --  
   Activo bit,  
   UsuarioCreacionID int    
  )  
 INSERT @ListaProduccionFormulaBatch  
  (  
   ProduccionFormulaID,  
   OrganizacionID,  
   ProductoId, --  
   FormulaID,  
   RotomixID,  
   Batch,  
   CantidadProgramada,  
   CantidadReal, --  
   Activo,  
   UsuarioCreacionID  
  )  
 SELECT  
   ProduccionFormulaID = t.item.value('./ProduccionFormulaID[1]', 'INT'),
   OrganizacionID = t.item.value('./OrganizacionID[1]', 'INT'),
   ProductoID = t.item.value('./ProductoID[1]', 'INT'),
   FormulaID = t.item.value('./FormulaID[1]', 'INT'),
   RotomixID = t.item.value('./RotomixID[1]', 'INT'),
   Batch = t.item.value('./Batch[1]', 'INT'),
   CantidadProgramada = t.item.value('./CantidadProgramada[1]', 'INT'),
   CantidadReal = t.item.value('./CantidadReal[1]', 'INT'),
   Activo = t.item.value('./Activo[1]', 'INT'),
   UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
  FROM @XmlProduccionFormulaBatch.nodes('ROOT/ListaProduccionFormulaBatch') AS T(item) 
INSERT ProduccionFormulaBatch  
  (  
   ProduccionFormulaID,  
   OrganizacionID,  
   ProductoId, --  
   FormulaID,  
   RotomixID,  
   Batch,  
   CantidadProgramada,  
   CantidadReal, --  
   Activo,  
   FechaCreacion,  
   UsuarioCreacionID  
  )  
 SELECT   
   ProduccionFormulaID,  
   OrganizacionID,  
   ProductoId, --  
   FormulaID,  
   RotomixID,  
   Batch,  
   CantidadProgramada,  
   CantidadReal, --  
   Activo,  
   GETDATE(),  
   UsuarioCreacionID  
  FROM @ListaProduccionFormulaBatch  
 WHERE 1=1  
 SELECT @@IDENTITY AS INSERTO  
 SET NOCOUNT OFF;  
END  

GO
