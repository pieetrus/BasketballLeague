﻿using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Shots.Queries.GetShotsList
{
    public class GetShotsListQuery : IRequest<IEnumerable<Shot>>
    {
    }
}
