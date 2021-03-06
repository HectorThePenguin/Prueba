USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_GrabarMuerte]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_GrabarMuerte]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_GrabarMuerte]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 20/02/2014
-- Description:	Obtiene los grados a mostrar
/*DeteccionGanado_GrabarMuerte	'<ROOT>
			  <MuerteGrabar>
					<Organizacion>4</Organizacion>
					<CorralID>1</CorralID>
					<Arete></Arete>
					<AreteMetalico></AreteMetalico>
					<Observaciones>67</Observaciones>
					<LoteID>2</LoteID>
					<OperadorDeteccion>4</OperadorDeteccion>
					<FotoDeteccion>VACA1.png</FotoDeteccion>
					<EstatusID>3</EstatusID>
					<Activo>6</Activo>
					<UsuarioID>6</UsuarioID>
			  </MuerteGrabar>
			</ROOT>'*/
--======================================================
CREATE PROCEDURE [dbo].[DeteccionGanado_GrabarMuerte]
@XmlMuerte XML
AS
BEGIN
	/* Se crea tabla temporal para almacenar el XML */
	DECLARE @MuerteTem AS TABLE
	(
		Arete VARCHAR(15),
		AreteMetalico VARCHAR(15),
		Observaciones VARCHAR(255),
		LoteID INT,
		OperadorDeteccion INT,
		FechaDeteccion DATETIME,
		FotoDeteccion VARCHAR(250),
		EstatusID INT,
		Activo INT,
		FechaCreacion DATETIME,
		UsuarioCreacionID INT
	)
	/* Se llena tabla temporal con info del XML */
	INSERT @MuerteTem(
			Arete,
			AreteMetalico,
			Observaciones,
			LoteID,
			OperadorDeteccion,
			FechaDeteccion,
			FotoDeteccion,
			EstatusID,
			Activo,
			FechaCreacion,
			UsuarioCreacionID
		)
	SELECT 
		Arete  = T.item.value('./Arete[1]', 'VARCHAR(15)'),
		AreteMetalico  = T.item.value('./AreteMetalico[1]', 'VARCHAR(15)'),
		Observaciones    = T.item.value('./Observaciones[1]', 'VARCHAR(255)'),
		LoteID    = T.item.value('./LoteID[1]', 'INT'),
		OperadorDeteccion  = T.item.value('./OperadorDeteccion[1]', 'INT'),
		FechaDeteccion   = GETDATE(),
		FotoDeteccion = T.item.value('./FotoDeteccion[1]','VARCHAR(250)'),
		EstatusID    = T.item.value('./EstatusID[1]', 'INT'),
		Activo  = 1,
		FechaCreacion = GETDATE(),
		UsuarioCreacionID = T.item.value('./UsuarioID[1]', 'INT')
	FROM  @XmlMuerte.nodes('ROOT/MuerteGrabar') AS T(item)
	DECLARE @organizacion INT
	DECLARE @Lote INT
	DECLARE @CorralID INT
	DECLARE @UsuarioCreacionID INT
	DECLARE @FechaEntrada DATETIME
	DECLARE @FolioEntrada INT
	DECLARE @GrupoCorral INT
	SELECT 
		@organizacion = T.item.value('./Organizacion[1]', 'INT'),
		@Lote = T.item.value('./LoteID[1]', 'INT'),
		@CorralID = T.item.value('./CorralID[1]', 'INT'),
		@UsuarioCreacionID = T.item.value('./UsuarioID[1]', 'INT')
	FROM @XmlMuerte.nodes('ROOT/MuerteGrabar') AS T(item)
	SELECT @GrupoCorral = GrupoCorralID
	FROM Corral (NOLOCK) C
	INNER JOIN TipoCorral TC
	ON (C.TipoCorralID = TC.TipoCorralID)
	WHERE C.CorralID = @CorralID
	IF ((SELECT COUNT(1) FROM @MuerteTem WHERE Arete != '' OR AreteMetalico != '') = 0) 
	BEGIN
		DECLARE @CalculoArete VARCHAR(12)
		SET @CalculoArete = RIGHT('0000'+CAST(@organizacion AS VARCHAR(10)),4)+RIGHT('0000000'+ISNULL((SELECT TOP 1 CAST(FolioEntrada AS VARCHAR(12)) FROM EntradaGanado WHERE LoteID = @Lote AND CorralID = @CorralID),'0'),8)
		SET @FechaEntrada = ISNULL((SELECT TOP 1 FechaEntrada FROM EntradaGanado WHERE LoteID = @Lote AND CorralID = @CorralID AND Activo = 1),GETDATE())
		SET @FolioEntrada = ISNULL((SELECT TOP 1 FolioEntrada FROM EntradaGanado WHERE LoteID = @Lote AND CorralID = @CorralID AND Activo = 1),0)
		DECLARE @Variable varchar(15)
		EXEC DeteccionGanado_GenerarArete @CalculoArete,@AreteCalculado = @Variable OUTPUT
		UPDATE  @MuerteTem SET Arete = @Variable
		/*EXEC CorteGanado_GuardarAnimal @Variable,'',@FechaEntrada,1,1,1,0,@organizacion,@FolioEntrada,0,0,1,0,0,1,@UsuarioCreacionID*/
	END
	INSERT INTO Muertes (Arete,AreteMetalico,Comentarios,LoteID,
											 OperadorDeteccion,FotoDeteccion,FechaDeteccion,EstatusID,Activo,
											 FechaCreacion,UsuarioCreacionID)
	SELECT 
		Arete,
		AreteMetalico,
		Observaciones,
		LoteID,
		OperadorDeteccion,
		FotoDeteccion,
		FechaDeteccion,
		EstatusID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID 
	FROM @MuerteTem
	UPDATE D
	SET D.Activo = 0,
		D.UsuarioModificacionID = MT.UsuarioCreacionID,
		D.FechaModificacion = GETDATE()
	FROM Deteccion D
	INNER JOIN @MuerteTem MT ON ((D.Arete = MT.Arete AND MT.Arete != '') OR (D.AreteMetalico = MT.AreteMetalico AND MT.AreteMetalico != ''))
	IF ((SELECT COUNT(Arete) FROM @MuerteTem WHERE Arete != '' OR AreteMetalico != '') > 0)
	BEGIN
		UPDATE SG 
		SET Notificacion = 0 
		FROM SupervisionGanado SG
		INNER JOIN @MuerteTem MT
		ON (SG.LoteID = MT.LoteID) AND ((SG.Arete = MT.Arete AND MT.Arete != '') OR (MT.AreteMetalico != '' AND MT.Arete = SG.AreteMetalico ))
	END
	ELSE
	BEGIN
		DECLARE @SupervisionGanadoID INT
		SELECT TOP 1 
			@SupervisionGanadoID = SupervisionGanadoID
		FROM SupervisionGanado (NOLOCK) SG 
		INNER JOIN @MuerteTem MT
		ON (SG.LoteID = MT.LoteID)
		UPDATE SG 
		SET Notificacion = 0 
		FROM SupervisionGanado SG
		WHERE SupervisionGanadoID = @SupervisionGanadoID
	END
END

GO
