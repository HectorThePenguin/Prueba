USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConsultarFactores_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConsultarFactores_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[ConsultarFactores_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Edgar Villarreal
-- Create date: 07/04/2014
-- Description:	Actualiza el CalidadMezcladoFactor
/*ConsultarFactores_Actualizar '
 <ROOT>
 <CalidadMezclado>
  <TipoMuestraID>1</TipoMuestraID> 
  <Factor>1.1111</Factor> 
  <PesoBaseHumeda>50</PesoBaseHumeda> 
  <PesoBaseSeca>424</PesoBaseSeca> 
  <UsuarioModifica>5</UsuarioModifica> 
  </CalidadMezclado>
 <CalidadMezclado>
  <TipoMuestraID>3</TipoMuestraID> 
  <Factor>1.3333</Factor> 
  <PesoBaseHumeda>500</PesoBaseHumeda> 
  <PesoBaseSeca>42</PesoBaseSeca> 
  <UsuarioModifica>5</UsuarioModifica> 
  </CalidadMezclado>
  </ROOT>'
'*/
--======================================================
create PROCEDURE [dbo].[ConsultarFactores_Actualizar]
@XmlCalidadMezclado XML
AS
BEGIN
	DECLARE @TmpCalidadMezcladoFactor TABLE(TipoMuestraID INT,Factor DECIMAL(14,2),PesoBaseHumeda INT,PesoBaseSeca INT,UsuarioModifica INT)
	INSERT INTO @TmpCalidadMezcladoFactor
	SELECT 
			TipoMuestraID  	= T.item.value('./TipoMuestraID[1]', 'INT'),
			Factor  				= T.item.value('./Factor[1]', 'DECIMAL(14,2)'),
			PesoBaseHumeda  = T.item.value('./PesoBaseHumeda[1]', 'INT'),
			PesoBaseSeca    = T.item.value('./PesoBaseSeca[1]', 'INT'),
			UsuarioModifica = T.item.value('./UsuarioModifica[1]', 'INT')
		FROM  @XmlCalidadMezclado.nodes('ROOT/CalidadMezclado') AS T(item)
	UPDATE CMF
	SET CMF.TipoMuestraID = TCMF.TipoMuestraID,
			CMF.Factor = TCMF.Factor,
			CMF.PesoBaseHumeda = TCMF.PesoBaseHumeda,
			CMF.PesoBaseSeca = TCMF.PesoBaseSeca,
			CMF.FechaModificacion = GETDATE(),
			CMF.UsuarioModificacionID = TCMF.UsuarioModifica
	FROM CalidadMezcladoFactor CMF
	INNER JOIN @TmpCalidadMezcladoFactor TCMF ON TCMF.TipoMuestraID = CMF.TipoMuestraID
END

GO
