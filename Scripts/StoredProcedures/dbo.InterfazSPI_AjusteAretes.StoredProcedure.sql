USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfazSPI_AjusteAretes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfazSPI_AjusteAretes]
GO
/****** Object:  StoredProcedure [dbo].[InterfazSPI_AjusteAretes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InterfazSPI_AjusteAretes] 
	@OrganizacionId INT 
	, @FechaSacrificio SMALLDATETIME
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
	
		--declare @OrganizacionId INT					= 1
		--declare @FechaSacrificio SMALLDATETIME		= '2015-06-29'

		declare @tipoSalidaSacrificio int

		create table #parametros (
			f datetime
			, o int
			, s int
			, r datetime
			, c varchar(20)
		)

		select @tipoSalidaSacrificio = TipoMovimientoID from TipoMovimiento where Descripcion = 'Salida por sacrificio'
	
		insert into #parametros (f, o, s, r, c)
		select @FechaSacrificio, @OrganizacionId, @tipoSalidaSacrificio, DATEADD(d, 0, DATEDIFF(d, 0, GETDATE())), 'CTACTOGAN'

		exec sp_AjusteAretes @OrganizacionId, @FechaSacrificio

		select
			LoteID
		into
			#osd
		from
			OrdenSacrificio (nolock) os
			inner join OrdenSacrificioDetalle (nolock) osd on
				os.OrdenSacrificioId = osd.OrdenSacrificioId
			inner join #parametros p on
				dateadd(d, 0, datediff(d, 0, os.FechaOrden)) = p.f
				and os.OrganizacionID = p.o

		select	
			cast(spi.Arete as bigint) Arete
			, l.LoteID
			, spi.OrganizacionId
		into
			#spi
		from
			InterfazSPI spi
			inner join Lote l on 
				spi.OrganizacionId = l.OrganizacionID
				and RIGHT('0000' + LTRIM(RTRIM(spi.Lote)), 4) = RIGHT('0000' + LTRIM(RTRIM(l.Lote)), 4)
			inner join Corral c on 
				l.CorralID = c.CorralID
				and RIGHT('000' + LTRIM(RTRIM(spi.Corral)), 3) = RIGHT('000' + LTRIM(RTRIM(c.Codigo)), 3)
			inner join #parametros p on
				spi.FechaSacrificio = p.f
				and spi.OrganizacionId = p.o
			inner join #osd osd on
				l.LoteID = osd.LoteID

		select
			cast(a.Arete as bigint) Arete
			, am.AnimalID
			, am.LoteID 
			, am.OrganizacionID
		into
			#inv
		from
			AnimalMovimiento am
			inner join Animal a on
				am.AnimalID = a.AnimalID
				and am.Activo = 1
			inner join #parametros p on
				am.TipoMovimientoID != p.s
				and am.OrganizacionId = p.o

		select
			i.AnimalID
			, s.Arete
			, s.LoteID
		into 
			#Ajustados
		from
			#spi s
			inner join #inv i on
				s.Arete = i.Arete
				and s.LoteID = i.LoteID

		declare @c1 as int 
		declare @c2 as int

		select @c1 = (select COUNT(1) from #spi), @c2 = (select COUNT(1) from #Ajustados)

		if @c1 != @c2
		begin
			RAISERROR ('No concuerda el numero de aretes ajustados con el numero de aretes sacrificados. Favor de informar a sistemas.', 16,1)
		end

		update 
			InterfazSPI 
		set 
			Procesado = 1
		where
			FechaSacrificio = @FechaSacrificio
			and OrganizacionId = @OrganizacionId

		select 
			LoteID
		into
			#sacrificados
		from
			#spi
		group by
			LoteID

		-- Generar Calculo de Costos de Sacrificio
		SELECT 
			c.Codigo as Corral
			, l.CorralID
			, ac.CostoID
			, l.Lote
			, l.LoteID
			, SUM(ac.Importe) AS Costo
		INTO 
			#Costeo
		FROM 
			AnimalCosto ac (NOLOCK) 
			INNER JOIN #Ajustados a (NOLOCK) ON
				ac.AnimalID = a.AnimalID
			INNER JOIN Lote l (NOLOCK) ON
				a.LoteID = l.LoteID
			INNER JOIN Corral c (NOLOCK) ON
				l.CorralID = c.CorralID
		GROUP BY 
			c.Codigo
			, l.CorralID
			, ac.CostoID
			, l.Lote
			, l.LoteID
		ORDER BY 
			c.Codigo
			, ac.CostoID

		SELECT 
			cv.Valor + cs.ClaveContable AS CTA_CON
			,cs.CostoID
		INTO 
			#Cuentas
		FROM CuentaValor cv (NOLOCK) 
			INNER JOIN Cuenta c (NOLOCK) ON cv.CuentaID = c.CuentaID
			INNER JOIN #parametros p ON
				cv.OrganizacionId = p.o
				AND c.ClaveCuenta = p.c
			CROSS JOIN Costo cs (NOLOCK) 

		SELECT 
			0 AS CostoCorralId
			, cast(tc.CostoID AS VARCHAR(2)) AS CodigoCosto		--COD_CTO
			, '51' AS TipoMovimiento							--TIP_MOV
			, cast(osd.FolioSalida AS VARCHAR(6)) AS FOLIO		
			, p.f AS FECHA
			, tc.Costo AS IMPORTE
			, tc.Corral											--NUM_CORR
			, Right(tc.Lote,4) as Proceso								--NUM_PRO
			, cs.CTA_CON AS CuentaContable
			, '0' AS NUM_LIN
			, tc.Costo AS IMPORTE_A
			, p.r AS FEC_ACT
		FROM 
			#Costeo tc
			INNER JOIN #Cuentas cs ON 
				tc.CostoID = cs.CostoID
			INNER JOIN #sacrificados a ON
				tc.LoteID = a.LoteID
			INNER JOIN OrdenSacrificioDetalle osd (NOLOCK) ON 
				a.LoteID = osd.LoteID
			INNER JOIN OrdenSacrificio os (NOLOCK) ON 
				osd.OrdenSacrificioID = os.OrdenSacrificioID
				AND os.Activo = 1
			INNER JOIN #parametros p ON
				os.OrganizacionId = p.o
				AND DATEADD(d, 0, datediff(d, 0, os.FechaOrden)) = p.f
		GROUP BY 
			cast(tc.CostoID AS VARCHAR(2))
			, osd.FolioSalida
			, tc.Costo
			, tc.Corral
			, tc.Lote
			, cs.CTA_CON
			, tc.Costo
			, p.f
			, p.r

		drop table #Ajustados, #Costeo, #Cuentas, #inv, #parametros, #spi, #sacrificados
	END TRY
	BEGIN CATCH
		declare @mensaje varchar(max)
		set @mensaje = error_message()
		RAISERROR (@mensaje, 16, 1)
	END CATCH
END


GO
