-> main

=== main ===
Hello, are you looking for something?
    *[Can you tell me what are you doing?]
        -> checking_lava_quality
    * [How can I get inside this building?]
        -> rafinery_code
    * [Sorry, didn't mean to disturb you.]
        -> bywaj

=== bywaj ===
Bye.
-> DONE
=== rafinery_code ===
You want to go to rafinery huh. I'm not sure, but I've heard that there is some code that you can ask for an entry code in museum. You should go there and ask, maybe they can help you more.
    * [Thanks, I will do that.]
-> DONE
=== checking_lava_quality ===
I am just checking the quality of the lava that is extracted in rafinery and distributed around the city.
    *[I see. Thanks]
-> DONE