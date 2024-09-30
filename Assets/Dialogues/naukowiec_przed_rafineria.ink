-> main

=== main ===
Hello, how can I help you?
    * [Can you tell me how to get inside of this building?]
        -> to_refinery
    * [I am just passing by, sorry. Bye]
    ->DONE
        
=== bywaj ===
You can't go inside then. Bye!
-> DONE

== to_refinery ==
This is the refinery, this is where all the inventions come to life and all plans for the city improvements are tested, if you want to go through the door you need the code. Don't you know that?
    * [Exchange #requiresItem: Torch #getsItem: BeigeGem]
        -> trade
    * [I don't.]
        -> bywaj
        
=== trade ===
I see that you do. Enjoy the laboratory inside, but please do not disturb the scientists!
-> DONE
