-> main

=== main ===
Dzień dobry, w czym mogę pomóc?
    *[Co tutaj robisz?]
        -> jobDescription
    *[Poproszę kod do rafinerii]
        -> rafiteryCode
    *[Przepraszam, nie chciałem przeszkadzać]
        -> DONE
        
        
=== jobDescription ===
Daję Różne uprawnienia bardzo ważnym osobom, ty nie wyglądasz na kogoś ważnego, nie przeszkadzaj mi więcej.
    *[Dobrze, rozumiem.]
        -> main
    
=== rafiteryCode ===
Kod do rafinerii? Do tego potrzebny jest gleft.
    *[Nie mam glejtu]
        W takim razię proszę mi nie zawracać głowy
        -> main
    *[Proszę, oto mój glejt #requiresItem: Torch]
        -> giveGlejt
        
=== giveGlejt ===
Oh, w takim razie proszę, o to twój kod
    *[Dziękuję! #getsItem: Torch]
    -> DONE