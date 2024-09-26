-> main

=== main ===
Hi. Did you managed to get the mineral?
    *[Not yet.]
        -> DONE
    *[Yes, #requiresItem: czysty_mineral]
        -> czysty_mineral
    * [Sorry, didn't mean to disturb you.]
        -> bywaj

=== bywaj ===
Bye.
-> DONE

=== czysty_mineral ===
Wow it looks amazing. Thank you for your help it will definietely move our research forward. Please, take this as a thank you.
[ getItem: gem2]
-> DONE