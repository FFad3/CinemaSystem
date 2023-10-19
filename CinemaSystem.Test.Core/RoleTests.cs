using CinemaSystem.Core.ValueObjects.Auth;
using CinemaSystem.Core.ValueObjects.Common;

namespace CinemaSystem.Test.Core
{
    public class RoleTests
    {
        [Theory]
        [MemberData(nameof(ValidRoles))]
        public void CreateRoles_Theory_Success(string roleName)
        {
            //Arrange
            //Act
            var role = new Role(EntityId.Generate(), roleName);

            //Assert
            Assert.Equal(roleName.ToUpper(), role.RoleName);
        }

        public static IEnumerable<object[]> ValidRoles()
        {
            yield return new object[] { new RoleName("Test") };
            yield return new object[] { "Role1" };
            yield return new object[] { "role" };
            yield return new object[] { "T  T" };
        }
    }
}
