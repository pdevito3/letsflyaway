namespace RecipeManagement.FunctionalTests.FunctionalTests.Recipe;

using RecipeManagement.SharedTestHelpers.Fakes.Recipe;
using RecipeManagement.Dtos.Recipe;
using RecipeManagement.FunctionalTests.TestUtilities;
using Microsoft.AspNetCore.JsonPatch;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class PartialRecipeUpdateTests : TestBase
{
    [Test]
    public async Task patch_recipe_returns_nocontent_when_using_valid_patchdoc_on_existing_entity()
    {
        // Arrange
        var fakeRecipe = FakeRecipe.Generate(new FakeRecipeForCreationDto().Generate());
        var patchDoc = new JsonPatchDocument<RecipeForUpdateDto>();
        patchDoc.Replace(r => r.Title, "Easily Identified Value For Test");
        await InsertAsync(fakeRecipe);

        // Act
        var route = ApiRoutes.Recipes.Patch.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
        var result = await _client.PatchJsonRequestAsync(route, patchDoc);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}