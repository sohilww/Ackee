using System;
using Ackee.Domain.Model.TestUtility;
using FluentAssertions;
using Xunit;

namespace Ackee.Domain.Model.UnitTest
{
    public class EntityTest
    {
        private Book _entity;

        public EntityTest()
        {
            _entity = new Book(10);
        }
        [Fact]
        public void should_construct_entity()
        {
            _entity.Should().BeAssignableTo(typeof(Entity<>));
        }

        [Fact]
        public void entity_should_have_id()
        {
            _entity.Id.DbId.Should().BeGreaterThan(0);
        }

        [Fact]
        public void entity_should_have_Id_when_constructed()
        {
            var randomId = 10;
            var entity=new Book(randomId);

            entity.Id.DbId.Should().Be(randomId);
        }

        [Fact]
        public void both_entity_with_same_id_should_be_equal()
        {
            var id = 10;
            var firstEntity=new Book(id);
            var secondEntity=new Book(id);

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
            var firstEntity = new Book(10);
            var secondEntity = new Book(11);

            firstEntity.Should().NotBe(secondEntity);
        }

        [Fact]
        public void entity_has_CreationDateTime()
        {
            _entity.CreatingDateTime.Should().BeLessThan(DateTime.Now.TimeOfDay);
        }
    }
}