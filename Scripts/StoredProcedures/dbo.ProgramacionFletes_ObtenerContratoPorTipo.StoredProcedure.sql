USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_ObtenerContratoPorTipo]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionFletes_ObtenerContratoPorTipo]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_ObtenerContratoPorTipo]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Edgar Villarreal
-- Create date: 13/05/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProgramacionFletes_ObtenerContratoPorTipo 1
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionFletes_ObtenerContratoPorTipo]
@Activo BIT,
@TipoFleteID INT 
AS
BEGIN
	SET NOCOUNT ON;
			SELECT 	F.ContratoID 		AS ContratoID,
					C.Folio 		 	AS Folio,
					O.Descripcion 		AS Descripcion,
					F.Activo 			AS Activo,
					F.OrganizacionID 	AS OrganizacionID
			FROM Flete (NOLOCK) F
			INNER JOIN Contrato (NOLOCK) 		AS C ON C.ContratoID=F.ContratoID
			INNER JOIN Organizacion (NOLOCK) 	AS O ON O.OrganizacionID=F.OrganizacionID
				WHERE 	F.Activo=@Activo
				AND 	C.Activo = @Activo
				AND 	O.Activo = @Activo
				AND 	C.TipofleteID=@TipoFleteID
			GROUP BY F.ContratoID,C.Folio,F.OrganizacionID,O.Descripcion,F.Activo
	SET NOCOUNT OFF;
END

GO
