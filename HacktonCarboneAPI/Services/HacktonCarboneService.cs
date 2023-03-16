using HacktonCarboneAPI.Models;
using System;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class HacktonCarboneService : IHacktonCarboneService
{
    private const int DUREE_AMORTISSEMENT = 50;
    private const int PS_ECO = 144;
    private const int PS_APPARTEMENT = 525;
    private const int PS_OTHER = 425;
    private const double INTENSITE_ELECTRIQUE = 0.0569;
    private const double EMPREINTE_UNITAIRE = 50681.899666667;
    private const double DEFAULT_CHAUFFAGE = 0;
    private const double CONSTRUCTION_VOITURE = 6700;
    private const double AMO_PARTICULIER = 0.1;
    private const int DUREE_VIE = 19;
    private const int KM_AN_PARTAGE = 15130;

    public double CalculateHabitation(Habitation habitation)
    {
        double carboneHabitation = Construction(habitation.Surface, habitation.Age, habitation.IsEco, habitation.IsApart)
                + (habitation.ConsoElec * INTENSITE_ELECTRIQUE)
                + 0;
        if (habitation.HasClim)
        {
            carboneHabitation += EMPREINTE_UNITAIRE * habitation.NbClim;
        }
        return Math.Truncate(carboneHabitation) / 1000;
    }

    public double CalculateVehicule(Vehicule vehicule)
    {
        string immatJson = File.ReadAllText(@"./APIimmat.json");
        Immat immat = JsonSerializer.Deserialize<Immat>(immatJson);
        //string modele = immat.data.modele;
        string modeleJson = File.ReadAllText(@"./APImodele.json");
        Modele modele = JsonSerializer.Deserialize<Modele>(modeleJson);
        double carboneVehicule = 0;
        if (immat != null && modele != null)
        {
            carboneVehicule += Voiture(vehicule, immat, modele);
        }
        return Math.Truncate(carboneVehicule) / 1000;
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

    private double Chauffage(bool hasGaz, bool hasBois, bool hasFioul, bool hasReseauChal, bool hasGPL)
    {
        double chauffage = 0;
        if (hasGaz || hasBois || hasFioul || hasReseauChal || hasGPL)
        {
            // TODO
            /**
            if (hasGaz)
            {
                
            }
            if (hasBois)
            {

            }
            if (hasFioul)
            {

            }
            if (hasReseauChal)
            {

            }
            if (hasGPL)
            {

            }
            **/
        }
        else
        {
            chauffage = DEFAULT_CHAUFFAGE;
        }
        return chauffage;
    }

    private double Voiture(Vehicule vehicule, Immat immat, Modele modele)
    {
        double vehiculeCarbone = 0;
        if (vehicule.KilometrageVoiture > 0)
        {
            double constructAmo = CONSTRUCTION_VOITURE * AMO_PARTICULIER;
            if (!vehicule.ProprioVoiture)
            {
                double facteurLoc = vehicule.KilometrageVoiture / KM_AN_PARTAGE;
                constructAmo = (CONSTRUCTION_VOITURE / DUREE_VIE) * facteurLoc;
            }
            double auKm = 0;
            if (immat.data.energieNGC.Equals("ELECTRIQUE"))
            {
                switch (modele.gabarit)
                {
                    case "petite":
                        auKm = 0.0159;
                        break;
                    case "moyenne":
                        auKm = 0.0198;
                        break;
                    default:
                        auKm = 0.0273;
                        break;
                }
            }
            else
            {
                double empreinteLitre = 2.7;
                if (immat.data.energieNGC.Equals("DIESEL"))
                {
                    empreinteLitre = 3.07;
                }
                else if (immat.data.energieNGC.Equals("ESSENCE E85"))
                {
                    empreinteLitre = 1.11;
                }
                auKm = (modele.consommation / 100) * empreinteLitre;
            }
            double usage = vehicule.KilometrageVoiture * auKm;
            vehiculeCarbone = (constructAmo + usage) / vehicule.NbVoyageurs;
        }
        if (vehicule.HasMoto)
        {
            double moto = 0.0763;
            if (!vehicule.IsScooter && vehicule.CmCube > 250)
            {
                moto = 0.191;
            }
            vehiculeCarbone += moto;
        }
        return vehiculeCarbone;
    }
}
