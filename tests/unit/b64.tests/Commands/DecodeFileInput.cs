using b64.Commands;
using b64.Exceptions;
using Shouldly;
using Xunit;

namespace b64.tests.Commands
{
    public class DecodeFileInput
    {
        #region Edge Cases

        [Fact]
        public void DoesNotAccept_EmptyString()
        {
            var cmd = new DecodeFileInputCommand(new List<string> { "" });
            cmd.Accept().ShouldBe(false);
        }

        [Fact]
        public void DoesNotAccept_TooManyParameters()
        {
            var cmd = new DecodeFileInputCommand(new List<string> { "decode", "-f", "somepotentialfile.txt", "thisIsTooMuch" });
            cmd.Accept().ShouldBe(false);
        }

        [Fact]
        public void Throws_WhenFileNotFound()
        {
            var cmd = new DecodeFileInputCommand(new List<string> { "decode", "-f", "test_not_existing.txt" });
            cmd.Accept().ShouldBe(true);
            Should.Throw<InputFileNotFoundException>(cmd.Execute);
        }

        #endregion

        #region Happy Path

        [Fact]
        public void Executes_Decodes_FromFile()
        {
            try
            {
                File.WriteAllText("test.txt", "dGVzdA==");
                var cmd = new DecodeFileInputCommand(new List<string> { "decode", "-f", "test.txt" });
                cmd.Accept().ShouldBe(true);
                cmd.Execute().ShouldBe("test");
            }
            finally
            {
                File.Delete("test.txt");
            }
        }

        #endregion
    }
}
