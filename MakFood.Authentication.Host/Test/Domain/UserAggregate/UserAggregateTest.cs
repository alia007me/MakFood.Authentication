using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Domain.Model.Enums;
using MakFood.Authentication.Infraustraucture.Substructure.Base.DomainExceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.UnitTest.Domain.UserAggregate
{
    public class UserAggregateTest
    {
        [Fact]
        public void User_Can_Get_Group()
        {
            //Arrange 
            var sut = new User("ali","1","test@gmail.com","09123456789",Role.Customer);
            var group = new UserGroup(1,Guid.NewGuid());


            //Act
            sut.AddGroupsToUser(group);


            //Assert
            Assert.Equal(1, sut.Groups.Count());

        }
        [Fact]
        public void Username_Will_Not_Be_Empty()
        {

            //Arrange


            //Act


            //Assert
            Assert.ThrowsAny<ValidationFailedDomainException>(() => new User("", "1", "test@gmail.com", "09123456789", Role.Customer));
        }
        [Fact]
        public void Password_Will_Not_Be_Empty()
        {
            //Arrange

            //Act

            //Assert
            Assert.ThrowsAny<ValidationFailedDomainException>(() => new User("ali", "", "test@gmail.com", "09123456789", Role.Customer));

        }
        [Fact]
        public void Emails_Have_Only_Gmail_Suffix()
        {
            //Arrange

            //Act

            //Assert
            Assert.ThrowsAny<ValidationFailedDomainException>(() => new User("ali", "4", "test@Yahoo.com", "09123456789", Role.Customer));
        }

    }
}
