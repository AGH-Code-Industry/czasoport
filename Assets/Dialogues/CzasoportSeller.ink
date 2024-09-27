-> main

=== main ===
Oh you look interesting. Similar to something I found recently... do you want to take a look?
    * [You've got my attention, please go on.]
        -> show
    * [No, thank you. Bye.]
    ->bywaj
        
=== bywaj ===
Bye!
-> DONE

== show ==
    * [Exchange #requiresItem: ResultGlejt #getsItem: CzasoportPart]
        -> trade

    * [I don't want anything. Bye.]
        -> bywaj
        
=== trade ===
Have a nice day!
-> DONE
