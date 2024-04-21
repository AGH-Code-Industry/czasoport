-> main

=== main ===
Hello, how can I help you?
    *[Can you tell me what your job here is?]
        -> spacePlanning
    * [I have an idea to improve the docks. Let me tell you...]
        -> grantPrivilage
    * [Sorry, didn't want to interrupt...]
        -> DONE
        
=== spacePlanning ===
I deal with space planning and implement new investments in the city. Please go away, I need to work now.
    *[Okay, got it thank you.]
-> main

=== grantPrivilage ===
Great idea! Thank you! If we ever use it, you will be our honorary guest there. Here, take this.
*[Thank you #getsItem: Torch]
-> DONE