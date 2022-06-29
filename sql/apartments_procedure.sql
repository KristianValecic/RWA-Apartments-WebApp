use RwaApartmani
go

create proc AddTag
	@guid uniqueidentifier,
	@typeNameEng nvarchar(250),
	@name nvarchar(250)
as
begin 
	declare @tagTypeId int
	select @tagTypeId = id from TagType where NameEng = @typeNameEng

	insert into Tag(Guid, CreatedAt, TypeId, Name)
	values(@guid, GETDATE(), @tagTypeId, @name)
end
go

create proc LoadAllTypesOfTags
as
begin  
	select name, NameEng from TagType
end
go

create PROC LoadTags
AS
BEGIN
	select t.Id, t.name, tt.Name as 'TypeOfTag', count(ta.ApartmentId) as 'RecurrenceCounter'
	from TaggedApartment as ta
	full join Tag as t on
	t.Id = ta.TagId
	inner join TagType as tt on
	 tt.Id = t.TypeId
	group by t.Id, t.name, tt.Name
END
go

create PROC LoadTagsForApartment
	@apartID int
AS
BEGIN
	select t.* from Apartment as a
	inner join TaggedApartment as ta on
	a.id = ta.ApartmentId
	inner join tag as t on
	t.Id = ta.TagId
	where a.id = @apartID
END
go

create PROC DeleteTag
	@tadID int
AS
BEGIN
	delete from Tag where Id = @tadID
END
go

create PROC AuthUser
	@username nvarchar(250),
	@password nvarchar(250)
AS
BEGIN
	select * from aspnetusers
	where UserName = @username and PasswordHash = @password
END
go

create PROC LoadUsers
AS
BEGIN
	select * from AspNetUsers
END
go

create PROC AddUser
	@guid uniqueidentifier,
	@username nvarchar(250),
	@email nvarchar(250),
	@adress nvarchar(250),
	@password nvarchar(250)
AS
BEGIN
	if not exists (select * from aspnetusers where UserName = @username and Email = @email)
	begin
		insert into aspnetusers(Guid, CreatedAt, Email, PasswordHash, UserName, 
		Address, EmailConfirmed, PhoneNumberConfirmed, LockoutEnabled, AccessFailedCount)
		values(@guid, getdate(), @email, @password, @username, @adress, 0, 0, 0, 0)
		select 1 as 'Succsess'
	end
else
	begin
		select 0 as 'Succsess'
	end
END
go

create PROC UpdateUser
	@id int,
	@username nvarchar(250),
	@email nvarchar(250),
	@address nvarchar(250)
AS
BEGIN
	update aspnetusers set 
	Email = @email, 
	UserName = @username, 
	Address = @address
	where Id = @id
END
go



create proc DeleteUser
	@id int
as
begin
	delete AspNetUsers where id = @id
end

--kada kreiras procedure pazi na imenovanja stupaca

create PROC LoadApartments
AS
BEGIN
	select a.Id, a.Guid, a.name, c.Name as 'city', o.Name as 'owner', Price,
	a.MaxAdults, a.MaxChildren, a.TotalRooms, s.NameEng as 'Status', a.Address, a.BeachDistance
	from Apartment as a
	inner join City as c on
	a.CityId = c.Id
	inner join ApartmentOwner as o on
	a.OwnerId = o.Id
	inner join ApartmentStatus as s on
	a.StatusId = s.Id
	where a.DeletedAt is null
END
go

create PROC loadImagesForAparment
	--@guid uniqueidentifier
	@apartmentID int
AS
BEGIN
	select * from ApartmentPicture
	where ApartmentId = @apartmentID
END
go

create PROC UpdateApartment
	@id int,
	@name nvarchar(250),
	@City nvarchar(250),
	@Address nvarchar(250),
	@MaxChildren int,
	@MaxAdults int,
	@Status nvarchar(250),
	@Price money,
	@TotalRooms int,
	@BeachDistance int
AS
BEGIN
	declare @StatusId int, @CityId int

	select @StatusId = id from ApartmentStatus 
	where Name = @Status

	select @CityId = id from City 
	where Name = @City

	update Apartment set 
	Name = @name, 
	Address = @Address, 
	Price = @Price,
	MaxAdults = @MaxAdults,
	MaxChildren = @MaxChildren,
	TotalRooms = @TotalRooms,
	BeachDistance = @BeachDistance,
	StatusId = @StatusId,
	CityId = @CityId
	--TypeId = @TypeId,
	where Id = @id
END
go

create PROC DeleteApartment
	--@guid uniqueidentifier
	@id int
AS
BEGIN
	update Apartment set
	DeletedAt = GETDATE()
	where id = @id
END
go

create PROC AddApartment
	@Guid uniqueidentifier,
	@OwnerGuid uniqueidentifier,
	@Name nvarchar(250),
	@Owner nvarchar(250),
	@City nvarchar(250),
	@Address nvarchar(250),
	@MaxChildren int,
	@MaxAdults int,
	@Status nvarchar(250),
	@Price money,
	@TotalRooms int,
	@BeachDistance int
AS
BEGIN
	declare @StatusId int, @CityId int, @OwnerId nvarchar(250)---, @TypeId nvarchar(250)

	select @StatusId = id from ApartmentStatus 
	where Name = @Status

	select @CityId = Id from City 
	where Name = @City

	if exists (select * from ApartmentOwner 
	where Name = @Owner)
	begin 
		select @OwnerId = id from ApartmentOwner 
		where Name = @Owner	
	end
	else
	begin 
		insert into ApartmentOwner(Guid, CreatedAt, Name)
		values(@OwnerGuid, -- romijeni na radnom
		GETDATE(), @Owner)

		select @OwnerId = id from ApartmentOwner 
		where Name = @Owner	
	end

	/*declare @ApartmentId int
	SELECT @ApartmentId = IsNull(MAX(ID),0) FROM AspNetUsers*/

	insert into Apartment(Guid, CreatedAt, OwnerId, TypeId, StatusId, CityId, Address, Name,
	Price, MaxAdults, MaxChildren, TotalRooms, BeachDistance, NameEng)
	values( @guid, GETDATE(), @OwnerId, 999, @StatusId, @CityId, @Address, @Name,
	@Price, @MaxAdults, @MaxChildren, @TotalRooms, @BeachDistance, 'Default')
END
go

create proc AddTagToApartment
	@Guid uniqueidentifier,
	@apartID int,
	@tagID int
as
begin
	if not exists (select * from TaggedApartment where TagId = @tagID and ApartmentId = @apartID)
		begin
			insert into TaggedApartment(Guid, ApartmentId, TagId)
			values(@Guid, @apartID, @tagID)
			select 1 as 'Succsess'
		end
	else
		begin
			select 0 as 'Succsess'
		end
end
go

create proc RemoveTagFromApartment
	@tagID int,
	@apartID int
as
begin
	delete from TaggedApartment
	where TagId = @tagID and ApartmentId = @apartID
end
go

create proc GetTagById
	@tagID int
as
begin
	select * from Tag
	where Id = @tagID
end
go

create proc LoadAllCities
as
begin 
	select * from City
end

create proc LoadAllStatuses
as
begin 
	select * from ApartmentStatus
end

create proc LoadAllReservationsForApartment
	@ApartID int
as
begin	
		select r.id, r.UserId, r.Details, r.UserName as 'name', r.UserEmail,
		r.UserAddress, r.UserPhone, u.UserName as 'Username' from ApartmentReservation as r
		left join AspNetUsers as u on
		u.id = r.UserId
		where ApartmentId = @ApartID
end
go

/*create function funcLoadAllReservationsForApartment
(
	@ApartID int
)
returns TABLE
as
RETURN 
	select r.UserId from ApartmentReservation as r
	left join AspNetUsers as u on
	u.id = r.UserId
	where ApartmentId = @ApartID
go*/
--select * from dbo.funcLoadAllReservationsForApartment(3)


create proc AddReservationToApartment
	@Guid uniqueidentifier,
	@username nvarchar(250),
	@details nvarchar(250),
	@apartID int
as
begin
	if not exists (select * from ApartmentReservation where UserName = @username and Details = @details)
		begin
			declare @userid int
			select @userid = id from AspNetUsers where UserName = @username
			
			if @userid is not null
				begin
					insert into ApartmentReservation(Guid, CreatedAt, ApartmentId, Details, UserId)
					values(@Guid, GETDATE(), @apartID, @details, @userid)
				end
			else
				begin
					insert into ApartmentReservation(Guid, CreatedAt, ApartmentId, Details, UserName)
					values(@Guid, GETDATE(), @apartID, @details, @username)
				end
			select 1 as 'Succsess'
		end
	else
		begin
			select 0 as 'Succsess'
		end
end
go

create proc RemoveReservationFromApartment
	@reservationID int,
	@apartID int
as
begin
	delete from ApartmentReservation
	where id = @reservationID and ApartmentId = @apartID
end
go

alter proc SetReserved
	@apartID int
as
begin
	declare @statusid int
	select @statusid = id from ApartmentStatus
	where Name = 'rezervirano'

	update Apartment set StatusId = @statusid
	where Apartment.Id = @apartID
end
go

create proc GetApartment
	@name nvarchar(250),
	@address nvarchar(250),
	@owner nvarchar(250)
as
begin
	--select * from Apartment
	declare @ownerid int

	select @ownerid = id from ApartmentOwner 
	where Name = @owner


	select a.Id, a.Guid, a.name, c.Name as 'city', o.Name as 'owner', Price,
	a.MaxAdults, a.MaxChildren, a.TotalRooms, s.Name as 'Status', a.Address, a.BeachDistance
	from Apartment as a
	inner join City as c on
	a.CityId = c.Id
	inner join ApartmentOwner as o on
	a.OwnerId = o.Id
	inner join ApartmentStatus as s on
	a.StatusId = s.Id
	where OwnerId = @ownerid and a.Name = @name
	and a.address = @address
end
go

create proc GetApartmentById
	@id int
as
begin
	select a.Id, a.Guid, a.name, c.Name as 'city', o.Name as 'owner', Price,
	a.MaxAdults, a.MaxChildren, a.TotalRooms, s.Name as 'Status', a.Address, a.BeachDistance
	from Apartment as a
	inner join City as c on
	a.CityId = c.Id
	inner join ApartmentOwner as o on
	a.OwnerId = o.Id
	inner join ApartmentStatus as s on
	a.StatusId = s.Id
	where a.id = @id
end
go


