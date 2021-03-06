USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SupervisorEnfermeria_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SupervisorEnfermeria_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[SupervisorEnfermeria_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 02/05/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SupervisorEnfermeria_ObtenerPorPagina
--======================================================
CREATE Procedure [dbo].[SupervisorEnfermeria_ObtenerPorPagina]
 @OrganizacionID int
,@SupervisorEnfermeriaID int
,@OperadorID int
,@EnfermeriaID int
,@Activo bit
AS
Begin
	Select 
	 se.SupervisorEnfermeriaID
	,se.OperadorID
	,se.EnfermeriaID
	,se.Activo
	,e.OrganizacionID
	From SupervisorEnfermeria se
	inner join Enfermeria e on e.EnfermeriaID = se.EnfermeriaID
	inner join Operador o on o.OperadorID = se.OperadorID
	where @OrganizacionID in (e.OrganizacionID, 0)
		And @SupervisorEnfermeriaID in (se.SupervisorEnfermeriaID, 0)
		And @OperadorID in (se.OperadorID, 0)
		And @EnfermeriaID in (se.EnfermeriaID, 0)
		And @Activo = se.Activo
End

GO
