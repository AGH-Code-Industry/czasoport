-> main

=== main ===
Hello, how can I help you?
    *[I need an entry code to rafinery.]
        -> administrator
    * [Do you know anything about the tax fraud commited by the cleaner?]
        -> tax
    * [Sorry, didn't mean to disturb you.]
        -> bywaj

=== bywaj ===
Bye. NEXT!
-> DONE
=== administrator ===
Find administrator and ask him about it. He should be at the very back of the building.
-> DONE

=== tax ===
Hmmm. From what i remember he didn't fill the A38 form which was required for some tax assesment. Do I have the form?
*[No.]
-> okienko1
*[Exhchange #requiresItem: A38 #getsItem: cleaner_to_artist]
-> trade


=== okienko1===
You can then find it in the window number 1. It's on the right. Thank you. NEXT!
-> DONE

=== trade ===
Okay, thanks. I will get on it in a minute. Thank you. NEXT!
-> DONE