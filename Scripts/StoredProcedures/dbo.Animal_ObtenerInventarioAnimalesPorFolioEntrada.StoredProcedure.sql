USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerInventarioAnimalesPorFolioEntrada]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerInventarioAnimalesPorFolioEntrada]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerInventarioAnimalesPorFolioEntrada]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:    César Valdez Figueroa
-- Create date: 03/04/2014
-- Description:  Obtener Animales del inventario por folio de entrada
-- Origen: APInterfaces
/* 
Animal_ObtenerInventarioAnimalesPorFolioEntrada 
 '<ROOT>
  <Partidas>
    <FolioEntrada>1594</FolioEntrada>
  </Partidas>
  <Partidas>
    <FolioEntrada>1597</FolioEntrada>
  </Partidas>
  <Partidas>
    <FolioEntrada>1600</FolioEntrada>
  </Partidas>
</ROOT>',5
*/
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerInventarioAnimalesPorFolioEntrada] @FolioEntrada XML
	,@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Partidas AS TABLE ([FolioEntrada] INT)

	INSERT @Partidas ([FolioEntrada])
	SELECT [FolioEntrada] = t.item.value('./FolioEntrada[1]', 'INT')
	FROM @FolioEntrada.nodes('ROOT/Partidas') AS T(item)

	SELECT A.AnimalID
		,A.Arete
		,A.PesoCompra
		,A.TipoGanadoID
		,A.OrganizacionIDEntrada
		,A.FolioEntrada
		,A.Activo
	FROM Animal A(NOLOCK)
	INNER JOIN @Partidas P ON (A.FolioEntrada = P.FolioEntrada)
	WHERE A.OrganizacionIDEntrada = @OrganizacionID

	SET NOCOUNT OFF
END

GO
