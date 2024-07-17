-> main

=== main ===
Hello traveler, I don't think I recognize you. Are you interested in any of my goods?
    * [Exhchange #requiresItem: Rock #getsItem: Rock]
        -> trade
    * [Exhchange #requiresItem: Crystal #getsItem: Torch #getsItem: Torch]
        -> trade
    * [I don't want anything. Bye.]
        -> bywaj
        
=== bywaj ===
Bye!
-> DONE
        
=== trade ===
If you need anything else, feel free to come back here!
-> DONE
-> main