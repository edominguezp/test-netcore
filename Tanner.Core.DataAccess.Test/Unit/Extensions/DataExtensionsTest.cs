using Tanner.Core.DataAccess.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace Tanner.Core.DataAccess.Test.Unit.DataAccess.Extensions
{
    public class DataExtensionsTest : BaseTest<DataExtensionsTest>
    {
        public DataExtensionsTest(ITestOutputHelper output) : base(output)
        {
        }

        protected override void ConfigServices()
        {
        }

        protected override void Configure()
        {
        }


        #region NormalizeString

        [Fact]
        public void NormalizeString_PassNullString_ReturnNull()
        {
            // arrange
            const string text = null;

            // action
            string result = text.NormalizeString();

            //assert
            Assert.Null(result);
        }

        [Fact]
        public void NormalizeString_PassString_ReturnStringWithOutLeadingAndTrailingWhiteSpace()
        {
            // arrange
            const string text = " Hola  mundo, aquí estoy ";

            // action
            string result = text.NormalizeString();

            //assert
            string expected = text.Trim();
            Assert.Equal(expected, result);
        }

        #endregion


        #region NormalizeObject<T>


        #endregion


        #region FillRUT

        #endregion

    }
}
