-> main

=== main ===
Hello, I'm pretty busy, what do you want?
    *[Can you tell me what is your job here?]
        -> guarding
    * [Sorry, what does this sign next to you mean?]
        -> firesign
    * [Sorry, can you tell me a bit about this statue? It doesn't look like a normal thlakahan.]
        -> green_tribe
    * [Sorry, didn't mean to disturb you.]
        -> bywaj
        
=== bywaj ===
Bye.
-> DONE
== green_tribe==
Oh right. That is a legendary green tribe which used to come here in the past near docks. They claimed to be able to live without atl. Hehe, can you imagine? Without atl!
    * [Hmm, interesting. I should go and check that out later.]
-> DONE
=== guarding ===
I am a guard, can't you see?! You better not touch any of those items here, they are valuable artifacts and relics of the past.
    *[Okay, got it thank you.]
-> DONE
== firesign ==
It shows that open fire is forbidden inside the museum. But don't worry, we have an safety system which will go off as soon as any fire is detected. We all have to leave the building when the system starts because we are very sensitive to cold.
    * [Okay, thank you.]
-> DONE