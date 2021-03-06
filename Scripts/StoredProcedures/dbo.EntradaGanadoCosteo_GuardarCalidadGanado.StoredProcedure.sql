USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_GuardarCalidadGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoCosteo_GuardarCalidadGanado]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_GuardarCalidadGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 31/10/2013
-- Description:  Guardar el detalle de la entrada de ganado calidad (EntradaGanadoCalidad)
-- EntradaGanadoCosteo_GuardarCalidadGanado
-- ===============================================================
CREATE PROCEDURE [dbo].[EntradaGanadoCosteo_GuardarCalidadGanado]
	@XmlDetalle XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Detalle AS TABLE
	  (
		[EntradaGanadoCalidadID] INT,
		[EntradaGanadoID]	 INT,
		[CalidadGanadoID]		 INT,
		[Valor]					 INT,
		[Activo]				 BIT,
		[UsuarioCreacionID]		 INT,
		[UsuarioModificacionID]	 INT
	  )
	INSERT @Detalle
		   (
			[EntradaGanadoCalidadID],
			[EntradaGanadoID],
			[CalidadGanadoID],
			[Valor],
			[Activo],
			[UsuarioCreacionID],
			[UsuarioModificacionID]
			)
		SELECT 
			[EntradaGanadoCalidadID] = t.item.value('./EntradaGanadoCalidadID[1]', 'INT'),
			[EntradaGanadoID]	 = t.item.value('./EntradaGanadoID[1]', 'INT'),
			[CalidadGanadoID]        = t.item.value('./CalidadGanadoID[1]', 'INT'),
			[Valor]					 = t.item.value('./Valor[1]', 'INT'),
			[Activo]				 = t.item.value('./Activo[1]', 'BIT'),
			[UsuarioCreacionID]      = t.item.value('./UsuarioCreacionID[1]', 'INT'),
			[UsuarioModificacionID]  = t.item.value('./UsuarioModificacionID[1]', 'INT')
		FROM @XmlDetalle.nodes('ROOT/EntradaGanadoCalidad') AS T(item)
	UPDATE ec
	SET    
		[EntradaGanadoID]	= dt.[EntradaGanadoID],
		[CalidadGanadoID]		= dt.[CalidadGanadoID],
		[Valor]					= dt.[Valor],
		[Activo]				= dt.[Activo],
		[FechaModificacion]		= GETDATE(),
		[UsuarioModificacionID]	= NULLIF(dt.[UsuarioModificacionID], 0)
	FROM  EntradaGanadoCalidad ec
	INNER JOIN @Detalle dt
		ON dt.EntradaGanadoCalidadID = ec.EntradaGanadoCalidadID
	INSERT EntradaGanadoCalidad
		   (			
			[EntradaGanadoID],
			[CalidadGanadoID],
			[Valor],
			[Activo],
			[FechaCreacion], 
			[UsuarioCreacionID]
			)
	SELECT 		
		[EntradaGanadoID],
		[CalidadGanadoID],
		[Valor],
		[Activo],
		GETDATE(),
		[UsuarioCreacionID]
	FROM   @Detalle
	WHERE  EntradaGanadoCalidadID = 0
	SET NOCOUNT OFF;
END

GO
