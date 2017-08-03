USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ActualizarEmbarqueRuteoProgramacion]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ActualizarEmbarqueRuteoProgramacion]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ActualizarEmbarqueRuteoProgramacion]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Luis Alfonso Sandoval Huerta 
-- Create date: 05/06/2017 11:00:00 a.m.
-- Description: Procedimiento almacenado que guarda la
-- informacion ingresada en la seccion de programacion.
-- SpName     : ProgramacionEmbarque_ActualizarEmbarqueRuteoProgramacion '<ListaRuteoDetalle><RuteoDetalle><FechaProgramada>2017-06-12</FechaProgramada><EmbarqueRuteoID>3</EmbarqueRuteoID><OrganizacionID>3</OrganizacionID><Horas>01:00:00</Horas><Kilometros>6</Kilometros></RuteoDetalle></ListaRuteoDetalle>', 1
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ActualizarEmbarqueRuteoProgramacion]
@XmlRuteoDetalle		XML,
@UsuarioModificacionID  INT
AS
BEGIN
	SET NOCOUNT ON;
	-- Actualzar Información De Ruteo

	CREATE TABLE ##tRuteoDetalle(FechaProgramada SMALLDATETIME, EmbarqueRuteoID INT,OrganizacionID INT, Horas TIME(0), Kilometros INT)
	
	INSERT INTO ##tRuteoDetalle(FechaProgramada,EmbarqueRuteoID, OrganizacionID,Horas,Kilometros)
		SELECT 
			T.item.value('./FechaProgramada[1]', 'SMALLDATETIME'),
			T.item.value('./EmbarqueRuteoID[1]', 'INT'),	
			T.item.value('./OrganizacionID[1]', 'INT'),	
			T.item.value('./Horas[1]', 'TIME(0)'),
			T.item.value('./Kilometros[1]', 'INT')
		FROM  @XmlRuteoDetalle.nodes('ListaRuteoDetalle/RuteoDetalle') AS T(item);

	UPDATE EmbarqueRuteo 
	SET
	  Horas = T.Horas,
	  FechaProgramada = T.FechaProgramada,
	  FechaModificacion = GETDATE(),
	  UsuarioModificacionID = @UsuarioModificacionID
	FROM ##tRuteoDetalle T
	INNER JOIN EmbarqueRuteo er ON T.EmbarqueRuteoID = er.EmbarqueRuteoID;

	DROP TABLE ##tRuteoDetalle;

	SET NOCOUNT OFF;
END

GO