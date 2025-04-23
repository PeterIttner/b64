using b64.Commands;
using b64.Exceptions;
using Shouldly;
using Xunit;

namespace b64.tests.Commands
{
    public class Decode
    {
        #region Edge Cases

        [Fact]
        public void DoesNotAccept_EmptyString()
        {
            var cmd = new DecodeCommand(new List<string> { "" });
            cmd.Accept().ShouldBe(false);
        }

        [Fact]
        public void DoesNotAccept_MoreThanTwoArguments()
        {
            var cmd = new DecodeCommand(new List<string> { "", "", "" });
            cmd.Accept().ShouldBe(false);
        }

        [Fact]
        public void Accepts_TwoArguments()
        {
            var cmd = new DecodeCommand(new List<string> { "decode", "me" });
            cmd.Accept().ShouldBe(true);
        }

        [Fact]
        public void Execute_Throws_WhenInputInvalid()
        {
            var cmd = new DecodeCommand(new List<string> { "decode", "me" });
            Should.Throw<InvalidFormatException>(cmd.Execute);
        }

        #endregion

        #region Happy Path

        [Fact]
        public void Execute_Decodes_ValidString()
        {
            var cmd = new DecodeCommand(new List<string> { "decode", "dGVzdA==" });
            cmd.Execute().ShouldBe("test");
        }

        #endregion
    }
}
