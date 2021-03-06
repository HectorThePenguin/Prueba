USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ContratoDetalle_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ContratoDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ContratoDetalle_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 21/05/2014
-- Description: Crea un nuevo contrato detalle
-- ContratoDetalle_Crear
--=============================================
CREATE PROCEDURE [dbo].[ContratoDetalle_Crear] @XmlContratoDetalle XML
AS
BEGIN
	DECLARE @tmpContratoDetalle AS TABLE (
		ContratoID INT
		,IndicadorID INT
		,PorcentajePermitido DECIMAL(10, 2)
		,Activo INT
		,UsuarioCreacionID INT
		)
	INSERT @tmpContratoDetalle (
		ContratoID
		,IndicadorID
		,PorcentajePermitido
		,Activo
		,UsuarioCreacionID
		)
	SELECT ContratoID = T.item.value('./ContratoID[1]', 'INT')
		,IndicadorID = T.item.value('./IndicadorID[1]', 'INT')
		,PorcentajePermitido = T.item.value('./PorcentajePermitido[1]', 'decimal(10,2)')
		,Activo = T.item.value('./Activo[1]', 'INT')
		,UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM @XmlContratoDetalle.nodes('ROOT/XmlContratoDetalle') AS T(item)
	/* Se crea registro en la tabla de Orden sacrificio*/
	INSERT INTO ContratoDetalle (
		ContratoID
		,IndicadorID
		,PorcentajePermitido
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		)
	SELECT ContratoID
		,IndicadorID
		,PorcentajePermitido
		,Activo
		,GETDATE()
		,UsuarioCreacionID
	FROM @tmpContratoDetalle
END

GO
