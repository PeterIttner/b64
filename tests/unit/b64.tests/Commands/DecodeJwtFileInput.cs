using b64.Commands;
using b64.Exceptions;
using Shouldly;
using Xunit;

namespace b64.tests.Commands
{
    public class DecodeJwtFileInput
    {
        #region Edge Cases

        [Fact]
        public void DoesNotAccept_EmptyString()
        {
            var cmd = new DecodeJWTFileInputCommand(new List<string> { "" });
            cmd.Accept().ShouldBe(false);
        }

        [Fact]
        public void DoesNotAccept_TooManyParameters()
        {
            var cmd = new DecodeJWTFileInputCommand(new List<string> { "decode", "--jwt", "sometoken", "thisIsTooMuch" });
            cmd.Accept().ShouldBe(false);
        }

        [Fact]
        public void Throws_WhenFileDoesNotExist()
        {
            var cmd = new DecodeJWTFileInputCommand(new List<string> { "decode", "--jwt", "-f", "not-existing-file" });
            cmd.Accept().ShouldBe(true);
            Should.Throw<InputFileNotFoundException>(cmd.Execute);
        }

        [Fact]
        public void Throws_WhenJWTIsNotValid()
        {
            try
            {
                File.WriteAllText("invalid.json", "abc.invalid.token");
                var cmd = new DecodeJWTFileInputCommand(new List<string> { "decode", "--jwt", "-f", "invalid.json" });
                cmd.Accept().ShouldBe(true);
                Should.Throw<InvalidFormatException>(cmd.Execute);
            }
            finally
            {
                File.Delete("invalid.json");
            }
        }

        #endregion

        #region Happy Path

        [Fact]
        public void Executes_Decodes_JsonWebToken()
        {
            try
            {
                File.WriteAllText("test-jwt.json", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTUxNjIzOTAyMn0.KMUFsIDTnFmyG3nMiGM6H9FNFUROf3wh7SmqJp-QV30");
                var cmd = new DecodeJWTFileInputCommand(new List<string> { "decode", "--jwt", "-f", "test-jwt.json" });
                cmd.Accept().ShouldBe(true);
                cmd.Execute().ShouldContain(@"""alg"": ""HS256""");
                cmd.Execute().ShouldContain(@"""typ"": ""JWT""");
                cmd.Execute().ShouldContain(@"""sub"": ""1234567890""");
                cmd.Execute().ShouldContain(@" ""name"": ""John Doe""");
            }
            finally
            {
                File.Delete("test-jwt.json");
            }
        }

        #endregion
    }
}
