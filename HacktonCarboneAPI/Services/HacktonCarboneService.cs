using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class HacktonCarboneService : IHacktonCarboneService
{
    private const int DUREE_AMORTISSEMENT = 50;
    private const int PS_ECO = 144;
    private const int PS_APPARTEMENT = 525;
    private const int PS_OTHER = 425;
    private const double INTENSITE_ELECTRIQUE = 0.0569;
    private const double EMPREINTE_UNITAIRE = 50681.899666667;

    public double CalculateHabitation(Habitation habitation)
    {
        double carboneHabitation = Construction(habitation.Surface, habitation.Age, habitation.IsEco, habitation.IsApart)
                + (habitation.ConsoElec * INTENSITE_ELECTRIQUE)
                + 0;
        if (habitation.HasClim)
        {
            carboneHabitation += EMPREINTE_UNITAIRE * habitation.NbClim;
        }
        return carboneHabitation;
    }

    public double CalculateVehicule(Vehicule vehicule)
    {
        throw new NotImplementedException();
    }

    private double Construction(double surface, int age, bool isEco, bool isAppart)
    {
        double construction = 0;
        if (age < DUREE_AMORTISSEMENT)
        {
            int ParSurface = PS_OTHER;
            if (isEco)
            {
                ParSurface = PS_ECO;
            }
            else if (isAppart)
            {
                ParSurface = PS_APPARTEMENT;
            }
            double AnnuelleParSurface = ParSurface / DUREE_AMORTISSEMENT;
            construction = surface * AnnuelleParSurface;
        }
        return construction;
    }
}
