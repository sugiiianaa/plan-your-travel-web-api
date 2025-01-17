﻿using PlanYourTravel.Domain.Commons.Primitives;
using PlanYourTravel.Domain.Entities.LocationAggregate;
using PlanYourTravel.Domain.Enums;

namespace PlanYourTravel.Domain.Entities.AirportAggregate
{
    public sealed class Airport : AuditableEntity
    {
        // EF private constructor
        public Airport() { }

        private Airport(Guid id, string name, AirportCode code, Guid locationId, AirportFlightType flightType) : base(id)
        {
            Name = name;
            Code = code;
            LocationId = locationId;
            FlightType = flightType;
        }

        public string Name { get; private set; }
        public AirportCode Code { get; private set; }
        public AirportFlightType FlightType { get; private set; }

        // Foreign Key
        public Guid LocationId { get; private set; }

        // Navigation Property 
        public Location Location { get; private set; }

        public static Airport Create(
            Guid id,
            string name,
            AirportCode code,
            Guid locationId,
            AirportFlightType flightType)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("id");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (locationId == Guid.Empty)
            {
                throw new ArgumentNullException("location");
            }

            return new Airport(id, name, code, locationId, flightType);
        }

        public void UpdateAirportDetails(
            string newName,
            AirportFlightType flightType,
            Guid locationId)
        {
            if (string.IsNullOrEmpty(newName))
            {
                throw new ArgumentNullException("name");
            }

            if (locationId == Guid.Empty)
            {
                throw new ArgumentNullException("location");
            }

            Name = newName;
            FlightType = flightType;
        }
    }
}
