﻿using System;
using FluentAssertions;
using NUnit.Framework;
using __NAME__.App.Domain;

namespace __NAME__.Tests.Domain.Examples
{
    [TestFixture]
    public class ExampleEntityTester
    {
        private const string NAME = "test";

        [Test]
        public void should_create_example_entity()
        {
            var now = DateTime.Now;
            var entity = new ExampleEntity(NAME);

            entity.Name.Should().Be(NAME);
            entity.Status.Should().Be(ExampleStatus.Open);
            entity.DateCreated.Should().BeWithin(1.Seconds()).After(now);
            entity.DateUpdated.Should().BeWithin(1.Seconds()).After(now);
        }

        [Test]
        public void should_update_status_to_closed()
        {
            var created = DateTime.Now;
            var entity = new ExampleEntity(NAME);

            var updated = DateTime.Now;
            entity.Close();

            entity.Status.Should().Be(ExampleStatus.Closed);
            entity.DateCreated.Should().BeWithin(1.Seconds()).After(created);
            entity.DateUpdated.Should().BeWithin(1.Seconds()).After(updated);
        }
    }
}
