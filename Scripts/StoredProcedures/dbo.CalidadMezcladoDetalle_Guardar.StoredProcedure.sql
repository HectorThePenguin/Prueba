USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezcladoDetalle_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadMezcladoDetalle_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezcladoDetalle_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Edgar Villarreal
-- Create date: 07/04/2014
-- Description:	Actualiza el CalidadMezcladoFactor
/*CalidadMezcladoDetalle_Guardar '
  <ROOT>
  <CalidadMezclado>
    <TipoMuestra>M Inicial</TipoMuestra>
    <NumeroMuestras>M2</NumeroMuestras>
    <Peso>123</Peso>
    <Particulas>123</Particulas>
    <CalidadMezcladoID>19</CalidadMezcladoID>
    <UsuarioCreacionID>5</UsuarioCreacionID>
  </CalidadMezclado>
  <CalidadMezclado>
    <TipoMuestra>M Media</TipoMuestra>
    <NumeroMuestras>M4</NumeroMuestras>
    <Peso>123</Peso>
    <Particulas>123</Particulas>
    <CalidadMezcladoID>19</CalidadMezcladoID>
    <UsuarioCreacionID>5</UsuarioCreacionID>
  </CalidadMezclado>
</ROOT>',1
'*/
--======================================================
create PROCEDURE [dbo].[CalidadMezcladoDetalle_Guardar]
@XmlCalidadMezclado XML,
@Activo BIT
AS
BEGIN
	DECLARE @TmpCalidadMezcladoDetalle TABLE(TipoMuestra VARCHAR(20),NumeroMuestras VARCHAR(20),Peso INT,Particulas INT, CalidadMezcladoID INT,UsuarioCreacionID INT )
	INSERT INTO @TmpCalidadMezcladoDetalle
	SELECT 
			TipoMuestra 					= T.item.value('./TipoMuestra[1]', 'VARCHAR(20)'),
			NumeroMuestras  			= T.item.value('./NumeroMuestras[1]', 'VARCHAR(20)'),
			Peso  								= T.item.value('./Peso[1]', 'INT'),
			Particulas    				= T.item.value('./Particulas[1]', 'INT'),
			CalidadMezcladoID    	= T.item.value('./CalidadMezcladoID[1]', 'INT'),
			UsuarioCreacionID    	= T.item.value('./UsuarioCreacionID[1]', 'INT')
		FROM  @XmlCalidadMezclado.nodes('ROOT/CalidadMezclado') AS T(item)
	INSERT INTO CalidadMezcladoDetalle
			(	CalidadMezcladoID,
				TipoMuestraID,
				NumeroMuestra,
				Peso ,
				Particulas,
				Activo,
				FechaCreacion,
				UsuarioCreacionID 
			)
	SELECT  TCMD.CalidadMezcladoID,
					TM.TipoMuestraID,
					TCMD.NumeroMuestras,
					TCMD.Peso,
					TCMD.Particulas,
					@Activo,
					GETDATE(),
					TCMD.UsuarioCreacionID
	FROM @TmpCalidadMezcladoDetalle TCMD 
	INNER JOIN TipoMuestra TM ON TM.Descripcion =  TCMD.TipoMuestra
END

GO
