using System.Collections.Generic;
using CloudStorage.Models;

namespace CloudStorage.Tests.Mocks
{
    public static class AuthResultMocks
    {

        public static AuthResult AuthSuccessResultMock = new AuthResult()
        {
            Success = true,
            Token = "header.payload.signature",
        };

        public static AuthResult AuthUnsuccessResultMock = new AuthResult()
        {
            Success = false,
            Token = null,
            Errors = new List<string>{
                "Some error occurs"
            }
        };
    }
}