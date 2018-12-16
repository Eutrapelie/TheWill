using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CodeLine
{
    GeoffroyD1000, //Première fois qu'on parle à Geoffroy - Jour 1 - Premier choix
    GeoffroyD1001, // Second Choix
    GeoffroyD1002, // Troisième Choix
    GeoffroyD1003, // Troisième Choix - Contrat+
    GeoffroyD1004, // Permet de pas boucler dans le hall
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
    AbigailD1005,// Entrée dans le salon ; premier dialogue obligatoire 


    HippoD1000,//Première fois qu'on parle à Hippo - Jour 1
    HippoD1001,//
    HippoD1002,//



    ErnestD1000,//Première fois qu'on parle à Ernest - Jour 1
    ErnestD1001,//Choix 1
    ErnestD1002,//Choix 2
    ErnestD1003,//Choix 3
    ErnestD1004,//Dial supp "Abi?"
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

    OphelieD1000,//Première fois qu'on parle à Ernest - Jour 1
    OphelieD1001,//Choix 1
    OphelieD1002,//Choix 2
    OphelieD1003,//Inutilisé
    OphelieD2000,//Parler de la piste -> Débloquer Dialogue Hippo
	OphelieD2001,//Journal partie Abi lue
	OphelieD2002,//
	OphelieD2003,//Ophelie reste
	OphelieD2004,//Ophelie reste ++
	OphelieD2005,//Ophelie part
	
	
    LeontineD1000,//Première fois qu'on parle à Leontine - Jour 1
    LeontineD1001,//
    LeontineD1002,//
    LeontineD1003,//
    LeontineD1004,//
    LeontineD1005,//
    LeontineD1006,//
    LeontineD1007,// Jour 1, sortie vers la chambre
    LeontineD1008,// Jour 1, permet de pas boucler sur son intro
	
	KimD2001,//Première entrée cuisine
	
	ObjetJournalC0001,// Si Journal Chambre de Celeste a ete vu
	
	HippOpheD2000,//Enigme en cours
	HippOpheD2001,//Enigme Succes
	HippOpheD2002,//Enigme Echec
	
}
