USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[show]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[show]
GO
/****** Object:  StoredProcedure [dbo].[show]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[show]  
 @vlNombre [char](20)  
WITH EXECUTE AS CALLER  
AS  
Select 'SELECT * FROM '+ name + ' (NOLOCK)' From sysobjects(NOLOCK)  where name like'%'+Rtrim(@vlNombre)+'%' and type ='U'        
GO
