USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_GuardarNuevasIncidencias]    Script Date: 18/03/2016 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Incidencia_GuardarNuevasIncidencias]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_GuardarNuevasIncidencias]    Script Date: 18/03/2016 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Edgar Villarreal
-- Create date: 18/03/2016
-- Description: Guarda las nuevas incidencias 
-- Incidencia_GuardarNuevasIncidencias  1
--=============================================
CREATE PROCEDURE [dbo].[Incidencia_GuardarNuevasIncidencias] 
@XmlIncidencias XML, 
@TipoFolioID INT
AS
BEGIN

CREATE TABLE #tmpIncidenciaGlobal
(
	TmpIncidenciaIDINT INT NOT NULL IDENTITY(1,1),
	Alerta INT,
	Organizacion INT,
	Folio INT,
	Fecha SMALLDATETIME,
	FechaVencimiento SMALLDATETIME,
	NivelAlerta INT,
	XmlConsulta XML,
	Estatus INT,
	Activo BIT,
	UsuarioCreacion INT
)
CREATE TABLE #tempOrganizaciones
(
	OrganizacionID INT,
	Folio INT NULL
) 
INSERT #tempOrganizaciones (OrganizacionID)
SELECT DISTINCT
		Organizacion = (CASE WHEN t.item.value('./OrganizacionID[1]', 'INT') = 0 THEN NULL ELSE t.item.value('./OrganizacionID[1]', 'INT') END)
FROM @XmlIncidencias.nodes('ROOT/Incidencias') AS T(item)


	SET NOCOUNT ON;



	DECLARE @OrganizacionID INT 
	DECLARE cursors CURSOR FOR
		SELECT OrganizacionID
			FROM #tempOrganizaciones

    OPEN cursors
    FETCH NEXT FROM cursors INTO @OrganizacionID
    WHILE @@FETCH_STATUS = 0
    BEGIN

		IF @OrganizacionID IS NOT NULL
			BEGIN

			CREATE TABLE #tmpIncidencia 
			(
				TmpIncidenciaIDINT INT NOT NULL IDENTITY(1,1),
				Alerta INT,
				Organizacion INT,
				Folio INT,
				Fecha SMALLDATETIME,
				FechaVencimiento SMALLDATETIME,
				NivelAlerta INT,
				XmlConsulta XML,
				Estatus INT,
				Activo BIT,
				UsuarioCreacion INT
			)

			DECLARE @ValorFolio INT
      EXEC Folio_Obtener @OrganizacionID, @TipoFolioID, @Folio = @ValorFolio output
			
			INSERT #tmpIncidencia
			(
				Alerta,
				Organizacion,
				Fecha,
				FechaVencimiento,

				NivelAlerta,
				XmlConsulta,

				Estatus,
				Activo,
				UsuarioCreacion
			)

			SELECT
					Alerta = t.item.value('./AlertaID[1]', 'INT'),
					Organizacion = (CASE WHEN t.item.value('./OrganizacionID[1]', 'INT') = 0 THEN NULL ELSE t.item.value('./OrganizacionID[1]', 'INT') END),
					--Folio = (@ValorFolio + TmpIncidenciaID - 1),
					Fecha = GETDATE(),
					FechaVencimiento = dateadd(HOUR,  t.item.value('./HorasRespuesta[1]', 'INT'), getdate()),
					NivelAlerta = t.item.value('./NivelAlertaId[1]', 'INT'),
					XmlConsulta = CONVERT (XML,t.item.value('./XmlConsulta[1]', 'VARCHAR(MAX)')),
					Estatus = t.item.value('./EstatusId[1]', 'INT'),
					Activo = t.item.value('./Activo[1]', 'BIT'),
					UsuarioCreacion = t.item.value('./UsuarioCreacionID[1]', 'INT')
			FROM @XmlIncidencias.nodes('ROOT/Incidencias') AS T(item) WHERE @OrganizacionID = t.item.value('./OrganizacionID[1]', 'INT')
			
			UPDATE #tmpIncidencia SET FechaVencimiento = dateadd(DAY, 1, FechaVencimiento) WHERE DATEPART(dw, FechaVencimiento) = 1
			
			UPDATE #tmpIncidencia
			SET Folio = TmpIncidenciaIDINT + @ValorFolio

			UPDATE Folio 
			SET Valor = (SELECT MAX(Folio) FROM #tmpIncidencia)
			WHERE OrganizacionID = @OrganizacionID AND @TipoFolioID = TipoFolioID
			
			INSERT INTO #tmpIncidenciaGlobal 
				(Alerta,
				Organizacion,
				Folio,
				Fecha,
				FechaVencimiento,
				NivelAlerta,
				XmlConsulta,
				Estatus,
				Activo,
				UsuarioCreacion)
			SELECT Alerta,
				Organizacion,
				Folio,
				Fecha,
				FechaVencimiento,
				NivelAlerta,
				XmlConsulta,
				Estatus,
				Activo,
				UsuarioCreacion FROM #tmpIncidencia 

			DROP TABLE #tmpIncidencia
			
			--UPDATE #tempOrganizaciones SET Folio = @ValorFolio WHERE @OrganizacionID = @OrganizacionID
			END
      FETCH NEXT FROM cursors INTO @OrganizacionID
    END
    CLOSE cursors
    DEALLOCATE cursors
		SET NOCOUNT OFF;	

INSERT INTO Incidencia
	(
		AlertaID,
		OrganizacionID,
		Folio,
		Fecha,
		FechaVencimiento,
		NivelAlertaID,
		XmlConsulta,
		EstatusID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	) 
SELECT Alerta,
		 Organizacion,
		 Folio,
		 Fecha,
		 FechaVencimiento,
		 NivelAlerta,
		 XmlConsulta,
		 Estatus,
		 Activo,
		 UsuarioCreacion,
		 GETDATE()
FROM #tmpIncidenciaGlobal

END

GO