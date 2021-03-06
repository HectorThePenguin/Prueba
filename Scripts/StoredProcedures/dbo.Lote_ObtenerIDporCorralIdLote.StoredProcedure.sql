USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerIDporCorralIdLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerIDporCorralIdLote]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerIDporCorralIdLote]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Cesar Fernando Vega Vazquez  
-- Create date: 15/01/2014 12:00:00 a.m.  
-- Description:   
-- SpName     : Lote_ObtenerIDporCorralIdLote 1, '<root><CorralLote><Corral>3238</Corral><Lote>1301</Lote></CorralLote></root>'  
--======================================================  
CREATE PROCEDURE [dbo].[Lote_ObtenerIDporCorralIdLote]  
	@organizacionId int  
	, @xml xml  
AS  
BEGIN  
	SET NOCOUNT ON  
	DECLARE @LoteCorral TABLE  
	(  
		Corral INT  
		, Lote VARCHAR(10)  
	)  
	INSERT INTO @LoteCorral (Corral, Lote)  
	SELECT  
		l.b.value('Corral[1]', 'INT') as Corral  
		, l.b.value('Lote[1]', 'VARCHAR(10)') as Lote   
	FROM  
		@xml.nodes('/root/CorralLote') as l(b)  
	SELECT  
		l.LoteID  
		, l.OrganizacionID  
		, l.CorralID  
		, l.Lote  
	FROM   
		Lote as l  
		INNER JOIN @LoteCorral as lc ON   
			RIGHT('0000' + LTRIM(RTRIM(l.Lote)), 4) = RIGHT('0000' + LTRIM(RTRIM(lc.Lote)), 4) 
			AND l.CorralID = lc.Corral  
	WHERE  
		l.OrganizacionID = @organizacionId  
	SET NOCOUNT OFF  
END

GO
