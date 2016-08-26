namespace AutoFixture.Demo
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using FluentAssertions;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Xunit;

  public class ClassAutoDataAttributeTests
  {
    [Theory, ClassAutoData(typeof(DataClass))]
    public void ShouldInjectFromClassDataAndFromDefault(
			string classParameter1,
			string classParameter2,
			int defaultAutoParameter)
    {
      classParameter1.Should().Be("value1");
      classParameter2.Should().Be("value2");
      defaultAutoParameter.Should().NotBe(0);
    }

    private class DataClass : IEnumerable<object[]>
    {
      public IEnumerator<object[]> GetEnumerator()
      {
        yield return new[] { "value1", "value2" };
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return GetEnumerator();
      }
    }

    private class DefaultAutoDataAttribute : AutoDataAttribute
    {
      public DefaultAutoDataAttribute()
        : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
      {
      }
    }

    public class ClassAutoDataAttribute : CompositeDataAttribute
    {
      public ClassAutoDataAttribute(Type @class)
          : base(
              new ClassDataAttribute(@class),
              new DefaultAutoDataAttribute())
      {
      }
    }
  }
}
