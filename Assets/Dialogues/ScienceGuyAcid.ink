-> main

=== main ===
Hey! Be careful out there! Don't touch anything! A small drop of acid and you are burnt right through!
    *[Acid you say... And could strong acid burn through this? #requiresItem: RafineryGem]
        -> acid 
    * [Sorry, didn't mean to disturb you.]
        -> bywaj

=== bywaj ===
Bye.
-> DONE

=== acid ===
In the blink of an eye.
    *[Thank you. #getsItem: RafineryGemOpened]
        -> DONE