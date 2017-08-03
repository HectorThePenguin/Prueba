IF EXISTS(SELECT * FROM sysobjects WHERE id=OBJECT_ID('rp_kill_db_processes') AND sysstat &0xf=4)
	DROP PROCEDURE rp_kill_db_processes
GO

CREATE proc rp_kill_db_processes
	@dbname varchar(20)
as

Declare @dbid int,
        @spid int,
        @str nvarchar(128)

select 	@dbid = dbid 
from 	master..sysdatabases
where 	name = @dbname

declare spidcurs cursor for
	select 	spid 
	from 	master..sysprocesses 
	where 	dbid = @dbid

open 	spidcurs

fetch 	next 
from 	spidcurs 
into 	@spid

While @@fetch_status = 0
Begin
	Select @str = 'Kill '+convert(nvarchar(30),@spid)
    	exec(@str)

	fetch 	next 
	from 	spidcurs 
	into 	@spid
End

Deallocate spidcurs
