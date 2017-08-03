USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InsertaRango_Pintos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InsertaRango_Pintos]
GO
/****** Object:  StoredProcedure [dbo].[InsertaRango_Pintos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author: José Luis Medina Murillo  
-- Create date: 03/06/2015  
-- Description: Cambio de rangos pintos mxli  
-- Empresa:   
-- Tipo 1 -Pintos 2 -Normales    
-- =============================================    
CREATE PROCEDURE [dbo].[InsertaRango_Pintos]  
@Corral VARCHAR(3) ,   
@OrganizacionId INT,   
@Sexo varchar(3)  
AS    
BEGIN    
   
 Declare @CorralId int  
 --Declare @Sexo varchar(1)  
  Delete FROM CorralRango where OrganizacionID = @OrganizacionId   
     
 --Obtenemos el CorralID e Insertamos el nuevo registro del corral de pintos  
 select @CorralId = CorralId from  corral WHERE organizacionid=2 and CODIGO = @Corral  
   
 --select @Sexo = Sexo FROM Animal A   
 -- LEFT JOIN AnimalMovimiento am ON am.AnimalID = a.AnimalID AND am.Activo = 1  
 -- LEFT JOIN Corral c ON c.CorralID = am.CorralID  
 -- LEFT JOIN TipoGanado tg ON tg.TipoGanadoID = A.TipoGanadoID  
 --WHERE C.OrganizacionID = @OrganizacionId and C.Codigo = @Corral  
  
 Insert into CorralRango Values (@OrganizacionId,@CorralId,@Sexo,1,600,1,getdate(),1267,NULL,NULL)  
  
END
GO
