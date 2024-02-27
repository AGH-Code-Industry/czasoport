-> main

=== main ===
Co ode mnie chcesz?
    * [Pokaż mi swoje towary]
        -> show
        
=== bywaj ===
Bywaj
-> DONE

== show ==
    * [Wymiana #requiresItem: BeigeGem #getsItem: Torch]
        -> trade
    * [Wymiana #requiresItem: PinkGem #getsItem: Torch]
        -> trade
    * [Bywaj]
        -> bywaj
        
=== trade ===
Dziękuję za wymianę
-> DONE
-> main