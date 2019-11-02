using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CodeLine
{
    GeoffroyD1000, //Première fois qu'on parle à Geoffroy - Jour 1 - Premier choix - Genre
    GeoffroyD1001, // Choix info Abi
    GeoffroyD1002, // Choix info Hippo
    GeoffroyD1003, // Choix info meilleur héritier
    GeoffroyD1004, // Permet de pas boucler dans le hall au jour 1 - s'obtient au passage Hall > Salon du premier jour
    GeoffroyD1005, // Si aide Contrat Acceptée
	GeoffroyD2001, // Avoir son contrat
	GeoffroyD2002,
	GeoffroyD2003, //Suite quête après contrat
	GeoffroyD2004, //Chemin Ernest - Assassinat
	GeoffroyD2005, //Chemin Ernest - Incendie
	GeoffroyD2006, //Chemin Ernest - Autre
	GeoffroyD2007, //Chemin Ernest - Assassinat - Choix +1a
	GeoffroyD2008, //Chemin Ernest - Assassinat - Choix +1b
	GeoffroyD2009, //Chemin Ernest - Assassinat - Choix -1a
	GeoffroyD2010, //Chemin Ernest - Assassinat - Choix -1b


    AbigailD1000,//Première fois qu'on parle à Aby - Jour 1
    AbigailD1001,// Intro Dialogue
    AbigailD1002,// idem - Choix 1 et 2
    AbigailD1003,// idem - Choix 3
    AbigailD1004,// idem - Choix 4
    AbigailD1005,// Entrée dans le salon ; premier dialogue obligatoire  - s'obtient dans le behavior après le premier dialogue (avant la lettre)
    
    AbigailD2000,//Première fois qu'on parle à Aby - Jour 2
    AbigailD2001,//Autorisation Grenier
    AbigailD2002,//Info Clé Armoire Céleste
    AbigailD2003,//Info sur Mérite Geoffroy, mauvais comptable
    AbigailD2004,//Info sur Mérite Geoffroy, bon majordome - Bloque fin Abigail


    HippoD1000,//Première fois qu'on parle à Hippo - Jour 1
    HippoD1001,//Premier Choix (Succession manoir)
    HippoD1002,//Second Choix (Mort Celestia)



    ErnestD1000,//Première fois qu'on parle à Ernest - Jour 1
    ErnestD1001,//Choix 1 - Hippo
    ErnestD1002,//Choix 2 - Ophé
    ErnestD1003,//Choix 3 - Dial supp "Abi?"
    ErnestD1004,//Choix 4 - Perso
    ErnestD1003a,//Choix 3 - Aide
    ErnestD1003b,//Choix 3 - Neutre
    ErnestD1003c,//Choix 3 - Refus
	ErnestD2001,//Premier Dialogue J2
	ErnestD2002,//Premier echec à l'ouverture du Phone
	ErnestD2003,//Succes ouverture Phone
	ErnestD2004,//Revelation Tel - secret gardé
	ErnestD2005,//Revelation Tel - fin autres
	ErnestD2006,//Revelation Tel - fin aide drague Abi
	ErnestD2007,//Revelation Tel - fin laisse faire
	ErnestD2008,//Incendie - Allumage
	ErnestD2009,//Incendie - Bras baissé
	ErnestD2010,//Incendie - Incendie Évité

    OphelieD1000,//Première fois qu'on parle à Ophélie - Jour 1
    OphelieD1001,//Choix 1
    OphelieD1002,//Choix 2
    OphelieD1003,//Inutilisé
    OphelieD2000,//Parler de la piste -> Débloquer Dialogue Hippo
	OphelieD2001,//Journal partie Abi lue
	OphelieD2002,//
	OphelieD2003,//Ophelie reste
	OphelieD2004,//Ophelie reste ++
	OphelieD2005,//Ophelie part
	
	
    LeontineD1000,//Choix Hippo
    LeontineD1001,//Choix Ernest
    LeontineD1002,//Choix Geoffroy
    LeontineD1003,//Choix Ophé
    LeontineD1004,//Choix Abi
    LeontineD1005,//Choix Situation
    LeontineD1006,//
    LeontineD1007,// Jour 1, sortie vers la chambre
    LeontineD1008,// Jour 1, permet de pas boucler sur son intro
    LeontineD2000,// Jour 2, débloquage Map
	
	KimD2001,//Première entrée cuisine
	
	ObjetJournalC0001,// Si Journal Chambre de Celeste a ete vu
	
	HippOpheD2000,//Enigme en cours
	HippOpheD2001,//Enigme Succes
	HippOpheD2002,//Enigme Echec
	
	
	ItemLetterTuto0001,// la lettre a été cliquée
	PreuveAttachement001,//
	PreuveAttachement002,//Première Nuit, dialogue Cuisine
	PreuveAttachement003,
	PreuveAttachement004,
	PreuveAttachement005,
	PreuveAttachement006,
	PreuveAttachement007,
	PreuveAttachement008,
	PreuveAttachement009,
	PreuveAttachement010,
}
