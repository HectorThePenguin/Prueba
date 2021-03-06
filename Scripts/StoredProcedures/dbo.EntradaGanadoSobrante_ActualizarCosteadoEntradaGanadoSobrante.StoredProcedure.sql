USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoSobrante_ActualizarCosteadoEntradaGanadoSobrante]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoSobrante_ActualizarCosteadoEntradaGanadoSobrante]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoSobrante_ActualizarCosteadoEntradaGanadoSobrante]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		C�sar Valdez
-- Create date: 2014-12-09
-- Description:	Metodo para Cambiar el estado de costeado de un animal en la tabla EntradaGanadoSobrante
-- EntradaGanadoSobrante_ActualizarCosteadoEntradaGanadoSobrante 1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanadoSobrante_ActualizarCosteadoEntradaGanadoSobrante]
	@CabezasSobrante XML
AS
BEGIN	
	DECLARE @CabezasSobranteTMP AS 
	  TABLE (EntradaGanadoSobranteID INT, 
			 AnimalID BIGINT, 
			 UsuarioModificacionID INT
			);
	/* Se llenan las cabezas que se ban actualziar en la tabla temporal */
	INSERT @CabezasSobranteTMP (EntradaGanadoSobranteID, AnimalID, UsuarioModificacionID)
	SELECT EntradaGanadoSobranteID = t.item.value('./EntradaGanadoSobranteID[1]', 'INT'),
	       AnimalID = t.item.value('./AnimalID[1]', 'BIGINT'),
		   UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')
	  FROM @CabezasSobrante.nodes('ROOT/CabezasSobrante') AS T(item);
	/* Actualizar la tabla EntradaGanadoSobrante */
	UPDATE EGS
	   SET Costeado = 1,
		   FechaModificacion = GETDATE(),
		   UsuarioModificacionID = CS.UsuarioModificacionID
      FROM EntradaGanadoSobrante EGS
	 INNER JOIN @CabezasSobranteTMP CS ON CS.EntradaGanadoSobranteID = EGS.EntradaGanadoSobranteID 
	                                  AND CS.AnimalID = EGS.AnimalID;
END

GO
