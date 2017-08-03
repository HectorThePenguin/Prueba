USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAlertasConsulta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionAlertasConsulta]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAlertasConsulta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Juan Diego Valenzuela Rivera
-- Create date: 14/03/2016
-- Description:  Genera los datos del Modulo Configuracion de Alertas por medio de un filtrado
-- Modificado : 18/03/2016
--						  Torres Lugo Manuel
-- Modifico		: Se Agregaron nuevos campos al SP para obtener el resto de datos en la Edicion
-- Origen: APInterfaces
-- EXEC ConfiguracionAlertas Descripcion(un filtrado con la propiedad LIKE), 1 (Activo o Inactivo 1 o 0)
-- =============================================
CREATE PROCEDURE [dbo].[ConfiguracionAlertasConsulta]
		@Descripcion VARCHAR(256),
		@Activo bit,
		@Inicio INT,
		@Limite INT
AS
BEGIN
SET NOCOUNT ON;
		SELECT
					ROW_NUMBER() OVER (ORDER BY A.Descripcion ASC) AS RowNum,
					AC.AlertaConfiguracionID,
					A.AlertaID,
					A.Descripcion AS Descripcion,
					AC.Datos,
					AC.Fuentes,
					AC.Condiciones,
					CASE WHEN AC.Agrupador IS NULL THEN '' ELSE AC.Agrupador END AS Agrupador,
					AC.Activo AS Estatus,
					AC.NivelAlertaID,
					NA.Descripcion AS NivelDescripcion

		INTO #Datos
		FROM AlertaConfiguracion AC
		INNER JOIN Alerta AS A ON A.AlertaID = AC.AlertaID
		INNER JOIN NivelAlerta AS NA ON NA.NivelAlertaID = AC.NivelAlertaID

		WHERE A.Descripcion like '%' + @Descripcion + '%'
		AND AC.Activo = @Activo

		SELECT
					AlertaConfiguracionID,
					AlertaID,
					Descripcion,
					Datos,
					Fuentes,
					Condiciones,
					Agrupador,
					Estatus,
					NivelAlertaID,
					NivelDescripcion

		FROM #Datos 
		WHERE RowNum BETWEEN @Inicio AND @Limite

		SELECT 
			COUNT(AlertaConfiguracionID)AS TotalReg
		From #Datos


		DROP TABLE #Datos
		SET NOCOUNT OFF;
END