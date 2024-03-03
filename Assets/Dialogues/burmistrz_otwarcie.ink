-> main

=== main ===
A stranger! What are you doing here?! We have a crisis here, don't disturb us!
    * [What happened, maybe I can help you?]
        -> lost
    * [I'm sorry. Bye.]
    ->bywaj
        
=== bywaj ===
Goodbye.
-> DONE

== lost ==
I wanted to officially open this new building. It will be our new renovated tomb! Very important for all of us, but I can't do it without my mace! I lost it and I don't even know where. Did you see it? If you find it, I will grant you a big prize!
    * [Exhchange #requiresItem: Torch #getsItem: BeigeGem]
        -> trade
    * [I will look for it, don't worry.]
        -> bywaj
        
=== trade ===
LUCKY ME! That's it! That's my mace. Thank you traveler, take this as a sign of my gratitude. 
-> DONE
