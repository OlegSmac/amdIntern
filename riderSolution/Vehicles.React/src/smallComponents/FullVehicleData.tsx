import React, { type FC } from 'react'

export type Vehicle = {
    brand: string;
    model: string;
    transmissionType: string;
    fuelType: string;
    color: string;
    year: number;
    enginePower: number;
    mileage: number;
    maxSpeed: number;
    engineVolume: number;
    fuelConsumption: number;
    vehicleType: string;
    //car-specific
    bodyType?: string;
    seats?: number;
    doors?: number;
    // motorcycle-specific
    hasSidecar?: boolean;
    // truck-specific
    cabinType?: string;
    loadCapacity?: number;
}

interface VehicleProps {
    vehicle: Vehicle;
}

const FullVehicleData : FC<VehicleProps> = ({vehicle}) => {
    return (
        <div className="vehicle-properties">
            <h2>Vehicle Properties</h2>
            <table className="vehicle-table">
                <tbody>
                    <tr><td><strong>Brand</strong></td><td>{vehicle.brand}</td></tr>
                    <tr><td><strong>Model</strong></td><td>{vehicle.model}</td></tr>
                    <tr><td><strong>Year</strong></td><td>{vehicle.year}</td></tr>
                    <tr><td><strong>Transmission Type</strong></td><td>{vehicle.transmissionType}</td></tr>
                    <tr><td><strong>Engine Volume</strong></td><td>{vehicle.engineVolume} L</td></tr>
                    <tr><td><strong>Engine Power</strong></td><td>{vehicle.enginePower} hp</td></tr>
                    <tr><td><strong>Fuel Type</strong></td><td>{vehicle.fuelType}</td></tr>
                    <tr><td><strong>Fuel Consumption</strong></td><td>{vehicle.fuelConsumption} L/100km</td></tr>
                    <tr><td><strong>Color</strong></td><td>{vehicle.color}</td></tr>
                    <tr><td><strong>Mileage</strong></td><td>{vehicle.mileage} km</td></tr>
                    <tr><td><strong>Max Speed</strong></td><td>{vehicle.maxSpeed} km/h</td></tr>
                    {vehicle.vehicleType === 'car' && (
                    <>
                        <tr><td><strong>Body Type</strong></td><td>{vehicle.bodyType || '-'}</td></tr>
                        <tr><td><strong>Seats</strong></td><td>{vehicle.seats ?? '-'}</td></tr>
                        <tr><td><strong>Doors</strong></td><td>{vehicle.doors ?? '-'}</td></tr>
                    </>
                    )}

                    {vehicle.vehicleType === 'motorcycle' && (
                        <tr><td><strong>Has Sidecar</strong></td><td>{vehicle.hasSidecar ? 'Yes' : 'No'}</td></tr>
                    )}

                    {vehicle.vehicleType === 'truck' && (
                    <>
                        <tr><td><strong>Cabin Type</strong></td><td>{vehicle.cabinType || '-'}</td></tr>
                        <tr><td><strong>Load Capacity</strong></td><td>{vehicle.loadCapacity ? `${vehicle.loadCapacity} kg` : '-'}</td></tr>
                    </>
                    )}
                </tbody>
            </table>
        </div>
    );
}

export default FullVehicleData;