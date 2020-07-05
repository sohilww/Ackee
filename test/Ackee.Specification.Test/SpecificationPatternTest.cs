using FluentAssertions;
using Xunit;

namespace Ackee.Specification.Test
{
    public class SpecificationPatternTest
    {
        private object _dummyObject;

        public SpecificationPatternTest()
        {
            _dummyObject = new object();
        }

        public ISpecification<object> FalseSpecification => new FalseSpecificationTest();
        public ISpecification<object> TrueSpecification => new TrueSpecificationTest();

        [Fact]
        public void And_Of_Two_true_Specification_return_true()
        {
            TrueSpecification.And(TrueSpecification).IsSatisfiedBy(_dummyObject).Should().BeTrue();
        }

        [Fact]
        public void one_true_and_one_false_return_false()
        {
            TrueSpecification
                .And(FalseSpecification)
                .IsSatisfiedBy(_dummyObject)
                .Should().BeFalse();
        }

        [Fact]
        public void one_true_or_one_false_return_true()
        {
            TrueSpecification.Or(FalseSpecification).IsSatisfiedBy(_dummyObject).Should().BeTrue();
        }

        [Fact]
        public void two_false_return_false_on_or_composite()
        {
            FalseSpecification.Or(FalseSpecification).IsSatisfiedBy(_dummyObject).Should().BeFalse();
        }

        [Fact]
        public void not_false_return_true()
        {
            FalseSpecification.Not().IsSatisfiedBy(_dummyObject).Should().BeTrue();
        }

        [Fact]
        public void true_linq_return_true()
        {
            new TrueLinqSpecificationTest().IsSatisfiedBy(_dummyObject).Should().BeTrue();
        }
        
    }
}
