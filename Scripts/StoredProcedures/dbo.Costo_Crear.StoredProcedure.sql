USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Costo_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Costo_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Costo_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para insertar un Costo
-- 
--=============================================
CREATE PROCEDURE [dbo].[Costo_Crear] 
	 @ClaveContable VARCHAR(3)
	,@Descripcion VARCHAR(50)
	,@TipoCostoID INT
	,@TipoProrrateoID INT
	,@RetencionID INT
	,@AbonoA CHAR(10)
	,@Activo BIT
	,@UsuarioCreacionID INT
	,@CompraIndividual BIT
	,@Compra BIT
	,@Recepcion BIT
	,@Gasto BIT
	,@Costo BIT
	,@TipoCostoIDCentro INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Costo (
		ClaveContable
		,Descripcion
		,TipoCostoID
		,TipoProrrateoID
		,Activo
		,UsuarioCreacionID
		,AbonoA
		,RetencionID
		,FechaCreacion
		,CompraIndividual
		,Compra
		,Recepcion
		,Gasto
		,Costo
		,TipoCostoIDCentro
		)
	VALUES (
		@ClaveContable
		,@Descripcion
		,@TipoCostoID
		,@TipoProrrateoID
		,@Activo
		,@UsuarioCreacionID
		,@AbonoA
		,@RetencionID
		,GETDATE()
		,@CompraIndividual
		,@Compra
		,@Recepcion
		,@Gasto
		,@Costo
		,@TipoCostoIDCentro
		)
		SET NOCOUNT OFF;
END

GO
