-> main

=== main ===
Hello, how can I help you?
    *[Can I get the A38 form?]
    -> A38
    * [Sorry, didn't mean to disturb you.]
        -> bywaj

=== bywaj ===
Bye. NEXT!
-> DONE

=== A38 ===
To get the A38 form you need to fill in the blue form first. Do you have it?
*[Exchange #requiresItem: BlueForm #getsItem: A38]
-> trade
*[No. I don't.]
-> Noform


=== Noform ===
You can then find it in the window number 1. Thank you. NEXT!
-> DONE

=== trade ===
Thank you. NEXT!
-> DONE