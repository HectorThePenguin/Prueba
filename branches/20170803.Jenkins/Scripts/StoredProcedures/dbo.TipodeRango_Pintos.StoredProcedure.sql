USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipodeRango_Pintos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipodeRango_Pintos]
GO
/****** Object:  StoredProcedure [dbo].[TipodeRango_Pintos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
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
CREATE PROCEDURE [dbo].[TipodeRango_Pintos]
@OrganizacionId INT 
AS  
BEGIN  
	
	  Select count(*) FROM CorralRango where OrganizacionID=2

END
GO
