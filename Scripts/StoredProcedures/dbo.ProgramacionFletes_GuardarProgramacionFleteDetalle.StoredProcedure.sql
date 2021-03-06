USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_GuardarProgramacionFleteDetalle]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionFletes_GuardarProgramacionFleteDetalle]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_GuardarProgramacionFleteDetalle]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Edgar.Villarreal
-- Fecha: 2014-03-12
-- Descripci�n:	Guardar en AnimalSalida con xmll
/* EXEC ProgramacionFletes_GuardarProgramacionFleteDetalle '<ROOT>
  <ProgramacionFleteDetalle>
    <FleteID>5</FleteID>
	<FleteDetalleID>5<FleteDetalleID>
    <CostoID>4</CostoID>
    <Tarifa>123.00</Tarifa>
    <Activo>1</Activo>
    <UsuarioCreacionID>5</UsuarioCreacionID>
  </ProgramacionFleteDetalle>
  <ProgramacionFleteDetalle>
    <FleteID>5</FleteID>
	<FleteDetalleID>5<FleteDetalleID>
    <CostoID>4</CostoID>
    <Tarifa>234.00</Tarifa>
    <Activo>1</Activo>
    <UsuarioCreacionID>5</UsuarioCreacionID>
  </ProgramacionFleteDetalle>
  <ProgramacionFleteDetalle>
    <FleteID>5</FleteID>
	<FleteDetalleID>5<FleteDetalleID>
    <CostoID>4</CostoID>
    <Tarifa>345.00</Tarifa>
    <Activo>1</Activo>
    <UsuarioCreacionID>5</UsuarioCreacionID>
  </ProgramacionFleteDetalle>
  <ProgramacionFleteDetalle>
    <FleteID>5</FleteID>
	<FleteDetalleID>5<FleteDetalleID>
    <CostoID>4</CostoID>
    <Tarifa>1</Tarifa>
    <Activo>1</Activo>
    <UsuarioCreacionID>5</UsuarioCreacionID>
  </ProgramacionFleteDetalle>
</ROOT>',1*/
-- =============================================
CREATE PROCEDURE [dbo].[ProgramacionFletes_GuardarProgramacionFleteDetalle]
		@XmlGuardarProgramacionFlete XML,
		@Activo INT
AS
BEGIN
    DECLARE @insertar INT
	DECLARE @FleteDetalleTemporal TABLE 
			(
			 [FleteID] INT,
			 [FleteDetalleID] INT,
			 [CostoID] INT,
			 [Tarifa] DECIMAL(17,4),
			 [Activo] INT,
			 [UsuarioCreacionID] INT,
			 [Opcion] INT
			)
	INSERT INTO @FleteDetalleTemporal(
		FleteID,
		FleteDetalleID,
		CostoID,
		Tarifa,
		Activo,
		UsuarioCreacionID,
		Opcion
	)
	SELECT 
		t.item.value('./FleteID[1]', 'INT'),
		t.item.value('./FleteDetalleID[1]', 'INT'),
		t.item.value('./CostoID[1]', 'INT'),
		t.item.value('./Tarifa[1]', 'DECIMAL(17,4)'),
		t.item.value('./Activo[1]', 'INT'),
		t.item.value('./UsuarioCreacionID[1]', 'INT'),
		t.item.value('./Opcion[1]', 'INT')
	FROM   @XmlGuardarProgramacionFlete.nodes('ROOT/ProgramacionFleteDetalle') AS T(item)
		INSERT INTO FleteDetalle(
			FleteID,
			CostoID,
			Tarifa,
			Activo,
			FechaCreacion,
			UsuarioCreacionID
		)
		SELECT FleteID,
			CostoID,
			Tarifa,
			Activo,
			GETDATE(),
			UsuarioCreacionID
		FROM @FleteDetalleTemporal 
		WHERE Opcion = 1
	UPDATE FD
	SET Tarifa = TMP.Tarifa,
		CostoID = TMP.CostoID,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = TMP.UsuarioCreacionID
	FROM FleteDetalle FD
	INNER JOIN @FleteDetalleTemporal TMP ON(FD.FleteID = TMP.FleteID AND FD.FleteDetalleID = TMP.FleteDetalleID)
	WHERE Opcion = 2
END

GO
