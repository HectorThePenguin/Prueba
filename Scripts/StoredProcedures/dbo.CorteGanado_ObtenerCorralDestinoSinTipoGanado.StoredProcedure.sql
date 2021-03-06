USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerCorralDestinoSinTipoGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_ObtenerCorralDestinoSinTipoGanado]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerCorralDestinoSinTipoGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Cesar.valdez
-- Fecha: 2013-12-17
-- Origen: APInterfaces
-- Descripci�n:	Obtiene La info del animal en base al arete y a la organizacion
-- EXEC CorteGanado_ObtenerCorralDestinoSinTipoGanado 'M',150,1,7
-- =============================================
CREATE PROCEDURE [dbo].[CorteGanado_ObtenerCorralDestinoSinTipoGanado]
	@Sexo CHAR(1), 
	@Peso INT,
	@OrganizacionID INT,
	@DiasValido INT
AS
BEGIN
	DECLARE @PesoMaximoConfigurado AS INT;
	DECLARE @CorralIDConfigurado AS INT;
	SET @CorralIDConfigurado = COALESCE((SELECT TOP 1 CR.CorralID
	  FROM CorralRango CR
	 INNER JOIN Corral C ON C.CorralID = CR.CorralID 
	  LEFT JOIN Lote L ON ( C.CorralID = L.CorralID AND L.Activo = 1 )
	 WHERE CR.Sexo = @Sexo
	   AND @Peso BETWEEN CR.RangoInicial AND CR.RangoFinal
	   AND CR.OrganizacionID = @OrganizacionID
	   AND CR.Activo = 1
	   AND C.Activo = 1
	   AND L.FechaCierre IS NULL
	   AND C.Capacidad > ISNULL(L.Cabezas,0)
	   AND (DATEDIFF(day, L.FechaInicio, GETDATE())<= @DiasValido OR L.FechaInicio IS NULL)),0)
	IF @CorralIDConfigurado > 0
		BEGIN
			SELECT TOP 1
				CR.OrganizacionID,CR.CorralID,C.Codigo,CR.Sexo,CR.RangoInicial,CR.RangoFinal,CR.Activo,
				CR.FechaCreacion,CR.UsuarioCreacionID,CR.FechaModificacion,CR.UsuarioModificacionID
			  FROM CorralRango CR
			 INNER JOIN Corral C ON C.CorralID = CR.CorralID 
			 WHERE CR.Sexo = @Sexo
			   AND CR.OrganizacionID = @OrganizacionID
			   AND CR.CorralID = @CorralIDConfigurado
		END 
	ELSE BEGIN
			/* Se obtienen el rango maximo configurado  */
			SELECT TOP 1 @PesoMaximoConfigurado = RangoFinal, @CorralIDConfigurado = CR.CorralID
		      FROM CorralRango CR
			 INNER JOIN Corral C ON C.CorralID = CR.CorralID 
			  LEFT JOIN Lote L ON ( C.CorralID = L.CorralID AND L.Activo = 1 )
		     WHERE CR.Sexo = @Sexo
			   AND CR.OrganizacionID = @OrganizacionID
			   AND CR.Activo = 1
			   AND C.Activo = 1
			   AND L.FechaCierre IS NULL
			   AND C.Capacidad > ISNULL(L.Cabezas,0)
			   AND (DATEDIFF(day, L.FechaInicio, GETDATE())<= @DiasValido OR L.FechaInicio IS NULL)
			 ORDER BY CR.RangoFinal DESC
			IF (@PesoMaximoConfigurado < @Peso ) 
				BEGIN
					SELECT TOP 1
						   CR.OrganizacionID,CR.CorralID,C.Codigo,CR.Sexo,CR.RangoInicial,CR.RangoFinal,CR.Activo,
						   CR.FechaCreacion,CR.UsuarioCreacionID,CR.FechaModificacion,CR.UsuarioModificacionID
					  FROM CorralRango CR
					 INNER JOIN Corral C ON C.CorralID = CR.CorralID 
					 WHERE CR.Sexo = @Sexo
					   AND CR.OrganizacionID = @OrganizacionID
					   AND CR.CorralID = @CorralIDConfigurado
				END
		END 
END

GO
