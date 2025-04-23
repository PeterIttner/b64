using b64.Commands;
using b64.Exceptions;
using Shouldly;
using Xunit;

namespace b64.tests.Commands
{
    public class DecodeJwt
    {
        #region Edge Cases

        [Fact]
        public void DoesNotAccept_EmptyString()
        {
            var cmd = new DecodeJWTCommand(new List<string> { "" });
            cmd.Accept().ShouldBe(false);
        }

        [Fact]
        public void DoesNotAccept_TooManyParameters()
        {
            var cmd = new DecodeJWTCommand(new List<string> { "decode", "--jwt", "sometoken", "thisIsTooMuch" });
            cmd.Accept().ShouldBe(false);
        }

        [Fact]
        public void Throws_WhenJWTIsNotValid()
        {
            var cmd = new DecodeJWTCommand(new List<string> { "decode", "--jwt", "invalid-token" });
            cmd.Accept().ShouldBe(true);
            Should.Throw<InvalidFormatException>(cmd.Execute);
        }

        #endregion

        #region Happy Path

        [Fact]
        public void Executes_Decodes_JsonWebToken()
        {
            var cmd = new DecodeJWTCommand(new List<string> { "decode", "--jwt", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTUxNjIzOTAyMn0.KMUFsIDTnFmyG3nMiGM6H9FNFUROf3wh7SmqJp-QV30" });
            cmd.Accept().ShouldBe(true);
            cmd.Execute().ShouldContain(@"""alg"": ""HS256""");
            cmd.Execute().ShouldContain(@"""typ"": ""JWT""");
            cmd.Execute().ShouldContain(@"""sub"": ""1234567890""");
            cmd.Execute().ShouldContain(@" ""name"": ""John Doe""");
        }

        #endregion
    }
}
