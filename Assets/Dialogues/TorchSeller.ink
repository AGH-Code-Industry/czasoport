-> main

=== main ===
Hello traveller, you don't look familiar. Do you want to buy anything?
    * [Yes, please. Show me your goods.]
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
    * [Bye]
        -> bywaj
        
=== trade ===
It was a pleasure to trade with you. Thanks.
-> DONE
-> main