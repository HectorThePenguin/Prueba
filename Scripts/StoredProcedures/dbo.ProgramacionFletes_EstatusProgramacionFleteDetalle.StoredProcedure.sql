USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_EstatusProgramacionFleteDetalle]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionFletes_EstatusProgramacionFleteDetalle]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_EstatusProgramacionFleteDetalle]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Roque Solis
-- Fecha: 2014-06-13
-- Descripci�n:	Actualiza el estatus al flete detalle
/* EXEC ProgramacionFletes_EstatusProgramacionFleteDetalle '<ROOT>
  <ProgramacionFleteDetalle>
    <FleteDetalleID>1</FleteDetalleID>
    <Activo>0</Activo>
    <UsuarioModificacionID>5</UsuarioModificacionID>
  </ProgramacionFleteDetalle>
  <ProgramacionFleteDetalle>
    <FleteDetalleID>2</FleteDetalleID>
    <Activo>0</Activo>
    <UsuarioModificacionID>5</UsuarioModificacionID>
  </ProgramacionFleteDetalle>
</ROOT>'*/
-- =============================================
CREATE PROCEDURE [dbo].[ProgramacionFletes_EstatusProgramacionFleteDetalle]
		@XmlFleteDetalle XML
AS
BEGIN
	DECLARE @FleteDetalleTemporal TABLE 
			(
			 [FleteDetalleID] INT,
			 [Activo] INT,
			 [UsuarioModificacionID] INT
			)
	INSERT INTO @FleteDetalleTemporal(
		FleteDetalleID,
		Activo,
		UsuarioModificacionID
	)
	SELECT 
		t.item.value('./FleteDetalleID[1]', 'INT'),
		t.item.value('./Activo[1]', 'INT'),
		t.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM   @XmlFleteDetalle.nodes('ROOT/ProgramacionFleteDetalle') AS T(item)
	UPDATE FD
	SET Activo = TMP.Activo,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = TMP.UsuarioModificacionID
	FROM FleteDetalle FD
	INNER JOIN @FleteDetalleTemporal TMP ON(FD.FleteDetalleID = TMP.FleteDetalleID)
END

GO
