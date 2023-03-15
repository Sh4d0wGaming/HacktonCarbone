package models;

public record Habitation (
    boolean isAppartement,
    double nbPersonnes,
    int ageLogement,
    boolean isEcoConstruit,
    double surface,
    double consoElec,
    boolean hasClim,
    double nbClim
) {}
