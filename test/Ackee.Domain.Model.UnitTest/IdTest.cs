using System.ComponentModel;
using FluentAssertions;
using Xunit;

namespace Ackee.Domain.Model.UnitTest
{
    public class IdTest
    {
        [Fact]
        public void should_create_id()
        {
            const int id = 10;
            var productId = new ProductId(id);

            productId.DbId.Should().Be(id);
        }

        [Fact]
        public void when_both_dbIds_value_is_same_the_equal_should_return_true()
        {
            const int id = 10;
            var firstProductId = new ProductId(id);
            var secondProductId = new ProductId(id);

            firstProductId.Should().Be(secondProductId);
        }
    }

    public class ProductId : Id<int>
    {
        public ProductId(int id) : base(id)
        {
        }
    }
}