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
You are not authorized to perform this action.
*[Repaint other Stall]
    -> stragansRepaint
*[Go back to main menu]
    -> main

=== notAuthorizedMove ===
You are not authorized to perform this action.
*[Move other Stall]
    -> stragansMove
*[Go back to main menu]
    -> main
    
=== notAuthorizedRemove ===
You are not authorized to perform this action.
*[Remove other Stall]
    -> stragansRemove
*[Go back to main menu]
    -> main

=== notAuthorized ===
You are not authorized to perform this action.
-> main


=== stragansRepaint ===
Which one do you want to repaint?
    *[Stall 1]
        -> notAuthorizedRepaint
    *[Stall 2]
        -> notAuthorizedRepaint
    *[Stall 3]
        -> notAuthorizedRepaint
    *[Stall 4]
        -> notAuthorizedRepaint
    *[Stall 5]
        -> notAuthorizedRepaint
    *[Stall 6]
        -> notAuthorizedRepaint
    *[Stall 7]
        -> notAuthorizedRepaint
    *[Stall 8]
        -> notAuthorizedRepaint
    *[Stall 9]
        -> notAuthorizedRepaint
    *[Stall 10]
        -> notAuthorizedRepaint
    *[Stall 11]
        -> notAuthorizedRepaint
    *[Stall 12]
        -> notAuthorizedRepaint
    *[Stall 13]
        -> notAuthorizedRepaint
    *[Stall 14]
        -> notAuthorizedRepaint
    *[Stall 15]
        -> notAuthorizedRepaint
        
        
        
=== stragansRemove ===
Which one do you want to remove?
    *[Stall 1]
        -> notAuthorizedRemove
    *[Stall 2]
        -> notAuthorizedRemove
    *[Stall 3]
        -> notAuthorizedRemove
    *[Stall 4]
        -> notAuthorizedRemove
    *[Stall 5]
        -> notAuthorizedRemove
    *[Stall 6]
        -> notAuthorizedRemove
    *[Stall 7]
        -> notAuthorizedRemove
    *[Stall 10]
        -> notAuthorizedRemove
    *[Stall 11]
        -> notAuthorizedRemove
    *[Stall 12]
        -> notAuthorizedRemove
    *[Stall 13]
        -> notAuthorizedRemove
    *[Stall 14]
        -> notAuthorizedRemove
    *[Stall 15]
        -> notAuthorizedRemove
        
=== stragansMove ===
Which one do you want to move?
    *[Stall 1]
        -> notAuthorizedMove
    *[Stall 2]
        -> notAuthorizedMove
    *[Stall 3]
        -> notAuthorizedMove
    *[Stall 4]
        -> notAuthorizedMove
    *[Stall 5]
        -> notAuthorizedMove
    *[Stall 6]
        -> notAuthorizedMove
    *[Stall 7]
        -> notAuthorizedMove
    *[Stall 8]
        -> move
    *[Stall 9]
        -> notAuthorizedMove
    *[Stall 10]
        -> notAuthorizedMove
    *[Stall 11]
        -> notAuthorizedMove
    *[Stall 12]
        -> notAuthorizedMove
    *[Stall 13]
        -> notAuthorizedMove
    *[Stall 14]
        -> notAuthorizedMove
    *[Stall 15]
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
Request for moving Stall 8 successfully ordered.
Stall will be moved next month.
-> END