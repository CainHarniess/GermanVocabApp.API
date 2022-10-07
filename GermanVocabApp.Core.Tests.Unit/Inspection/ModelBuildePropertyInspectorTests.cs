using GermanVocabApp.Core.SourceGeneration.Builders;
using GermanVocabApp.Core.SourceGeneration.Builders.Inspection;
using Moq;
using System.Reflection;

namespace GermanVocabApp.Core.Tests.Unit.Inspection;

public class ModelBuildPropertyInspectorTests
{
    private readonly Mock<IBuiltInTypeDictProvider> _mockBuiltInTypeDictProvider;
    private readonly NullableReferenceTypeInspector _nullReferenceTypeInspector;
    private readonly ModelBuilderPropertyInspector _inspector;
    private readonly PropertyInfo[] _properties;

    public ModelBuildPropertyInspectorTests()
    {
        _mockBuiltInTypeDictProvider = new Mock<IBuiltInTypeDictProvider>();

        var mockDict = new Dictionary<string, string>
        {
            { "BuiltInType", "builtintype"},
            { "Int32", "int"},
        };
        _mockBuiltInTypeDictProvider.Setup(p => p.Provide())
                                    .Returns(mockDict);

        _nullReferenceTypeInspector = new NullableReferenceTypeInspector();
        _inspector = new ModelBuilderPropertyInspector(_mockBuiltInTypeDictProvider.Object, _nullReferenceTypeInspector);

        Type testType = typeof(ComplexType);
        _properties = testType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                              .ToArray();
    }
    
    [Fact]
    public void Inspect_ShouldHaveCorrectMemberName_WhenStandardComplexType()
    {
        ModelBuilderPropertyInfo result = _inspector.Inspect(_properties[0]);
        Assert.Equal("_complexTypeProperty", result.MemberName);
    }

    [Fact]
    public void Inspect_ShouldHaveCorrectMemberTypeName_WhenStandardComplexType()
    {
        ModelBuilderPropertyInfo result = _inspector.Inspect(_properties[0]);
        Assert.Equal("ComplexType", result.MemberTypeName);
    }

    [Fact]
    public void Inspect_ShouldHaveCorrectPropertyName_WhenStandardComplexType()
    {
        ModelBuilderPropertyInfo result = _inspector.Inspect(_properties[0]);
        Assert.Equal("ComplexTypeProperty", result.PropertyName);
    }

    [Fact]
    public void Inspect_ShouldReturnBuiltInTypeName_WhenTypeIsInBuiltInDict()
    {
        ModelBuilderPropertyInfo result = _inspector.Inspect(_properties[1]);
        Assert.Equal("builtintype", result.MemberTypeName);
    }

    [Fact]
    public void Inspect_ShouldReturnCorrectTypeName_WhenNullableReferenceType()
    {
        ModelBuilderPropertyInfo result = _inspector.Inspect(_properties[2]);
        Assert.Equal("ComplexType?", result.MemberTypeName);
    }

    [Fact]
    public void Inspect_ShouldReturnCorrectTypeName_WhenNullableValueType()
    {
        ModelBuilderPropertyInfo result = _inspector.Inspect(_properties[3]);
        Assert.Equal("DateTime?", result.MemberTypeName);
    }

    [Fact]
    public void Inspect_ShouldReturnCorrectTypeName_WhenNullableBuiltInReferenceType()
    {
        ModelBuilderPropertyInfo result = _inspector.Inspect(_properties[4]);
        Assert.Equal("builtintype?", result.MemberTypeName);
    }

    [Fact]
    public void Inspect_ShouldReturnCorrectTypeName_WhenNonNullableInterfaceType()
    {
        ModelBuilderPropertyInfo result = _inspector.Inspect(_properties[5]);
        Assert.Equal("ITestInterface", result.MemberTypeName);
    }

    [Fact]
    public void Inspect_ShouldReturnCorrectTypeName_WhenNullableInterfaceType()
    {
        ModelBuilderPropertyInfo result = _inspector.Inspect(_properties[6]);
        Assert.Equal("ITestInterface?", result.MemberTypeName);
    }

    [Fact]
    public void Inspect_ShouldReturnCorrectTypeName_WhenConcreteGenericReferenceType()
    {
        ModelBuilderPropertyInfo result = _inspector.Inspect(_properties[7]);
        Assert.Equal("ITestInterface?", result.MemberTypeName);
    }

    [Fact]
    public void Inspect_ShouldReturnCorrectTypeName_WhenConcreteGenericValueType()
    {
        ModelBuilderPropertyInfo result = _inspector.Inspect(_properties[8]);
        Assert.Equal("ITestInterface?", result.MemberTypeName);
    }

    [Fact]
    public void Inspect_ShouldReturnCorrectTypeName_WhenConcreteGenericBuiltIn()
    {
        ModelBuilderPropertyInfo result = _inspector.Inspect(_properties[9]);
        Assert.Equal("ITestInterface?", result.MemberTypeName);
    }

    private class ComplexType
    {
        public ComplexType ComplexTypeProperty { get; set; }
        public BuiltInType BuiltInTypeProperty { get; set; }
        public ComplexType? NullableReferenceType { get; set; }
        public DateTime? NullableValueType { get; set; }
        public BuiltInType? NullableBuiltInReferenceType { get; set; }
        public ITestInterface InterfaceType { get; set; }
        public ITestInterface? NullableInterfaceType { get; set; }
        public List<ComplexType> ConcreteGenericReferenceType { get; set; }
        public List<DateTime> ConcreteGenericValueType { get; set; }
        public List<int> ConcreteGenericBuiltInType { get; set; }
    }

    private class BuiltInType
    {

    }

    private interface ITestInterface
    {

    }
}



