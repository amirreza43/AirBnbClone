using System;
using Xunit;
using FluentAssertions;
using web;

namespace test.Models
{
    public class CustomerTest{

        [Fact]
        public void ShouldCreateCustomer(){

            Customer customer= new Customer();
            customer.Properties.Should().HaveCount(0);
            customer.DateRanges.Should().HaveCount(0);
        }

        [Fact]
        public void ShouldCreateCustomerFromDto(){
            CustomerDto customerDto=new CustomerDto(){Name="John"};
            Customer customer=new Customer(customerDto);
            customer.Properties.Should().HaveCount(0);
            customer.DateRanges.Should().HaveCount(0);
            customer.Name.Should().Be("John");
        }
    
    }
}