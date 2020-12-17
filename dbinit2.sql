INSERT INTO dbo.Hotels
(Name, Location, Description, Season)
VALUES('Trump Hotel', 'New-York', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 2),('Plaza', 'New-York', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 2),('SLS Hotel', 'Beverly Hills','Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 0),('Baur au Lac', 'Zurich', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 2),('Le Grand Bellevue', 'Gstaad' ,'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 1);

INSERT INTO dbo.additionalconvs
(Name)
VALUES('Parking'), ('Mountain view'), ('Sea view'), ('City view'), ('Mini bar'), ('Cinema'), ('Conference Hall'), ('Swimming pool'), ('Safe box');

SET IDENTITY_INSERT dbo.rooms ON

INSERT INTO dbo.rooms
(Id, RoomType)
VALUES(1, 0), (2, 1), (3, 2), (4, 3), (5, 4), (6, 5), (7, 6), (8, 7), (9, 8), (10, 9), (11, 10), (12, 11), (13, 12);

SET IDENTITY_INSERT dbo.rooms OFF

INSERT INTO dbo.hotelrooms
(HotelId, RoomId, Price, Number, MaxAdults, MaxChildren)
VALUES(1, 1, 2500, 2, 2, 2),(2, 2, 7350, 4, 4, 0),(3, 3, 3000, 1, 6, 3),(4, 4, 1200, 8, 2, 0),(5, 5, 470, 6, 3, 1),(1, 6, 900,2, 5, 5),(2, 7, 450,1, 2, 4);

Select * from dbo.HotelRooms

INSERT INTO dbo.roomconvs
(AdditionalConvId, HotelRoomId, Price)
VALUES(4, 1, 100), (2, 2, 100), (7, 3, 100), (2, 4, 100), (9, 5, 100);

INSERT INTO dbo.hotelconvs
(AdditionalConvId, HotelId, Price)
VALUES(1, 1, 50), (8, 2, 60), (3, 2, 60);

INSERT INTO dbo.orders
(IsActive, AppUserId) 
VALUES (1, 'fec514f4-4920-4714-a4ff-48d18db90a98');

INSERT INTO dbo.orderdetails 
(OrderDate, CheckInDate, CheckOutDate, TotalPrice, HotelRoomId, OrderId) 
VALUES ('2019-11-01 10:10:10.000000', '2019-11-01 10:10:10.000000', '2019-11-01 10:10:10.000000', '2500', '7', 3);
