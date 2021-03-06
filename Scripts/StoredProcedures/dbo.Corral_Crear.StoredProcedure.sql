USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Corral_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Corral_Crear]
@OrganizacionID	INT
,@Codigo	CHAR(10)
,@TipoCorralID	INT
,@Capacidad	INT
,@MetrosLargo	INT
,@MetrosAncho	BIGINT
,@Seccion	INT
,@Orden	INT
,@Activo	BIT
,@UsuarioCreacionID	INT
AS
/*
=============================================
-- Author: Gilberto Carranza
-- Create date: 25-10-2013
-- Description:	Crea un Corral
-- =============================================
*/
BEGIN	
	SET NOCOUNT ON;
	INSERT INTO Corral
	(
		OrganizacionID
		,Codigo
		,TipoCorralID
		,Capacidad
		,MetrosLargo
		,MetrosAncho
		,Seccion
		,Orden
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
	)
	VALUES
	(	
		@OrganizacionID
		,@Codigo
		,@TipoCorralID
		,@Capacidad
		,@MetrosLargo
		,@MetrosAncho
		,@Seccion
		,@Orden
		,@Activo
		,GETDATE()
		,@UsuarioCreacionID
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
