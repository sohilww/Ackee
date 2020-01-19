using Ackee.Domain.Model.Utility;
using FluentAssertions;
using Xunit;

namespace Ackee.Domain.Model.UnitTest
{
    public class EntityTest
    {
        private BookEntity _entity;

        public EntityTest()
        {
            _entity = new BookEntity(10);
        }
        [Fact]
        public void should_construct_entity()
        {
            _entity.Should().BeAssignableTo(typeof(Entity<>));
        }

        [Fact]
        public void entity_should_have_id()
        {
            _entity.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public void entity_should_have_Id_when_constructed()
        {
            var randomId = 10;
            var entity=new BookEntity(randomId);

            entity.Id.Should().Be(randomId);
        }

        [Fact]
        public void both_entity_with_same_id_should_be_equal()
        {
            var id = 10;
            var firstEntity=new BookEntity(id);
            var secondEntity=new BookEntity(id);

            firstEntity.Should().Be(secondEntity);
        }

        [Fact]
        public void when_entity_is_null_equal()
        {
            _entity.Should().NotBe(null);
        }
        [Fact]
        public void entity_with_different_id_is_not_equal()
        {
            var firstEntity = new BookEntity(10);
            var secondEntity = new BookEntity(11);

            firstEntity.Should().NotBe(secondEntity);
        }
    }
}