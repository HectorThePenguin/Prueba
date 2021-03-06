USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_ValidarCheckList]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladora_ValidarCheckList]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_ValidarCheckList]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 23/06/2014
-- Description:  Obtener CheckList
-- CheckListRoladora_ValidarCheckList 4
-- =============================================
CREATE PROCEDURE [dbo].[CheckListRoladora_ValidarCheckList]
 @OrganizacionID INT
 , @Turno INT = NULL
AS
BEGIN
	SET NOCOUNT ON
		SELECT TOP 1 CLRG.CheckListRoladoraGeneralID
			,  CLR.CheckListRoladoraID
			,  CLRG.Turno
			,  CLRG.FechaInicio
			,  CLRG.UsuarioIDSupervisor
			,  CLRG.Observaciones
			,SurfactanteInicio
			,SurfactanteFin
			,ContadorAguaInicio
			,ContadorAguaFin
			,GranoEnteroFinal
		FROM CheckListRoladoraGeneral CLRG
		LEFT OUTER JOIN CheckListRoladora CLR
			ON (CLRG.CheckListRoladoraGeneralID = CLR.CheckListRoladoraGeneralID)
		LEFT OUTER JOIN Roladora R
			ON (CLR.RoladoraID = R.RoladoraID
				AND R.OrganizacionID = @OrganizacionID)
		ORDER BY CLRG.CheckListRoladoraGeneralID DESC
	SET NOCOUNT OFF
END

GO
