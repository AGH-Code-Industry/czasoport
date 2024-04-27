-> main

=== main ===
Huh, is there someone else here except me? Who are you, can you help me?
    * [How can I help you?]
        -> show
    * [...You saw nothing, I wasn't here.]
    ->bywaj
        
=== bywaj ===
Oh...
-> DONE

== show ==
I lost my arm in an accident, I need something to attach it back to my body. In return I can give you this. I have no use for it anymore...
    * [Exhchange #requiresItem: BeigeGem #getsItem: Torch]
        -> trade
    * [I can't help you right now. Bye.]
        -> bywaj
        
=== trade ===
Finally, thank you.
-> DONE
