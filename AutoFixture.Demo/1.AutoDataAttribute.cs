namespace AutoFixture.Demo
{
  using System;
  using FluentAssertions;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Xunit;

  public class AutoDataAttributeTests
  {
    [Theory, DefaultAutoData]
    public void ShouldInjectDefaultParameters(TestClass input)
    {
      input.TestValue.Should().NotBeNullOrEmpty();
      input.TestValue.Should().NotBe("MyCustomTestValue");
    }

    [Theory, DefaultAutoData(typeof(CustomizedFixture))]
    public void ShouldInjectPredefinedParameters(TestClass input)
    {
      input.TestValue.Should().Be("MyCustomTestValue");
    }

    public class DefaultAutoDataAttribute : AutoDataAttribute
    {
      public DefaultAutoDataAttribute()
        : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
      {
      }

      public DefaultAutoDataAttribute(Type customFixtureType)
        : base(CreateFixture(customFixtureType).Customize(new AutoNSubstituteCustomization()))
      {
      }

      private static IFixture CreateFixture(Type customFixtureType)
      {
        var ctor = customFixtureType.GetConstructor(Type.EmptyTypes);
        return (IFixture)ctor.Invoke(null);
      }
    }

    public class TestClass
    {
      public string TestValue { get; set; }
    }

    public class CustomizedFixture : Fixture
    {
      public CustomizedFixture()
      {
        Customize<TestClass>(c => c.With(x => x.TestValue, "MyCustomTestValue"));
      }
    }
  }
}
