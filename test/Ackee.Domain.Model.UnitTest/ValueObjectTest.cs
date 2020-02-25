using Ackee.Domain.Model.TestUtility;
using FluentAssertions;
using Xunit;

namespace Ackee.Domain.Model.UnitTest
{
    public class ValueObjectTest
    {
        [Fact]
        public void should_construct_valueObject()
        {
            var valueObject = new IBANValueObject("test");

            valueObject.Should().BeAssignableTo<ValueObject>();

        }

        [Fact]
        public void both_valueObject_with_same_data_should_be_equal()
        {
            var ibanCode = "124852";
            var firstIban=new IBANValueObject(ibanCode);
            var secondIban=new IBANValueObject(ibanCode);

            firstIban.Should().Be(secondIban);
        }
    }

    
}