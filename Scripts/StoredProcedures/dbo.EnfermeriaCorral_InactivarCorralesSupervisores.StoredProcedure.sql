USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EnfermeriaCorral_InactivarCorralesSupervisores]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EnfermeriaCorral_InactivarCorralesSupervisores]
GO
/****** Object:  StoredProcedure [dbo].[EnfermeriaCorral_InactivarCorralesSupervisores]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Vel�zquez Araujo
-- Create date: 07/06/2014
-- Description: inactiva los registros en las tabla EnfermeriaCorral y SupervisorEnfermeria al dar de baja una enfermeria
-- SpName     : EnfermeriaCorral_InactivarCorralesSupervisores
--======================================================
CREATE PROCEDURE [dbo].[EnfermeriaCorral_InactivarCorralesSupervisores]
@EnfermeriaID INT
AS
BEGIN
UPDATE EnfermeriaCorral set  Activo = 0
where EnfermeriaID = @EnfermeriaID
update SupervisorEnfermeria set Activo = 0
where EnfermeriaID = @EnfermeriaID
END

GO
