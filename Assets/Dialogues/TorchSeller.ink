-> main

=== main ===
Hello traveler, you don't look familiar. Do you want to buy anything?
    * [Yes, please. Show me your goods.]
        -> show
    * [No, thank you. I'm not interested right now.]
    ->DONE
        
=== bywaj ===
Bye!
-> DONE

== show ==
    * [Exchange #requiresItem: BeigeGem #getsItem: Torch]
        -> trade
    * [Exchange #requiresItem: PinkGem #getsItem: Torch]
        -> trade
    * [Bye]
        -> bywaj
        
=== trade ===
It was a pleasure to trade with you. Thanks.
-> DONE
-> main