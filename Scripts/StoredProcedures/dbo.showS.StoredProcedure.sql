USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[showS]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[showS]
GO
/****** Object:  StoredProcedure [dbo].[showS]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create Procedure [dbo].[showS] @vlNombre char(20)        
as        
Select 'Sp_helpText '+ name From sysobjects(NOLOCK)  where name like'%'+Rtrim(@vlNombre)+'%' and type ='P'        
  
  
  
GO
