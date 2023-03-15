package controllers;

import models.Habitation;
import models.Vehicule;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import services.HacktonCarboneService;
import services.IHacktonCarboneService;

@RestController
public class HacktonCarboneController {
    @Autowired
    private HacktonCarboneService hcService;

    @GetMapping(path = "/")
    public ResponseEntity<String> test() {
        return ResponseEntity.ok("Test");
    }

    @GetMapping(path = "/habitation")
    public ResponseEntity<Double> calculateHabitation(@RequestBody Habitation habitation) {
        return ResponseEntity.ok(hcService.CalculateHabitation(habitation));
    }

    @GetMapping(path = "/vehicule")
    public ResponseEntity<Double> calculateHabitation(@RequestBody Vehicule vehicule) {
        return ResponseEntity.ok(0d);
    }
}
