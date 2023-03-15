package controllers;

import models.Habitation;
import models.Vehicule;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import services.IHacktonCarboneService;

@RestController
@RequestMapping(path = "/hacktoncarbone", produces = "application/json")
@CrossOrigin(origins = "*")
public class HacktonCarboneController {
    private IHacktonCarboneService hcService;

    public HacktonCarboneController(IHacktonCarboneService hacktonCarboneService) {
        this.hcService = hacktonCarboneService;
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
