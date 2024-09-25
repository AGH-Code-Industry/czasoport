-> main

=== main ===
Hello, are you looking for something?
    *[Can you tell me what are you doing?]
        -> checking_lava_quality
    * [How can I get inside this building?]
        -> rafinery_code
    *[Can I go to all rooms and everywhere inside? Is it safe?]
        -> red_doors
    * [Sorry, didn't mean to disturb you.]
        -> bywaj

=== bywaj ===
Bye.
-> DONE
=== rafinery_code ===
You want to go to rafinery huh. I'm not sure, but I've heard that there is some code that you can ask for an entry code in museum. You should go there and ask, maybe they can help you more.
    * [Thanks, I will do that.]
-> main

=== red_doors ===
It is pretty safe yeah, if the door is red you can go inside no problem, anyone can go inside. But if a door is green then probably there is something important inside and you probably need a key.
    *[Okay, so green means that you can't go inside, got it]
        ->main

=== checking_lava_quality ===
I am just checking the quality of the lava that is extracted in rafinery and distributed around the city.
    *[I see. Thanks]
-> main