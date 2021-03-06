USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CambioRango_Pintos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CambioRango_Pintos]
GO
/****** Object:  StoredProcedure [dbo].[CambioRango_Pintos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
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
CREATE PROCEDURE [dbo].[CambioRango_Pintos]
@OrganizacionId INT, 
@tipo int 
AS  
BEGIN  
	
	IF @tipo = 1 
	BEGIN
		--Eliminamos la tabla de respaldo
		drop table CorralRango_mxli

		---Se respaldan los Corrales 
		Select * into CorralRango_mxli FROM CorralRango where OrganizacionID=2

		--Eliminamos los corrales de la Organización
		Delete FROM CorralRango where OrganizacionID = @OrganizacionId 
	END

	IF @tipo = 2 
	BEGIN
		---Despues que lo trabajan, se elimina el rango insertado.
		Delete FROM CorralRango where OrganizacionID=2

		---Se regresa el respaldo de la corralrango.
		insert into CorralRango
		SELECT * FROM CorralRango_mxli (NOLOCK)
	END

END
GO
