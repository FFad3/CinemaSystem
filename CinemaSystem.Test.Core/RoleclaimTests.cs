using CinemaSystem.Core.Exceptions;
using CinemaSystem.Core.ValueObjects.Auth;
using CinemaSystem.Core.ValueObjects.Common;

namespace CinemaSystem.Test.Core
{
    public class RoleClaimTests
    {
        [Theory]
        [MemberData(nameof(ValidRoleClaims))]
        public void CreateClaims_Theory_Success(string claimName)
        {
            //Arrange
            //Act
            var claim = new RoleClaim(EntityId.Generate(), claimName);

            //Assert
            Assert.Equal(claimName.ToUpper(), claim.ClaimName);
        }

        [Theory]
        [MemberData(nameof(InvalidRoleClaims))]
        public void CreateClaims_Theory_Error(string claimName)
        {
            //Arrange
            //Act
            Action act = () => new RoleClaim(EntityId.Generate(), claimName);
            //Assert
            Assert.Throws<InvalidTextException>(() => act());
        }

        [Fact]
        public void Claim_ChangeName_Succes()
        {
            //Arrange
            var id = EntityId.Generate();
            const string newName = "RANDOM";
            var claim = new RoleClaim(id, "Claim");
            //Act
            claim.ChangeName(newName);
            //Assert
            Assert.Multiple(
                () => Assert.Equal(id, claim.Id),
                () => Assert.Equal(newName, claim.ClaimName)
                );
        }

        [Fact]
        public void Claim_ChangeName_Failure()
        {
            //Arrange
            var id = EntityId.Generate();
            const string newName = "";
            var claim = new RoleClaim(id, "Claim");
            //Act
            void act() => claim.ChangeName(newName);
            //Assert
            Assert.Throws<InvalidTextException>(() => act());
        }

        [Fact]
        public void Claim_Equals_True()
        {
            //Arrange
            var id = EntityId.Generate();
            var claimeName = "test";
            var claim1 = new RoleClaim(id, claimeName);
            var claim2 = (object) new RoleClaim(id, claimeName);
            //Act
            var isEqual = claim1.Equals(claim2);
            //Asset
            Assert.True(isEqual);
        }

        [Fact]
        public void Claim_Equals_False()
        {
            //Arrange
            var id = EntityId.Generate();
            var claim1 = new RoleClaim(id, "test");
            var claim2 = (object) new RoleClaim(id, "test2");
            //Act
            var isEqual = claim1.Equals(claim2);
            //Asset
            Assert.False(isEqual);
        }

        public static IEnumerable<object[]> ValidRoleClaims()
        {
            yield return new object[] { "NewClaim" };
            yield return new object[] { "xxxx" };
            yield return new object[] { "TTTt" };
            yield return new object[] { new ClaimName("Test") };
        }

        public static IEnumerable<object[]> InvalidRoleClaims()
        {
            yield return new object[] { null! };
            yield return new object[] { "" };
            yield return new object[] { "x" };
            yield return new object[] { "xx" };
            yield return new object[] { "xxx" };
        }
    }
}