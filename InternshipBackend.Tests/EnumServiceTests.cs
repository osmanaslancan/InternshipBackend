using InternshipBackend.Core;
using InternshipBackend.Core.Data;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using InternshipBackend.Resources.Enum;
using InternshipBackend.Tests.Mocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;

namespace InternshipBackend.Tests;

public class EnumServiceTests
{
    [Fact]
    public void Returns_Enum_Id_And_Vaues_If_Its_In_Correct_Namespace()
    {
        var builder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("InternshipBackend.Test.MockAssembly"), AssemblyBuilderAccess.Run);
        // Create a dynamic module in the assembly
        var moduleBuilder = builder.DefineDynamicModule("DynamicModule");

        // Create a dynamic enum type in the module
        var enumBuilder = moduleBuilder.DefineEnum("InternshipBackend.Data.DynamicEnum", TypeAttributes.Public, typeof(int));
        
        // Add enum values
        enumBuilder.DefineLiteral("Value1", 0);
        enumBuilder.DefineLiteral("Value2", 1);
        enumBuilder.DefineLiteral("Value3", 2);

        // Create the enum type
        var dynamicEnumType = enumBuilder.CreateType();

        // Arrange
        var stringLocalizer = new MockStringLocalizer<Enums>(getString: (key) => new LocalizedString(key, key, true));
        var typeSourceProvider = new TypeSourceProvider(dynamicEnumType.Assembly);
        var enumService = new EnumService(stringLocalizer, typeSourceProvider);
        var key = "DynamicEnum";
        // Act
        var result = enumService.GetEnumOrDefault(key);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count);
            
        for (int i = 0; i < result.Count; i++)
        {
            Assert.Equal($"Value{i + 1}", result[i].Id);
            Assert.Equal($"Value{i + 1}", result[i].Name);
        }
    }

    [Fact]
    public void Returns_Null_If_Enum_Is_Not_In_Correct_Namespace()
    {
        // Arrange
        var stringLocalizer = new MockStringLocalizer<Enums>(getString: (key) => new LocalizedString(key, key, true));
        var typeSourceProvider = new TypeSourceProvider(typeof(EnumService).Assembly);
        var enumService = new EnumService(stringLocalizer, typeSourceProvider);
        var key = "DynamicEnum";

        // Act
        var result = enumService.GetEnumOrDefault(key);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Returns_Description_If_Field_Has()
    {
        var builder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("InternshipBackend.Test.MockAssembly"), AssemblyBuilderAccess.Run);
        // Create a dynamic module in the assembly
        var moduleBuilder = builder.DefineDynamicModule("DynamicModule");

        // Create a dynamic enum type in the module
        var enumBuilder = moduleBuilder.DefineEnum("InternshipBackend.Data.DynamicEnum", TypeAttributes.Public, typeof(int));

        // Add enum values
        var fieldBuilder = enumBuilder.DefineLiteral("Value1", 0);
        fieldBuilder.SetCustomAttribute(new CustomAttributeBuilder(typeof(DescriptionAttribute).GetConstructor([typeof(string)])!, ["Value1 Description"]));
        enumBuilder.DefineLiteral("Value2", 1);
        enumBuilder.DefineLiteral("Value3", 2);

        // Create the enum type
        var dynamicEnumType = enumBuilder.CreateType();

        // Arrange
        var stringLocalizer = new MockStringLocalizer<Enums>(getString: (key) => new LocalizedString(key, key, true));
        var typeSourceProvider = new TypeSourceProvider(dynamicEnumType.Assembly);
        var enumService = new EnumService(stringLocalizer, typeSourceProvider);
        var key = "DynamicEnum";
        // Act
        var result = enumService.GetEnumOrDefault(key);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count);

        Assert.Equal("Value1 Description", result[0].Name);
        Assert.Equal("Value1", result[0].Id);
        for (int i = 1; i < result.Count; i++)
        {
            Assert.Equal($"Value{i + 1}", result[i].Id);
            Assert.Equal($"Value{i + 1}", result[i].Name);
        }
    }
}
