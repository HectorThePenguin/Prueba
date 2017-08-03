IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerDetectoresArete]'))
		DROP FUNCTION [dbo].[ObtenerDetectoresArete]
GO
CREATE FUNCTION ObtenerDetectoresArete(@Arete varchar(15),@LoteID INT)
Returns VARCHAR(1000)
AS
BEGIN
	DECLARE @AnimalID BIGINT
	DECLARE @AnimalMovimientoID BIGINT
	DECLARE @CorralID BIGINT
	DECLARE @Detectores VARCHAR(1000)

	SELECT top 1 @Detectores = rtrim(op.Nombre) + ' ' + rtrim(op.ApellidoPaterno) + ' ' + rtrim(op.ApellidoMaterno) 
	FROM Deteccion det INNER JOIN Operador op ON det.OperadorID = op.OperadorID
	WHERE det.Arete = @Arete order by DeteccionID desc
		 
	IF ISNULL(@Detectores,'') = '' BEGIN 
		SELECT @AnimalID = AnimalID
		FROM Animal 
		WHERE Arete = @Arete

		IF(@AnimalID IS NULL)
		BEGIN
			SELECT @AnimalID = AnimalID
			FROM AnimalHistorico 
			WHERE Arete = @Arete
		END

		SELECT TOP 1 @AnimalMovimientoID = AnimalMovimientoID 
		FROM (
			SELECT TOP 1 MIN(AnimalMovimientoID) AS AnimalMovimientoID,TipoMovimientoID
			FROM AnimalMovimiento 
			WHERE AnimalID = @AnimalID AND LoteID = @LoteID  --Primer movimiento
			GROUP BY AnimalID,TipoMovimientoID
			UNION ALL
			SELECT TOP 1 MAX(AnimalMovimientoID) AS AnimalMovimientoID,TipoMovimientoID
			FROM AnimalMovimiento
			WHERE AnimalID = @AnimalID AND TipoMovimientoID = 17 --Transferencia
			GROUP BY TipoMovimientoID
		) Movimientos
		ORDER BY TipoMovimientoID DESC

		IF(@AnimalMovimientoID IS NULL)
		BEGIN
			SELECT TOP 1 @AnimalMovimientoID = AnimalMovimientoID 
			FROM (
				SELECT TOP 1 MIN(AnimalMovimientoID) AS AnimalMovimientoID,TipoMovimientoID
				FROM AnimalMovimientoHistorico
				WHERE AnimalID = @AnimalID AND LoteID = @LoteID --Primer movimiento
				GROUP BY AnimalID,TipoMovimientoID
				UNION ALL
				SELECT TOP 1 MAX(AnimalMovimientoID) AS AnimalMovimientoID,TipoMovimientoID
				FROM AnimalMovimientoHistorico
				WHERE AnimalID = @AnimalID AND TipoMovimientoID = 17 --Transferencia
				GROUP BY TipoMovimientoID
			) Movimientos
			ORDER BY TipoMovimientoID DESC
			
			SELECT @CorralID = CorralID
			FROM AnimalMovimientoHistorico
			WHERE AnimalMovimientoID = @AnimalMovimientoID
		END
		ELSE
		BEGIN
			SELECT @CorralID = CorralID
			FROM AnimalMovimiento
			WHERE AnimalMovimientoID = @AnimalMovimientoID
		END

		SET @Detectores = (SELECT 
			STUFF(Nombre+' '+ApellidoPaterno+' '+ApellidoMaterno+', ', 1, 0, '')
		FROM CorralDetector CD
		INNER JOIN Operador O ON (CD.OperadorID = O.OperadorID)
		INNER JOIN Rol R ON (R.RolID = O.RolID)
		WHERE CorralID = @CorralID AND CD.Activo = 1 AND O.Activo = 1 AND R.Activo = 1 AND R.RolID = 2
		ORDER BY O.OperadorID
		 FOR XML PATH(''))
	END
	
	RETURN ISNULL(@Detectores,'')
END
GO