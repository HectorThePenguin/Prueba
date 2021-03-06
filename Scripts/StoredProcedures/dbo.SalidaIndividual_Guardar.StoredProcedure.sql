USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividual_Guardar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividual_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividual_Guardar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Pedro Delgado
-- Create date: 27/02/2014
-- Description: Grabar la salida del arete
-- Origen     : APInterfaces
/*EXEC SalidaIndividual_Guardar
	'<ROOT>
		<SalidaRecuperacionGrabar>
			<Arete>48400406522752</Arete>
			<Organizacion>4</Organizacion>
			<CodigoCorral>001</CodigoCorral>
			<CodigoCorraletaID>2</CodigoCorraletaID>
			<UsuarioCreacionID>1</UsuarioCreacionID>
			<TipoMovimiento>15</TipoMovimiento>
		</SalidaRecuperacionGrabar>
	 </ROOT>'*/
-- =============================================
CREATE PROCEDURE [dbo].[SalidaIndividual_Guardar]
@XmlSalidaRecuperacion XML
AS
BEGIN
	DECLARE @Arete VARCHAR(15)
	DECLARE @Organizacion INT
	DECLARE @CodigoCorral VARCHAR(10)
	DECLARE @CodigoCorraletaID INT
	DECLARE @UsuarioCreacionID INT
	DECLARE @TipoMovimientoID INT
	DECLARE @Cabezas INT
	DECLARE @Activo INT
	SELECT 
		@Arete = T.item.value('./Arete[1]', 'VARCHAR(15)'),
		@Organizacion = T.item.value('./Organizacion[1]', 'INT'),
		@CodigoCorral = T.item.value('./CodigoCorral[1]', 'VARCHAR(10)'),
		@CodigoCorraletaID = T.item.value('./CodigoCorraletaID[1]', 'INT'),
		@UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT'),
		@TipoMovimientoID = T.item.value('./TipoMovimiento[1]', 'INT')
	FROM @XmlSalidaRecuperacion.nodes('ROOT/SalidaRecuperacionGrabar') AS T(item)
	DECLARE @AnimalID INT
	DECLARE @LoteID INT
	-- DECLARE @CorraletaID INT
	--Obtener AnimalID
	SELECT @AnimalID = AnimalID 
	FROM Animal 
	WHERE Arete = @Arete AND OrganizacionIDEntrada = @Organizacion AND Activo = 1
	--Obtener LoteID
	SELECT @LoteID = LoteID,@Cabezas = Cabezas
	FROM Lote (NOLOCK) L
	INNER JOIN Corral (NOLOCK) C
	ON (L.CorralID = C.CorralID AND L.OrganizacionID = C.OrganizacionID)
	WHERE Codigo = @CodigoCorral AND L.OrganizacionID = @Organizacion AND L.Activo = 1 AND C.Activo = 1
	/*--Obtener CorralID de Corraleta
		SELECT @CorraletaID = CorralID 
		FROM Corral 
		WHERE Codigo = @CodigoCorraletaID AND OrganizacionID = @Organizacion AND Activo = 1
	*/
	--INSERTA EL REGISTRO
	INSERT INTO AnimalSalida (AnimalID,LoteID,CorraletaID,TipoMovimientoID,
														FechaSalida,Activo,FechaCreacion,UsuarioCreacionID)
	SELECT @AnimalID,@LoteID,@CodigoCorraletaID,@TipoMovimientoID,GETDATE(),1,GETDATE(),@UsuarioCreacionID
END

GO
