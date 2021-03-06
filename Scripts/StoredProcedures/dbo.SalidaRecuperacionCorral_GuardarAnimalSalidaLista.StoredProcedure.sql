USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaRecuperacionCorral_GuardarAnimalSalidaLista]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaRecuperacionCorral_GuardarAnimalSalidaLista]
GO
/****** Object:  StoredProcedure [dbo].[SalidaRecuperacionCorral_GuardarAnimalSalidaLista]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Edgar.Villarreal
-- Fecha: 2014-03-12
-- Descripci�n:	Guardar en AnimalSalida con xmll
/* EXEC SalidaRecuperacionCorral_GuardarAnimalSalidaLista '<ROOT>
 <AnimalSalida>
		<AnimalID>1</AnimalID> 
		<LoteID>1</LoteID> 
		<CorraletaID>2</CorraletaID> 
		<TipoMovimientoID>10</TipoMovimientoID> 
		<Activo>1</Activo> 
		<UsuarioCreacionID>5</UsuarioCreacionID> 
  </AnimalSalida>
 <AnimalSalida>
		<AnimalID>2</AnimalID> 
		<LoteID>1</LoteID> 
		<CorraletaID>2</CorraletaID> 
		<TipoMovimientoID>10</TipoMovimientoID> 
		<Activo>1</Activo> 
		<UsuarioCreacionID>5</UsuarioCreacionID> 
  </AnimalSalida>
 <AnimalSalida>
		<AnimalID>3</AnimalID> 
		<LoteID>1</LoteID> 
		<CorraletaID>2</CorraletaID> 
		<TipoMovimientoID>10</TipoMovimientoID> 
		<Activo>1</Activo> 
		<UsuarioCreacionID>5</UsuarioCreacionID> 
  </AnimalSalida>
 <AnimalSalida>
		<AnimalID>4</AnimalID> 
		<LoteID>1</LoteID> 
		<CorraletaID>2</CorraletaID> 
		<TipoMovimientoID>10</TipoMovimientoID> 
		<Activo>1</Activo> 
  <UsuarioCreacionID>5</UsuarioCreacionID> 
  </AnimalSalida>
  </ROOT>',1*/
-- =============================================
CREATE PROCEDURE [dbo].[SalidaRecuperacionCorral_GuardarAnimalSalidaLista]
		@XmlGuardarAnimalSalida XML,
		@Activo INT
AS
BEGIN
			DECLARE @IdentityInicio BIGINT;	
			DECLARE @IdentityFinal BIGINT;	
			SET @IdentityInicio = (SELECT COUNT(*)
					     FROM AnimalSalida
								where Activo=@Activo
					   )
		DECLARE @AnimalSalidaTemporal TABLE
				([AnimalID] INT,
				 [LoteID] INT,
				 [CorraletaID] INT,
				 [TipoMovimientoID] INT,
				 [Activo] INT,
				 [UsuarioCreacionID] INT
				)
		INSERT INTO @AnimalSalidaTemporal(
		AnimalID,
		LoteID,
		CorraletaID,
		TipoMovimientoID,
		Activo,
		UsuarioCreacionID)
  SELECT 
    t.item.value('./AnimalID[1]', 'INT'),
    t.item.value('./LoteID[1]', 'INT'),
    t.item.value('./CorraletaID[1]', 'INT'),
    t.item.value('./TipoMovimientoID[1]', 'INT'),
    t.item.value('./Activo[1]', 'INT'),
		t.item.value('./UsuarioCreacionID[1]', 'INT')
   FROM   @XmlGuardarAnimalSalida.nodes('ROOT/AnimalSalida') AS T(item)
			INSERT INTO AnimalSalida(
				AnimalID,
				LoteID,
				CorraletaID,
				TipoMovimientoID,
				FechaSalida,
				Activo,
				FechaCreacion,
				UsuarioCreacionID)
			SELECT AST.AnimalID,AST.LoteID,AST.CorraletaID,AST.TipoMovimientoID,GETDATE(),AST.Activo,GETDATE(),AST.UsuarioCreacionID
			FROM AnimalSalida ASa
			RIGHT OUTER JOIN @AnimalSalidaTemporal AS AST ON AST.AnimalID = ASa.AnimalID
			WHERE ASa.AnimalID IS NULL;
				SET @IdentityFinal = (SELECT COUNT(*)
					     FROM AnimalSalida
								where Activo=@Activo
					   )
			SELECT @IdentityFinal - @IdentityInicio;
END

GO
