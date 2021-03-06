USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionSacrificio_GuardarAnimalSalida]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionSacrificio_GuardarAnimalSalida]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionSacrificio_GuardarAnimalSalida]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Jesus Alvarez
-- Create date: 04/03/2014
-- Description:	Guarda AnimalSalida.
/*ProgramacionSacrificio_GuardarAnimalSalida
'<ROOT>
  <AnimalesGuardar>
    <Arete>
      <AreteNodo>
        <Arete>48400406522752</Arete>
      </AreteNodo>
      <AreteNodo>
        <Arete>48400406263752</Arete>
      </AreteNodo>
    </Arete>
  </AnimalesGuardar>
</ROOT>'
, 4, 14, 1, '010', 1, 10*/
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionSacrificio_GuardarAnimalSalida]
@XmlAnimales XML,
@OrganizacionID INT,
@TipoMovimientoID INT,
@UsuarioCreacionID INT,
@LoteID INT,
@CorraletaID INT,
@OrdenSacrificioDetalleID INT
AS
BEGIN
	DECLARE @OrdenSacrificioID INT
	DECLARE @Tabla TABLE (AnimalID INT, LoteID INT, CorraletaID INT, TipoMovimientoID INT, FechaSalida DATETIME, Activo INT, FechaCreacion DATETIME, UsuarioCreacionID INT)
	INSERT INTO @Tabla (AnimalID, LoteID, CorraletaID, TipoMovimientoID, FechaSalida, Activo, FechaCreacion, UsuarioCreacionID)
	SELECT
			A.AnimalID, @LoteID, @CorraletaID, @TipoMovimientoID, GETDATE(), 1, GETDATE(), @UsuarioCreacionID
		FROM @XmlAnimales.nodes('ROOT/AnimalesGuardar/Arete/AreteNodo') AS T(item)
		INNER JOIN Animal A
		ON(T.item.value('./Arete[1]', 'VARCHAR(15)') = A.Arete)
	SELECT @OrdenSacrificioID = OrdenSacrificioID FROM OrdenSacrificioDetalle WHERE OrdenSacrificioDetalleID = @OrdenSacrificioDetalleID;
    INSERT INTO AnimalSalida (AnimalID,LoteID,CorraletaID,TipoMovimientoID,
														FechaSalida,Activo,FechaCreacion,UsuarioCreacionID,OrdenSacrificioID)
	SELECT AnimalID, LoteID, CorraletaID, TipoMovimientoID, 
														FechaSalida,Activo,FechaCreacion,UsuarioCreacionID,@OrdenSacrificioID
	FROM @Tabla
	UPDATE OrdenSacrificioDetalle
	SET Activo = 0
	WHERE OrdenSacrificioDetalleID = @OrdenSacrificioDetalleID
	SELECT 1
END

GO
