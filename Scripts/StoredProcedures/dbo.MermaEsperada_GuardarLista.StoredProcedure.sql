USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MermaEsperada_GuardarLista]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MermaEsperada_GuardarLista]
GO
/****** Object:  StoredProcedure [dbo].[MermaEsperada_GuardarLista]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 20/02/2014
-- Description:	Obtiene los grados a mostrar
/*MermaEsperada_GuardarLista	
'
	<ROOT>
		<MermaEsperada>
			<MermaObjeto>
				<OrganizacionOrigenID>2</OrganizacionOrigenID>
				<OrganizacionDestinoID>3</OrganizacionDestinoID>
				<Merma>12.00</Merma>
				<UsuarioID>1</UsuarioID>
				<Nuevo>1</Nuevo>
			</MermaObjeto>
			<MermaObjeto>
				<OrganizacionOrigenID>2</OrganizacionOrigenID>
				<OrganizacionDestinoID>4</OrganizacionDestinoID>
				<Merma>55.00</Merma>
				<UsuarioID>1</UsuarioID>
				<Nuevo>2</Nuevo>
			</MermaObjeto>
			<MermaObjeto>
				<OrganizacionOrigenID>2</OrganizacionOrigenID>
				<OrganizacionDestinoID>5</OrganizacionDestinoID>
				<Merma>17.67</Merma>
				<UsuarioID>1</UsuarioID>
				<Nuevo>3</Nuevo>
			</MermaObjeto>
		</MermaEsperada>
	</ROOT>
'*/
--======================================================
CREATE PROCEDURE [dbo].[MermaEsperada_GuardarLista]
@XmlMermas XML
AS
BEGIN
	SELECT 
		OrganizacionOrigenID = T.item.value('./OrganizacionOrigenID[1]', 'INT'),
		OrganizacionDestinoID = T.item.value('./OrganizacionDestinoID[1]', 'INT'),
		Merma = T.item.value('./Merma[1]', 'DECIMAL(12,2)'),
		UsuarioID = T.item.value('./UsuarioID[1]', 'INT'),
		Nuevo = T.item.value('./Nuevo[1]', 'INT')
	INTO #MermaEsperada
	FROM @XmlMermas.nodes('ROOT/MermaEsperada/MermaObjeto') AS T(item)
	DELETE FROM MermaEsperada WHERE OrganizacionOrigenID IN (SELECT DISTINCT OrganizacionOrigenID FROM #MermaEsperada)
	INSERT INTO MermaEsperada 
	(OrganizacionOrigenID, OrganizacionDestinoID,Merma,Activo,UsuarioCreacionID,FechaCreacion)
	SELECT
		OrganizacionOrigenID,
		OrganizacionDestinoID,
		Merma,
		1,
		UsuarioID,
		GETDATE()
	FROM #MermaEsperada
	WHERE Nuevo != 3
	DROP TABLE #MermaEsperada
END

GO
