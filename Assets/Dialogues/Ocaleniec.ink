-> main

=== main ===
Oh, another surviovior? I though I am the only one left...ugh, my stupid arm..
 *[What happened to you?]
  -> atl_level
 *[Can I help you?]
  -> glue
 * [Sorry, didn't mean to disturb you.]
  -> bywaj

=== bywaj ===
Disturb...As if I were super busy..
-> DONE

=== bywaj2 ===
Eh...Of course you don't. You're probably just in my imagination anyways..
-> DONE

=== atl_level ===
What happened? I don't remember myself at this point, the atl level was rising so fast, all builiding started to collaps...I'm not sure what exactly happened...a tragedy I suppose...
*[Oh, well, I guess I will not learn much from you, bye]
-> bywaj2

=== glue ===
I am slowly dying anyways, but if you have a glue or something else to attach my arm back. That would allow me to leave in peace.
I'm willing to even give you this mace of power in return. Do you want that?
* [Exhchange #requiresItem: klej #getsItem: bulawa]
        -> trade

* [I don't have anything like that. Sorry..]
        -> bywaj2

=== trade ===
Oh my, finally I can be whole again, thank you, whoever you are!
-> DONE