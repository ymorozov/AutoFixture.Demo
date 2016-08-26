namespace AutoFixture.Demo
{
  using FluentAssertions;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Xunit;

  public class AutoInlineDataAttributeTests
  {
    [Theory]
    [DefaultInlineAutoData("101")]
    public void ShouldUseAutoAndInlineParameters(string inlineParameter, string autoParameter)
    {
      inlineParameter.Should().Be("101");
      autoParameter.Should().NotBeNullOrEmpty();

    }

    private class DefaultAutoDataAttribute : AutoDataAttribute
    {
      public DefaultAutoDataAttribute()
        : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
      {
      }
    }

    private class DefaultInlineAutoDataAttribute : InlineAutoDataAttribute
    {
      public DefaultInlineAutoDataAttribute(params object[] objects)
        : base(new DefaultAutoDataAttribute(), objects)
      {
      }
    }
  }
}
