using Application.Game;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly IMediator _mediator;

    public GameController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public Task<GameQuestion> GetQuestion()
    {
        return _mediator.Send(new GameQuestionRequest
        {
            Type = GameType.MusicOrArtits,
            Playlist = new Playlist
            {
                Code = "37i9dQZF1EIdChYeHNDfK5",
            }
        });
    }
}