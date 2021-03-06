USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionCorte_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionCorte_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionCorte_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:       Jose Carlos Gallegos Castro
-- Origen:       APInterfaces
-- Create date:  21/11/2013
-- Description:  Guarda las Partidas seleccionadas para Corte
-- 
-- =============================================
CREATE PROCEDURE [dbo].[ProgramacionCorte_Crear]  
 @XmlProgramacionCorte XML
AS
BEGIN
 SET NOCOUNT ON;
 DECLARE @ProgramacionCorteTemporal AS TABLE
   (
       [FolioProgramacionID] INT,
	   [OrganizacionID] INT,
	   [FolioEntradaID] INT,
	   [Activo] BIT,
	   [UsuarioCreacionID] INT
	)
 INSERT @ProgramacionCorteTemporal
    ([FolioProgramacionID],
	 [OrganizacionID],
     [FolioEntradaID],
     [Activo],
     [UsuarioCreacionID]
	)
  SELECT 
    [FolioProgramacionID] = t.item.value('./FolioProgramacionID[1]', 'INT'),
    [OrganizacionID]      = t.item.value('./OrganizacionID[1]', 'INT'),
    [FolioEntradaID]      = t.item.value('./FolioEntradaID[1]', 'INT'),
    [Activo]              = t.item.value('./Activo[1]', 'BIT'),
    [UsuarioCreacionID]   = t.item.value('./UsuarioCreacionID[1]', 'INT')
   FROM   @XmlProgramacionCorte.nodes('ROOT/ProgramacionCorte') AS T(item)
 INSERT ProgramacionCorte
     ([OrganizacionID],
      [FolioEntradaID],
      [FechaProgramacion],
      [Activo],
      [FechaCreacion],
      [UsuarioCreacionID]
	  )
 SELECT
      [OrganizacionID],
      [FolioEntradaID],
      GETDATE(),
      [Activo],
      GETDATE(),
      [UsuarioCreacionID]
 FROM  @ProgramacionCorteTemporal
 SET NOCOUNT OFF;
END

GO
