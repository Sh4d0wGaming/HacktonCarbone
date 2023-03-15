package services;

import models.Habitation;
import models.Vehicule;

public interface IHacktonCarboneService {
    public double CalculateHabitation(Habitation habitation);
    public double CalculateVehicule(Vehicule vehicule);
}
