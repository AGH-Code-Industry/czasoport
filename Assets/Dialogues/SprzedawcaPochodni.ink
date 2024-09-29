-> main

=== main ===
Hello, how can I help you?
 *[How much is for the torch?]
  -> torch
  * [Sorry, didn't mean to disturb you.]
        -> bywaj

=== bywaj ===
Goodbye, then. Come back if you need a torch!
-> DONE


=== torch ===
Hmm, for this one? One pretty crystal I would say.
* [Exhchange #requiresItem: ShinyRock #getsItem: Torch]
        -> trade

* [I don't need it now. Bye.]
        -> bywaj

=== trade ===
Have a nice day!
-> DONE