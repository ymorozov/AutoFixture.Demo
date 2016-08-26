namespace AutoFixture.Demo
{
  using System.Linq;
  using FluentAssertions;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Xunit;

  public class RandomGeneratorTests
  {
    [Theory, DefaultAutoData]
    public void ShouldGenerateRandomValues(Generator<string> generator, int count)
    {
      var result = generator.Take(count).ToArray();
      var sut = new ParamsTestData(result);
      sut.Params.Length.Should().Be(count);
    }

    private class ParamsTestData
    {
      public string[] Params { get; }

      public ParamsTestData(params string[] args)
      {
        Params = args;
      }
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
