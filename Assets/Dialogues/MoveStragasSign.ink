-> main

=== main ===
What do you want to do?
    *[Repaint]
        -> stragansRepaint
    * [Move]
        -> stragansMove
    * [Add]
        -> notAuthorized
    * [Remove]
        -> stragansRemove
      
      
=== notAuthorizedRepaint ===
You are not authorized to perform this action
*[Repaing other Stragan]
    -> stragansRepaint
*[Go back to main menu]
    -> main

=== notAuthorizedMove ===
You are not authorized to perform this action
*[Move other Stragan]
    -> stragansMove
*[Go back to main menu]
    -> main
    
=== notAuthorizedRemove ===
You are not authorized to perform this action
*[Remove other Stragan]
    -> stragansRemove
*[Go back to main menu]
    -> main

=== notAuthorized ===
You are not authorized to perform this action
-> main


=== stragansRepaint ===
Which one do you want to repaint?
    *[Stragan 1]
        -> notAuthorizedRepaint
    *[Stragan 2]
        -> notAuthorizedRepaint
    *[Stragan 3]
        -> notAuthorizedRepaint
    *[Stragan 4]
        -> notAuthorizedRepaint
    *[Stragan 5]
        -> notAuthorizedRepaint
    *[Stragan 6]
        -> notAuthorizedRepaint
    *[Stragan 7]
        -> notAuthorizedRepaint
    *[Stragan 8]
        -> notAuthorizedRepaint
    *[Stragan 9]
        -> notAuthorizedRepaint
    *[Stragan 10]
        -> notAuthorizedRepaint
    *[Stragan 11]
        -> notAuthorizedRepaint
    *[Stragan 12]
        -> notAuthorizedRepaint
    *[Stragan 13]
        -> notAuthorizedRepaint
    *[Stragan 14]
        -> notAuthorizedRepaint
    *[Stragan 15]
        -> notAuthorizedRepaint
        
        
        
=== stragansRemove ===
Which one do you want to remove?
    *[Stragan 1]
        -> notAuthorizedRemove
    *[Stragan 2]
        -> notAuthorizedRemove
    *[Stragan 3]
        -> notAuthorizedRemove
    *[Stragan 4]
        -> notAuthorizedRemove
    *[Stragan 5]
        -> notAuthorizedRemove
    *[Stragan 6]
        -> notAuthorizedRemove
    *[Stragan 7]
        -> notAuthorizedRemove
    *[Stragan 10]
        -> notAuthorizedRemove
    *[Stragan 11]
        -> notAuthorizedRemove
    *[Stragan 12]
        -> notAuthorizedRemove
    *[Stragan 13]
        -> notAuthorizedRemove
    *[Stragan 14]
        -> notAuthorizedRemove
    *[Stragan 15]
        -> notAuthorizedRemove
        
=== stragansMove ===
Which one do you want to move?
    *[Stragan 1]
        -> notAuthorizedMove
    *[Stragan 2]
        -> notAuthorizedMove
    *[Stragan 3]
        -> notAuthorizedMove
    *[Stragan 4]
        -> notAuthorizedMove
    *[Stragan 5]
        -> notAuthorizedMove
    *[Stragan 6]
        -> notAuthorizedMove
    *[Stragan 7]
        -> notAuthorizedMove
    *[Stragan 8]
        -> move
    *[Stragan 9]
        -> notAuthorizedMove
    *[Stragan 10]
        -> notAuthorizedMove
    *[Stragan 11]
        -> notAuthorizedMove
    *[Stragan 12]
        -> notAuthorizedMove
    *[Stragan 13]
        -> notAuthorizedMove
    *[Stragan 14]
        -> notAuthorizedMove
    *[Stragan 15]
        -> notAuthorizedMove
        
=== move ===
Where do you want it to move? (You need a permission to move it to the desired place)
    *[Place 1]
        -> notAuthorized
    *[Place 2]
        -> notAuthorized
    *[Place 3]
        -> notAuthorized
    *[Place 4]
        -> notAuthorized
    *[Place 5]
        -> notAuthorized
    *[Place 6]
        -> notAuthorized
    *[Place 7]
        -> notAuthorized
    *[Place 8]
        -> notAuthorized
    *[Place 9 #requiresItem: permission8]
        -> movingStragan8
    *[Place 10]
        -> notAuthorized
    *[Place 11]
        -> notAuthorized
    *[Place 12]
        -> notAuthorized
    *[Place 13]
        -> notAuthorized
    *[Place 14]
        -> notAuthorized
    *[Place 15]
        -> notAuthorized


=== movingStragan8 ===
Request for moving stragan 8 successfuly ordered.
Stragan will be moved next month.
-> END