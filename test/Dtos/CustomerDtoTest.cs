using System;
using Xunit;
using FluentAssertions;
using web;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace test.Dtos
{
    public class CustomerDtoTest{

        [Fact]
        public void ShouldCreateCustomerDto(){

            CustomerDto customerDto=new CustomerDto(){Name="Jane"};
            var context= new ValidationContext(customerDto);
            Action act=()=>Validator.ValidateObject(customerDto, context, true);
            act.Should().NotThrow();
        }

        [Fact]
        public void ShouldFailCreateCustomerDtoMinLength(){
            CustomerDto customerDto=new CustomerDto(){Name="Su"};
            var context=new ValidationContext(customerDto);
            Action action=()=>Validator.ValidateObject(customerDto, context, true);
            action.Should().Throw<ValidationException>().Where(exceptionExpression=>exceptionExpression.Message.Contains("minimum length of '3'"));

        }

        [Fact]
        public void ShouldFailCreateCustomerDtoMaxLength(){
            CustomerDto customerDto=new CustomerDto(){Name="qwertyuiopasdfgh"};
            var context=new ValidationContext(customerDto);
            Action action=()=>Validator.ValidateObject(customerDto, context, true);
            action.Should().Throw<ValidationException>().Where(exceptionExpression=>exceptionExpression.Message.Contains("maximum length of '15'"));
        }
    }
}