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
I don't know anything about rafinery.
-> DONE

=== tax ===
Hmmm. If I remember correctly, he didn't fill in the A38 form which was required for some tax assessment. Do you have the form?
*[No.]
-> okienko1
*[Exchange #requiresItem: A38]
-> trade


=== okienko1===
Then, you can find it in the window number 1. It's on the right. Thank you. NEXT!
-> DONE

=== trade ===
Okay. I will get on it in a minute. Thank you. NEXT!
-> DONE