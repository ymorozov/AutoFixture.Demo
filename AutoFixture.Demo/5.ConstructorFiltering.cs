namespace AutoFixture.Demo
{
  using System.Collections.Generic;
  using FluentAssertions;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Xunit;

  public class ConstructorFilteringTests
  {
    [Theory, DefaultAutoData]
    public void ShouldCallSmallestConstructor([Modest]ConstructorTestClass sut)
    {
      sut.ParameterlessConstructorCalled.Should().BeTrue();
    }

    [Theory, DefaultAutoData]
    public void ShouldCallBiggestConstructor([Greedy]ConstructorTestClass sut)
    {
      sut.FattestConstructorCalled.Should().BeTrue();
    }

    [Theory, DefaultAutoData]
    public void ShouldCallEnumerableConstructor([FavorEnumerables]ConstructorTestClass sut)
    {
      sut.EnumerablesConstructorCalled.Should().BeTrue();
    }

    [Theory, DefaultAutoData]
    public void ShouldCallArrayConstructor([FavorArrays]ConstructorTestClass sut)
    {
      sut.ArrayConstructorCalled.Should().BeTrue();
    }

    [Theory, DefaultAutoData]
    public void ShouldCallListConstructor([FavorLists]ConstructorTestClass sut)
    {
      sut.ListConstructorCalled.Should().BeTrue();
    }

    public class ConstructorTestClass
    {
      public ConstructorTestClass(object randomParam)
      {
      }

      public ConstructorTestClass()
      {
        this.ParameterlessConstructorCalled = true;
      }

      public ConstructorTestClass(object param1, object parameter2, object param3)
      {
        this.FattestConstructorCalled = true;
      }

      public ConstructorTestClass(params object[] items)
      {
        this.ArrayConstructorCalled = true;
      }

      public ConstructorTestClass(IEnumerable<object> items)
      {
        this.EnumerablesConstructorCalled = true;
      }

      public ConstructorTestClass(IList<object> items)
      {
        this.ListConstructorCalled = true;
      }

      public bool FattestConstructorCalled { get; private set; }

      public bool ParameterlessConstructorCalled { get; private set; }

      public bool EnumerablesConstructorCalled { get; private set; }

      public bool ArrayConstructorCalled { get; private set; }

      public bool ListConstructorCalled { get; private set; }
    }

    private class DefaultAutoDataAttribute : AutoDataAttribute
    {
      public DefaultAutoDataAttribute()
        : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
      {
      }
    }
  }
}
