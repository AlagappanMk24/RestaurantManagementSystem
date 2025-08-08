using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Restaurant.Application.Features.Ratings.Commands.UpdateRating;

public class UpdateRatingCommand : IRequest
{
    [JsonIgnore]
    public int Id { get; set; }

    [DefaultValue(3)]
    public int Stars { get; set; }
}
