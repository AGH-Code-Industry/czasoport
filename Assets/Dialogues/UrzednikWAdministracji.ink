-> main

=== main ===
Hello, how can I help you?
    *[Can you tell me what is your job here?]
        -> jobDescription
    *[I need the code to the rafinery.]
        -> rafiteryCode
    *[Sorry, I didn't want to interupt...]
        -> DONE
        
        
=== jobDescription ===
I give permissions and licences to important thlakahans...You don't look like an important thlakahan.
    *[Okay, got it thank you.]
        -> main
    
=== rafiteryCode ===
Code to rafinery? You need to have a written permission for that.
    *[Well I don't have a written permission for that]
        In that case, please don't disturb me any longer. Come back if you have the permission.
        -> main
    *[Here's the written permission for the rafinery code #requiresItem: Torch]
        -> giveGlejt
        
=== giveGlejt ===
Oh! Sure, here you are. The rafinery code!
    *[Thank you! #getsItem: Torch]
    -> DONE