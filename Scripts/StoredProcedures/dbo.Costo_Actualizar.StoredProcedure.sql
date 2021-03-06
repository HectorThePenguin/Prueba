USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Costo_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Costo_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Costo_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para actualizar un Costo
-- 
--=============================================
CREATE PROCEDURE [dbo].[Costo_Actualizar] 
	 @CostoID INT
	,@ClaveContable VARCHAR(3)
	,@Descripcion VARCHAR(50)
	,@TipoCostoID INT
	,@TipoProrrateoID INT
	,@Activo BIT
	,@UsuarioModificacionID INT
	,@RetencionID INT
	,@AbonoA CHAR(10)
	,@CompraIndividual BIT
	,@Compra BIT
	,@Recepcion BIT
	,@Gasto BIT
	,@Costo BIT
	,@TipoCostoIDCentro INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Costo
	SET ClaveContable = @ClaveContable
		,Descripcion = @Descripcion
		,TipoCostoID = @TipoCostoID
		,TipoProrrateoID = @TipoProrrateoID
		,Activo = @Activo
		,FechaModificacion = GETDATE()
		,RetencionID = @RetencionID
		,AbonoA = @AbonoA
		,CompraIndividual = @CompraIndividual
		,Compra = @Compra
		,Recepcion = @Recepcion
		,Gasto = @Gasto
		,Costo = @Costo
		,TipoCostoIDCentro = @TipoCostoIDCentro
		,UsuarioModificacionID = @UsuarioModificacionID
	WHERE CostoID = @CostoID
	SET NOCOUNT OFF;
END

GO
