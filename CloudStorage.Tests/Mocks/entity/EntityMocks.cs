using System;
using System.Collections.Generic;
using CloudStorage.Entity;

namespace CloudStorage.Tests.Mocks
{
    public static class EntityMocks
    {
        public static User User = new User()
        {
            Id = new Guid("c93ce4b4-54b7-11ee-8c99-0242ac120002"),
            FirstName = "User",
            LastName = "Test",
            Email = "user@test.com",
            Password = "123",
            CreatedDocuments = new List<Document>(),
            AccessedDocuments = new List<Document>(),
        };

        public static Document Document = new Document()
        {
            Id = new Guid("c93ce4b4-54b7-11ee-8c99-0242ac120002"),
            FileName = "some_photo",
            FileExtension = "jpeg",
            OwnerId = new Guid("c93ce4b4-54b7-11ee-8c99-0242ac120002"),
            AllowedUsers = new List<User>(){
                EntityMocks.User
            }
        };
    }
}
