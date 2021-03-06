USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCambio_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCambio_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TipoCambio_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 16/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoCambio_Crear
--======================================================
CREATE PROCEDURE [dbo].[TipoCambio_Crear] @MonedaID INT
	,@Descripcion VARCHAR(50)
	,@Cambio DECIMAL(10, 4)
	,@Fecha DATETIME
	,@Activo BIT
	,@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT TipoCambio (
		MonedaID
		,Descripcion
		,Cambio
		,Fecha
		,Activo
		,UsuarioCreacionID
		,FechaCreacion
		)
	VALUES (
		@MonedaID
		,@Descripcion
		,@Cambio
		,@Fecha
		,@Activo
		,@UsuarioCreacionID
		,GETDATE()
		)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
