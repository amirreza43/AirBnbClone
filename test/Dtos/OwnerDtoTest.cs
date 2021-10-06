using System;
using Xunit;
using FluentAssertions;
using web;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace test.Dtos
{
    public class OwnerDtoTest{

        [Fact]
        public void ShouldCreateOwnerDto(){

            OwnerDto ownerDto=new OwnerDto(){Name="John"};
            var context=new ValidationContext(ownerDto);
            Action act=()=>Validator.ValidateObject(ownerDto, context, true);
            act.Should().NotThrow();
        }

        [Fact]
        public void ShouldFailToCreateOwnerDtoMinLengthValidation(){
            OwnerDto ownerDto=new OwnerDto(){Name="Jo"};
            var context=new ValidationContext(ownerDto);
            Action action=()=>Validator.ValidateObject(ownerDto, context, true);
            action.Should().Throw<ValidationException>().Where(e=>e.Message.Contains("minimum length of '3'"));
        }

        [Fact]
           public void ShouldFailToCreateOwnerDtoMaxLengthValidation(){
            OwnerDto ownerDto=new OwnerDto(){Name="JoJoJoJoJoJoJoJo"};
            var context=new ValidationContext(ownerDto);
            Action action=()=>Validator.ValidateObject(ownerDto, context, true);
            action.Should().Throw<ValidationException>().Where(e=>e.Message.Contains("maximum length of '15'"));
        }
    }
}