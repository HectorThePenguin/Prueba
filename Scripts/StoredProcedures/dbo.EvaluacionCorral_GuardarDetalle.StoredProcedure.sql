USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EvaluacionCorral_GuardarDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EvaluacionCorral_GuardarDetalle]
GO
/****** Object:  StoredProcedure [dbo].[EvaluacionCorral_GuardarDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================      
-- Author:    Marco Zamora      
-- Create date: 21/11/2013      
-- Description:  Guardar las preguntas realizadas en la Evaluacion de Riesgos de Corral      
-- Origen:	  APInterfaces      
-- =============================================      
CREATE PROCEDURE [dbo].[EvaluacionCorral_GuardarDetalle]        
 @XmlEvaluacionCorralDetalle XML      
AS      
BEGIN      
 SET NOCOUNT ON;      
 DECLARE @Detalle AS TABLE      
   (      
  [OrganizacionID] INT,      
  [EvaluacionID]        INT,      
  [PreguntaID]                   INT,      
  [Respuesta]                      VARCHAR(100),      
  [FechaModificacion]             SMALLDATETIME,
  [UsuarioModificacion]         VARCHAR(50),
  [UsuarioCreacion]         VARCHAR(50)    
   )      
 INSERT @Detalle      
     ([OrganizacionID],      
    [EvaluacionID],      
    [PreguntaID],      
    [Respuesta],      
    [FechaModificacion],      
    [UsuarioModificacion],
	[UsuarioCreacion])      
  SELECT       
    [OrganizacionID] = t.item.value('./OrganizacionID[1]', 'INT'),      
    [EvaluacionID] = t.item.value('./EvaluacionID[1]', 'INT'),      
    [PreguntaID]            = t.item.value('./PreguntaID[1]', 'INT'),      
    [Respuesta]               = t.item.value('./Respuesta[1]', 'VARCHAR(100)'),      
    [FechaModificacion]                = GETDATE(),      
    [UsuarioModificacion]               = t.item.value('./UsuarioModificacion[1]', 'VARCHAR(50)'),
	[UsuarioCreacion]               = t.item.value('./UsuarioCreacion[1]', 'VARCHAR(50)')     
   FROM   @XmlEvaluacionCorralDetalle.nodes('ROOT/EvaluacionDetalle') AS T(item)      
 INSERT EvaluacionCorralDetalle
     ([EvaluacionCorralID],      
    [PreguntaID],      
    [Respuesta],    
    [Activo],    
	[FechaCreacion],  
	[UsuarioCreacion]         
   )      
 SELECT [EvaluacionID],      
    [PreguntaID],      
    [Respuesta],     
    1,    
	GETDATE(),
	[UsuarioCreacion]  
 FROM   @Detalle      
 SET NOCOUNT OFF;      
END

GO
