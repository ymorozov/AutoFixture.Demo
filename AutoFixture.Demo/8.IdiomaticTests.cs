namespace AutoFixture.Demo
{
  using System;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Idioms;
  using Ploeh.AutoFixture.Xunit2;
  using Xunit;

  public class IdiomaticTests
  {
    [Theory, DefaultAutoData]
    public void ShouldProtectInputParametersFromNulls(GuardClauseAssertion assertion)
    {
      assertion.Verify(typeof(TestClassWithGuards));
    }

    [Theory, DefaultAutoData]
    public void ShouldImplementWritablePropertiesProperly(WritablePropertyAssertion assertion)
    {
      assertion.Verify(typeof(TestClassWithGuards));
    }

    [Theory, DefaultAutoData]
    public void ShouldInitializePropertiesByConstructoreProperly(ConstructorInitializedMemberAssertion assertion)
    {
      assertion.Verify(typeof(TestClassWithGuards));
    }

    public class TestClassWithGuards
    {
      private string property1;

      public TestClassWithGuards(string parameter1)
      {
        if (parameter1 == null)
        {
          throw new ArgumentNullException();
        }

        Parameter1 = parameter1;
      }

      public string Parameter1 { get; }

      public string Property1
      {
        get
        {
          return property1;
        }
        set
        {
          if (value == null)
          {
            throw new ArgumentNullException();
          }

          property1 = value;
        }
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
