﻿using MealCenter.Core.DomainObjects;
using MealCenter.Registration.Domain.Restaurants;

namespace MealCenter.Registration.Domain.Clients
{
    public class Client : Entity, IAggregateRoot
    {
        public string IdentityUserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string ImageUrl { get; private set; }
        public string Description { get; private set; }
        public bool Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }

        //EF Rel.
        public Table Table { get; private set; }

        public Client(string identityUserId, string firstName, string lastName, bool status, string imageUrl, 
            string description)
        {
            IdentityUserId = identityUserId;
            FirstName = firstName;
            LastName = lastName;
            Status = status;
            ImageUrl = imageUrl;
            Description = description;
        }

        protected Client() { }

        public void Activate() => Status = true;

        public void Deactivate() => Status = false;

        public void UpdateImage(string newImageUrl)
        {
            ImageUrl = newImageUrl;
        }
    }
}