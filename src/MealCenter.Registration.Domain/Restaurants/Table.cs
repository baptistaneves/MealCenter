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

        public Table(Guid restaurantId, int tableNumber, bool status)
        {
            RestaurantId = restaurantId;
            State = TableState.TableIsFree;
            TableNumber = tableNumber;
            Status = status;
        }

        //EF
        protected Table() { }

        public void AssociateToRestaurant(Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public void Activate() => Status = true;

        public void Deactivate() => Status = false;

        public void FreeTable()
        {
            State = TableState.TableIsFree;
            ClientId = Guid.Empty;
        }
        public void OccupyTable(Guid clientId)
        {
            State = TableState.TableIsOccupied;
            ClientId = clientId;
        }

    }
}
