USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDeteccionTecnica_GuardarSupervision]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SupervisionDeteccionTecnica_GuardarSupervision]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDeteccionTecnica_GuardarSupervision]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Andres Vejar
-- Create date: 19/02/2013
-- Description: Registra en el encabezado de la supervision de tecnica de deteccion
-- Empresa: Apinterfaces
-- =============================================
CREATE PROCEDURE [dbo].[SupervisionDeteccionTecnica_GuardarSupervision]
@OrganizacionId INT,
@OperadorId	INT,
@FechaSupervision DATETIME,
@CriterioSupervisionId INT,
@Observaciones varchar(255),
@UsuarioCreacionId INT
AS
BEGIN
INSERT INTO SupervisionDetectores (OrganizacionId, OperadorId, FechaSupervision, CriterioSupervisionId, Observaciones, Activo, FechaCreacion, UsuarioCreacionId)
VALUES (@OrganizacionId, @OperadorId, @FechaSupervision, @CriterioSupervisionId, @Observaciones, 1, GETDATE(), @UsuarioCreacionId)
SELECT @@Identity as SupervisionDetectoresId
END

GO
