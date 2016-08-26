namespace AutoFixture.Demo
{
  using System.Reflection;
  using FluentAssertions;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Kernel;
  using Ploeh.AutoFixture.Xunit2;
  using Xunit;

  public class CustomizationTests
  {
    [Theory, DefaultAutoData]
    public void ShouldCustomizeInPlace(CustomizeValuesHolder sut)
    {
      sut.CustomizeValue.Should().Be("TestString");
    }

    [Theory, DefaultAutoData]
    public void ShouldCustomizeByRegistration(RegisterValuesHolder sut)
    {
      sut.RegisterValue.Should().Be("TestRegister");
    }

    [Theory, DefaultAutoData]
    public void ShouldCustomizebySpecimenBuilder(CustomizeValuesHolder customSut)
    {
      customSut.CustomizeValue.Should().Be("SpecimenString");
    }

    public class CustomizeValuesHolder
    {
      public string CustomizeValue { get; set; }
    }

    public class RegisterValuesHolder
    {
      public string RegisterValue { get; set; }
    }

    private class DefaultAutoDataAttribute : AutoDataAttribute
    {
      public DefaultAutoDataAttribute()
        : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
      {
        Fixture.Customize(new Customization());
        Fixture.Register(() => new RegisterValuesHolder { RegisterValue = "TestRegister" });
        Fixture.Customizations.Add(new SpecimenBuilder());
      }

      private class Customization : ICustomization
      {
        public void Customize(IFixture fixture)
        {
          fixture.Customize<CustomizeValuesHolder>(c => c.With(d => d.CustomizeValue, "TestString"));
        }
      }

      private class SpecimenBuilder : ISpecimenBuilder
      {
        public object Create(object request, ISpecimenContext context)
        {
          var pi = request as ParameterInfo;
          if (pi == null || pi.ParameterType != typeof(CustomizeValuesHolder) || pi.Name != "customSut")
          {
            return new NoSpecimen();
          }

          return new CustomizeValuesHolder { CustomizeValue = "SpecimenString" };
        }
      }
    }
  }
}
