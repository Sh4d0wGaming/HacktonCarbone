package services;

import models.Habitation;
import models.Vehicule;
import org.springframework.stereotype.Service;

@Service
public class HacktonCarboneService implements IHacktonCarboneService {

    private static final int DUREE_AMORTISSEMENT = 50;
    private static final int PS_ECO = 144;
    private static final int PS_APPARTEMENT = 525;
    private static final int PS_OTHER = 425;
    private static final double INTENSITE_ELECTRIQUE = 0.0569;
    private static final double EMPREINTE_UNITAIRE = 50681.899666667;
    @Override
    public double CalculateHabitation(Habitation habitation) {
        double carboneHabitation = Construction(habitation.surface(), habitation.age(), habitation.isEco(), habitation.isAppart())
                + (habitation.consoElec()*INTENSITE_ELECTRIQUE)
                + 0
                + EMPREINTE_UNITAIRE * habitation.nbClim();
        return carboneHabitation;
    }

    @Override
    public double CalculateVehicule(Vehicule vehicule) {
        return 0;
    }

    private double Construction(double surface, int age, boolean isEco, boolean isAppart) {
        double construction = 0;
        if (age < DUREE_AMORTISSEMENT) {
            int ParSurface = PS_OTHER;
            if (isEco) {
                ParSurface = PS_ECO;
            } else if (isAppart) {
                ParSurface = PS_APPARTEMENT;
            }
            double AnnuelleParSurface = ParSurface / DUREE_AMORTISSEMENT;
            construction = surface * AnnuelleParSurface;
        }
        return construction;
    }

}
