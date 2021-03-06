USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Corral_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Corral_Actualizar]
@CorralID INT
,@OrganizacionID	INT
,@Codigo	CHAR(10)
,@TipoCorralID	INT
,@Capacidad	INT
,@MetrosLargo	INT
,@MetrosAncho	BIGINT
,@Seccion	INT
,@Orden	INT
,@Activo	BIT
,@UsuarioModificacionID INT
AS
/*
=============================================
-- Author: Gilberto Carranza
-- Create date: 25-10-2013
-- Description:	Actualiza un Corral
-- =============================================
*/
BEGIN	
	SET NOCOUNT ON;
	UPDATE Corral
	SET 
		OrganizacionID = @OrganizacionID
		,Codigo = @Codigo
		,TipoCorralID = @TipoCorralID
		,Capacidad = @Capacidad
		,MetrosLargo = @MetrosLargo
		,MetrosAncho = @MetrosAncho
		,Seccion = @Seccion
		,Orden = @Orden
		,Activo = @Activo
		,FechaModificacion = GETDATE()
		,UsuarioModificacionID = @UsuarioModificacionID
	WHERE CorralID = @CorralID
	SELECT @@ROWCOUNT
	SET NOCOUNT OFF;
END

GO
