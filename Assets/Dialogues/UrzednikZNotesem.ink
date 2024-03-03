-> main

=== main ===
Hello, how can I help you?
    *[Can you tell me what is your job here?]
        -> spacePlanning
    * [I have an idea to improve docks. Let me tell you...]
        -> grantPrivilage
    * [Sorry, didn't want to interupt...]
        -> DONE
        
=== spacePlanning ===
I deal with space planning and implement new investments in the city. Please go away, I need to work now.
    *[Okay, got it thank you.]
-> main

=== grantPrivilage ===
Great idea! Thank you! If we will ever implement this idea you will bve our honorary quest there. Here, take this.
*[Thank you #getsItem: Torch]
-> DONE