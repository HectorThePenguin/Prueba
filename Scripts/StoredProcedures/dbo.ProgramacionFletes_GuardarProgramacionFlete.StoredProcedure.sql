USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_GuardarProgramacionFlete]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionFletes_GuardarProgramacionFlete]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_GuardarProgramacionFlete]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Edgar.Villarreal
-- Fecha: 2014-03-12
-- Descripci�n:	Guardar en AnimalSalida con xmll
/* EXEC ProgramacionFletes_GuardarProgramacionFlete '<ROOT>
  <ProgramacionFlete>
    <ContratoID>10</ContratoID>
    <OrganizacionID>5</OrganizacionID>
    <ProveedorID>4859</ProveedorID>
    <MermaPermitida>67.00</MermaPermitida>
    <UsuarioCreacionID>5</UsuarioCreacionID>
    <UsuarioModificacionID>5</UsuarioModificacionID>
    <Activo>1</Activo>
    <Observaciones>12</Observaciones>
    <Opcion>1</Opcion>
  </ProgramacionFlete>
  <ProgramacionFlete>
    <ContratoID>10</ContratoID>
    <OrganizacionID>5</OrganizacionID>
    <ProveedorID>4381</ProveedorID>
    <MermaPermitida>0.10</MermaPermitida>
    <UsuarioCreacionID>5</UsuarioCreacionID>
    <UsuarioModificacionID>5</UsuarioModificacionID>
    <Activo>1</Activo>
    <Observaciones>asdasda asd asd as ad asd asd asd</Observaciones>
    <Opcion>1</Opcion>
  </ProgramacionFlete>
</ROOT>',1*/
-- =============================================
CREATE PROCEDURE [dbo].[ProgramacionFletes_GuardarProgramacionFlete]
		@XmlGuardarProgramacionFlete XML,
		@Activo INT
AS
BEGIN
	DECLARE @FleteTemporal TABLE 
			(
			 [OrganizacionID] INT,
			 [ContratoID] INT,
			 [ProveedorID] INT,
			 [MermaPermitida] DECIMAL(10,3),
			 [TipoTarifaID] INT,
			 [Observacion] VARCHAR(255),
			 [Activo] INT,
			 [UsuarioCreacionID] INT,
			 [Opcion] INT
			)
	INSERT INTO @FleteTemporal(
		OrganizacionID,
		ContratoID,
		ProveedorID,
		MermaPermitida,
		TipoTarifaID,
		Observacion,
		Activo,
		UsuarioCreacionID,
		Opcion)
	SELECT 
		t.item.value('./OrganizacionID[1]', 'INT'),
		t.item.value('./ContratoID[1]', 'INT'),
		t.item.value('./ProveedorID[1]', 'INT'),
		t.item.value('./MermaPermitida[1]', 'DECIMAL(10,3)'),
		t.item.value('./TipoTarifaID[1]','INT'),
		t.item.value('./Observaciones[1]', 'VARCHAR(255)'),
		t.item.value('./Activo[1]', 'INT'),
		t.item.value('./UsuarioCreacionID[1]', 'INT'),
		t.item.value('./Opcion[1]', 'INT')
	FROM   @XmlGuardarProgramacionFlete.nodes('ROOT/ProgramacionFlete') AS T(item)
	INSERT INTO Flete(
		OrganizacionID,
		ContratoID,
		ProveedorID,
		MermaPermitida,
		TipoTarifaID,
		Observaciones,
		Activo,
		FechaCreacion,
		UsuarioCreacionID)
	SELECT OrganizacionID ,ContratoID,ProveedorID,MermaPermitida,TipoTarifaID,Observacion,Activo,GETDATE(),UsuarioCreacionID
	FROM @FleteTemporal 
	WHERE Opcion = 1
	UPDATE 	F
	SET MermaPermitida = TMP.MermaPermitida, 
		Observaciones = TMP.Observacion,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = TMP.UsuarioCreacionID
	FROM Flete F
	INNER JOIN @FleteTemporal TMP ON (F.OrganizacionID = TMP.OrganizacionID AND 
									  F.ContratoID = TMP.ContratoID AND 
									  F.ProveedorID = TMP.ProveedorID)
	WHERE TMP.Opcion = 2
END

GO
