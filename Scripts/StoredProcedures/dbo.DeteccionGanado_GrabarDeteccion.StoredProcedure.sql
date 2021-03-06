USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_GrabarDeteccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_GrabarDeteccion]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_GrabarDeteccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 20/02/2014
-- Description:	Obtiene los grados a mostrar
/*DeteccionGanado_GrabarDeteccion	'
<ROOT>
  <DeteccionGrabar>
    <CorralCodigo />
    <CorralID>1</CorralID>
    <Arete>asdada</Arete>
    <AreteMetalico>asdasdasasd</AreteMetalico>
    <Observaciones>asasdasdasd</Observaciones>
    <LoteID>2</LoteID>
    <GradoID>5</GradoID>
    <TipoDeteccionID>1</TipoDeteccionID>
    <OperadorID>4</OperadorID>
    <FotoDeteccion>339bacca-05be-4941-87c4-378cf90a3d51-det.jpg</FotoDeteccion>
    <NoFierro>asdasda</NoFierro>
    <Observaciones>asasdasdasd</Observaciones>
    <DescripcionGanado>asdasdasdas</DescripcionGanado>
    <UsuarioCreacionID>5</UsuarioCreacionID>
    <Sintomas>
      <SintomasNodo>
        <SintomaID>1</SintomaID>
      </SintomasNodo>
      <SintomasNodo>
        <SintomaID>2</SintomaID>
      </SintomasNodo>
      <SintomasNodo>
        <SintomaID>3</SintomaID>
      </SintomasNodo>
      <SintomasNodo>
        <SintomaID>14</SintomaID>
      </SintomasNodo>
      <SintomasNodo>
        <SintomaID>15</SintomaID>
      </SintomasNodo>
    </Sintomas>
    <Problemas>
      <ProblemasNodo>
        <ProblemaID>7</ProblemaID>
      </ProblemasNodo>
      <ProblemasNodo>
        <ProblemaID>3</ProblemaID>
      </ProblemasNodo>
    </Problemas>
  </DeteccionGrabar>
</ROOT>'*/
--======================================================
CREATE PROCEDURE [dbo].[DeteccionGanado_GrabarDeteccion]
@XmlDeteccion XML
AS
BEGIN
	DECLARE @DeteccionTem TABLE(
		Arete VARCHAR(15),
		AreteMetalico VARCHAR(15),
		FotoDeteccion VARCHAR(250),
		LoteID INT,
		OperadorID INT,
		TipoDeteccionID INT,
		GradoID INT,
		Observaciones VARCHAR(250),		
		NoFierro VARCHAR(10),
		DescripcionGanadoID int,
		UsuarioCreacionID INT
	)
	INSERT INTO @DeteccionTem
	SELECT 
			T.item.value('./Arete[1]', 'VARCHAR(15)'),
			T.item.value('./AreteMetalico[1]', 'VARCHAR(15)'),
			T.item.value('./FotoDeteccion[1]', 'VARCHAR(250)'),
			T.item.value('./LoteID[1]', 'INT'),
			T.item.value('./OperadorID[1]', 'INT'),
			T.item.value('./TipoDeteccionID[1]', 'INT'),
			T.item.value('./GradoID[1]', 'INT'),
			T.item.value('./Observaciones[1]', 'VARCHAR(250)'),			
			T.item.value('./NoFierro[1]', 'VARCHAR(10)'),
			T.item.value('./DescripcionGanadoID[1]','INT'),
			T.item.value('./UsuarioCreacionID[1]','INT')			
		FROM @XmlDeteccion.nodes('ROOT/DeteccionGrabar') AS T(item)
	--INSERTA EL MOVIMIENTO EN LA GENERAL
	INSERT INTO Deteccion (Arete,AreteMetalico,FotoDeteccion,LoteID,OperadorID,
													TipoDeteccionID,GradoID,Observaciones,
													NoFierro,FechaDeteccion,DescripcionGanadoID,FechaCreacion,UsuarioCreacionID,Activo)
	SELECT Arete,AreteMetalico,FotoDeteccion,LoteID,OperadorID,
													TipoDeteccionID,GradoID,Observaciones,
													NoFierro,GETDATE(),DescripcionGanadoID,GETDATE(),UsuarioCreacionID,1
	FROM @DeteccionTem
	DECLARE @UsuarioCreacionID INT
	DECLARE @DeteccionID INT
	SELECT @DeteccionID = @@IDENTITY
	SELECT
		@UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]','INT')
	FROM @XmlDeteccion.nodes('ROOT/DeteccionGrabar') AS T(item)
	--INSERTA LOS SINTOMAS SELECCIONADOS
	INSERT INTO DeteccionSintoma (DeteccionID,SintomaID,Activo,FechaCreacion,UsuarioCreacionID)
	SELECT 
			@DeteccionID,
			T.item.value('./SintomaID[1]', 'INT'),1,GETDATE(),@UsuarioCreacionID
		FROM @XmlDeteccion.nodes('ROOT/DeteccionGrabar/Sintomas/SintomasNodo') AS T(item)
	--INSERTA LOS SINTOMAS DE LOS PROBLEMAS SELECCIONADOS
	INSERT INTO DeteccionSintoma (DeteccionID,SintomaID,Activo,FechaCreacion,UsuarioCreacionID)
	SELECT DISTINCT
			@DeteccionID,
			S.SintomaID,1,GETDATE(),@UsuarioCreacionID
	FROM Sintoma S
	INNER JOIN ProblemaSintoma PS
	ON (S.SintomaID = PS.SintomaID) 
	WHERE PS.ProblemaID IN ( 
								SELECT 
									T.item.value('./ProblemaID[1]', 'INT')
								FROM @XmlDeteccion.nodes('ROOT/DeteccionGrabar/Problemas/ProblemasNodo') AS T(item)
								WHERE T.item.value('./ProblemaID[1]', 'INT') NOT IN (1,4)
							 )
	AND S.SintomaID NOT IN (SELECT SintomaID FROM DeteccionSintoma WHERE DeteccionID = @DeteccionID)
	IF ((SELECT COUNT(Arete) FROM @DeteccionTem WHERE Arete != '' OR AreteMetalico != '') > 0)
	BEGIN
		UPDATE SG 
		SET Notificacion = 0 
		FROM SupervisionGanado SG
		INNER JOIN @DeteccionTem DT
		ON (SG.LoteID = DT.LoteID) AND ((SG.Arete = DT.Arete AND DT.Arete != '') OR (DT.AreteMetalico != '' AND DT.Arete = SG.AreteMetalico ))
	END
	ELSE
	BEGIN
		DECLARE @SupervisionGanadoID INT
		SELECT TOP 1 
			@SupervisionGanadoID = SupervisionGanadoID
		FROM SupervisionGanado (NOLOCK) SG 
		INNER JOIN @DeteccionTem DT
		ON (SG.LoteID = DT.LoteID)
		UPDATE SG 
		SET Notificacion = 0 
		FROM SupervisionGanado SG
		WHERE SupervisionGanadoID = @SupervisionGanadoID
	END
END

GO
