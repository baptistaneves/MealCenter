﻿using MealCenter.Identity.Domain;
using MediatR;

namespace MealCenter.Identity.Application.Queries
{
    public class GetAdminUserProfileByIdentityIdQuery : IRequest<UserProfile>
    {
        public Guid Id { get; set; }
    }
}
