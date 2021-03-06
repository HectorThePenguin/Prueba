USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ContratoHumedad_CrearContratoHumedad]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ContratoHumedad_CrearContratoHumedad]
GO
/****** Object:  StoredProcedure [dbo].[ContratoHumedad_CrearContratoHumedad]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 22/08/2014
-- Description: Crea registros por medio de un xml
-- ContratoHumedad_CrearContratoHumedad 
--=============================================
CREATE PROCEDURE [dbo].[ContratoHumedad_CrearContratoHumedad]
@XmlContratoHumedad XML 
AS
BEGIN

	DECLARE @tmpContratoHumedad AS TABLE
	(
		ContratoID INT,
		FechaInicio DATETIME,
		PorcentajeHumedad DECIMAL(10,2),
		Activo INT,
		UsuarioCreacionID INT
	)
	
	INSERT @tmpContratoHumedad(
		ContratoID,
		FechaInicio,
		PorcentajeHumedad,
		Activo,
		UsuarioCreacionID
		)
	SELECT 
		ContratoID = T.item.value('./ContratoID[1]', 'INT'),
		FechaInicio = T.item.value('./FechaInicio[1]', 'DATETIME'),
		PorcentajeHumedad = T.item.value('./PorcentajeHumedad[1]', 'DECIMAL(10,2)'),
		Activo = T.item.value('./Activo[1]', 'INT'),
		UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM  @XmlContratoHumedad.nodes('ROOT/XmlContratoHumedad') AS T(item)
	
			/* Se crea registro en la tabla contrato parcial*/
			INSERT INTO ContratoHumedad(
				ContratoID,
				FechaInicio,
				PorcentajeHumedad,
				Activo,
				FechaCreacion,
			    UsuarioCreacionID
				)
			SELECT ContratoID,
				   FechaInicio,
				   PorcentajeHumedad,
				   Activo,
				   GETDATE(),
				   UsuarioCreacionID
			FROM @tmpContratoHumedad
END

GO
