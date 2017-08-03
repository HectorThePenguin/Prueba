USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[sp_AjusteAretes]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[sp_AjusteAretes]
GO
/****** Object:  StoredProcedure [dbo].[sp_AjusteAretes]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_AjusteAretes]
	@organizacionId [int],
	@fechaSacrificio [smalldatetime],
	@longitud [int] = 4
WITH EXECUTE AS CALLER
AS
EXTERNAL NAME [SIAP].[StoredProcedures].[sp_AjusteAretes]
GO
