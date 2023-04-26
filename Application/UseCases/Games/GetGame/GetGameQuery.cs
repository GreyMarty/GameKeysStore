using Application.Models.ReadModels;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.UseCases.Games.GetGame;
public record GetGameQuery(Expression<Func<Game, bool>> Predicate) : IRequest<GameReadModel>;
