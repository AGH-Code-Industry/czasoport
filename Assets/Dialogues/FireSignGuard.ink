-> main

=== main ===
Hi, are you enjoying the exhibition?
    *[Who are you?]
        -> guard
    * [I'd enjoy it more if I knew what it is. Can you tell me a bit about those?]
        -> exhibition
    * [Quite a bit, yes. But what does this sign say?]
        -> fire_hazard
    * [I'm just passing by, thanks for asking though. Bye.]
        -> bye

=== bye ===
Goodbye! Take care.
-> DONE

=== guard ===
I'm just a security guard here. Making sure the memories of the past are safe.

*[Can you tell me a bit more about them?]
->exhibition

=== exhibition ===
I'm not an expert myself haha, but I will try. This is for example a mace that was the symbol of power in the past, this is a paining of some green tribe that believed they could survive without atl. This...
*[Fascinating. Thank you!]
-> bye
*[Very interesting, what about this sign?]
-> fire_hazard


=== fire_hazard ===
Oh, it's a safety rule. You see, some of the things here are rather vulnerable and fire and heat can damage them.
You've probably noticed how we all love atl right? The fire here is to remind everyone not to bring anything hot here.
*[What happens if they do?]
-> escape



=== escape ===
Don't worry, nothing bad, the museum simply starts a Super Cold Mode Procedure, and everyone have to leave the building as fast as they can, unless they want to harden and become a stone statue. That way any heat source including atl, is immediately neutralized.

*[Oh okay. I will remember that. Bye.]
-> bye

->DONE