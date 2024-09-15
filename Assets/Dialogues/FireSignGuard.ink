-> main

=== main ===
Hi, are you enjoying the exhibition?
    *[Who are you?]
        -> guard
    * [I would enjoy it more if I knew what is it. Can you tell me a bit about those?]
        -> exhibition
    * [Quite a bit, yes. But what does this sign say?]
        -> fire_hazard
    * [Yes thank you for asking. Bye.]
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
You've probably noticed how we all love atl right? The fire here is to remind everyone to not bring anything hot here.
*[What happens if they do?]
-> escape



=== escape ===
Don't worry, nothing too bad, the museum just starts a super cold mode, and everyone have to leave the building immediately unless they want to harden and become a stone statue. But that way any fire, atl or anything else that is hot is immediately neutralized.

*[Oh okay. I will remember that. Bye]
-> bye

=== trade ===
Thank you traveler! Now we can officially open the gates! Let's go people!
->DONE