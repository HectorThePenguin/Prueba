USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GenerarArchivoDataLink_ObtenerRepartoDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GenerarArchivoDataLink_ObtenerRepartoDetalle]
GO
/****** Object:  StoredProcedure [dbo].[GenerarArchivoDataLink_ObtenerRepartoDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 01/04/2014
-- Description: 
-- SpName     : GenerarArchivoDataLink_ObtenerRepartoDetalle 2, 2, '20150513'
--======================================================
CREATE PROCEDURE [dbo].[GenerarArchivoDataLink_ObtenerRepartoDetalle]  
@OrganizacionID INT,  
@TipoServicioID INT,  
@FechaReparto DATETIME  
AS  
BEGIN  
    CREATE TABLE #CORRALES  
    (  
        Servicio int  
        ,Corral varchar(10)  
        ,Formula char(50)  
        ,Kilos int  
        ,Cero int  
        ,Seccion int  
        ,Uno int  
        ,Orden int  
    )  
    SET NOCOUNT ON;  
    INSERT INTO #CORRALES  
    SELECT   
        RD.TipoServicioID AS Servicio,  
        C.Codigo AS Corral,  
        f.Descripcion AS Formula,  
        RD.CantidadProgramada AS Kilos,  
        0 AS Cero,  
        --f.FormulaID AS Seccion,  
    C.Seccion AS Seccion,  
        1 AS Uno,  
        CAST(c.Seccion AS varchar)+RIGHT('0000'+CAST(C.Orden AS varchar),4) AS Seccion  
        FROM Reparto R  
        INNER JOIN RepartoDetalle RD (NOLOCK) ON RD.RepartoID = R.RepartoID  
        INNER JOIN Corral C (NOLOCK) ON C.CorralID = R.CorralID  
        INNER JOIN Formula f ON f.FormulaID = RD.FormulaIDProgramada  
    WHERE R.OrganizacionID = @OrganizacionID  
    AND RD.TipoServicioID = @TipoServicioID  
    AND cast(convert(char(8),R.Fecha, 112) as smalldatetime) = cast(convert(char(8), @FechaReparto, 112) as smalldatetime)  
    AND (CantidadProgramada > 0)  
    --INSERT INTO #CORRALES  
    --SELECT  
    --    @TipoServicioID AS Servicio,  
    --    C.Codigo AS Corral,  
    --    f.Descripcion AS Formula,  
    --    s.KilosProgramados AS Kilos,  
    --    0 AS Cero,  
    --    --f.FormulaID AS Seccion,  
    --C.Seccion AS Seccion,  
    --    1 AS Uno,  
    --    CAST(c.Seccion AS varchar)+RIGHT('0000'+CAST(C.Orden AS varchar),4) AS Seccion  
    --    FROM ServicioAlimento S  
    --    INNER JOIN Corral C (NOLOCK) ON C.CorralID = S.CorralID  
    --    INNER JOIN Formula f ON f.FormulaID = S.FormulaID  
    --WHERE S.OrganizacionID = @OrganizacionID      
    --AND not exists (select Corral from #CORRALES CO where co.Corral = c.Codigo)  
    --AND S.Activo = 1  
    --JAGR Se agrega cambio para la zona de retiro  
    UPDATE #CORRALES SET Seccion = 9 WHERE Formula = 'F4R'  
    UPDATE #CORRALES SET Seccion = 6 WHERE Formula IN ('F0T','F1T', 'F4T', 'F5T','F6T','F4RT')  
    UPDATE #CORRALES SET Seccion = 7 WHERE Formula IN ('F0 UE','F1 UE','F4 UE','F0 UET','F1 UET','F4 UET')  
    IF @OrganizacionID = 1  
    BEGIN  
       SELECT Servicio   
        ,Corral  
        ,Formula  
        ,Kilos   
        ,Cero   
        ,Seccion   
        ,Uno  
        ,CAST(c.Orden AS int) Orden  
        FROM #CORRALES c              
       --ORDER BY Orden, Seccion, Formula  
        ORDER BY Seccion, Formula, Orden  
     END  
    IF @OrganizacionID = 2  
    BEGIN  
       SELECT Servicio   
        ,c.Corral  
        ,c.Formula  
        ,c.Kilos   
        ,c.Cero   
        ,CASE c.Formula   
	WHEN 'FS' THEN 3  
    WHEN 'F1' THEN 1  
    WHEN 'F2' THEN 2  
    WHEN 'F4' THEN 4  
    WHEN 'F5' THEN 5  
    WHEN 'F4R' THEN 9  
    WHEN 'F0' THEN 8  
    WHEN 'F0 UE' THEN 8  
	ELSE
		c.Seccion
    END  
    AS Seccion   
        ,c.Uno  
        ,CAST(c.Orden AS int) Orden  
        FROM #CORRALES c  
    INNER JOIN Formula f ON f.Descripcion = c.Formula  
        ORDER BY Formula, Seccion , Orden  
     END  
 IF @OrganizacionID = 5  
 BEGIN  
       SELECT Servicio   
        ,c.Corral  
        ,c.Formula  
        ,c.Kilos   
        ,c.Cero   
        ,CASE c.Formula   
    WHEN 'F1' THEN 1  
    WHEN 'F2' THEN 2  
    WHEN 'F4' THEN 4  
    WHEN 'F5' THEN 5  
    WHEN 'F6' THEN 6  
    WHEN 'F4R' THEN 9  
    WHEN 'F0' THEN 8  
    WHEN 'FS' THEN 0  
	ELSE
		c.Seccion
    END  
    AS Seccion   
        ,c.Uno  
        ,CAST(c.Orden AS int) Orden  
        FROM #CORRALES c  
    INNER JOIN Formula f ON f.Descripcion = c.Formula  
        ORDER BY Formula, Seccion , Orden  
     END      
     IF @OrganizacionID = 4  
     BEGIN  
       SELECT Servicio   
        ,Corral  
        ,Formula  
        ,Kilos   
        ,Cero   
        ,Seccion   
        ,Uno  
        ,Orden  
        FROM #CORRALES              
        ORDER BY Seccion, Formula, Orden  
     END        
    SET NOCOUNT OFF;  
END  

GO
