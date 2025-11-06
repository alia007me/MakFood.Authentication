using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Domain.Model.Enums;
using MakFood.Authentication.Infraustraucture.Substructure.Base.DomainExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.UnitTest.Domain.GroupAggregate
{
    public class GroupAggregateTest
    {
        [Fact]
        public void Group_Can_Have_Permission()
        {
            //Arrange
            var sut = new Group("group", "");
            var permission = new GroupPermission(1,2);

            //Act
            sut.AddPermissionsToGroup(permission);

            //Assert
            Assert.Equal(1,sut.Permissions.Count());
        }
        [Fact]
        public void GroupName_Will_Not_Be_Empty()
        {
            //Arrange

            //Act

            //Assert
            Assert.ThrowsAny<ValidationFailedDomainException>(() => new Group("",""));
        }
        [Fact]
        public void Service_In_Permission_Will_Not_Be_Empty()
        {
            //Arrange

            //Act

            //Assert
            Assert.ThrowsAny<ValidationFailedDomainException>(() => new Permission("", "Charge", "Charging wallet"));
        }
        [Fact]
        public void Method_In_Permission_Will_Not_Be_Empty()
        {
            //Arrange

            //Act

            //Assert
            Assert.ThrowsAny<ValidationFailedDomainException>(() => new Permission("Wallet", "", "Charging wallet"));
        }
        [Fact]
        public void Changes_Permission_To_Active()
        {
            //Arrange
            var sut = new Permission("Wallet", "Charge", "Charging wallet");

            //Act
            sut.ActivatePermission();

            //Assert
            Assert.Equal(PermissionStatus.Activated, sut.Status);
        }
        [Fact]
        public void Changes_Permission_To_Deactive()
        {
            //Arrange
            var sut = new Permission("Wallet", "Charge", "Charging wallet");

            //Act
            sut.DeactivatePermission();

            //Assert
            Assert.Equal(PermissionStatus.Deactivated, sut.Status);
        }
        [Fact]
        public void Changes_Permission_To_Deleted()
        {
            //Arrange
            var sut = new Permission("Wallet", "Charge", "Charging wallet");

            //Act
            sut.DeletePermission();

            //Assert
            Assert.Equal(PermissionStatus.Deleted, sut.Status);
        }
    }
}
