using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
// ReSharper disable ClassNeverInstantiated.Global

namespace Dummy;

public class SaveItem
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public record Request(string Code, string Name, decimal Price);

    public record Response(int ID);

    public static Results<Ok<Response>, ProblemHttpResult> Handle(Request saveItemRequest)
    {
        // biri biri
        // biri biri

        return TypedResults.Ok(new Response(12));
    }
}