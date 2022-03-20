using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInoviceGenarator
{
    public class RideRepository
    {
        Dictionary<string, List<Ride>> userRides = null;
        public RideRepository()
        {
            this.userRides = new Dictionary<string, List<Ride>>();
        }
        public void AddRide(string userId, Ride[] rides)
        {
            bool rideList = this.userRides.ContainsKey(userId);
            try
            {
                if (!rideList)
                {
                    List<Ride> list = new List<Ride>();
                    list.AddRange(rides);
                    this.userRides.Add(userId, list);
                }
            }
            catch (CabInoviceException)
            {
                throw new CabInoviceException(CabInoviceException.ExceptionType.INVALID_USER_ID, "Invalid user Id");
            }
        }
        public Ride[] GetRides(string userId)
        {
            try
            {
                return this.userRides[userId].ToArray();
            }
            catch (CabInoviceException)
            {
                throw new CabInoviceException(CabInoviceException.ExceptionType.INVALID_USER_ID, "Invalid User Id");
            }
        }

    }
}