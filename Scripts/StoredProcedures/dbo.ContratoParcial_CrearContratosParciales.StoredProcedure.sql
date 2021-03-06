USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ContratoParcial_CrearContratosParciales]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ContratoParcial_CrearContratosParciales]
GO
/****** Object:  StoredProcedure [dbo].[ContratoParcial_CrearContratosParciales]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 23/05/2014
-- Description: Crea un nuevo almacen movimiento costo
-- ContratoParcial_CrearContratosParciales '<ROOT>
--  <XmlContratoParcial>
--    <ContratoID>3</ContratoID>
--    <Cantidad>56000</Cantidad>
--   <Importe>0.44200</Importe>
--    <TipoCambioID>2</TipoCambioID>
--    <Activo>1</Activo>
--    <UsuarioCreacionID>5</UsuarioCreacionID>
--  </XmlContratoParcial>
--</ROOT>'
--=============================================
CREATE PROCEDURE [dbo].[ContratoParcial_CrearContratosParciales]
@XmlContratoParcial XML
AS
BEGIN
	DECLARE @tmpContratoParcial AS TABLE
	(
		ContratoID INT,
		Cantidad INT,
		Importe DECIMAL(18,4),
		TipoCambioID INT,
		Activo INT,
		UsuarioCreacionID INT
	)
	INSERT @tmpContratoParcial(
		ContratoID,
		Cantidad,
		Importe,
		TipoCambioID,
		Activo,
		UsuarioCreacionID
		)
	SELECT 
		ContratoID = T.item.value('./ContratoID[1]', 'INT'),
		Cantidad = T.item.value('./Cantidad[1]', 'INT'),
		Importe = T.item.value('./Importe[1]', 'DECIMAL(18,4)'),
		TipoCambioID = T.item.value('./TipoCambioID[1]', 'INT'),
		Activo = T.item.value('./Activo[1]', 'INT'),
		UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM  @XmlContratoParcial.nodes('ROOT/XmlContratoParcial') AS T(item)
			/* Se crea registro en la tabla contrato parcial*/
			INSERT INTO ContratoParcial(
				ContratoID,
				Cantidad,
				Importe,
				TipoCambioID,
				Activo,
				FechaCreacion,
			    UsuarioCreacionID
				)
			SELECT ContratoID,
				   Cantidad,
				   Importe,
				   TipoCambioID,
				   Activo,
				   GETDATE(),
				   UsuarioCreacionID
			FROM @tmpContratoParcial
END

GO
