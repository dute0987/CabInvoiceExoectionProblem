using CabInvoiceGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInoviceGenarator
{
    public class InvoiceGenerator
    {
        RideType rideType;
        private RideRepository rideRepository;
        private double MINIMUM_COST_PER_KM;
        private int COST_PER_TIME;
        private double MINIMUM_FARE;

        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
            this.rideRepository = rideRepository;
            try
            {
                if (rideType.Equals(RideType.PREMIUM))
                {
                    this.MINIMUM_COST_PER_KM = 15;
                    this.COST_PER_TIME = 2;
                    this.MINIMUM_FARE = 20;
                }
                else if (rideType.Equals(RideType.NORMAL))
                {
                    this.MINIMUM_COST_PER_KM = 10;
                    this.COST_PER_TIME = 1;
                    this.MINIMUM_FARE = 5;
                }
            }
            catch (CabInoviceException)
            {
                throw new CabInoviceException(CabInoviceException.ExceptionType.INVALID_RIDE_TYPE, "invalid ride");


            }
        }
        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0;

            try
            {
                totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_TIME;
            }
            catch (CabInoviceException)
            {
                if (rideType.Equals(null))
                {
                    throw new CabInoviceException(CabInoviceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride");

                }
                if (distance > 0)
                {
                    throw new CabInoviceException(CabInoviceException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
                }
                if (time > 0)
                {
                    throw new CabInoviceException(CabInoviceException.ExceptionType.INVALID_TIME, "Invalid time");
                }
            }
            return totalFare;
        }
        public InvoiceSummary CalulateFare(Ride[] rides)
        {
            double totalFare = 0;
            try
            {
                foreach (Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
            }
            catch (CabInoviceException)
            {
                if (rides == null)
                {
                    throw new CabInoviceException(CabInoviceException.ExceptionType.NULL_RIDES, "Null Rides");
                }
            }
            return new InvoiceSummary(rides.Length, totalFare);
        }

    }
}