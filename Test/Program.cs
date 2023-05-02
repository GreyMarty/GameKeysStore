using Microsoft.Extensions.DependencyInjection;
using Application;
using AutoMapper;
using Domain.Entities;
using Application.Data;
using Application.Models.ReadModels;

var serviceCollection = new ServiceCollection();
serviceCollection.AddApplicationServices();

var serviceProvider = serviceCollection.BuildServiceProvider();

var mapper = serviceProvider.GetService<IMapper>();

var list = new PagedList<Game>(new Game[] { new Game() }, 0, 0, 0);

var result = mapper.Map<IPagedList<GameReadModel>>((IPagedList<Game>)list);

Console.Write(1);
