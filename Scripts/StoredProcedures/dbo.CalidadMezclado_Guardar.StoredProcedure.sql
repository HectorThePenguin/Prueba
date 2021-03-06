USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezclado_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadMezclado_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezclado_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Edgar Villarreal
-- Create date: 20/10/2014
-- Description:  Guardar el Almacen Movimiento con la fecha del movimiento como parametro
-- CalidadMezclado_Guardar 1, 1, 1, 1, '20141010' , '20141010' , 1, 1, 1,null,null,0, 123, 1, 1, 1, 1,1
-- =============================================
CREATE PROCEDURE [dbo].[CalidadMezclado_Guardar]
		@organizacionID 			INT,
		@TipoTecnicaID 				INT,
		@UsuarioIDLaboratorio INT,
		@FormulaID 						INT,
		@FechaPremezcla datetime,
		@FechaBatch datetime,
		@TipoLugarMuestraID 	INT,
		@CamionRepartoID 			INT,
		@ChoferID 						INT,
		@MezcladoraID 				INT,
		@OperadorIDOperador	  INT,
		@LoteID INT,
		@Batch INT,
		@TiempoMezclado INT,
		@OperadorIDLaboratorista INT,
		@GramosMicrot INT,
		@Activo BIT,
		@UsuarioID INT
AS
  BEGIN
      SET NOCOUNT ON
	DECLARE @IdentityID BIGINT;
	INSERT INTO CalidadMezclado ( OrganizacionID, 
			TipoTecnicaID , 
			Fecha , 
			UsuarioIDLaboratorio , 
			FormulaID , 
			FechaPremezcla , 
			FechaBatch , 
			TipoLugarMuestraID , 
			CamionRepartoID ,
			ChoferID ,
			MezcladoraID ,
			OperadorIDMezclador ,
			LoteID ,
			Batch ,
			TiempoMezclado ,
			OperadorIDAnalista ,
			GramosMicrotoxina ,	
			Activo ,
			FechaCreacion,
			UsuarioCreacionID) 
	VALUES (@organizacionID ,
		@TipoTecnicaID ,
		CAST(GETDATE() as DATE) ,
		@UsuarioIDLaboratorio ,
		@FormulaID ,
		CAST(@FechaPremezcla as DATE) ,
		CAST(@FechaBatch as DATE),
		@TipoLugarMuestraID ,
		@CamionRepartoID ,
		@ChoferID ,
		@MezcladoraID ,
		@OperadorIDOperador ,
		CASE WHEN @LoteID = 0 THEN NULL ELSE @LoteID END ,
		@Batch ,
		@TiempoMezclado ,
		@OperadorIDLaboratorista ,
		@GramosMicrot ,
		@Activo ,
		CAST(GETDATE() as DATE) ,
		@UsuarioID )
	-- Se obtiene el id Insertado 
	SET @IdentityID = (SELECT @@IDENTITY)
	SELECT CalidadMezcladoID,
		OrganizacionID, 
			TipoTecnicaID, 
			Fecha, 
			UsuarioIDLaboratorio, 
			FormulaID, 
			FechaPremezcla, 
			FechaBatch, 
			TipoLugarMuestraID, 
			CamionRepartoID,
			ChoferID,
			MezcladoraID,
			OperadorIDMezclador,
			LoteID,
			Batch,
			TiempoMezclado,
			OperadorIDAnalista,
			GramosMicrotoxina,	
			Activo,
			FechaCreacion,
			UsuarioCreacionID
	FROM CalidadMezclado (nolock)
	WHERE CalidadMezcladoID = @IdentityID
	SET NOCOUNT OFF
  END

GO
