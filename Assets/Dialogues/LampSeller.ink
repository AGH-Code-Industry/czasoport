-> main

=== main ===
Hello traveler, I don't think I recognize you. Are you interested in any of my goods?
    * [Hello, let me see your offer.]
        -> show
    * [No, thank you. I'm not interested right now.]
    ->DONE
        
=== bywaj ===
Bye!
-> DONE

== show ==
    * [Exhchange #requiresItem: BeigeGem #getsItem: Torch]
        -> trade
    * [Exhchange #requiresItem: PinkGem #getsItem: Torch]
        -> trade
    * [I don't want anything. Bye.]
        -> bywaj
        
=== trade ===
If you need anything else, feel free to come back here!
-> DONE
-> main