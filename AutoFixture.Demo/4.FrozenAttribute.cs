namespace AutoFixture.Demo
{
  using FluentAssertions;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.DataAnnotations;
  using Ploeh.AutoFixture.Xunit2;
  using Xunit;

  public class FrozenAttributeTests
  {
    [Theory, DefaultAutoData]
    public void ShouldInjectSameInstance(
      [Frozen]DependencyBaseClass dependency,
      FrozenDataClass sut)
    {
      sut.ConstructorDependency.Should().Be(dependency);
      sut.PropertyDependency.Should().Be(dependency);
    }

    [Theory, DefaultAutoData]
    public void ShouldInjectInstanceByPropertyName(
      [Frozen(Matching.PropertyName)]DependencyBaseClass propertyDependency,
      FrozenDataClass sut)
    {
      sut.ConstructorDependency.Should().NotBe(propertyDependency);
      sut.PropertyDependency.Should().Be(propertyDependency);
    }

    [Theory, DefaultAutoData]
    public void ShouldInjectInstanceByParameterName(
      [Frozen(Matching.ParameterName)]DependencyBaseClass constructorDependency,
      FrozenDataClass sut)
    {
      sut.ConstructorDependency.Should().Be(constructorDependency);
      sut.PropertyDependency.Should().NotBe(constructorDependency);
    }

    [Theory, DefaultAutoData]
    public void ShouldInjectDerievedToBase(
			[Frozen(Matching.DirectBaseType)]DependencyDerievedClass constructorDependency,
			FrozenDataClass sut)
    {
      sut.ConstructorDependency.Should().Be(constructorDependency);
      sut.PropertyDependency.Should().Be(constructorDependency);
    }

    private class DefaultAutoDataAttribute : AutoDataAttribute
    {
      public DefaultAutoDataAttribute()
        : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
      {
      }
    }

    public class FrozenDataClass
    {
      public FrozenDataClass(DependencyBaseClass constructorDependency)
      {
        ConstructorDependency = constructorDependency;
      }

      public DependencyBaseClass ConstructorDependency { get; private set; }

      public DependencyBaseClass PropertyDependency { get; set; }
    }

    public class DependencyDerievedClass : DependencyBaseClass
    {
    }

    public class DependencyBaseClass
    {
      string Value { get; set; }
    }
  }
}
