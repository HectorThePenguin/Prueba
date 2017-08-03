USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[showV]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[showV]
GO
/****** Object:  StoredProcedure [dbo].[showV]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[showV]      
 @vlNombre [char](20)      
WITH EXECUTE AS CALLER      
AS      
Select 'Sp_helpText '+ name From sysobjects(NOLOCK)  where name like'%'+Rtrim(@vlNombre)+'%' and type ='V' 
GO
