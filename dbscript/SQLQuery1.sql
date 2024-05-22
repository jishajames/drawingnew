select * from Login
insert into Login(Username,Password,IsAdmin)values('admin',PWDENCRYPT('admin'),1)

select  EncryptByPassPhrase('key', 'admin' )
declare @pass varbinary(200) =(select  EncryptByPassPhrase('key', 'admin' ))
--select convert(nvarchar(max),@pass,2)
update Login set Password='admin'