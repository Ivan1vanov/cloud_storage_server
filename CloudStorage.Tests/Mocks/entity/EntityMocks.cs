using System;
using System.Collections.Generic;
using CloudStorage.Entity;

namespace CloudStorage.Tests.Mocks
{
    public static class EntityMocks {
        public static User UserEntity = new User(){
            Id = new Guid("c93ce4b4-54b7-11ee-8c99-0242ac120002"),
            FirstName = "User",
            LastName = "Test",
            Email = "user@test.com",
            Password = "123",
            CreatedDokuments = new List<Dokument>(),
            AccessedDokuments = new List<Dokument>(),
        };
    }
}