package models;

public record Habitation (
    boolean isAppart,
    double nbPersonnes,
    int age,
    boolean isEco,
    double surface,
    double consoElec,
    boolean hasClim,
    double nbClim
) {}
