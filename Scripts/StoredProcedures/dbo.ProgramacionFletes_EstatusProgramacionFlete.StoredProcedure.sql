USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_EstatusProgramacionFlete]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionFletes_EstatusProgramacionFlete]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_EstatusProgramacionFlete]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Roque Solis
-- Fecha: 2014-06-13
-- Descripci�n:	Actualiza el estatus a los fletes
/* EXEC ProgramacionFletes_EstatusProgramacionFlete '<ROOT>
  <ProgramacionFleteDetalle>
    <FleteID>1</FleteID>
    <Activo>0</Activo>
    <UsuarioModificacionID>5</UsuarioModificacionID>
  </ProgramacionFleteDetalle>
  <ProgramacionFleteDetalle>
    <FleteID>2</FleteID>
    <Activo>0</Activo>
    <UsuarioModificacionID>5</UsuarioModificacionID>
  </ProgramacionFleteDetalle>
</ROOT>'*/
-- =============================================
CREATE PROCEDURE [dbo].[ProgramacionFletes_EstatusProgramacionFlete]
		@XmlGuardarProgramacionFlete XML
AS
BEGIN
	DECLARE @FleteDetalleTemporal TABLE 
			(
			 [FleteID] INT,
			 [Activo] INT,
			 [UsuarioModificacionID] INT
			)
	INSERT INTO @FleteDetalleTemporal(
		FleteID,
		Activo,
		UsuarioModificacionID
	)
	SELECT 
		t.item.value('./FleteID[1]', 'INT'),
		t.item.value('./Activo[1]', 'INT'),
		t.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM   @XmlGuardarProgramacionFlete.nodes('ROOT/ProgramacionFleteDetalle') AS T(item)
	UPDATE F
	SET Activo = TMP.Activo,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = TMP.UsuarioModificacionID
	FROM Flete F
	INNER JOIN @FleteDetalleTemporal TMP ON(F.FleteID = TMP.FleteID)
END

GO
