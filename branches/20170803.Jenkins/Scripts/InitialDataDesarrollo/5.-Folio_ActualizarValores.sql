SET NOCOUNT ON;
DECLARE @query varchar(MAX)
DECLARE @TipoFolioId int
DECLARE @Folios as table
(
	OrganizacionId int,
	TipoFolioId int,
	Folio int
) 

SET @TipoFolioId = 1 --Programación Embarque
Insert @Folios(OrganizacionId,TipoFolioId,Folio)
Select OrganizacionId,@TipoFolioId,max(FolioEmbarque) AS [Folio] 
From Embarque
Group By OrganizacionId

SET @TipoFolioId = 2 --Entrada Ganado
Insert @Folios(OrganizacionId,TipoFolioId,Folio)
Select OrganizacionId,@TipoFolioId,max(FolioEntrada) AS [Folio] 
From EntradaGanado
Group By OrganizacionId

SET @TipoFolioId = 3 --Lote
Insert @Folios(OrganizacionId,TipoFolioId,Folio)
Select OrganizacionId,@TipoFolioId,max(LoteID) AS [Folio] 
From Lote
Group By OrganizacionId

Delete Folio

DBCC CHECKIDENT (Folio, reseed, 0)

Insert Folio(OrganizacionID,TipoFolioID,Valor)
Select OrganizacionID,TipoFolioID,Folio From  @Folios

Select f.*,tf.Descripcion 
From Folio f
Inner Join TipoFolio tf on tf.TipoFolioID = f.TipoFolioId 

SET NOCOUNT ON;    

