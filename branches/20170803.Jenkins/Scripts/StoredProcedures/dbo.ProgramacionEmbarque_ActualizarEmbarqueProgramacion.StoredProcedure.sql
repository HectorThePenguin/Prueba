USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ActualizarEmbarqueProgramacion]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ActualizarEmbarqueProgramacion]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ActualizarEmbarqueProgramacion]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Luis Alfonso Sandoval Huerta 
-- Create date: 05/06/2017 11:00:00 a.m.
-- Description: Procedimiento almacenado que guarda la
-- informacion ingresada en la seccion de programacion.
-- SpName     : ProgramacionEmbarque_ActualizarEmbarqueProgramacion '1900-01-01', '1900-01-01', 'ResponsableEmbarque Prueba', 14763, '05:00:00.0000000', 2, 3, 1, 1
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ActualizarEmbarqueProgramacion]
@CitaCarga 				SMALLDATETIME,
@CitaDescarga 			SMALLDATETIME,
@ResponsableEmbarque 	VARCHAR(255),
@EmbarqueID 			INT,
@HorasTransito			INT,
@OrganizacionDestinoID 	INT,
@OrganizacionOrigenID 	INT,
@TipoEmbarqueID 		INT,
@UsuarioModificacionID  INT
AS
BEGIN
	SET NOCOUNT ON;
	-- Actualizar Información De Embarque Para La Pantalla De Programación
	UPDATE Embarque SET          	
		CitaCarga 				= @CitaCarga,			
		CitaDescarga 			= @CitaDescarga,		
		ResponsableEmbarque 	= @ResponsableEmbarque,
		HorasTransito 			= @HorasTransito,		
		OrganizacionDestinoID 	= @OrganizacionDestinoID,
		OrganizacionOrigenID 	= @OrganizacionOrigenID,
		TipoEmbarqueID 			= @TipoEmbarqueID,
		FechaModificacion 		= GETDATE(),	
		UsuarioModificacionID 	= @UsuarioModificacionID
	WHERE EmbarqueID = @EmbarqueID;	

	SET NOCOUNT OFF;
END

GO