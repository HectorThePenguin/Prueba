USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Configuracion_GuardarCambios]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Configuracion_GuardarCambios]
GO
/****** Object:  StoredProcedure [dbo].[Configuracion_GuardarCambios]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 31/03/2014
-- Description:	Guarda los cambios en la pantalla de configuracion, ajuste
-- de formula y estado de comedero
/*
Configuracion_GuardarCambios 
		'<ROOT>
		  <RepartoDetalle>
			<OrganizacionID>1</OrganizacionID>
			<RepartoID>115183</RepartoID>
			<Lote>0</Lote>
			<TipoServicioID>2</TipoServicioID>
			<FormulaIDProgramada>6</FormulaIDProgramada>
			<CantidadProgramada>3000</CantidadProgramada>
			<EstadoComederoID>1</EstadoComederoID>
			<Observaciones>SIN LLORAR</Observaciones>
			<FechaReparto>2014-09-09T00:00:00</FechaReparto>
			<UsuarioModificacionID>5</UsuarioModificacionID>
		  </RepartoDetalle>
		</ROOT>'
*/
--======================================================
CREATE PROCEDURE [dbo].[Configuracion_GuardarCambios]
@XmlReparto XML
AS
BEGIN
	DECLARE @Tabla TABLE (
		OrganizacionID  INT,
		RepartoID BIGINT,
		Lote VARCHAR(20),
		TipoServicioID INT,
		FormulaIDProgramada INT,
		CantidadProgramada  INT,
		EstadoComederoID INT,
		Observaciones VARCHAR(255),
		FechaReparto DATE,
		UsuarioModificacionID INT
	)
	INSERT INTO @Tabla
	SELECT 
			OrganizacionID  = T.item.value('./OrganizacionID[1]', 'INT'),
			RepartoID  = T.item.value('./RepartoID[1]', 'BIGINT'),
			Lote  = CASE WHEN T.item.value('./Lote[1]', 'VARCHAR(20)') = '0' THEN NULL ELSE T.item.value('./Lote[1]', 'VARCHAR(20)') END,
			TipoServicioID    = T.item.value('./TipoServicioID[1]', 'INT'),
			FormulaIDProgramada    = T.item.value('./FormulaIDProgramada[1]', 'INT'),
			CantidadProgramada  = T.item.value('./CantidadProgramada[1]', 'INT'),
			EstadoComederoID = T.item.value('./EstadoComederoID[1]','INT'),
			Observaciones    = T.item.value('./Observaciones[1]', 'VARCHAR(255)'),
			FechaReparto = CONVERT(CHAR(8),T.item.value('./FechaReparto[1]','DATETIME'),112),
			UsuarioModificacionID = T.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM  @XmlReparto.nodes('ROOT/RepartoDetalle') AS T(item)
	DECLARE @CantidadProgramada INT
	DECLARE @CantidadServida INT
	/*SELECT 
		@RepartoID = R.RepartoID
	FROM RepartoDetalle RD
	INNER JOIN Reparto R ON (R.RepartoID = RD.RepartoID)
	INNER JOIN Lote L ON (R.LoteID = L.LoteID)
	INNER JOIN @Tabla Tmp ON (L.Lote = Tmp.Lote AND RD.TipoServicioID = Tmp.TipoServicioID)
	WHERE RD.Activo = 1 AND R.Activo = 1
		AND R.OrganizacionID = Tmp.OrganizacionID
		AND RD.Servido = 0*/
	-- DECLARE @PorcentajeVespertino VARCHAR(50)
	-- DECLARE @PorcentajeMatutino VARCHAR(50)
	DECLARE @RepartoDetalleID BIGINT;
	/*
	SELECT 
			@PorcentajeMatutino = Valor 
	FROM Parametro P
	INNER JOIN ParametroOrganizacion PO ON (P.ParametroID = PO.ParametroID)
	INNER JOIN @Tabla Tmp ON (PO.OrganizacionID = Tmp.OrganizacionID)
	WHERE Clave = 'porcentajeMatutino'
	SELECT 
			@PorcentajeVespertino = Valor 
	FROM Parametro P
	INNER JOIN ParametroOrganizacion PO ON (P.ParametroID = PO.ParametroID)
	INNER JOIN @Tabla Tmp ON (PO.OrganizacionID = Tmp.OrganizacionID)
	WHERE Clave = 'porcentajeVespertino'
	*/
	SELECT @RepartoDetalleID = COALESCE(RD.RepartoDetalleID,0)
	  FROM RepartoDetalle RD
	 INNER JOIN Reparto R ON (R.RepartoID = RD.RepartoID)
	 INNER JOIN @Tabla Tmp ON (Tmp.RepartoID = R.RepartoID AND RD.TipoServicioID = Tmp.TipoServicioID)
	 WHERE RD.Activo = 1 AND R.Activo = 1 
	   AND R.OrganizacionID = Tmp.OrganizacionID 
	   AND RD.Servido = 0
	   AND Tmp.RepartoID > 0
	   AND CONVERT(CHAR(8),R.Fecha,112) = CONVERT(CHAR(8),Tmp.FechaReparto,112);
	IF @RepartoDetalleID IS NULL
		BEGIN
			INSERT INTO RepartoDetalle 
				(RepartoID,TipoServicioID,FormulaIDProgramada,CantidadProgramada,Servido,
							Cabezas,EstadoComederoID,Observaciones,Activo,FechaCreacion,UsuarioCreacionID, Ajuste)
			SELECT DISTINCT
				RepartoID = R.RepartoID,
				TipoServicioID = RT.TipoServicioID,
				FormulaIDProgramada = RT.FormulaIDProgramada,
				CantidadProgramada = RT.CantidadProgramada - 
						(CASE WHEN RT.TipoServicioID = 1 THEN (SELECT CantidadProgramada FROM RepartoDetalle WHERE R.RepartoID = RepartoID AND  TipoServicioID = 2)
							 ELSE (SELECT CantidadProgramada FROM RepartoDetalle WHERE R.RepartoID = RepartoID AND TipoServicioID = 1) END),
				Servido = 0,
				Cabezas = COALESCE(L.Cabezas,1),
				EstadoComederoID = RT.EstadoComederoID,
				Observaciones = RT.Observaciones,
				Activo = 1,
				FechaCreacion = GETDATE(),
				UsuarioCreacionID = RT.UsuarioModificacionID,
				Ajuste = 1
			FROM Reparto R
			INNER JOIN Corral C ON (C.CorralID = R.CorralID AND C.OrganizacionID = R.OrganizacionID)
			LEFT JOIN Lote L ON (R.LoteID = L.LoteID AND L.CorralID = R.CorralID )
			INNER JOIN @Tabla RT ON (RT.RepartoID = R.RepartoID)
			WHERE CONVERT(CHAR(8),R.Fecha,112) = CONVERT(CHAR(8),RT.FechaReparto,112)
		END
	ELSE
		UPDATE RD
		SET RD.FormulaIDProgramada = Tmp.FormulaIDProgramada,
				RD.CantidadProgramada = Tmp.CantidadProgramada - 
					(CASE WHEN Tmp.TipoServicioID = 1 THEN (SELECT CantidadProgramada FROM RepartoDetalle WHERE R.RepartoID = RepartoID AND  TipoServicioID = 2)
						 ELSE (SELECT CantidadProgramada FROM RepartoDetalle WHERE R.RepartoID = RepartoID AND TipoServicioID = 1) END),
				RD.EstadoComederoID = Tmp.EstadoComederoID,
				RD.Observaciones = Tmp.Observaciones,
				RD.FechaModificacion = GETDATE(), 
				RD.UsuarioModificacionID = Tmp.UsuarioModificacionID,
				RD.Ajuste = 1
		FROM RepartoDetalle RD
		INNER JOIN Reparto R ON (R.RepartoID = RD.RepartoID)
		-- INNER JOIN Lote L ON (R.LoteID = L.LoteID)
		INNER JOIN @Tabla Tmp ON (Tmp.RepartoID = R.RepartoID AND RD.TipoServicioID = Tmp.TipoServicioID)
		WHERE RD.Activo = 1 AND R.Activo = 1 
				AND R.OrganizacionID = Tmp.OrganizacionID 
				AND RD.Servido = 0
				AND Tmp.RepartoID > 0
				AND CONVERT(CHAR(8),R.Fecha,112) = CONVERT(CHAR(8),Tmp.FechaReparto,112)
END

GO
