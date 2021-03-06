USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_ObtenerPorNotificacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladora_ObtenerPorNotificacion]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_ObtenerPorNotificacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 23/06/2014
-- Description:  Obtener CheckList
-- CheckListRoladora_ObtenerPorNotificacion 4
-- =============================================
CREATE PROCEDURE [dbo].[CheckListRoladora_ObtenerPorNotificacion]
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
		DECLARE @RoladoraID TABLE
		(
			CheckListRoladoraID	INT
			,Horas INT
		)	
		declare @Turno INT
		set @Turno = (SELECT TOP 1 CLRG.Turno
		FROM CheckListRoladoraGeneral CLRG
		INNER JOIN CheckListRoladora CLR
			ON CLRG.CheckListRoladoraGeneralID = CLR.CheckListRoladoraGeneralID					
		INNER JOIN Roladora R
			ON (CLR.RoladoraID = R.RoladoraID
				AND R.OrganizacionID = @OrganizacionID)
		ORDER BY CLRG.CheckListRoladoraGeneralID DESC)
		DECLARE @FechaActual DATETIME
		SET @FechaActual = GETDATE()
		INSERT INTO @RoladoraID 
		SELECT CLR.CheckListRoladoraID
		,DATEDIFF(hh,CLR.FechaCheckList,@FechaActual) AS Horas
		FROM Roladora R
		INNER JOIN CheckListRoladora CLR ON (R.RoladoraID = CLR.RoladoraID
				AND R.OrganizacionID = @OrganizacionID)
		inner join CheckListRoladoraGeneral clrg on CLR.CheckListRoladoraGeneralID = clrg.CheckListRoladoraGeneralID AND  clrg.Turno = @Turno		
		INNER JOIN CheckListRoladoraDetalle CLRD on CLR.CheckListRoladoraID = CLRD.CheckListRoladoraID		
		where dbo.ValidarNotificacionCheckList(CLR.FechaCheckList,@FechaActual) = 1	
		SELECT	 
		CLRG.Turno
			   , R.Descripcion AS Roladora
			   , RTRIM(CONVERT(CHAR(5), DATEADD(hh, tR.Horas, CLR.FechaCheckList) , 108)) 
					+ ' ' + 
				 RIGHT(RTRIM(CONVERT(CHAR(40), GETDATE() , 109)), 2) AS Hora
		FROM CheckListRoladoraGeneral CLRG
		INNER JOIN CheckListRoladora CLR
			ON (CLRG.CheckListRoladoraGeneralID = CLR.CheckListRoladoraGeneralID)
			INNER JOIN CheckListRoladoraDetalle CLRD on CLR.CheckListRoladoraID = CLRD.CheckListRoladoraID		
		INNER JOIN Roladora R
			ON (CLR.RoladoraID = R.RoladoraID)
		INNER JOIN @RoladoraID tR
			ON (CLR.CheckListRoladoraID = tR.CheckListRoladoraID)
			where CLRG.Turno = @Turno
			group by CLRG.Turno,R.Descripcion,CLR.FechaCheckList,tR.Horas,clr.CheckListRoladoraID
	SET NOCOUNT OFF;
END

GO
