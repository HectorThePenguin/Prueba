USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionMateriaPrima_ActualizarPesajeMateriaPrima]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RecepcionMateriaPrima_ActualizarPesajeMateriaPrima]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionMateriaPrima_ActualizarPesajeMateriaPrima]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Roque Solis
-- Fecha: 2014-06-13
-- Descripci�n:	Actualiza el estatus de los pesajes
/* EXEC RecepcionMateriaPrima_ActualizarPesajeMateriaPrima '<ROOT>
  <PesajeMateriaPrima>
    <PesajeMateriaPrimaID>1</PesajeMateriaPrimaID>
    <EstatusID>2</EstatusID>
    <UsuarioModificacionID>5</UsuarioModificacionID>
  </PesajeMateriaPrima>
  <PesajeMateriaPrima>
    <PesajeMateriaPrimaID>2</PesajeMateriaPrimaID>
    <EstatusID>2</EstatusID>
    <UsuarioModificacionID>5</UsuarioModificacionID>
  </PesajeMateriaPrima>
</ROOT>'*/
-- =============================================
CREATE PROCEDURE [dbo].[RecepcionMateriaPrima_ActualizarPesajeMateriaPrima]
		@XmlPesajeDetalle XML
AS
BEGIN
	DECLARE @PesajeMateriaPrimaTemporal TABLE 
			(
			 [PesajeMateriaPrimaID] INT,
			 [EstatusID] INT,
			 [UsuarioModificacionID] INT
			)
	INSERT INTO @PesajeMateriaPrimaTemporal(
		PesajeMateriaPrimaID,
		EstatusID,
		UsuarioModificacionID
	)
	SELECT 
		t.item.value('./PesajeMateriaPrimaID[1]', 'INT'),
		t.item.value('./EstatusID[1]', 'INT'),
		t.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM   @XmlPesajeDetalle.nodes('ROOT/PesajeMateriaPrima') AS T(item)
	UPDATE PMP
	SET PMP.UsuarioIDRecibe = TMP.UsuarioModificacionID,
		PMP.FechaModificacion = GETDATE(),
		PMP.FechaRecibe = GETDATE(),
		PMP.UsuarioModificacionID = TMP.UsuarioModificacionID,
		PMP.EstatusID = CASE TMP.EstatusID WHEN 0 THEN PMP.EstatusID ELSE TMP.EstatusID END ,
		PMP.Activo = 0  
	FROM PesajeMateriaPrima PMP
	INNER JOIN @PesajeMateriaPrimaTemporal TMP ON(PMP.PesajeMateriaPrimaID = TMP.PesajeMateriaPrimaID)
END

GO
