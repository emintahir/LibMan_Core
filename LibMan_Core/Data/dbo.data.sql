SET IDENTITY_INSERT [dbo].[Categories] ON
INSERT INTO [dbo].[Categories] ([Id], [Name], [Description]) VALUES (1, N'Category A', N'History')
INSERT INTO [dbo].[Categories] ([Id], [Name], [Description]) VALUES (2, N'Category B', N'Geography')
INSERT INTO [dbo].[Categories] ([Id], [Name], [Description]) VALUES (3, N'Category C', N'Science')
INSERT INTO [dbo].[Categories] ([Id], [Name], [Description]) VALUES (4, N'Category D', N'Novel')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Books] ON
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [Publisher], [YearPublished], [DateAddedToLibrary], [BookLocAtLibrary], [ImageName], [Pages], [CopiesOwned], [CopiesAvailable], [CategoryId]) VALUES (1, N'Book A', N'Auth A', N'Pub A', 2007, N'2021-10-18 00:00:00', NULL, N'1_Book A_20211025084151.png', 200, 2, 1, 3)
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [Publisher], [YearPublished], [DateAddedToLibrary], [BookLocAtLibrary], [ImageName], [Pages], [CopiesOwned], [CopiesAvailable], [CategoryId]) VALUES (2, N'Book B', N'Auth B', N'Pub B', 2010, N'2021-10-18 00:00:00', NULL, N'2_Book B_20211025082148.png', 150, 2, 2, 4)
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [Publisher], [YearPublished], [DateAddedToLibrary], [BookLocAtLibrary], [ImageName], [Pages], [CopiesOwned], [CopiesAvailable], [CategoryId]) VALUES (3, N'Book C', N'Auth C', N'Pub C', 2005, N'2021-10-18 00:00:00', NULL, N'3_Book C_20211025084200.png', NULL, 3, 2, 1)
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [Publisher], [YearPublished], [DateAddedToLibrary], [BookLocAtLibrary], [ImageName], [Pages], [CopiesOwned], [CopiesAvailable], [CategoryId]) VALUES (4, N'Book D', N'Auth D', N'Pub D', 2006, N'2021-10-18 00:00:00', NULL, NULL, 300, 2, 2, NULL)
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [Publisher], [YearPublished], [DateAddedToLibrary], [BookLocAtLibrary], [ImageName], [Pages], [CopiesOwned], [CopiesAvailable], [CategoryId]) VALUES (5, N'Book E', N'Auth E', NULL, NULL, N'2021-10-23 00:00:00', NULL, NULL, NULL, 3, 2, 2)
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [Publisher], [YearPublished], [DateAddedToLibrary], [BookLocAtLibrary], [ImageName], [Pages], [CopiesOwned], [CopiesAvailable], [CategoryId]) VALUES (6, N'Book F', N'Auth F', NULL, 1960, N'2021-10-24 00:00:00', NULL, N'6_Book F_20211025082201.png', 260, 1, 0, 4)
SET IDENTITY_INSERT [dbo].[Books] OFF
SET IDENTITY_INSERT [dbo].[Borrowers] ON
INSERT INTO [dbo].[Borrowers] ([Id], [NationalId], [Name], [Surname], [BirthDate], [Phone], [Address]) VALUES (1, NULL, N'Borrower A', N'A.', N'1980-01-01 00:00:00', N'05000000000', N'İstanbul')
INSERT INTO [dbo].[Borrowers] ([Id], [NationalId], [Name], [Surname], [BirthDate], [Phone], [Address]) VALUES (2, NULL, N'Borrower B', N'B.', N'1970-01-01 00:00:00', N'05000000000', N'Ankara')
INSERT INTO [dbo].[Borrowers] ([Id], [NationalId], [Name], [Surname], [BirthDate], [Phone], [Address]) VALUES (3, NULL, N'Borrower C', N'C.', N'1990-01-01 00:00:00', N'05100000000', N'İzmir')
INSERT INTO [dbo].[Borrowers] ([Id], [NationalId], [Name], [Surname], [BirthDate], [Phone], [Address]) VALUES (4, NULL, N'Borrower D', N'D.', N'2000-01-01 00:00:00', N'05310000000', N'Bursa')
SET IDENTITY_INSERT [dbo].[Borrowers] OFF
SET IDENTITY_INSERT [dbo].[Lends] ON
INSERT INTO [dbo].[Lends] ([Id], [BorrowerId], [LendDate], [DueDate], [IsReturned], [ReturnDate]) VALUES (10, 1, N'2020-10-01 00:00:00', N'2020-01-11 00:00:00', 1, N'2020-01-11 00:00:00')
INSERT INTO [dbo].[Lends] ([Id], [BorrowerId], [LendDate], [DueDate], [IsReturned], [ReturnDate]) VALUES (11, 2, N'2020-02-02 00:00:00', N'2020-02-12 00:00:00', 1, N'2020-02-16 00:00:00')
INSERT INTO [dbo].[Lends] ([Id], [BorrowerId], [LendDate], [DueDate], [IsReturned], [ReturnDate]) VALUES (12, 3, N'2020-09-01 00:00:00', N'2020-09-11 00:00:00', 1, N'2020-09-10 00:00:00')
INSERT INTO [dbo].[Lends] ([Id], [BorrowerId], [LendDate], [DueDate], [IsReturned], [ReturnDate]) VALUES (13, 4, N'2021-08-01 00:00:00', N'2021-08-11 00:00:00', 1, N'2021-10-25 00:00:00')
INSERT INTO [dbo].[Lends] ([Id], [BorrowerId], [LendDate], [DueDate], [IsReturned], [ReturnDate]) VALUES (35, 2, N'2021-10-25 00:00:00', N'2021-11-04 00:00:00', 1, N'2021-10-25 00:00:00')
INSERT INTO [dbo].[Lends] ([Id], [BorrowerId], [LendDate], [DueDate], [IsReturned], [ReturnDate]) VALUES (36, 3, N'2021-09-01 00:00:00', N'2021-09-11 00:00:00', 0, NULL)
INSERT INTO [dbo].[Lends] ([Id], [BorrowerId], [LendDate], [DueDate], [IsReturned], [ReturnDate]) VALUES (37, 3, N'2021-10-25 00:00:00', N'2021-11-04 00:00:00', 0, NULL)
SET IDENTITY_INSERT [dbo].[Lends] OFF
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (10, 1)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (12, 1)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (13, 1)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (37, 1)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (10, 2)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (11, 3)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (36, 3)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (10, 4)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (11, 4)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (12, 4)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (35, 4)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (12, 5)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (13, 5)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (35, 5)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (36, 5)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (35, 6)
INSERT INTO [dbo].[BookLends] ([LendId], [BookId]) VALUES (37, 6)

