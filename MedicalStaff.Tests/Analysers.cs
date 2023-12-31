using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Helpers.Analysers;
using MedicalStaff.WebService.Core.Helpers.Properties;
using MedicalStaff.WebService.Core.Models.Db.Physician;

namespace MedicalStaff.WebService.Tests
{
    public class Analysers
    {
        [Fact]
        public static void RegularExpressionsForCPFFormats()
        {
            Assert.True(RegularExpressionsUtility.Matches(ExpressionType.FormattedCPF, "129.177.457-70"));

            Assert.False(RegularExpressionsUtility.Matches(ExpressionType.FormattedCPF, "129-177.457.27"));

            Assert.False(RegularExpressionsUtility.Matches(ExpressionType.FormattedCPF, "129.177-4572712"));
        }

        [Fact]
        public static void Implements()
        {
            Assert.True(typeof(PhysicianAccount).Implements<IPhysicianAccount>());
        }

        [Fact]
        public static void RemoveSpecifically()
        {
            Assert.Equal("12917146777", "129.171.467-77".RemoveSpecifically(new[] { '.', '-' }));

            Assert.Equal("Inserting characters", "Inserting characters: ',', '.', '(', ')', ':', ';', '<', '>'".RemoveSpecifically(new[] { ',', '.', '\'', '(', ')', ':', ';', '<', '>' }).Trim());
        }

        [Fact]
        public void EqualsAny()
        {
            Assert.True('.'.EqualsAny(new[] { ',', '.', '*' }));

            Assert.False('$'.EqualsAny(new[] { ',', '.', '*' }));
        }

        [Fact]
        public void Join()
        {
            Assert.Equal("Joined.", new[] { 'J', 'o', 'i', 'n', 'e', 'd', '.' }.Join());
        }
    }
}