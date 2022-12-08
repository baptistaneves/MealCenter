using MealCenter.Core.DomainObjects;
using MealCenter.Registration.Domain.Clients;

namespace MealCenter.Registration.Domain.Restaurants
{
    public class Table : Entity
    {
        public Guid RestaurantId { get; private set; }
        public Guid ClientId { get; private set; }
        public string State { get; private set; }
        public int TableNumber { get; private set; }
        public bool Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }

        //EF Rel.
        public Restaurant Restaurant { get; private set; }
        public Client Client { get; private set; }

        public Table(Guid restaurantId, Guid clientId, string state, int tableNumber, bool status)
        {
            RestaurantId = restaurantId;
            ClientId = clientId;
            State = state;
            TableNumber = tableNumber;
            Status = status;
        }

        public void AssociateToRestaurant(Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public void Activate() => Status = true;

        public void Deactivate() => Status = false;

        public void FreeTable() => State = TableState.TableIsFree;
        public void OccupyTable() => State = TableState.TableIsOccupied;
    }
}
