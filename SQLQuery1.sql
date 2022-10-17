/*UPDATE Issuances SET IssuanceDateEnd = '10.02.2022' WHERE IssuanceNumber = 19*/

/*SELECT BookInventoryNumber FROM Books EXCEPT SELECT BookInventoryNumber FROM Issuances WHERE IssuanceDateEnd IS NULL*/

/*SELECT Issuances.IssuanceNumber, Issuances.BookInventoryNumber, Books.BookName, Books.BookAuthor, Books.PublisherName, Issuances.IssuanceDateStart, Issuances.IssuanceDateEnd
	FROM Issuances INNER JOIN Books ON Issuances.BookInventoryNumber = Books.BookInventoryNumber
	WHERE Id = 16 AND IssuanceDateEnd IS NULL*/

/*UPDATE Issuances SET IssuanceDateEnd = '2022.03.28' WHERE IssuanceNumber = 18*/


/*DELETE FROM Issuances WHERE IssuanceNumber = 33*/

/*ELECT Books.BookInventoryNumber, Books.PublisherName, Books.BookName, Books.BookAuthor, Books.NumberOfPages, Books.Price,
	Books.Quantity,*/


SELECT BookInventoryNumber FROM Books WHERE PublisherName = N'Куб знаний' AND BookAuthor = N'Громов' AND BookName = N'История России' /*EXCEPT SELECT BookInventoryNumber FROM Issuances*/
