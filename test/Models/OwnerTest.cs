using System;
using Xunit;
using FluentAssertions;
using web;

namespace test.Models
{
    public class OwnerTest{

        [Fact]
        public void ShouldCreateOwner(){

            Owner owner= new Owner();
            owner.Properties.Should().HaveCount(0);
        }

        [Fact]
        public void ShouldCreateOwnerFromDto(){
            OwnerDto ownerDto=new OwnerDto(){Name="John"};
            Owner owner=new Owner(ownerDto);
            owner.Properties.Should().HaveCount(0);
            owner.Name.Should().Be("John");
        }
    
    }
}